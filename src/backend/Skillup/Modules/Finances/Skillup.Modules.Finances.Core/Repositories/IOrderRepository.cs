using Skillup.Modules.Finances.Core.Entities;

namespace Skillup.Modules.Finances.Core.Repositories
{
    internal interface IOrderRepository
    {
        Task Add(Order order);
        Task<IEnumerable<Order>> GetByOrderer(Guid ordererId);
        Task<Order?> GetByBalanceHistoryId(Guid balanceHistoryId);
        Task<IEnumerable<OrderItem>> GetOrderItemsForAuthor(Guid authorId);
    }
}
