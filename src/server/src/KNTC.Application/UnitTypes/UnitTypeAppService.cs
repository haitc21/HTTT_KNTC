using KNTC.Localization;
using KNTC.Permissions;
using KNTC.Units;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using NPOI.POIFS.Properties;
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

namespace KNTC.CategoryUnitTypes;

public class UnitTypeAppService : CrudAppService<
            UnitType,
            UnitTypeDto,
            int,
            GetUnitTypeListDto,
            CreateAndUpdateUnitTypeDto>, IUnitTypeAppService
{
    private readonly UnitTypeManager _unitTypeManager;
    private readonly IDistributedCache<UnitTypeLookupCache> _cache;
    public UnitTypeAppService(IRepository<UnitType, int> repository,
        UnitTypeManager unitTypeManager,
        IDistributedCache<UnitTypeLookupCache> cache) : base(repository)
    {
        LocalizationResource = typeof(KNTCResource);
        CreatePolicyName = KNTCPermissions.UnitTypePermission.Create;
        UpdatePolicyName = KNTCPermissions.UnitTypePermission.Edit;
        DeletePolicyName = KNTCPermissions.UnitTypePermission.Delete;
        _unitTypeManager = unitTypeManager;
        _cache = cache;
    }

    public async override Task<PagedResultDto<UnitTypeDto>> GetListAsync(GetUnitTypeListDto input)
    {
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = $"{nameof(UnitType.OrderIndex)}, {nameof(UnitType.UnitTypeName)}";
        }

        var filter = !input.Keyword.IsNullOrEmpty() ? input.Keyword.ToUpper() : "";
        var queryable = await Repository.GetQueryableAsync();

        queryable = queryable
                    .WhereIf(!filter.IsNullOrEmpty(),
                             x => x.UnitTypeCode.ToUpper().Contains(filter)
                                 || x.UnitTypeName.ToUpper().Contains(filter)
                             )
                    .WhereIf(input.Status.HasValue, x => x.Status == input.Status)
                    .OrderBy(input.Sorting)
                    .Skip(input.SkipCount)
                    .Take(input.MaxResultCount);

        var queryResult = await AsyncExecuter.ToListAsync(queryable);


        var totalCount = await Repository.CountAsync(
                x => (input.Keyword.IsNullOrEmpty()
                    || (x.UnitTypeCode.ToUpper().Contains(input.Keyword) || x.UnitTypeName.ToUpper().Contains(input.Keyword)))
                && (!input.Status.HasValue || x.Status == input.Status)
                );

        return new PagedResultDto<UnitTypeDto>(
            totalCount,
            ObjectMapper.Map<List<UnitType>, List<UnitTypeDto>>(queryResult)
        );
    }
    public async Task<ListResultDto<UnitTypeLookupDto>> GetLookupAsync()
    {
        var cacheItem = await _cache.GetOrAddAsync(
        "UnitTypeLookup",
        async () =>
        {
            var entities = await Repository.GetListAsync(x => x.Status == Status.Active);
            var dtos = ObjectMapper.Map<List<UnitType>, List<UnitTypeLookupDto>>(entities);
            return new UnitTypeLookupCache() { Items = dtos };
        },
        () => new DistributedCacheEntryOptions
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddHours(12)
        });

        return new ListResultDto<UnitTypeLookupDto>(cacheItem.Items);
    }
    public async override Task<UnitTypeDto> CreateAsync(CreateAndUpdateUnitTypeDto input)
    {
        var entity = await _unitTypeManager.CreateAsync(input.UnitTypeCode,
                                                          input.UnitTypeName,
                                                          input.Description,
                                                          input.OrderIndex,
                                                          input.Status);
        await Repository.InsertAsync(entity);
        return ObjectMapper.Map<UnitType, UnitTypeDto>(entity);
    }

    public async override Task<UnitTypeDto> UpdateAsync(int id, CreateAndUpdateUnitTypeDto input)
    {
        var entity = await Repository.GetAsync(id, false);
        entity.SetConcurrencyStampIfNotNull(input.ConcurrencyStamp);
        await _unitTypeManager.UpdateAsync(entity,
                                           input.UnitTypeCode,
                                           input.UnitTypeName,
                                           input.Description,
                                           input.OrderIndex,
                                           input.Status);
        await Repository.UpdateAsync(entity);
        return ObjectMapper.Map<UnitType, UnitTypeDto>(entity);
    }

    [Authorize(KNTCPermissions.UnitTypePermission.Delete)]
    public async Task DeleteMultipleAsync(IEnumerable<int> ids)
    {
        await Repository.DeleteManyAsync(ids);
    }
}

