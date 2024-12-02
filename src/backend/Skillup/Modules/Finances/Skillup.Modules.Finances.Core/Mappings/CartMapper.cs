using Riok.Mapperly.Abstractions;
using Skillup.Modules.Finances.Core.DTO;
using Skillup.Modules.Finances.Core.Entities;

namespace Skillup.Modules.Finances.Core.Mappings
{
    [Mapper]
    internal partial class CartMapper
    {
        public CartDto CartToDto(Cart cart)
        {
            var itemMapper = new ItemMapper();
            var items = cart.Items.Select(x => itemMapper.ItemToDto(x.Item));
            return new CartDto()
            {
                Id = cart.Id,
                Total = items.Sum(x => x.Price),
                Items = items
            };
        }
    }
}
