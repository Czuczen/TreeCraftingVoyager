using System.Text.Json.Serialization;
using TreeCraftingVoyager.Server.Models.Dto.Shared.EntityDto;
using TreeCraftingVoyager.Server.Models.Entities.Shared;

namespace TreeCraftingVoyager.Server.Models.Dto.Shared;

public abstract class TreeNodeDto<TPrimaryKey, T> : EntityDto<TPrimaryKey>
    where TPrimaryKey : struct // type in Nullable<TPrimaryKey> in ParentId must be nullable type
    where T : TreeNodeDto<TPrimaryKey, T>
{
    public string Name { get; set; }

    public int DisplayOrder { get; set; }

    public Nullable<TPrimaryKey> ParentId { get; set; } // Nullable<> for mapper. TPrimaryKey? not working
    [JsonIgnore]
    public virtual T Parent { get; set; }

    public virtual ICollection<T> Childrens { get; set; } = new List<T>();
}

public abstract class TreeNodeDto<T> : TreeNodeDto<long, T>
    where T : TreeNodeDto<long, T>
{
}
