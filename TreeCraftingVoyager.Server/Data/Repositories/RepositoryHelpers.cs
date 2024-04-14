namespace TreeCraftingVoyager.Server.Data.Repositories;

public static class RepositoryHelpers
{
    public static string GetTableNameByEntityDbName(string entityDbName)
    {
        const string startFulName = "Microsoft.EntityFrameworkCore.DbSet`1[[TreeCraftingVoyager.Server.Models.Entities.";
        var tablesProperties = typeof(ApplicationDbContext).GetProperties().Where(item =>
            item.PropertyType.FullName != null && item.PropertyType.FullName.Contains(startFulName)).ToList();

        return tablesProperties.Single(item =>
        {
            var currEntityTableName = item.PropertyType.FullName?.Split(new[] { ",", "." }, StringSplitOptions.None)
                .SingleOrDefault(element => element == entityDbName);
            return !string.IsNullOrWhiteSpace(currEntityTableName);
        }).Name;
    }
}