using System.Linq.Expressions;
using TreeCraftingVoyager.Server.Attributes;
using TreeCraftingVoyager.Server.Configuration.Dependencies.DependencyLifecycleInterfaces;

namespace TreeCraftingVoyager.Server.Data.Repositories.Crud;

[RegisterOpenGenericInterfaceInDi(typeof(ICrudRepository<,,,,>))]
public interface ICrudRepository<TPrimaryKey, TEntityBase, TEntityDto, TUpdateDto, TCreateDto> : IPerWebRequestDependency
{
    Task<IEnumerable<TEntityDto>> GetAllRecursively();

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

    void Delete(TPrimaryKey id);

    Task DeleteAsync(TPrimaryKey id);

    void Delete(TEntityDto entity);

    Task DeleteAsync(TEntityDto entity);
}

[RegisterOpenGenericInterfaceInDi(typeof(ICrudRepository<,,,>))]
public interface ICrudRepository<TEntityBase, TEntityDto, TUpdateDto, TCreateDto> : 
    ICrudRepository<long, TEntityBase, TEntityDto, TUpdateDto, TCreateDto>
{ 
}