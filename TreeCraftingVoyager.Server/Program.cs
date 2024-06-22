using TreeCraftingVoyager.Server.Configuration;
using TreeCraftingVoyager.Server.Configuration.Extensions;
using TreeCraftingVoyager.Server.Logging;
using TreeCraftingVoyager.Server.Configuration.Dependencies;

var builder = WebApplication.CreateBuilder(args);

builder.AddFileLogger();

builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddIdentityServices();
builder.Services.RegisterDependenciesByConvention();
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddCorsPolicy();
builder.Services.AddControllers();
builder.Services.AddSwaggerDocumentation();

var app = builder.Build();

app.ConfigureMiddlewares();

app.Run();
