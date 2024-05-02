using TreeCraftingVoyager.Server.Models.Entities.Shared.EntityBase;

namespace TreeCraftingVoyager.Server.Data;

public static class DbContextExtensions
{
    public static string GetTableName<TPrimaryKey, TEntity>(this ApplicationDbContext context)
        where TPrimaryKey : struct
        where TEntity : class, IEntityBase<TPrimaryKey>, new()
    {
        var model = context.Model;
        var entityTypes = model.GetEntityTypes();
        var entityType = entityTypes.First(t => t.ClrType == typeof(TEntity));
        var tableNameAnnotation = entityType.GetAnnotation("Relational:TableName");
        return tableNameAnnotation.Value.ToString();
    }
}
