using Serilog;
using Skillup.Shared.Infrastructure;
using Skillup.Shared.Infrastructure.Modules;
using Skillup.Shared.Infrastructure.SerilogLogger;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureModules();

// Load modules and assemblies
var configuration = builder.Configuration;
var assemblies = ModuleLoader.LoadAssemblies(configuration, "Skillup.Modules.");
var modules = ModuleLoader.LoadModules(assemblies);

// Add modular infrastructure
builder.Services.AddModularInfrastructure(modules);
modules.ForEach(module => module.Register(builder.Services));

Log.Logger = LoggerConfig.CreateLogger(builder);

var app = builder.Build();

// Use modular infrastructure
app.UseModularInfrastructure();
modules.ForEach(module => module.Use(app));

// Cnfigure endpoints
app.MapControllers();
app.MapGet("/", () => "Inflow API");
app.MapModuleInfo();

app.Run();