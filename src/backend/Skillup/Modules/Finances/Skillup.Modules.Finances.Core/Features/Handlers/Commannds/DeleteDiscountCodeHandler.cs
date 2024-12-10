using MediatR;
using Skillup.Modules.Finances.Core.Features.Requests.Commannds;
using Skillup.Modules.Finances.Core.Repositories;

namespace Skillup.Modules.Finances.Core.Features.Handlers.Commannds
{
    internal class DeleteDiscountCodeHandler(IDiscountCodeRepository discountCodeRepository) : IRequestHandler<DeleteDiscountCodeRequest>
    {
        private readonly IDiscountCodeRepository _discountCodeRepository = discountCodeRepository;

        public async Task Handle(DeleteDiscountCodeRequest request, CancellationToken cancellationToken)
        {
            await _discountCodeRepository.DeleteById(request.DiscountCodeId);
        }
    }
}
