using MediatR;
using Skillup.Modules.Finances.Core.Entities;
using Skillup.Modules.Finances.Core.Features.Requests.Commannds;
using Skillup.Modules.Finances.Core.Repositories;
using Skillup.Modules.Finances.Core.ValueObjects;

namespace Skillup.Modules.Finances.Core.Features.Handlers.Commannds
{
    internal class AddDiscountCodeHandler(IDiscountCodeRepository discountCodeRepository) : IRequestHandler<AddDiscountCodeRequest>
    {
        private readonly IDiscountCodeRepository _discountCodeRepository = discountCodeRepository;

        public async Task Handle(AddDiscountCodeRequest request, CancellationToken cancellationToken)
        {
            DiscountCode? discountCodeToAdd = null;
            if (request.DiscountCodeDto.Type == DiscountCodeType.Percentage)
            {
                discountCodeToAdd = new PercentageDiscountCode(request.DiscountCodeDto);
            }

            if (request.DiscountCodeDto.Type == DiscountCodeType.FixedAmount)
            {
                discountCodeToAdd = new FixedAmountDiscountCode(request.DiscountCodeDto);
            }

            if (discountCodeToAdd == null)
                throw new Exception();  // TODO: CustomEx

            await _discountCodeRepository.Add(discountCodeToAdd);
        }
    }
}
