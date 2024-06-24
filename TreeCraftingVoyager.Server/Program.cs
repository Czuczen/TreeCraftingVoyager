using TreeCraftingVoyager.Server.Configuration.Dependencies;
using TreeCraftingVoyager.Server.Configuration;
using TreeCraftingVoyager.Server.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.AddFileLogger()
        .AddLocalizationServices()
        .AddDatabase()
        .AddIdentityServices()
        .AddDependenciesByConvention()
        .AddJwtAuthentication()
        .AddCorsPolicy()
        .AddControllers()
        .AddSwaggerDocumentation()
        .AddHttpsRedirection()
        .AddAuthorizationPolicies();

var app = builder.Build();

app.ConfigureMiddlewares();

app.Run();
