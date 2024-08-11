using Skillup.Shared.Infrastructure;
using Skillup.Shared.Infrastructure.Modules;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureModules();

// Load modules and assemblies
var configuration = builder.Configuration;
var assemblies = ModuleLoader.LoadAssemblies(configuration, "Skillup.Modules.");
var modules = ModuleLoader.LoadModules(assemblies);

// Add modular infrastructure
builder.Services.AddModularInfrastructure(modules);
modules.ForEach(module => module.Register(builder.Services));

var app = builder.Build();


// Use modular infrastructure
app.UseModularInfrastructure();
modules.ForEach(module => module.Use(app));

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Cnfigure endpoints
app.MapControllers();
app.MapGet("/", () => "Inflow API");
app.MapModuleInfo();

app.Run();