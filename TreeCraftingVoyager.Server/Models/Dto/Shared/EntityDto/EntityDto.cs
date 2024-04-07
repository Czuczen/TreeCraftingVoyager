namespace TreeCraftingVoyager.Server.Models.Dto.Shared.EntityDto;


public abstract class EntityDto<TPrimaryKey> : IEntityDto<TPrimaryKey>
{
    public TPrimaryKey Id { get; set; }
}

public abstract class EntityDto : IEntityDto<long>
{
    public long Id { get; set; }
}
