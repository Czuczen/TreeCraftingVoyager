using Microsoft.EntityFrameworkCore;
using TreeCraftingVoyager.Server.Configuration.Dependencies;
using TreeCraftingVoyager.Server.Data;
using TreeCraftingVoyager.Server.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add logging to the file
builder.AddFileLogger();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
       options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registration of dependencies according to convention
builder.Services.RegisterDependenciesByConvention();

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseDefaultFiles(); // needed??
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapFallbackToFile("/index.html");

app.Run();
