namespace TreeCraftingVoyager.Server.Models.Dto.Shared.EntityDto
{
    public interface IEntityDto<TPrimaryKey>
    {
        public TPrimaryKey Id { get; set; }
    }
}
