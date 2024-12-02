using Skillup.Modules.Finances.Core.Entities;

namespace Skillup.Modules.Finances.Core.Repositories
{
    internal interface ICartRepository
    {
        Task<Cart?> GetCart(Guid id);
        Task AddCartItem(CartItem cartItem);
        Task DeleteItemFromCart(Guid cartId, Guid itemId);
    }
}
