using System.Linq.Expressions;
using TreeCraftingVoyager.Server.Attributes;
using TreeCraftingVoyager.Server.Configuration.Dependencies.DependencyLifecycleInterfaces;
using TreeCraftingVoyager.Server.Models.Dto.Shared.EntityDto;
using TreeCraftingVoyager.Server.Models.Entities.Shared.EntityBase;

namespace TreeCraftingVoyager.Server.Data.Repositories.Crud;

[RegisterOpenGenericInterfaceInDi(typeof(ICrudRepository<,,,,>))]
public interface ICrudRepository<TPrimaryKey, TEntityBase, TEntityDto, TUpdateDto, TCreateDto> : 
    IPerWebRequestDependency
    where TEntityBase : class, IEntityBase<TPrimaryKey>, new()
    where TEntityDto : class, IEntityDto<TPrimaryKey>, new()
    where TUpdateDto : class, IEntityDto<TPrimaryKey>, new()
    where TCreateDto : class, new()
{
    TEntityDto GetById(TPrimaryKey id);

    Task<TEntityDto> GetByIdAsync(TPrimaryKey id);

    IEnumerable<TEntityDto> GetWhere(Expression<Func<TEntityBase, bool>> predicate);

    Task<IEnumerable<TEntityDto>> GetWhereAsync(Expression<Func<TEntityBase, bool>> predicate);

    IQueryable<TEntityDto> GetQuery(Func<IQueryable<TEntityBase>, IQueryable<TEntityBase>> queryBuilder);

    IEnumerable<TEntityDto> GetAll();

    Task<IEnumerable<TEntityDto>> GetAllAsync();

    TEntityDto Create(TCreateDto entity);

    Task<TEntityDto> CreateAsync(TCreateDto entity);

    TEntityDto Update(TUpdateDto entity);

    Task<TEntityDto> UpdateAsync(TUpdateDto entity);

    Task<IEnumerable<TEntityDto>> CreateOrUpdateMultiAsync(IEnumerable<TEntityDto> entities);

    void Delete(TPrimaryKey id);

    Task DeleteAsync(TPrimaryKey id);

    void Delete(TEntityDto entity);

    Task DeleteAsync(TEntityDto entity);
}

[RegisterOpenGenericInterfaceInDi(typeof(ICrudRepository<,,,>))]
public interface ICrudRepository<TEntityBase, TEntityDto, TUpdateDto, TCreateDto> : 
    ICrudRepository<long, TEntityBase, TEntityDto, TUpdateDto, TCreateDto>
    where TEntityBase : class, IEntityBase<long>, new()
    where TEntityDto : class, IEntityDto<long>, new()
    where TUpdateDto : class, IEntityDto<long>, new()
    where TCreateDto : class, new()
{ 
}