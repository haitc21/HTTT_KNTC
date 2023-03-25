using KNTC.Localization;
using KNTC.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Data;
using Volo.Abp.Domain.Repositories;

namespace KNTC.UnitTypes;

public class UnitTypeAppService : CrudAppService<
            UnitType,
            UnitTypeDto,
            int,
            GetUnitTypeListDto,
            CreateAndUpdateUnitTypeDto>, IUnitTypeAppService
{
    private readonly UnitTypeManager _unitTypeManager;
    public UnitTypeAppService(IRepository<UnitType, int> repository, UnitTypeManager unitTypeManager) : base(repository)
    {
        LocalizationResource = typeof(KNTCResource);
        CreatePolicyName = KNTCPermissions.UnitType.Create;
        UpdatePolicyName = KNTCPermissions.UnitType.Edit;
        DeletePolicyName = KNTCPermissions.UnitType.Delete;
        _unitTypeManager = unitTypeManager;
    }

    public async override Task<PagedResultDto<UnitTypeDto>> GetListAsync(GetUnitTypeListDto input)
    {
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = nameof(UnitType.OrderIndex);
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
        var unitTypes = await Repository.GetListAsync(x => x.Status == Status.Active);

        return new ListResultDto<UnitTypeLookupDto>(
            ObjectMapper.Map<List<UnitType>, List<UnitTypeLookupDto>>(unitTypes)
        );
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

    [Authorize(KNTCPermissions.UnitType.Delete)]
    public async Task DeleteMultipleAsync(IEnumerable<int> ids)
    {
        await Repository.DeleteManyAsync(ids);
    }
}

