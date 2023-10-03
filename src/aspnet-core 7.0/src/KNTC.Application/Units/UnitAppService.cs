using KNTC.CategoryUnitTypes;
using KNTC.Localization;
using KNTC.Permissions;
using KNTC.RedisCache;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using NPOI.POIFS.Properties;
using NPOI.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Caching;
using Volo.Abp.Data;
using Volo.Abp.Domain.Repositories;

namespace KNTC.Units;

public class UnitAppService : CrudAppService<
            Unit,
            UnitDto,
            int,
            GetUnitListDto,
            CreateAndUpdateUnitDto>, IUnitAppService
{
    private readonly UnitManager _unitManager;
    private readonly IDistributedCache<UnitLookupCache, UnitCacheKey> _cacheUnit;
    private readonly IDistributedCache<UnitLookupCache, UnitLookupByIdsKey> _cacheUnitByids;
    private readonly IDistributedCache<UnitLookupCache, UnitLookupByParentIdsKey> _cacheUnitByParentIds;
    private readonly IDistributedCache<UnitTreeLookupCache> _cacheUnitTree;
    private readonly IRedisCacheService _cacheService;

    public UnitAppService(IRepository<Unit, int> repository,
        UnitManager unitManager,
        IDistributedCache<UnitLookupCache, UnitCacheKey> cache,
        IDistributedCache<UnitTreeLookupCache> cacheUnitTree,
        IDistributedCache<UnitLookupCache, UnitLookupByIdsKey> cacheUnitByids,
        IDistributedCache<UnitLookupCache, UnitLookupByParentIdsKey> cacheUnitByParentIds,
        IRedisCacheService cacheService) : base(repository)
    {
        LocalizationResource = typeof(KNTCResource);
        CreatePolicyName = KNTCPermissions.UnitPermission.Create;
        UpdatePolicyName = KNTCPermissions.UnitPermission.Edit;
        DeletePolicyName = KNTCPermissions.UnitPermission.Delete;
        _unitManager = unitManager;
        _cacheUnit = cache;
        _cacheUnitTree = cacheUnitTree;
        _cacheUnitByids = cacheUnitByids;
        _cacheUnitByParentIds = cacheUnitByParentIds;
        _cacheService = cacheService;
    }

    public override async Task<PagedResultDto<UnitDto>> GetListAsync(GetUnitListDto input)
    {
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = $"{nameof(Unit.OrderIndex)}, {nameof(Unit.Id)}";
        }
        var filter = !input.Keyword.IsNullOrEmpty() ? input.Keyword.Trim().ToUpper() : "";
        var queryable = await Repository.GetQueryableAsync();

        queryable = queryable
                    .WhereIf(!filter.IsNullOrEmpty(),
                             x => x.UnitCode.ToUpper().Contains(filter)
                                 || x.UnitName.ToUpper().Contains(filter)
                                 || x.ShortName.ToUpper().Contains(filter)
                             )
                    .WhereIf(input.UnitTypeId.HasValue, x => x.UnitTypeId == input.UnitTypeId)
                    .WhereIf(input.ParentId.HasValue, x => x.ParentId == input.ParentId)
                    .WhereIf(input.Status.HasValue, x => x.Status == input.Status)
                    .OrderBy(input.Sorting)
                    .Skip(input.SkipCount)
                    .Take(input.MaxResultCount);

        var queryResult = await AsyncExecuter.ToListAsync(queryable);

        var totalCount = await Repository.CountAsync(
                x => (input.Keyword.IsNullOrEmpty()
                    || (x.UnitCode.ToUpper().Contains(input.Keyword)
                    || x.UnitName.ToUpper().Contains(input.Keyword)
                    || x.ShortName.ToUpper().Contains(input.Keyword)))
                && (!input.UnitTypeId.HasValue || x.UnitTypeId == input.UnitTypeId)
                && (!input.ParentId.HasValue || x.ParentId == input.ParentId)
                && (!input.Status.HasValue || x.Status == input.Status)
                );

        return new PagedResultDto<UnitDto>(
            totalCount,
            ObjectMapper.Map<List<Unit>, List<UnitDto>>(queryResult)
        );
    }

    // 
    public async Task<ListResultDto<UnitLookupDto>> GetLookupAsync(int unitTypeId, int? parentId = null)
    {
        Random random = new Random();
        int randomNumber = random.Next(1, 11);
        var cacheItem = await _cacheUnit.GetOrAddAsync(
        new UnitCacheKey(unitTypeId, parentId),
        async () => await GetListLookup(unitTypeId, parentId),
        () => new DistributedCacheEntryOptions
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(10).AddSeconds(randomNumber),
            
        });
        return new ListResultDto<UnitLookupDto>(cacheItem.Items);
    }

    public async Task<ListResultDto<UnitLookupDto>> GetLookupByIdsAsync(int[]? unitIds)
    {
        Random random = new Random();
        int randomNumber = random.Next(1, 11);
        var cacheItem = await _cacheUnitByids.GetOrAddAsync(
        new UnitLookupByIdsKey(unitIds),
        async () => await GetListLookupByIds(unitIds),
        () => new DistributedCacheEntryOptions
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(10).AddSeconds(randomNumber),
            
        });
        return new ListResultDto<UnitLookupDto>(cacheItem.Items);
    }

    public async Task<ListResultDto<UnitLookupDto>> GetLookupByParentIdsAsync(int unitTypeId, int[]? parentIds)
    {
        Random random = new Random();
        int randomNumber = random.Next(1, 11);
        var cacheItem = await _cacheUnitByParentIds.GetOrAddAsync(
        new UnitLookupByParentIdsKey(unitTypeId, parentIds),
        async () => await GetListLookupByParentIds(unitTypeId, parentIds),
        () => new DistributedCacheEntryOptions
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(10).AddSeconds(randomNumber),
            
        });
        return new ListResultDto<UnitLookupDto>(cacheItem.Items);
    }

    private async Task<UnitLookupCache> GetListLookup(int unitTypeId, int? parentId)
    {
        var queryable = await Repository.GetQueryableAsync();
        queryable = queryable
                    .Where(x => x.Status == Status.Active)
                    .Where(x => x.UnitTypeId == unitTypeId)
                    .WhereIf(parentId.HasValue, x => x.ParentId == parentId)
                    .OrderBy(nameof(UnitLookupDto.UnitName));
        var entities = await AsyncExecuter.ToListAsync(queryable);
        var dtos = ObjectMapper.Map<List<Unit>, List<UnitLookupDto>>(entities);
        var result = new UnitLookupCache() { Items = dtos };
        return result;
    }

    private async Task<UnitLookupCache> GetListLookupByIds(int[]? unitIds)
    {
        var queryable = await Repository.GetQueryableAsync();
        queryable = queryable
                    .Where(x => x.Status == Status.Active)
                    .WhereIf(!unitIds.IsNullOrEmpty(), x => unitIds.Contains(x.Id))
                    .OrderBy(nameof(UnitLookupDto.UnitName));
        var entities = await AsyncExecuter.ToListAsync(queryable);
        var dtos = ObjectMapper.Map<List<Unit>, List<UnitLookupDto>>(entities);

        var result = new UnitLookupCache() { Items = dtos };
        return result;
    }

    private async Task<UnitLookupCache> GetListLookupByParentIds(int unitTypeId, int[]? ParentIds)
    {
        var queryable = await Repository.GetQueryableAsync();
        queryable = queryable
                    .Where(x => x.Status == Status.Active)
                    .Where(x => x.UnitTypeId == unitTypeId)
                    .WhereIf(!ParentIds.IsNullOrEmpty(), x => x.ParentId.HasValue && ParentIds.Contains(x.ParentId.Value))
                    .OrderBy(nameof(UnitLookupDto.UnitName));
        var entities = await AsyncExecuter.ToListAsync(queryable);
        var dtos = ObjectMapper.Map<List<Unit>, List<UnitLookupDto>>(entities);
        var result = new UnitLookupCache() { Items = dtos };
        return result;
    }

    public override async Task<UnitDto> CreateAsync(CreateAndUpdateUnitDto input)
    {
        var entity = await _unitManager.CreateAsync(input.UnitCode.Trim(),
                                                   input.UnitName.Trim(),
                                                   input.ShortName.Trim(),
                                                   input.UnitTypeId,
                                                   input.ParentId,
                                                   input.Description,
                                                   input.OrderIndex,
                                                   input.Status);
        await Repository.InsertAsync(entity);
        await _cacheService.DeleteCacheKeysSContainAsync(nameof(UnitCacheKey));
        return ObjectMapper.Map<Unit, UnitDto>(entity);
    }

    public override async Task<UnitDto> UpdateAsync(int id, CreateAndUpdateUnitDto input)
    {
        var entity = await Repository.GetAsync(id, false);
        entity.SetConcurrencyStampIfNotNull(input.ConcurrencyStamp);
        await _unitManager.UpdateAsync(entity,
                                       input.UnitCode.Trim(),
                                       input.UnitName.Trim(),
                                       input.ShortName.Trim(),
                                       input.UnitTypeId,
                                       input.ParentId,
                                       input.Description,
                                       input.OrderIndex,
                                       input.Status);
        await Repository.UpdateAsync(entity);
        await _cacheService.DeleteCacheKeysSContainAsync(nameof(UnitCacheKey));
        return ObjectMapper.Map<Unit, UnitDto>(entity);
    }

    public override async Task DeleteAsync(int id)
    {
        var entity = await Repository.GetAsync(id, false);
        await _cacheService.DeleteCacheKeysSContainAsync(nameof(UnitCacheKey));
        await Repository.DeleteAsync(id);
    }

    [Authorize(KNTCPermissions.UnitPermission.Delete)]
    public async Task DeleteMultipleAsync(IEnumerable<int> ids)
    {
        if (ids.Count() <= 0) return;
        var lstCacheKey = new List<UnitCacheKey>();
        var entities = await Repository.GetListAsync(x => ids.Contains(x.Id), false);
        foreach (var entity in entities)
        {
            lstCacheKey.Add(new UnitCacheKey(entity.UnitTypeId, entity.ParentId));
        }
        await _cacheUnit.RemoveManyAsync(lstCacheKey);
        await Repository.DeleteManyAsync(ids);
    }
    
    public async Task<ListResultDto<UnitTreeLookupDto>> GetTreeLookupAsync(int id)
    {
        Random random = new Random();
        int randomNumber = random.Next(1, 11);
        var cacheItem = await _cacheUnitTree.GetOrAddAsync(
        $"T{nameof(UnitCacheKey)}_ree_{id}",
        async () => await GetTreeLookupFromDbAsync(id),
        () => new DistributedCacheEntryOptions
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(10).AddSeconds(randomNumber),
            
        });
        return new ListResultDto<UnitTreeLookupDto>(cacheItem.Items);
    }

    private async Task<UnitTreeLookupCache> GetTreeLookupFromDbAsync(int id)
    {
        var result = new List<UnitTreeLookupDto>();
        if (id != null)
        {
            var unit = await Repository.GetAsync(x => x.Id == id);
            var childNode = new UnitTreeLookupDto
            {
                Id = unit.Id,
                UnitName = unit.UnitName,
                UnitTypeId = unit.UnitTypeId,
                Children = await GetUnitChildrenRecursive(unit.Id)
            };
            result.Add(childNode);
        }
        else
        {
            var units = await Repository.GetListAsync();
            foreach (var unit in units)
            {
                var childNode = new UnitTreeLookupDto
                {
                    Id = unit.Id,
                    UnitName = unit.UnitName,
                    UnitTypeId = unit.UnitTypeId,
                    Children = await GetUnitChildrenRecursive(unit.Id)
                };
                result.Add(childNode);
            }
        }
        return new UnitTreeLookupCache()
        {
            Items = result
        };
    }
    private async Task<List<UnitTreeLookupDto>> GetUnitChildrenRecursive(int parentId)
    {
        var childUnits = await Repository.GetListAsync(x => x.ParentId== parentId);
        var childNodes = new List<UnitTreeLookupDto>();

        foreach (var childUnit in childUnits)
        {
            var childNode = new UnitTreeLookupDto
            {
                Id = childUnit.Id,
                UnitName = childUnit.UnitName,
                UnitTypeId = childUnit.UnitTypeId,
                Children = await GetUnitChildrenRecursive(childUnit.Id)
            };

            childNodes.Add(childNode);
        }

        return childNodes;
    }
}