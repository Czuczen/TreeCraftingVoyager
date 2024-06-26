﻿using TreeCraftingVoyager.Server.Attributes;
using TreeCraftingVoyager.Server.Configuration.Dependencies.DependencyLifecycleInterfaces;
using TreeCraftingVoyager.Server.Models.Dto.Shared.EntityDto;
using TreeCraftingVoyager.Server.Models.Entities.Shared.EntityBase;
using TreeCraftingVoyager.Server.Models.Entities.Shared;
using TreeCraftingVoyager.Server.Data.Repositories.Crud;

namespace TreeCraftingVoyager.Server.Data.Repositories.Tree;

[RegisterOpenGenericInterfaceInDi(typeof(ITreeRepository<,,,,>))]
public interface ITreeRepository<TPrimaryKey, TEntityBase, TEntityDto, TUpdateDto, TCreateDto> : 
    ICrudRepository<TPrimaryKey, TEntityBase, TEntityDto, TUpdateDto, TCreateDto>,
    IPerWebRequestDependency
    where TPrimaryKey : struct
    where TEntityBase : TreeNode<TPrimaryKey, TEntityBase>, IEntityBase<TPrimaryKey>, new()
    where TEntityDto : class, IEntityDto<TPrimaryKey>, new()
    where TUpdateDto : class, IEntityDto<TPrimaryKey>, new()
    where TCreateDto : class, new()
{
    Task<IEnumerable<TLeaveDto>> GetCurrentNodeAndHisChildrensWithLeaves<TLeaveBase, TLeaveDto>(long currNodeId)
        where TLeaveBase : class, IEntityBase<TPrimaryKey>, new()
        where TLeaveDto : class, IEntityDto<TPrimaryKey>, new();

    IQueryable<TLeaveBase> GetCurrentNodeAndHisChildrensWithLeaves<TLeaveBase>(long currNodeId)
        where TLeaveBase : class, IEntityBase<TPrimaryKey>, new();

    Task<IEnumerable<TEntityDto>> GetRootObjects();

    Task<IEnumerable<TEntityDto>> GetAllRecursively();

    Task<IEnumerable<TReturn>> GetAllRecursively<TReturn>();
}

[RegisterOpenGenericInterfaceInDi(typeof(ITreeRepository<,,,>))]
public interface ITreeRepository<TEntityBase, TEntityDto, TUpdateDto, TCreateDto> : 
    ITreeRepository<long, TEntityBase, TEntityDto, TUpdateDto, TCreateDto>
    where TEntityBase : TreeNode<TEntityBase>, IEntityBase<long>, new()
    where TEntityDto : class, IEntityDto<long>, new()
    where TUpdateDto : class, IEntityDto<long>, new()
    where TCreateDto : class, new()
{
}
