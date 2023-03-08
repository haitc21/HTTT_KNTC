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

namespace KNTC.Units;

public class UnitAppService : CrudAppService<
            Unit,
            UnitDto,
            int,
            GetUnitListDto,
            CreateAndUpdateUnitDto>, IUnitAppService
{
    public UnitAppService(IRepository<Unit, int> repository) : base(repository)
    {
        LocalizationResource = typeof(KNTCResource);
        CreatePolicyName = KNTCPermissions.Unit.Create;
        UpdatePolicyName = KNTCPermissions.Unit.Edit;
        DeletePolicyName = KNTCPermissions.Unit.Create;
    }
    public async override Task<PagedResultDto<UnitDto>> GetListAsync(GetUnitListDto input)
    {
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = nameof(Unit.OrderIndex);
        }
        var filter = input.Keyword.ToUpper();
        var queryable = await Repository.GetQueryableAsync();

        queryable = queryable
                    .WhereIf(!filter.IsNullOrEmpty(),
                             x => x.UnitCode.ToUpper().Contains(filter)
                                 || x.UnitName.ToUpper().Contains(filter)
                             )
                    .WhereIf(input.Status.HasValue, x => x.Status == input.Status)
                    .OrderBy(input.Sorting)
                    .Skip(input.SkipCount)
                    .Take(input.MaxResultCount);

        var queryResult = await AsyncExecuter.ToListAsync(queryable);


        var totalCount = await Repository.CountAsync(
                x => (input.Keyword.IsNullOrEmpty()
                    || (x.UnitCode.ToUpper().Contains(input.Keyword) || x.UnitName.ToUpper().Contains(input.Keyword)))
                && (!input.Status.HasValue || x.Status == input.Status)
                );

        return new PagedResultDto<UnitDto>(
            totalCount,
            ObjectMapper.Map<List<Unit>, List<UnitDto>>(queryResult)
        );
    }
    public async Task<ListResultDto<UnitLookupDto>> GetLookupAsync()
    {
        var units = await Repository.GetListAsync();

        return new ListResultDto<UnitLookupDto>(
            ObjectMapper.Map<List<Unit>, List<UnitLookupDto>>(units)
        );
    }
    [Authorize(KNTCPermissions.Unit.Delete)]
    public async Task DeleteMultipleAsync(IEnumerable<int> ids)
    {
        await Repository.DeleteManyAsync(ids);
    }
}

