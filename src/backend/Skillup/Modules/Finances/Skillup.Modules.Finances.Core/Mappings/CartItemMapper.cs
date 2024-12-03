using Riok.Mapperly.Abstractions;
using Skillup.Modules.Finances.Core.DTO;
using Skillup.Modules.Finances.Core.Entities;

namespace Skillup.Modules.Finances.Core.Mappings
{
    [Mapper]
    internal partial class CartItemMapper
    {
        public CartItemDto CartItemToDto(CartItem cartItem)
        {
            var itemMapper = new ItemMapper();
            return new CartItemDto()
            {
                Id = cartItem.ItemId,
                OrginalItem = itemMapper.ItemToDto(cartItem.Item),
                Price = cartItem.Price,
            };
        }
    }
}
