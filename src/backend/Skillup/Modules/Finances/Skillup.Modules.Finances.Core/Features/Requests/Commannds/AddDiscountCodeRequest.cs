﻿using MediatR;
using Skillup.Modules.Finances.Core.DTO;

namespace Skillup.Modules.Finances.Core.Features.Requests.Commannds
{
    internal class AddDiscountCodeRequest : AddDiscountCodeDto, IRequest;
}