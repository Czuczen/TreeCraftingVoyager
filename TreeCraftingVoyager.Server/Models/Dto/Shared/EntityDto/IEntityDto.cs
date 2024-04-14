namespace TreeCraftingVoyager.Server.Models.Dto.Shared.EntityDto;

public interface IEntityDto<TPrimaryKey>
{
    public TPrimaryKey Id { get; set; }
}

public interface IEntityDto : IEntityDto<long>
{
    public long Id { get; set; }
}
