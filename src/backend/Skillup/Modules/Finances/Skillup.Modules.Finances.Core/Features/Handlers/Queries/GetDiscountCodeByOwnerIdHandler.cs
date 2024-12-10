using MediatR;
using Skillup.Modules.Finances.Core.DTO;
using Skillup.Modules.Finances.Core.Features.Requests.Queries;
using Skillup.Modules.Finances.Core.Mappings;
using Skillup.Modules.Finances.Core.Repositories;

namespace Skillup.Modules.Finances.Core.Features.Handlers.Queries
{
    internal class GetDiscountCodeByOwnerIdHandler(IDiscountCodeRepository discountCodeRepository) : IRequestHandler<GetDiscountCodeByOwnerIdRequest, IEnumerable<DiscountCodeDto>>
    {
        private readonly IDiscountCodeRepository _discountCodeRepository = discountCodeRepository;

        public async Task<IEnumerable<DiscountCodeDto>> Handle(GetDiscountCodeByOwnerIdRequest request, CancellationToken cancellationToken)
        {
            var discountCodes = await _discountCodeRepository.GetByOwner(request.OwnerId);
            var mapper = new DiscountCodeMapper();
            return discountCodes.Select(mapper.DiscountCodeToDto);
        }
    }
}
