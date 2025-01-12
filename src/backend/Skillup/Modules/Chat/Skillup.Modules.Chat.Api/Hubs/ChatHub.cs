using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Skillup.Modules.Chat.Core.Entities;
using Skillup.Modules.Chat.Core.Repositories;
using Skillup.Shared.Abstractions.Time;
using Skillup.Shared.Infrastructure.Auth;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Skillup.Bootstrapper")]
namespace Skillup.Modules.Chat.Api.Hubs
{
    [Authorize]
    internal class ChatHub(IClock clock, IChatRepository chatRepository, ILogger<ChatHub> logger) : Hub
    {
        private readonly IClock _clock = clock;
        private readonly IChatRepository _chatRepository = chatRepository;
        private readonly ILogger<ChatHub> _logger = logger;

        public override async Task OnConnectedAsync()
        {
            try
            {
                var userId = Context?.User?.GetUserId();

                if (userId is null)
                    return;

                var chatId = Context?.GetHttpContext()?.Request.Query["chatId"];

                if (chatId is null)
                    return;

                await base.OnConnectedAsync();

                await Groups.AddToGroupAsync(Context!.ConnectionId, chatId!);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        public async Task SendMessage(string chatId, string content)
        {
            var userId = Context?.User?.GetUserId();

            if (userId is null)
                return;

            var message = new Message()
            {
                ChatId = Guid.Parse(chatId),
                Content = content,
                TimeStamp = _clock.CurrentDate(),
                SenderId = (Guid)userId,
            };

            await _chatRepository.AddMessage(message);
            await Clients.Group(chatId).SendAsync("ReceiveMessage", new { message.ChatId, message.Id, message.Content, message.TimeStamp, message.SenderId });
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            if (exception != null)
                _logger.LogError(exception?.Message);

            return base.OnDisconnectedAsync(exception);
        }
    }
}
