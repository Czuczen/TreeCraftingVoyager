using System.ComponentModel.DataAnnotations;
using TreeCraftingVoyager.Server.Models.Entities.Shared.EntityBase;

namespace TreeCraftingVoyager.Server.Models.Entities.Shared;

public abstract class TreeNode<TPrimaryKey, T> : EntityBase<TPrimaryKey>
    where TPrimaryKey : struct
    where T : TreeNode<TPrimaryKey, T>
{
    [StringLength(100)]
    public string Name { get; set; }

    public int DisplayOrder { get; set; }

    public TPrimaryKey? ParentId { get; set; }

    public virtual T Parent { get; set; }
   
    public virtual ICollection<T> Childrens { get; set; } = new List<T>();
}

public abstract class TreeNode<T> : TreeNode<long, T>
    where T : TreeNode<long, T>
{ 
}
