using MediatR;
using Skillup.Modules.Finances.Core.Entities;
using Skillup.Modules.Finances.Core.Features.Requests.Commannds;
using Skillup.Modules.Finances.Core.Repositories;
using Skillup.Modules.Finances.Core.ValueObjects;

namespace Skillup.Modules.Finances.Core.Features.Handlers.Commannds
{
    internal class AddDiscountCodeHandler(IDiscountCodeRepository discountCodeRepository, IUserRepository userRepository) : IRequestHandler<AddDiscountCodeRequest>
    {
        private readonly IDiscountCodeRepository _discountCodeRepository = discountCodeRepository;
        private readonly IUserRepository _userRepository = userRepository;

        public async Task Handle(AddDiscountCodeRequest request, CancellationToken cancellationToken)
        {
            var owner = await _userRepository.Get(request.OwnerId) ?? throw new Exception();

            DiscountCode? discountCodeToAdd = null;
            if (request.Type == DiscountCodeType.Percentage)
            {
                discountCodeToAdd = new PercentageDiscountCode(request);
            }

            if (request.Type == DiscountCodeType.FixedAmount)
            {
                discountCodeToAdd = new FixedAmountDiscountCode(request);
            }

            if (discountCodeToAdd == null)
                throw new Exception();  // TODO: CustomEx

            discountCodeToAdd.Owner = owner;
            discountCodeToAdd.OwnerId = request.OwnerId;

            await _discountCodeRepository.Add(discountCodeToAdd);
        }
    }
}
