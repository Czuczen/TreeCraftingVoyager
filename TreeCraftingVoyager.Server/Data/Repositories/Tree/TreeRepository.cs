using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
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
        ITreeRepository<TPrimaryKey, TEntityBase, TEntityDto, TUpdateDto, TCreateDto>
        where TPrimaryKey : struct
        where TEntityBase : TreeNode<TPrimaryKey, TEntityBase>, IEntityBase<TPrimaryKey>, new()
        where TEntityDto : class, IEntityDto<TPrimaryKey>, new()
        where TUpdateDto : class, IEntityDto<TPrimaryKey>, new()
        where TCreateDto : class, new()
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;


        public TreeRepository(
            ApplicationDbContext context, 
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<IEnumerable<TEntityDto>> GetRootObjects()
        {
            var ret = await _context.Set<TEntityBase>().Where(e => e.ParentId == null).ToListAsync();

            return _mapper.Map<IEnumerable<TEntityDto>>(ret);
        }

        public async Task<IEnumerable<TEntityDto>> GetAllRecursively()
        {
            var tableName = GetTableNameByEntityDbName(typeof(TEntityBase).Name);
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

            var entities = await _context.Set<TEntityBase>().FromSqlRaw(query).ToListAsync();

            return _mapper.Map<IEnumerable<TEntityDto>>(entities);
        }

        public async Task<IEnumerable<TReturn>> GetAllRecursively<TReturn>()
        {
            var tableName = GetTableNameByEntityDbName(typeof(TEntityBase).Name);
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

            var entities = await _context.Set<TEntityBase>().FromSqlRaw(query).ToListAsync();

            return _mapper.Map<IEnumerable<TReturn>>(entities);
        }

        // ====================================================================================================

        protected static string GetTableNameByEntityDbName(string entityDbName)
        {
            const string startFulName = "Microsoft.EntityFrameworkCore.DbSet`1[[TreeCraftingVoyager.Server.Models.Entities.";
            var tablesProperties = typeof(ApplicationDbContext).GetProperties().Where(item =>
                item.PropertyType.FullName != null && item.PropertyType.FullName.Contains(startFulName)).ToList();

            return tablesProperties.Single(item =>
            {
                var currEntityTableName = item.PropertyType.FullName?.Split(new[] { ",", "." }, StringSplitOptions.None)
                    .SingleOrDefault(element => element == entityDbName);
                return !string.IsNullOrWhiteSpace(currEntityTableName);
            }).Name;
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
