using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TreeCraftingVoyager.Server.Configuration.Dependencies;
using TreeCraftingVoyager.Server.Data;
using TreeCraftingVoyager.Server.Logging;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using TreeCraftingVoyager.Server.Constants;
using TreeCraftingVoyager.Server.Models.Management;

var builder = WebApplication.CreateBuilder(args);

// Add logging to the file
builder.AddFileLogger();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
       options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Identity services
builder.Services.AddIdentity<Account, Role>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Registration of dependencies according to convention
builder.Services.RegisterDependenciesByConvention();

// JWT Authentication Configuration
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        corsBuilder =>
        {
            corsBuilder.WithOrigins(ClientApps.OriginsListProd)
                       .AllowAnyHeader()
                       .AllowAnyMethod();
        });
});

// Add controllers
builder.Services.AddControllers();

// Add authorization policies
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
    options.AddPolicy("RequireUserRole", policy => policy.RequireRole("User"));
    // add more if needed
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // Update CORS policy to include localhost for development
    app.UseCors(options =>
    {
        options.WithOrigins(ClientApps.OriginsListDev)
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
}
else
{
    app.UseCors("AllowSpecificOrigins");
}

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseHttpsRedirection();

//app.UseMiddleware<ApiKeyMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapFallbackToFile("/index.html");

app.Run();
