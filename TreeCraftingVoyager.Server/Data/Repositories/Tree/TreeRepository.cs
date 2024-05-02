using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using TreeCraftingVoyager.Server.Attributes;
using TreeCraftingVoyager.Server.Data;
using TreeCraftingVoyager.Server.Data.Repositories.Crud;
using TreeCraftingVoyager.Server.Data.Repositories.Tree;
using TreeCraftingVoyager.Server.Models.Dto.Shared.EntityDto;
using TreeCraftingVoyager.Server.Models.Entities.Shared;
using TreeCraftingVoyager.Server.Models.Entities.Shared.EntityBase;

namespace TreeCraftingVoyager.Server.Data.Repositories.Tree
{
    [RegisterOpenGenericClassInDi(typeof(TreeRepository<,,,,>))]
    public class TreeRepository<TPrimaryKey, TEntityBase, TEntityDto, TUpdateDto, TCreateDto> :
        CrudRepository<TPrimaryKey, TEntityBase, TEntityDto, TUpdateDto, TCreateDto>,
        ITreeRepository<TPrimaryKey, TEntityBase, TEntityDto, TUpdateDto, TCreateDto>
        where TPrimaryKey : struct
        where TEntityBase : TreeNode<TPrimaryKey, TEntityBase>, IEntityBase<TPrimaryKey>, new()
        where TEntityDto : class, IEntityDto<TPrimaryKey>, new()
        where TUpdateDto : class, IEntityDto<TPrimaryKey>, new()
        where TCreateDto : class, new()
    {
        public TreeRepository(
            ApplicationDbContext context, 
            IMapper mapper)
            : base (context, mapper)
        {
        }

        public async Task<IEnumerable<TLeaveDto>> GetCurrentNodeAndHisChildrensWithLeaves<TLeaveBase, TLeaveDto>(long currNodeId)
            where TLeaveBase : class, IEntityBase<TPrimaryKey>, new()
            where TLeaveDto : class, IEntityDto<TPrimaryKey>, new()            
        {
            var entities = await GetCurrentNodeAndHisChildrensWithLeaves<TLeaveBase>(currNodeId).ToListAsync();

            return Mapper.Map<IEnumerable<TLeaveDto>>(entities);
        }

        public IQueryable<TLeaveBase> GetCurrentNodeAndHisChildrensWithLeaves<TLeaveBase>(long currNodeId)
            where TLeaveBase : class, IEntityBase<TPrimaryKey>, new()
        {
            var leaveTableName = Context.GetTableName<TPrimaryKey, TLeaveBase>();
            var entityName = typeof(TEntityBase).Name;
            var tableName = Context.GetTableName<TPrimaryKey, TEntityBase>();
            var relFieldName = entityName + "Id";
            var paramName = "@" + relFieldName;

            string query = $@"
                WITH RECURSIVE subnodes AS (
                    SELECT ""Id""
                    FROM ""{tableName}""
                    WHERE ""Id"" = {paramName}
                    UNION ALL
                    SELECT c.""Id""
                    FROM ""{tableName}"" c
                    INNER JOIN subnodes sc ON c.""ParentId"" = sc.""Id""
                )
                SELECT p.*
                FROM ""{leaveTableName}"" p
                JOIN subnodes sc ON p.""{relFieldName}"" = sc.""Id""
                ";

            return Context.Set<TLeaveBase>().FromSqlRaw(query, new NpgsqlParameter(relFieldName, currNodeId));
        }

        public async Task<IEnumerable<TEntityDto>> GetRootObjects()
        {
            var ret = await Context.Set<TEntityBase>().Where(e => e.ParentId == null).ToListAsync();

            return Mapper.Map<IEnumerable<TEntityDto>>(ret);
        }

        public async Task<IEnumerable<TEntityDto>> GetAllRecursively()
        {
            var tableName = Context.GetTableName<TPrimaryKey, TEntityBase>();
            var query = $@"
                WITH RECURSIVE Tree AS (
                    SELECT *
                    FROM ""{tableName}""
                    WHERE ""ParentId"" IS NULL
                    UNION ALL
                    SELECT c.*
                    FROM ""{tableName}"" c
                    JOIN Tree t ON c.""ParentId"" = t.""Id""
                )
                SELECT * FROM Tree;
                ";

            var entities = await Context.Set<TEntityBase>().FromSqlRaw(query).ToListAsync();

            return Mapper.Map<IEnumerable<TEntityDto>>(entities);
        }

        public async Task<IEnumerable<TReturn>> GetAllRecursively<TReturn>()
        {
            var tableName = Context.GetTableName<TPrimaryKey, TEntityBase>();
            var query = $@"
                WITH RECURSIVE Tree AS (
                    SELECT *
                    FROM ""{tableName}""
                    WHERE ""ParentId"" IS NULL
                    UNION ALL
                    SELECT c.*
                    FROM ""{tableName}"" c
                    JOIN Tree t ON c.""ParentId"" = t.""Id""
                )
                SELECT * FROM Tree;
                ";

            var entities = await Context.Set<TEntityBase>().FromSqlRaw(query).ToListAsync();

            return Mapper.Map<IEnumerable<TReturn>>(entities);
        }
    }
}

[RegisterOpenGenericClassInDi(typeof(TreeRepository<,,,>))]
public class TreeRepository<TEntityBase, TEntityDto, TUpdateDto, TCreateDto>
    : TreeRepository<long, TEntityBase, TEntityDto, TUpdateDto, TCreateDto>,
    ITreeRepository<TEntityBase, TEntityDto, TUpdateDto, TCreateDto>
    where TEntityBase : TreeNode<TEntityBase>, IEntityBase<long>, new()
    where TEntityDto : class, IEntityDto<long>, new()
    where TUpdateDto : class, IEntityDto<long>, new()
    where TCreateDto : class, new()
{
    public TreeRepository(
        ApplicationDbContext context,
        IMapper mapper)
        : base(context, mapper)
    {
    }
}
