using KNTC.Localization;
using KNTC.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;

namespace KNTC.UnitTypes;

public class UnitTypeAppService : CrudAppService<
            UnitType,
            UnitTypeDto,
            int,
            GetUnitTypeListDto,
            CreateAndUpdateUnitTypeDto>, IUnitTypeAppService
{
    public UnitTypeAppService(IRepository<UnitType, int> repository) : base(repository)
    {
        LocalizationResource = typeof(KNTCResource);
        CreatePolicyName = KNTCPermissions.UnitType.Create;
        UpdatePolicyName = KNTCPermissions.UnitType.Edit;
        DeletePolicyName = KNTCPermissions.UnitType.Create;
    }

    public async override Task<PagedResultDto<UnitTypeDto>> GetListAsync(GetUnitTypeListDto input)
    {
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = nameof(UnitType.OrderIndex);
        }
        var filter = input.Keyword.ToUpper();
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
        var unitTypes = await Repository.GetListAsync();

        return new ListResultDto<UnitTypeLookupDto>(
            ObjectMapper.Map<List<UnitType>, List<UnitTypeLookupDto>>(unitTypes)
        );
    }
    [Authorize(KNTCPermissions.UnitType.Delete)]
    public async Task DeleteMultipleAsync(IEnumerable<int> ids)
    {
        await Repository.DeleteManyAsync(ids);
    }
}

