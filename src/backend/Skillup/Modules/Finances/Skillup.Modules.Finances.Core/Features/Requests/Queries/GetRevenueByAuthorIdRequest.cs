﻿using MediatR;
using Skillup.Modules.Finances.Core.DTO;

namespace Skillup.Modules.Finances.Core.Features.Requests.Queries
{
    public record GetRevenueByAuthorIdRequest(Guid AuthorId) : IRequest<RevenueDto>;
}
