using MediatR;
using Skillup.Modules.Chat.Core.DTO;
using Skillup.Modules.Chat.Core.Mappers;
using Skillup.Modules.Chat.Core.Repositories;

namespace Skillup.Modules.Chat.Core.Features.Handlers
{
    internal class GetChatsByUserHandler(IChatRepository chatRepository) : IRequestHandler<GetChatsByUserRequest, IEnumerable<ChatDto>>
    {
        private readonly IChatRepository _chatRepository = chatRepository;

        public async Task<IEnumerable<ChatDto>> Handle(GetChatsByUserRequest request, CancellationToken cancellationToken)
        {
            var mapper = new ChatMapper();
            var chats = await _chatRepository.GetChatsByUserId(request.UserId);
            return chats.Select(x => mapper.ChatToDto(x, request.UserId)).ToList();
        }
    }
}
