using System.Text.Json.Serialization;
using TreeCraftingVoyager.Server.Models.Dto.Shared.EntityDto;

namespace TreeCraftingVoyager.Server.Models.Dto.Shared;

public abstract class TreeNodeDto<TPrimaryKey, T> : EntityDto<TPrimaryKey>
    where TPrimaryKey : struct
    where T : TreeNodeDto<TPrimaryKey, T>
{
    public string Name { get; set; }

    public int DisplayOrder { get; set; }

    public TPrimaryKey? ParentId { get; set; }

    [JsonIgnore]
    public virtual T Parent { get; set; }

    public virtual ICollection<T> Childrens { get; set; } = new List<T>();
}

public abstract class TreeNodeDto<T> : TreeNodeDto<long, T>
    where T : TreeNodeDto<long, T>
{
}
