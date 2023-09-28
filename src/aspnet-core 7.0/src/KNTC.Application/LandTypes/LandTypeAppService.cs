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

namespace KNTC.LandTypes;

public class LandTypeAppService : CrudAppService<
            LandType,
            LandTypeDto,
            int,
            GetLandTypesListDto,
            CreateAndUpdateLandTypeDto>, ILandTypeAppService
{
    private readonly LandTypeManager _landTypeManager;
    private readonly IDistributedCache<LandTypeLookupCache> _cache;

    public LandTypeAppService(IRepository<LandType, int> repository, LandTypeManager landTypeManager, IDistributedCache<LandTypeLookupCache> cache) : base(repository)
    {
        LocalizationResource = typeof(KNTCResource);
        CreatePolicyName = KNTCPermissions.LandTypePermission.Create;
        UpdatePolicyName = KNTCPermissions.LandTypePermission.Edit;
        DeletePolicyName = KNTCPermissions.LandTypePermission.Delete;
        _landTypeManager = landTypeManager;
        _cache = cache;
    }

    public override async Task<PagedResultDto<LandTypeDto>> GetListAsync(GetLandTypesListDto input)
    {
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = $"{nameof(LandType.OrderIndex)}, {nameof(LandType.LandTypeName)}";
        }

        var filter = !input.Keyword.IsNullOrEmpty() ? input.Keyword.Trim().ToUpper() : "";
        var queryable = await Repository.GetQueryableAsync();

        queryable = queryable
                    .WhereIf(!filter.IsNullOrEmpty(),
                             x => x.LandTypeCode.ToUpper().Contains(filter)
                                 || x.LandTypeName.ToUpper().Contains(filter)
                             )
                    .WhereIf(input.Status.HasValue, x => x.Status == input.Status)
                    .OrderBy(input.Sorting)
                    .Skip(input.SkipCount)
                    .Take(input.MaxResultCount);

        var queryResult = await AsyncExecuter.ToListAsync(queryable);

        var totalCount = await Repository.CountAsync(
                x => (input.Keyword.IsNullOrEmpty()
                    || (x.LandTypeCode.ToUpper().Contains(input.Keyword) || x.LandTypeName.ToUpper().Contains(input.Keyword)))
                && (!input.Status.HasValue || x.Status == input.Status)
                );

        return new PagedResultDto<LandTypeDto>(
            totalCount,
            ObjectMapper.Map<List<LandType>, List<LandTypeDto>>(queryResult)
        );
    }

    [ResponseCache(VaryByHeader = "User-Agent", Duration = 10)]
    public async Task<ListResultDto<LandTypeLookupDto>> GetLookupAsync()
    {
        Random random = new Random();
        int randomNumber = random.Next(1, 11);
        var cacheItem = await _cache.GetOrAddAsync(
        "All",
        async () =>
        {
            var entities = await Repository.GetListAsync(x => x.Status == Status.Active);
            var dtos = ObjectMapper.Map<List<LandType>, List<LandTypeLookupDto>>(entities);
            return new LandTypeLookupCache() { Items = dtos };
        },
        () => new DistributedCacheEntryOptions
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(1).AddSeconds(randomNumber),
            SlidingExpiration = TimeSpan.FromSeconds(30)
        });

        return new ListResultDto<LandTypeLookupDto>(cacheItem.Items);
    }

    public override async Task<LandTypeDto> CreateAsync(CreateAndUpdateLandTypeDto input)
    {
        var entity = await _landTypeManager.CreateAsync(input.LandTypeCode.Trim(),
                                                          input.LandTypeName.Trim(),
                                                          input.Description,
                                                          input.OrderIndex,
                                                          input.Status);
        await Repository.InsertAsync(entity);
        await _cache.RemoveAsync("All");
        return ObjectMapper.Map<LandType, LandTypeDto>(entity);
    }

    public override async Task<LandTypeDto> UpdateAsync(int id, CreateAndUpdateLandTypeDto input)
    {
        var entity = await Repository.GetAsync(id, false);
        entity.SetConcurrencyStampIfNotNull(input.ConcurrencyStamp);
        await _landTypeManager.UpdateAsync(entity,
                                           input.LandTypeCode.Trim(),
                                           input.LandTypeName.Trim(),
                                           input.Description,
                                           input.OrderIndex,
                                           input.Status);
        await Repository.UpdateAsync(entity);
        await _cache.RemoveAsync("All");
        return ObjectMapper.Map<LandType, LandTypeDto>(entity);
    }

    public override async Task DeleteAsync(int id)
    {
        await Repository.DeleteAsync(id);
        await _cache.RemoveAsync("All");
    }

    [Authorize(KNTCPermissions.LandTypePermission.Delete)]
    public async Task DeleteMultipleAsync(IEnumerable<int> ids)
    {
        await Repository.DeleteManyAsync(ids);
        await _cache.RemoveAsync("All");
    }
}