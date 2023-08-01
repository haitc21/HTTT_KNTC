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

namespace KNTC.SysConfigs;

[Authorize(KNTCPermissions.SysConfigsPermission.Default)]
public class SysConfigAppService : CrudAppService<
            SysConfig,
            SysConfigDto,
            int,
            GetSysConfigListDto,
            CreateSysConfigDto,
            UpdateSysConfigDto>, ISysConfigAppService
{
    private readonly SysConfigManager _configManager;
    private readonly IDistributedCache<SysConfigCacheItem, string> _cache;
    private readonly IDistributedCache<AllSysConfigCacheItem, string> _cacheAll;

    public SysConfigAppService(IRepository<SysConfig, int> repository,
        SysConfigManager configManager,
        IDistributedCache<SysConfigCacheItem, string> cache,
        IDistributedCache<AllSysConfigCacheItem, string> cacheAll) : base(repository)
    {
        LocalizationResource = typeof(KNTCResource);
        CreatePolicyName = KNTCPermissions.SysConfigsPermission.Create;
        UpdatePolicyName = KNTCPermissions.SysConfigsPermission.Edit;
        DeletePolicyName = KNTCPermissions.SysConfigsPermission.Delete;
        _configManager = configManager;
        _cache = cache;
        _cacheAll = cacheAll;
    }

    [AllowAnonymous]
    [ResponseCache(VaryByHeader = "User-Agent", Duration = 30)]
    public async Task<SysConfigCacheItem> GetByNameAsync(string name)
    {
        Random random = new Random();
        int randomNumber = random.Next(1, 11);
        var cacheItem = await _cache.GetOrAddAsync(
        $"{name}",
        async () =>
        {
            var entity = await Repository.GetAsync(x => x.Name == name);
            return new SysConfigCacheItem() { Name = name, Value = entity.Value };
        },
        () => new DistributedCacheEntryOptions
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(10).AddSeconds(randomNumber)
        });
        return cacheItem;
    }

    [Authorize(KNTCPermissions.SysConfigsPermission.Default)]
    public override async Task<PagedResultDto<SysConfigDto>> GetListAsync(GetSysConfigListDto input)
    {
        if (input.Sorting.IsNullOrEmpty())
        {
            input.Sorting = $"{nameof(SysConfig.Name)}";
        }
        var filter = !input.Keyword.IsNullOrEmpty() ? input.Keyword.Trim().ToUpper() : "";
        var queryable = await Repository.GetQueryableAsync();

        queryable = queryable
                    .WhereIf(!filter.IsNullOrEmpty(), x => x.Name.ToUpper().Contains(filter))
                    .OrderBy(input.Sorting)
                    .Skip(input.SkipCount)
                    .Take(input.MaxResultCount);
        var queryResult = await AsyncExecuter.ToListAsync(queryable);
        var totalCount = await Repository.CountAsync(x => input.Keyword.IsNullOrEmpty() || (x.Name.ToUpper().Contains(input.Keyword)));
        return new PagedResultDto<SysConfigDto>(
            totalCount,
            ObjectMapper.Map<List<SysConfig>, List<SysConfigDto>>(queryResult)
        );
    }

    [Authorize(KNTCPermissions.SysConfigsPermission.Create)]
    public override async Task<SysConfigDto> CreateAsync(CreateSysConfigDto input)
    {
        Random random = new Random();
        int randomNumber = random.Next(1, 11);
        var entity = await _configManager.CreateAsync(input.Name.Trim(), input.Value, input.Description);
        await Repository.InsertAsync(entity);
        await _cache.RemoveAsync($"{entity.Name}");
        await _cacheAll.RemoveAsync("All");
        return ObjectMapper.Map<SysConfig, SysConfigDto>(entity);
    }

    [Authorize(KNTCPermissions.SysConfigsPermission.Edit)]
    public override async Task<SysConfigDto> UpdateAsync(int id, UpdateSysConfigDto input)
    {
        var entity = await Repository.GetAsync(id, false);
        entity.SetConcurrencyStampIfNotNull(input.ConcurrencyStamp);
        await _configManager.UpdateAsync(entity, input.Value, input.Description);
        await Repository.UpdateAsync(entity);
        await _cache.RemoveAsync($"{entity.Name}");
        await _cacheAll.RemoveAsync("All");
        return ObjectMapper.Map<SysConfig, SysConfigDto>(entity);
    }

    [Authorize(KNTCPermissions.SysConfigsPermission.Delete)]
    public override async Task DeleteAsync(int id)
    {
        var entity = await Repository.GetAsync(id, false);
        await Repository.DeleteAsync(entity);
        await _cache.RemoveAsync($"{entity.Name}");
        await _cacheAll.RemoveAsync("All");
    }

    [Authorize(KNTCPermissions.SysConfigsPermission.Delete)]
    public async Task DeleteMultipleAsync(IEnumerable<int> ids)
    {
        if (ids.Count() <= 0) return;
        var lstCacheKey = new List<string>();
        var entities = await Repository.GetListAsync(x => ids.Contains(x.Id), false);
        foreach (var entity in entities)
        {
            lstCacheKey.Add($"{entity.Name}");
        }
        await _cache.RemoveManyAsync(lstCacheKey);
        await _cacheAll.RemoveAsync("All");
        await Repository.DeleteManyAsync(ids);
    }

    [AllowAnonymous]
    [ResponseCache(VaryByHeader = "User-Agent", Duration = 30)]
    public async Task<AllSysConfigCacheItem> GetAllConfigsAsync()
    {
        Random random = new Random();
        int randomNumber = random.Next(1, 11);
        var result = await _cacheAll.GetOrAddAsync(
        "All",
        async () =>
        {
            var entities = await Repository.GetListAsync();
            var items = ObjectMapper.Map<List<SysConfig>, List<SysConfigCacheItem>>(entities);
            return new AllSysConfigCacheItem() { Items = items };
        },
        () => new DistributedCacheEntryOptions
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(10).AddSeconds(randomNumber)
        });
        return result;
    }
}