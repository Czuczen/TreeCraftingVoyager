using Microsoft.EntityFrameworkCore;
using TreeCraftingVoyager.Server.Configuration.Dependencies.DependencyLifecycleInterfaces;

namespace TreeCraftingVoyager.Server.Data.SeedData;

public interface ISeeder : ITransientDependency
{
    int Order { get; }

    void Seed(ModelBuilder modelBuilder);
}