using Riok.Mapperly.Abstractions;
using Skillup.Modules.Finances.Core.DTO;
using Skillup.Modules.Finances.Core.Entities;

namespace Skillup.Modules.Finances.Core.Mappings
{
    [Mapper]
    internal partial class OrderMapper
    {
        public OrderDto OrderToDto(Order order)
        {
            var items = order.Items.Select(x => new OrderItemDto() { ItemId = x.ItemId, ItemPrice = x.ItemPrice });
            return new OrderDto()
            {
                Id = order.Id,
                Created = order.Created,
                Title = order.BalanceHistory.Title,
                TotalPrice = order.TotalPrice,
                Items = items
            };
        }
    }
}
