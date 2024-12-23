using Riok.Mapperly.Abstractions;
using Skillup.Modules.Chat.Core.DTO;
using Skillup.Modules.Chat.Core.Entities;

namespace Skillup.Modules.Chat.Core.Mappers
{
    [Mapper]
    internal partial class MessageMapper
    {
        public MessageDto? MessageToDto(Message? message, Guid userId)
        {
            if (message is null)
                return null;

            return new MessageDto()
            {
                Id = message.Id,
                ChatId = message.ChatId,
                Content = message.Content,
                TimeStamp = message.TimeStamp,
                SenderId = message.SenderId,
                SendedByYou = userId == message.SenderId
            };
        }
    }
}
