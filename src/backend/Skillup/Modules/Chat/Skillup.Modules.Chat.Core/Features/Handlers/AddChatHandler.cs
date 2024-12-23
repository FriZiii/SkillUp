using MediatR;
using Skillup.Modules.Chat.Core.Repositories;

namespace Skillup.Modules.Chat.Core.Features.Handlers
{
    internal class AddChatHandler(IChatRepository chatRepository) : IRequestHandler<AddChatRequest>
    {
        private readonly IChatRepository _chatRepository = chatRepository;

        public async Task Handle(AddChatRequest request, CancellationToken cancellationToken)
        {
            await _chatRepository.Add(new Entities.Chat()
            {
                AuthorId = request.AuthorId,
                CourseId = request.ItemId,
                UserId = request.UserId,
            });
        }
    }
}
