﻿using MediatR;
using Skillup.Modules.Finances.Core.DTO;
using Skillup.Modules.Finances.Core.Features.Requests.Queries;
using Skillup.Modules.Finances.Core.Mappings;
using Skillup.Modules.Finances.Core.Repositories;
using Skillup.Shared.Abstractions.Exceptions.GlobalExceptions;

namespace Skillup.Modules.Finances.Core.Features.Handlers.Queries
{
    internal class GetDiscountCodeByIdHandler(IDiscountCodeRepository discountCodeRepository) : IRequestHandler<GetDiscountCodeByIdRequest, DiscountCodeDto>
    {
        private readonly IDiscountCodeRepository _discountCodeRepository = discountCodeRepository;

        public async Task<DiscountCodeDto> Handle(GetDiscountCodeByIdRequest request, CancellationToken cancellationToken)
        {
            DiscountCodeMapper mapper = new();

            var discountCode = await _discountCodeRepository.GetById(request.DiscountCodeId) ?? throw new NotFoundException($"DiscountCode with ID {request.DiscountCodeId} not found");

            var dto = mapper.DiscountCodeToDto(discountCode);
            return dto;
        }
    }
}
