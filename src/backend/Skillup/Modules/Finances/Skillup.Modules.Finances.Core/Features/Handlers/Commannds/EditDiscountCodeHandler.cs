﻿using MediatR;
using Skillup.Modules.Finances.Core.DTO;
using Skillup.Modules.Finances.Core.Features.Requests.Commannds;
using Skillup.Modules.Finances.Core.Mappings;
using Skillup.Modules.Finances.Core.Repositories;

namespace Skillup.Modules.Finances.Core.Features.Handlers.Commannds
{
    internal class EditDiscountCodeHandler(IDiscountCodeRepository discountCodeRepository) : IRequestHandler<EditDiscountCodeRequest>
    {
        private readonly IDiscountCodeRepository _discountCodeRepository = discountCodeRepository;

        public async Task Handle(EditDiscountCodeRequest request, CancellationToken cancellationToken)
        {
            var discountCode = new AddDiscountCodeDto()
            {
                Id = request.Id,
                AppliesToEntireCart = request.AppliesToEntireCart,
                Code = request.Code,
                StartAt = request.StartAt,
                ExpireAt = request.ExpireAt,
                DiscountValue = request.DiscountValue,
                IsActive = request.IsActive,
                IsPublic = request.IsPublic,
                Type = request.Type,
            };

            DiscountCodeMapper mapper = new();
            await _discountCodeRepository.Update(mapper.AddDiscountCodeDtoToDiscountCode(discountCode));
        }
    }
}
