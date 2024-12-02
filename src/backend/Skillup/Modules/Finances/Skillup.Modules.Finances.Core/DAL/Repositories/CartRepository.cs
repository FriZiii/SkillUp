using Microsoft.EntityFrameworkCore;
using Skillup.Modules.Finances.Core.Entities;
using Skillup.Modules.Finances.Core.Repositories;

namespace Skillup.Modules.Finances.Core.DAL.Repositories
{
    internal class CartRepository(FinancesDbContext context) : ICartRepository
    {
        private readonly FinancesDbContext _context = context;
        private readonly DbSet<Cart> _carts = context.Carts;
        private readonly DbSet<CartItem> _cartItems = context.CartItems;

        public async Task AddCartItem(CartItem cartItem)
        {
            var cart = await _carts.FirstOrDefaultAsync(x => x.Id == cartItem.CartId);

            if (cart == null)
            {
                var newCart = new Cart
                {
                    Id = cartItem.CartId,
                    Items = [cartItem]
                };

                await _carts.AddAsync(newCart);
            }
            else
            {
                await _cartItems.AddAsync(cartItem);
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteItemFromCart(Guid cartId, Guid itemId)
        {
            var cart = await _carts.Include(x => x.Items)
                .FirstOrDefaultAsync(x => x.Id == cartId) ?? throw new Exception(); // TODO: custom ex
            var cartItemToDelete = await _cartItems.Include(x => x.Cart).FirstOrDefaultAsync(x => x.CartId == cartId && x.ItemId == itemId) ?? throw new Exception(); // TODO: custom ex

            _cartItems.Remove(cartItemToDelete);

            if (cart.Items.Count == 1)
                _carts.Remove(cartItemToDelete.Cart);

            await _context.SaveChangesAsync();
        }

        public async Task<Cart?> GetCart(Guid id)
            => await _carts
                .Include(x => x.Items)
                    .ThenInclude(x => x.Item)
                .FirstOrDefaultAsync(x => x.Id == id);
    }
}
