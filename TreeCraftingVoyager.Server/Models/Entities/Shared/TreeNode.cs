using System.ComponentModel.DataAnnotations;
using TreeCraftingVoyager.Server.Models.Entities.Shared.EntityBase;

namespace TreeCraftingVoyager.Server.Models.Entities.Shared;

public abstract class TreeNode<TPrimaryKey, T> : EntityBase<TPrimaryKey>
    where TPrimaryKey : struct // type in Nullable<TPrimaryKey> in ParentId must be nullable type
    where T : TreeNode<TPrimaryKey, T>
{
    [StringLength(100)]
    public string Name { get; set; }

    public int DisplayOrder { get; set; }

    public Nullable<TPrimaryKey> ParentId { get; set; } // Nullable<> for entity framework migrations. TPrimaryKey? not working
  
    public virtual T Parent { get; set; }
   
    public virtual ICollection<T> Childrens { get; set; } = new List<T>();
}

public abstract class TreeNode<T> : TreeNode<long, T>
    where T : TreeNode<long, T>
{ 
}
