
using MediatR;

namespace Skillup.Modules.Chat.Core.Features
{
    internal record AddChatRequest(Guid ItemId, Guid UserId, Guid AuthorId) : IRequest;
}
