using KNTC.Localization;
using KNTC.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

namespace KNTC.BaseMaps;

public class BaseMapAppService : CrudAppService<
            BaseMap,
            BaseMapDto,
            int,
            GetBaseMapListDto,
            CreateAndUpdateBaseMapDto>, IBaseMapAppService
{
    private readonly BaseMapManager _BaseMapManager;
    private readonly IDistributedCache<BaseMapLookupCache> _cache;

    public BaseMapAppService(IRepository<BaseMap, int> repository, BaseMapManager BaseMapManager, IDistributedCache<BaseMapLookupCache> cache) : base(repository)
    {
        LocalizationResource = typeof(KNTCResource);
        CreatePolicyName = KNTCPermissions.BaseMapPermission.Create;
        UpdatePolicyName = KNTCPermissions.BaseMapPermission.Edit;
        DeletePolicyName = KNTCPermissions.BaseMapPermission.Delete;
        _BaseMapManager = BaseMapManager;
        _cache = cache;
    }

    public override async Task<PagedResultDto<BaseMapDto>> GetListAsync(GetBaseMapListDto input)
    {
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = $"{nameof(BaseMap.OrderIndex)}, {nameof(BaseMap.BaseMapName)}";
        }

        var filter = !input.Keyword.IsNullOrEmpty() ? input.Keyword.Trim().ToUpper() : "";
        var queryable = await Repository.GetQueryableAsync();

        queryable = queryable
                    .WhereIf(!filter.IsNullOrEmpty(),
                             x => x.BaseMapCode.ToUpper().Contains(filter)
                                 || x.BaseMapName.ToUpper().Contains(filter)
                             )
                    .WhereIf(input.Status.HasValue, x => x.Status == input.Status)
                    .OrderBy(input.Sorting)
                    .Skip(input.SkipCount)
                    .Take(input.MaxResultCount);

        var queryResult = await AsyncExecuter.ToListAsync(queryable);

        var totalCount = await Repository.CountAsync(
                x => (input.Keyword.IsNullOrEmpty()
                    || (x.BaseMapCode.ToUpper().Contains(input.Keyword) || x.BaseMapName.ToUpper().Contains(input.Keyword)))
                && (!input.Status.HasValue || x.Status == input.Status)
                );

        return new PagedResultDto<BaseMapDto>(
            totalCount,
            ObjectMapper.Map<List<BaseMap>, List<BaseMapDto>>(queryResult)
        );
    }
    
    public async Task<ListResultDto<BaseMapLookupDto>> GetLookupAsync()
    {
        Random random = new Random();
        int randomNumber = random.Next(1, 11);
        var cacheItem = await _cache.GetOrAddAsync(
        "All",
        async () =>
        {
            var entities = await Repository.GetListAsync(x => x.Status == Status.Active);
            var dtos = ObjectMapper.Map<List<BaseMap>, List<BaseMapLookupDto>>(entities);
            return new BaseMapLookupCache() { Items = dtos };
        },
        () => new DistributedCacheEntryOptions
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(10).AddSeconds(randomNumber)
        });

        return new ListResultDto<BaseMapLookupDto>(cacheItem.Items);
    }

    public override async Task<BaseMapDto> CreateAsync(CreateAndUpdateBaseMapDto input)
    {
        var entity = await _BaseMapManager.CreateAsync(input.BaseMapCode.Trim(),
                                                          input.BaseMapName.Trim(),
                                                          input.Description,
                                                          input.OrderIndex,
                                                          input.Status);
        await Repository.InsertAsync(entity);
        await _cache.RemoveAsync("All");
        return ObjectMapper.Map<BaseMap, BaseMapDto>(entity);
    }

    public override async Task<BaseMapDto> UpdateAsync(int id, CreateAndUpdateBaseMapDto input)
    {
        var entity = await Repository.GetAsync(id, false);
        entity.SetConcurrencyStampIfNotNull(input.ConcurrencyStamp);
        await _BaseMapManager.UpdateAsync(entity,
                                           input.BaseMapCode.Trim(),
                                           input.BaseMapName.Trim(),
                                           input.Description,
                                           input.OrderIndex,
                                           input.Status);
        await Repository.UpdateAsync(entity);
        await _cache.RemoveAsync("All");
        return ObjectMapper.Map<BaseMap, BaseMapDto>(entity);
    }

    public override async Task DeleteAsync(int id)
    {
        await Repository.DeleteAsync(id);
        await _cache.RemoveAsync("All");
    }

    [Authorize(KNTCPermissions.BaseMapPermission.Delete)]
    public async Task DeleteMultipleAsync(IEnumerable<int> ids)
    {
        await Repository.DeleteManyAsync(ids);
        await _cache.RemoveAsync("All");
    }
}