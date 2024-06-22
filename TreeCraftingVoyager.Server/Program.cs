using TreeCraftingVoyager.Server.Configuration.Dependencies;
using TreeCraftingVoyager.Server.Configuration;
using TreeCraftingVoyager.Server.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.AddFileLogger()
        .AddLocalizationServices()
        .AddFileLogger()
        .AddDatabase()
        .AddIdentityServices()
        .AddDependenciesByConvention()
        .AddJwtAuthentication()
        .AddCorsPolicy()
        .AddControllers()
        .AddSwaggerDocumentation();

var app = builder.Build();

app.ConfigureMiddlewares();

app.Run();
