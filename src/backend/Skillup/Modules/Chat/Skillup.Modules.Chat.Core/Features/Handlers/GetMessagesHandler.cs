using MediatR;
using Skillup.Modules.Chat.Core.DTO;
using Skillup.Modules.Chat.Core.Mappers;
using Skillup.Modules.Chat.Core.Repositories;

namespace Skillup.Modules.Chat.Core.Features.Handlers
{
    internal class GetMessagesHandler(IChatRepository chatRepository) : IRequestHandler<GetMessagesRequest, IEnumerable<MessageDto>>
    {
        private readonly IChatRepository _chatRepository = chatRepository;

        public async Task<IEnumerable<MessageDto>> Handle(GetMessagesRequest request, CancellationToken cancellationToken)
        {
            var mapper = new MessageMapper();
            var history = await _chatRepository.GetMessages(request.ChatId);
            return history.Select(x => mapper.MessageToDto(x, request.UserId));
        }
    }
}
