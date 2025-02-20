﻿using MediatR;
using System.Text.Json.Serialization;

namespace Skillup.Modules.Auth.Core.Features.Commands.Account
{
    internal record SignUpRequest(string Email, string Password, bool AllowMarketingEmails) : IRequest
    {
        [JsonIgnore]
        public Guid UserId { get; init; } = Guid.NewGuid();
    }
}
