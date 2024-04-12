using AutoMapper;
using TreeCraftingVoyager.Server.Models.Dto.Shared.EntityDto;
using TreeCraftingVoyager.Server.Models.Entities.Shared.EntityBase;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TreeCraftingVoyager.Server.Attributes;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using TreeCraftingVoyager.Server.Models.Entities.Shared;

namespace TreeCraftingVoyager.Server.Data.Repositories.Crud;

[RegisterOpenGenericClassInDi(typeof(CrudRepository<,,,,>))]
public class CrudRepository<TPrimaryKey, TEntityBase, TEntityDto, TUpdateDto, TCreateDto> 
    : ICrudRepository<TPrimaryKey, TEntityBase, TEntityDto, TUpdateDto, TCreateDto>
    where TEntityBase : class, IEntityBase<TPrimaryKey>, new()
    where TEntityDto : class, IEntityDto<TPrimaryKey>, new()
    where TUpdateDto : class, IEntityDto<TPrimaryKey>, new()
    where TCreateDto : class, new()
{
    protected readonly ApplicationDbContext _context;
    protected readonly IMapper _mapper;


    public CrudRepository(
        ApplicationDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


    public TEntityDto GetById(TPrimaryKey id)
    {
        var entity = _context.Set<TEntityBase>().Find(id);
        if (entity == null)
            throw new KeyNotFoundException($"Entity of type {typeof(TEntityBase).FullName} with ID {id} was not found.");

        return _mapper.Map<TEntityDto>(entity);
    }

    public async Task<TEntityDto> GetByIdAsync(TPrimaryKey id)
    {
        var entity = await _context.Set<TEntityBase>().FindAsync(id);
        if (entity == null)
            throw new KeyNotFoundException($"Entity of type {typeof(TEntityBase).FullName} with ID {id} was not found.");

        return _mapper.Map<TEntityDto>(entity);
    }

    public IEnumerable<TEntityDto> GetWhere(Expression<Func<TEntityBase, bool>> predicate)
    {
        var entities = _context.Set<TEntityBase>().Where(predicate).ToList();
        return _mapper.Map<IEnumerable<TEntityDto>>(entities);
    }

    public async Task<IEnumerable<TEntityDto>> GetWhereAsync(Expression<Func<TEntityBase, bool>> predicate)
    {
        var entities = await _context.Set<TEntityBase>().Where(predicate).ToListAsync();
        return _mapper.Map<IEnumerable<TEntityDto>>(entities);
    }

    public IQueryable<TEntityDto> GetQuery(Func<IQueryable<TEntityBase>, IQueryable<TEntityBase>> queryBuilder)
    {
        var query = _context.Set<TEntityBase>().AsQueryable();
        query = queryBuilder(query);

        return query.Select(entity => _mapper.Map<TEntityDto>(entity));
    }

    public IEnumerable<TEntityDto> GetAll()
    {
        var entities = _context.Set<TEntityBase>().ToList();
        return _mapper.Map<IEnumerable<TEntityDto>>(entities);
    }

    public async Task<IEnumerable<TEntityDto>> GetAllAsync()
    {
        var entities = await _context.Set<TEntityBase>().ToListAsync();
        return _mapper.Map<IEnumerable<TEntityDto>>(entities);
    }

    public TEntityDto Create(TCreateDto entity)
    {
        var entityEntry = _context.Set<TEntityBase>().Add(_mapper.Map<TEntityBase>(entity));
        _context.SaveChanges();

        return _mapper.Map<TEntityDto>(entityEntry.Entity);
    }

    public async Task<TEntityDto> CreateAsync(TCreateDto entity)
    {
        var entityEntry = await _context.Set<TEntityBase>().AddAsync(_mapper.Map<TEntityBase>(entity));
        await _context.SaveChangesAsync();

        return _mapper.Map<TEntityDto>(entityEntry.Entity);
    }

    public TEntityDto Update(TUpdateDto entity)
    {
        var existingEntity = _context.Set<TEntityBase>().Find(entity.Id);
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

        var entityEntry = _context.Set<TEntityBase>().Update(existingEntity);
        _context.SaveChanges();

        return _mapper.Map<TEntityDto>(entityEntry.Entity);
    }

    public async Task<TEntityDto> UpdateAsync(TUpdateDto entity)
    {
        var existingEntity = await _context.Set<TEntityBase>().FindAsync(entity.Id);
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

        var entityEntry = _context.Set<TEntityBase>().Update(existingEntity);
        await _context.SaveChangesAsync();

        return _mapper.Map<TEntityDto>(entityEntry.Entity);
    }

    public void Delete(TPrimaryKey id)
    {
        var entity = _context.Set<TEntityBase>().Find(id);
        if (entity == null)
            throw new KeyNotFoundException($"Entity of type {typeof(TEntityBase).FullName} with ID {id} was not found.");

        _context.Set<TEntityBase>().Remove(entity);
        _context.SaveChanges();
    }

    public async Task DeleteAsync(TPrimaryKey id)
    {
        var entity = await _context.Set<TEntityBase>().FindAsync(id);
        if (entity == null)
            throw new KeyNotFoundException($"Entity of type {typeof(TEntityBase).FullName} with ID {id} was not found.");

        _context.Set<TEntityBase>().Remove(entity);
        await _context.SaveChangesAsync();
    }

    public void Delete(TEntityDto entity)
    {
        _context.Set<TEntityBase>().Remove(_mapper.Map<TEntityBase>(entity));
        _context.SaveChanges();
    }

    public async Task DeleteAsync(TEntityDto entity)
    {
        _context.Set<TEntityBase>().Remove(_mapper.Map<TEntityBase>(entity));
        await _context.SaveChangesAsync();
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
