using Riok.Mapperly.Abstractions;
using Skillup.Modules.Chat.Core.DTO;

namespace Skillup.Modules.Chat.Core.Mappers
{
    [Mapper]
    internal partial class ChatMapper
    {
        public ChatDto ChatToDto(Entities.Chat chat, Guid userId)
        {
            var messageMapper = new MessageMapper();
            var lastMessage = chat.Messages.OrderByDescending(x => x.TimeStamp).FirstOrDefault();

            return new ChatDto()
            {
                Id = chat.Id,
                AuthorId = chat.AuthorId,
                CourseId = chat.CourseId,
                UserId = chat.UserId,
                LastMessage = messageMapper.MessageToDto(lastMessage, userId)
            };
        }
    }
}
