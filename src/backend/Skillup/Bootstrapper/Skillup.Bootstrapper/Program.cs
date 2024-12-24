using Skillup.Modules.Chat.Core;
using Skillup.Shared.Infrastructure;
using Skillup.Shared.Infrastructure.Logging;
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

builder.AddLogger();

var app = builder.Build();

// Use modular infrastructure
app.UseModularInfrastructure();
modules.ForEach(module => module.Use(app));

// Configure endpoints
app.MapHub<ChatHub>("/chatHub");
app.MapControllers();
app.MapGet("/", () => "Inflow API");
app.MapModuleInfo();

app.Run();