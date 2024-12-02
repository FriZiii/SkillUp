using Riok.Mapperly.Abstractions;
using Skillup.Modules.Finances.Core.DTO;
using Skillup.Modules.Finances.Core.Entities;

namespace Skillup.Modules.Finances.Core.Mappings
{
    [Mapper]
    internal partial class ItemMapper
    {
        public ItemDto ItemToDto(Item item)
        {
            return new ItemDto()
            {
                Id = item.Id,
                AuthorId = item.AuthorId,
                Price = item.Price,
                Type = item.Type,
            };
        }
    }
}
