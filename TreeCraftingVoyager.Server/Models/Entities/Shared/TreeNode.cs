using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TreeCraftingVoyager.Server.Models.Entities.Shared.EntityBase;

namespace TreeCraftingVoyager.Server.Models.Entities.Shared;

public abstract class TreeNode<TPrimaryKey, T> : EntityBase<TPrimaryKey>
    where T : TreeNode<TPrimaryKey, T>
{
    public string Name { get; set; }

    public TPrimaryKey? ParentId { get; set; }

    //[ForeignKey("ParentId")]
    public virtual T Parent { get; set; }

    public virtual ICollection<T> Childrens { get; set; } = new List<T>();

    public int DisplayOrder { get; set; }
}

public abstract class TreeNode<T> : TreeNode<long, T>
    where T : TreeNode<long, T>
{ 
}
