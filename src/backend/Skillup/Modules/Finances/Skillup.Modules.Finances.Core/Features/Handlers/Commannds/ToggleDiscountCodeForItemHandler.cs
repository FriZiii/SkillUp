using MediatR;
using Skillup.Modules.Finances.Core.Features.Requests.Commannds;
using Skillup.Modules.Finances.Core.Repositories;

namespace Skillup.Modules.Finances.Core.Features.Handlers.Commannds
{
    internal class ToggleDiscountCodeForItemHandler(IDiscountCodeRepository discountCodeRepository, IItemRepository itemRepository) : IRequestHandler<ToggleDiscountCodeForItemRequest>
    {
        private readonly IDiscountCodeRepository _discountCodeRepository = discountCodeRepository;
        private readonly IItemRepository _itemRepository = itemRepository;

        public async Task Handle(ToggleDiscountCodeForItemRequest request, CancellationToken cancellationToken)
        {
            var discountCode = await _discountCodeRepository.GetById(request.DiscountCodeId) ?? throw new Exception(); // TODO: Custom Ex: discount code with id doesnt exist
            if (discountCode.AppliesToEntireCart)
                throw new Exception(); // TOOD: Custom Ex

            var item = await _itemRepository.GetById(request.ItemId) ?? throw new Exception(); // TODO: Custom ex: item with id doesnt exist

            discountCode.AppliesToEntireCart = false;

            await _discountCodeRepository.ToggleDiscountCodeForItem(request.DiscountCodeId, request.ItemId);
        }
    }
}
