using KNTC.Localization;
using KNTC.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
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
    private readonly IDistributedCache<UnitLookupCache, UnitCacheKey> _cache;

    public UnitAppService(IRepository<Unit, int> repository, UnitManager unitManager,
        IDistributedCache<UnitLookupCache, UnitCacheKey> cache) : base(repository)
    {
        LocalizationResource = typeof(KNTCResource);
        CreatePolicyName = KNTCPermissions.UnitPermission.Create;
        UpdatePolicyName = KNTCPermissions.UnitPermission.Edit;
        DeletePolicyName = KNTCPermissions.UnitPermission.Delete;
        _unitManager = unitManager;
        _cache = cache;
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

    public async Task<ListResultDto<UnitLookupDto>> GetLookupAsync(int unitTypeId, int? parentId = null)
    {
        Random random = new Random();
        int randomNumber = random.Next(1, 11);
        var cacheItem = await _cache.GetOrAddAsync(
        new UnitCacheKey(unitTypeId, parentId),
        async () => await GetListLookup(unitTypeId, parentId),
        () => new DistributedCacheEntryOptions
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(10).AddSeconds(randomNumber)
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

    public override async Task<UnitDto> CreateAsync(CreateAndUpdateUnitDto input)
    {
        var entity = await _unitManager.CreateAsync(input.UnitCode.Trim(),
                                                   input.UnitName.Trim(),
                                                   input.ShortName.Trim(),
                                                   input.UnitTypeId,
                                                   input.Description,
                                                   input.OrderIndex,
                                                   input.Status);
        await Repository.InsertAsync(entity);
        await _cache.RemoveAsync(new UnitCacheKey(entity.UnitTypeId, entity.ParentId));
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
                                       input.Description,
                                       input.OrderIndex,
                                       input.Status);
        await Repository.UpdateAsync(entity);
        await _cache.RemoveAsync(new UnitCacheKey(entity.UnitTypeId, entity.ParentId));
        return ObjectMapper.Map<Unit, UnitDto>(entity);
    }

    public override async Task DeleteAsync(int id)
    {
        var entity = await Repository.GetAsync(id, false);
        await _cache.RemoveAsync(new UnitCacheKey(entity.UnitTypeId, entity.ParentId));
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
        await _cache.RemoveManyAsync(lstCacheKey);
        await Repository.DeleteManyAsync(ids);
    }
}