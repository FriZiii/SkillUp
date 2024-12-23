using MediatR;
using Skillup.Modules.Chat.Core.DTO;

namespace Skillup.Modules.Chat.Core.Features
{
    public record GetMessagesRequest(Guid ChatId, Guid UserId) : IRequest<IEnumerable<MessageDto>>;
}
