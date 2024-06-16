using AutoMapper;
using TreeCraftingVoyager.Server.Models.Dto.Shared.EntityDto;
using TreeCraftingVoyager.Server.Models.Entities.Shared.EntityBase;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TreeCraftingVoyager.Server.Attributes;

namespace TreeCraftingVoyager.Server.Data.Repositories.Crud;

[RegisterOpenGenericClassInDi(typeof(CrudRepository<,,,,>))]
public class CrudRepository<TPrimaryKey, TEntityBase, TEntityDto, TUpdateDto, TCreateDto> 
    : ICrudRepository<TPrimaryKey, TEntityBase, TEntityDto, TUpdateDto, TCreateDto>
    where TEntityBase : class, IEntityBase<TPrimaryKey>, new()
    where TEntityDto : class, IEntityDto<TPrimaryKey>, new()
    where TUpdateDto : class, IEntityDto<TPrimaryKey>, new()
    where TCreateDto : class, new()
{
    protected readonly ApplicationDbContext Context;
    protected readonly IMapper Mapper;


    public CrudRepository(
        ApplicationDbContext context,
        IMapper mapper)
    {
        Context = context;
        Mapper = mapper;
    }


    public TEntityDto GetById(TPrimaryKey id)
    {
        var entity = Context.Set<TEntityBase>().Find(id);
        if (entity == null)
            throw new KeyNotFoundException($"Entity of type {typeof(TEntityBase).FullName} with ID {id} was not found.");

        return Mapper.Map<TEntityDto>(entity);
    }

    public async Task<TEntityDto> GetByIdAsync(TPrimaryKey id)
    {
        var entity = await Context.Set<TEntityBase>().FindAsync(id);
        if (entity == null)
            throw new KeyNotFoundException($"Entity of type {typeof(TEntityBase).FullName} with ID {id} was not found.");

        return Mapper.Map<TEntityDto>(entity);
    }

    public IEnumerable<TEntityDto> GetWhere(Expression<Func<TEntityBase, bool>> predicate)
    {
        var entities = Context.Set<TEntityBase>().Where(predicate).ToList();
        return Mapper.Map<IEnumerable<TEntityDto>>(entities);
    }

    public async Task<IEnumerable<TEntityDto>> GetWhereAsync(Expression<Func<TEntityBase, bool>> predicate)
    {
        var entities = await Context.Set<TEntityBase>().Where(predicate).ToListAsync();
        return Mapper.Map<IEnumerable<TEntityDto>>(entities);
    }

    public IQueryable<TEntityDto> GetQuery(Func<IQueryable<TEntityBase>, IQueryable<TEntityBase>> queryBuilder)
    {
        var query = Context.Set<TEntityBase>().AsQueryable();
        query = queryBuilder(query);

        return query.Select(entity => Mapper.Map<TEntityDto>(entity));
    }

    public IEnumerable<TEntityDto> GetAll()
    {
        var entities = Context.Set<TEntityBase>().ToList();
        return Mapper.Map<IEnumerable<TEntityDto>>(entities);
    }

    public async Task<IEnumerable<TEntityDto>> GetAllAsync()
    {
        var entities = await Context.Set<TEntityBase>().ToListAsync();
        return Mapper.Map<IEnumerable<TEntityDto>>(entities);
    }

    public TEntityDto Create(TCreateDto entity)
    {
        var entityEntry = Context.Set<TEntityBase>().Add(Mapper.Map<TEntityBase>(entity));
        Context.SaveChanges();

        return Mapper.Map<TEntityDto>(entityEntry.Entity);
    }

    public async Task<TEntityDto> CreateAsync(TCreateDto entity)
    {
        var entityEntry = await Context.Set<TEntityBase>().AddAsync(Mapper.Map<TEntityBase>(entity));
        await Context.SaveChangesAsync();

        return Mapper.Map<TEntityDto>(entityEntry.Entity);
    }

    public TEntityDto Update(TUpdateDto entity)
    {
        var existingEntity = Context.Set<TEntityBase>().Find(entity.Id);
        if (existingEntity == null)
            throw new KeyNotFoundException($"Entity of type {typeof(TEntityBase).FullName} with ID {entity.Id} was not found.");

        var updateType = typeof(TUpdateDto);
        var entityType = typeof(TEntityBase);
        var entityProperties = entityType.GetProperties().ToList();

        foreach (var updateProp in updateType.GetProperties())
        {
            var newVal = updateProp.GetValue(entity);
            var entProp = entityProperties.Single(item => item.Name == updateProp.Name);
            entProp.SetValue(existingEntity, newVal);
        }

        var entityEntry = Context.Set<TEntityBase>().Update(existingEntity);
        Context.SaveChanges();

        return Mapper.Map<TEntityDto>(entityEntry.Entity);
    }

    public async Task<TEntityDto> UpdateAsync(TUpdateDto entity)
    {
        var existingEntity = await Context.Set<TEntityBase>().FindAsync(entity.Id);
        if (existingEntity == null)
            throw new KeyNotFoundException($"Entity of type {typeof(TEntityBase).FullName} with ID {entity.Id} was not found.");

        var updateType = typeof(TUpdateDto);
        var entityType = typeof(TEntityBase);
        var entityProperties = entityType.GetProperties().ToList();

        foreach (var updateProp in updateType.GetProperties())
        {
            var newVal = updateProp.GetValue(entity);
            var entProp = entityProperties.Single(item => item.Name == updateProp.Name);
            entProp.SetValue(existingEntity, newVal);
        }

        var entityEntry = Context.Set<TEntityBase>().Update(existingEntity);
        await Context.SaveChangesAsync();

        return Mapper.Map<TEntityDto>(entityEntry.Entity);
    }


    public async Task<IEnumerable<TEntityDto>> CreateOrUpdateMultiAsync(IEnumerable<TEntityDto> entities)
    {
        var addEntities = new List<TEntityBase>();
        var updateEntities = new List<TEntityBase>();

        foreach (var item in entities)
        {
            if (EqualityComparer<TPrimaryKey>.Default.Equals(item.Id, default))
                addEntities.Add(Mapper.Map<TEntityBase>(item));
            else
            {
                var existingEntity = await Context.Set<TEntityBase>().FindAsync(item.Id);

                var updateType = typeof(TEntityDto);
                var entityType = typeof(TEntityBase);
                var entityProperties = entityType.GetProperties().ToList();

                foreach (var updateProp in updateType.GetProperties())
                {
                    var newVal = updateProp.GetValue(item);
                    var entProp = entityProperties.Single(item => item.Name == updateProp.Name);
                    entProp.SetValue(existingEntity, newVal);
                }

                updateEntities.Add(existingEntity);
            }
        }

        if (addEntities.Any())
            await Context.Set<TEntityBase>().AddRangeAsync(addEntities);

        if (updateEntities.Any())
            Context.Set<TEntityBase>().UpdateRange(updateEntities);

        await Context.SaveChangesAsync();

        return Mapper.Map<IEnumerable<TEntityDto>>(addEntities.Concat(updateEntities));
    }

    public void Delete(TPrimaryKey id)
    {
        var entity = Context.Set<TEntityBase>().Find(id);
        if (entity == null)
            throw new KeyNotFoundException($"Entity of type {typeof(TEntityBase).FullName} with ID {id} was not found.");

        Context.Set<TEntityBase>().Remove(entity);
        Context.SaveChanges();
    }

    public async Task DeleteAsync(TPrimaryKey id)
    {
        var entity = await Context.Set<TEntityBase>().FindAsync(id);
        if (entity == null)
            throw new KeyNotFoundException($"Entity of type {typeof(TEntityBase).FullName} with ID {id} was not found.");

        Context.Set<TEntityBase>().Remove(entity);
        await Context.SaveChangesAsync();
    }

    public void Delete(TEntityDto entity)
    {
        Context.Set<TEntityBase>().Remove(Mapper.Map<TEntityBase>(entity));
        Context.SaveChanges();
    }

    public async Task DeleteAsync(TEntityDto entity)
    {
        Context.Set<TEntityBase>().Remove(Mapper.Map<TEntityBase>(entity));
        await Context.SaveChangesAsync();
    }
}

[RegisterOpenGenericClassInDi(typeof(CrudRepository<,,,>))]
public class CrudRepository<TEntityBase, TEntityDto, TUpdateDto, TCreateDto>
    : CrudRepository<long, TEntityBase, TEntityDto, TUpdateDto, TCreateDto>, 
    ICrudRepository<TEntityBase, TEntityDto, TUpdateDto, TCreateDto>
    where TEntityBase : class, IEntityBase<long>, new()
    where TEntityDto : class, IEntityDto<long>, new()
    where TUpdateDto : class, IEntityDto<long>, new()
    where TCreateDto : class, new()
{
    public CrudRepository(
        ApplicationDbContext context,
        IMapper mapper) 
        : base (context, mapper)
    {
    }
}
