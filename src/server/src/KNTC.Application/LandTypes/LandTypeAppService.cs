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
using Volo.Abp.ObjectMapping;

namespace KNTC.LandTypes;

public class LandTypeAppService : CrudAppService<
            LandType,
            LandTypeDto,
            int,
            GetLandTypeListDto,
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
    public async override Task<PagedResultDto<LandTypeDto>> GetListAsync(GetLandTypeListDto input)
    {
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = $"{nameof(LandType.OrderIndex)}, {nameof(LandType.LandTypeName)}";
        }

        var filter = !input.Keyword.IsNullOrEmpty() ? input.Keyword.ToUpper() : "";
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
    public async Task<ListResultDto<LandTypeLookupDto>> GetLookupAsync()
    {
        var cacheItem = await _cache.GetOrAddAsync(
        "LandTypeLookup",
        async () =>
        {
            var entities = await Repository.GetListAsync(x => x.Status == Status.Active);
            var dtos = ObjectMapper.Map<List<LandType>, List<LandTypeLookupDto>>(entities);
            return new LandTypeLookupCache() { Items = dtos };
        },
        () => new DistributedCacheEntryOptions
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddHours(12)
        });

        return new ListResultDto<LandTypeLookupDto>(cacheItem.Items);
    }

    public async override Task<LandTypeDto> CreateAsync(CreateAndUpdateLandTypeDto input)
    {
        var entity = await _landTypeManager.CreateAsync(input.LandTypeCode,
                                                          input.LandTypeName,
                                                          input.Description,
                                                          input.OrderIndex,
                                                          input.Status);
        await Repository.InsertAsync(entity);
        await _cache.RemoveAsync("LandTypeLookup");
        return ObjectMapper.Map<LandType, LandTypeDto>(entity);
    }

    public async override Task<LandTypeDto> UpdateAsync(int id, CreateAndUpdateLandTypeDto input)
    {
        var entity = await Repository.GetAsync(id, false);
        entity.SetConcurrencyStampIfNotNull(input.ConcurrencyStamp);
        await _landTypeManager.UpdateAsync(entity,
                                           input.LandTypeCode,
                                           input.LandTypeName,
                                           input.Description,
                                           input.OrderIndex,
                                           input.Status);
        await Repository.UpdateAsync(entity);
        await _cache.RemoveAsync("LandTypeLookup");
        return ObjectMapper.Map<LandType, LandTypeDto>(entity);
    }
    public override async Task DeleteAsync(int id)
    {
        await Repository.DeleteAsync(id);
        await _cache.RemoveAsync("LandTypeLookup");
    }

    [Authorize(KNTCPermissions.LandTypePermission.Delete)]
    public async Task DeleteMultipleAsync(IEnumerable<int> ids)
    {
        await Repository.DeleteManyAsync(ids);
        await _cache.RemoveAsync("LandTypeLookup");
    }
}

