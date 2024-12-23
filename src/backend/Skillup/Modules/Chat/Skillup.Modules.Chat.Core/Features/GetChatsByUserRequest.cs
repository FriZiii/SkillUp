using MediatR;
using Skillup.Modules.Chat.Core.DTO;

namespace Skillup.Modules.Chat.Core.Features
{
    public record GetChatsByUserRequest(Guid UserId) : IRequest<IEnumerable<ChatDto>>;
}
