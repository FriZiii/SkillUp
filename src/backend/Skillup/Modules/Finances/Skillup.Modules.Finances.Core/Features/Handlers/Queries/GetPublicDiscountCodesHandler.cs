using MediatR;
using Skillup.Modules.Finances.Core.DTO;
using Skillup.Modules.Finances.Core.Features.Requests.Queries;
using Skillup.Modules.Finances.Core.Mappings;
using Skillup.Modules.Finances.Core.Repositories;

namespace Skillup.Modules.Finances.Core.Features.Handlers.Queries
{
    internal class GetPublicDiscountCodesHandler(IDiscountCodeRepository discountCodeRepository) : IRequestHandler<GetPublicDiscountCodesRequest, IEnumerable<DiscountCodeDto>>
    {
        private readonly IDiscountCodeRepository _discountCodeRepository = discountCodeRepository;

        public async Task<IEnumerable<DiscountCodeDto>> Handle(GetPublicDiscountCodesRequest request, CancellationToken cancellationToken)
        {
            var discountCodes = await _discountCodeRepository.GetPublic();
            DiscountCodeMapper mapper = new();

            return discountCodes.Select(mapper.DiscountCodeToDto);
        }
    }
}
