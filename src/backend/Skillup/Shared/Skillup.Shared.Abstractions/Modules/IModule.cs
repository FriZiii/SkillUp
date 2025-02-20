﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Skillup.Shared.Abstractions.Modules
{
    public interface IModule
    {
        string Name { get; }
        IEnumerable<string>? Policies => null;
        void Register(IServiceCollection services);
        void Use(IApplicationBuilder app);
    }
}
