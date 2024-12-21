using Microsoft.EntityFrameworkCore;
using Skillup.Modules.Finances.Core.Entities;
using Skillup.Modules.Finances.Core.Repositories;

namespace Skillup.Modules.Finances.Core.DAL.Repositories
{
    internal class OrderRepository(FinancesDbContext context) : IOrderRepository
    {
        private readonly FinancesDbContext _context = context;
        private readonly DbSet<Order> _orders = context.Orders;
        private readonly DbSet<OrderItem> _orderItems = context.OrderItems;

        public async Task Add(Order order)
        {
            await _orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task<Order?> GetByBalanceHistoryId(Guid balanceHistoryId)
            => await _orders.FirstOrDefaultAsync(x => x.BalanceHistoryId == balanceHistoryId);

        public async Task<IEnumerable<Order>> GetByOrderer(Guid ordererId)
            => await _orders.Where(x => x.OrdererId == ordererId).ToListAsync();

        public async Task<IEnumerable<OrderItem>> GetOrderItemsForAuthor(Guid authorId)
        {
            return await _orderItems.Where(x => x.Item.AuthorId == authorId)
                .Include(x => x.Order)
                .ToListAsync();
        }
    }
}
