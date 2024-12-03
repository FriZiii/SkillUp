using Skillup.Modules.Finances.Core.Entities;

namespace Skillup.Modules.Finances.Core.Repositories
{
    internal interface IDiscountCodeRepository
    {
        Task<DiscountCode?> GetById(Guid discountCodeId);
        Task<IEnumerable<DiscountCode>> GetByOwner(Guid ownerId);
        Task<IEnumerable<DiscountCode>> GetPublic();
        Task Update(DiscountCode discountCode);
        Task Add(DiscountCode discountCode);
        Task DeleteById(Guid discountCodeId);
        Task ToggleDiscountCodeForItem(Guid discountCodeId, Guid itemId);
        Task<DiscountCode?> GetByCode(string code);
    }
}
