using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using TreeCraftingVoyager.Server.Data.SeedData;
using TreeCraftingVoyager.Server.Logging;
using TreeCraftingVoyager.Server.Models.Entities;
using System.Data;
using TreeCraftingVoyager.Server.Models.Management;

namespace TreeCraftingVoyager.Server.Data;

public class ApplicationDbContext : IdentityDbContext<Account, Role, string>
{
    private readonly IEnumerable<ISeeder>? _seeders;


    public DbSet<Category> Categories { get; set; }

    public DbSet<Product> Products { get; set; }


    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IEnumerable<ISeeder>? seeders = null) 
        : base(options)
    {
        _seeders = seeders?.OrderBy(seeder => seeder.Order);
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.Parent)
                .WithMany(e => e.Childrens)
                .HasForeignKey(e => e.ParentId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        if (!base.Database.GetAppliedMigrations().Any() && _seeders != null)
            foreach (var seeder in _seeders)
                seeder.Seed(modelBuilder);
    }
}
