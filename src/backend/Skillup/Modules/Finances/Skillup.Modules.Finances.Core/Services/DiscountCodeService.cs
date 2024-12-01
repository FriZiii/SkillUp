using Skillup.Modules.Finances.Core.Entities;
using Skillup.Modules.Finances.Core.Repositories;
using Skillup.Modules.Finances.Core.ValueObjects;

namespace Skillup.Modules.Finances.Core.Services
{
    internal class DiscountCodeService(IDiscountCodeRepository discountCodeRepository) : IDiscountCodeService
    {
        private readonly IDiscountCodeRepository _discountCodeRepository = discountCodeRepository;

        public async Task<Currency> ApplyDiscount(Guid discountCodeId, Item item)
        {
            var discountCode = await _discountCodeRepository.GetById(discountCodeId) ?? throw new Exception(); // TODO: Custom Ex

            var discountedPrice = discountCode.Apply(item);

            //await _discountCodeRepository.Update(discountCode);

            return discountedPrice;
        }
    }
}
