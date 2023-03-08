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

namespace KNTC.LandTypes;

public class LandTypeAppService : CrudAppService<
            LandType,
            LandTypeDto,
            int,
            GetLandTypeListDto,
            CreateAndUpdateLandTypeDto>, ILandTypeAppService
{
    public LandTypeAppService(IRepository<LandType, int> repository) : base(repository)
    {
        LocalizationResource = typeof(KNTCResource);
        CreatePolicyName = KNTCPermissions.LandType.Create;
        UpdatePolicyName = KNTCPermissions.LandType.Edit;
        DeletePolicyName = KNTCPermissions.LandType.Create;
    }
    public async override Task<PagedResultDto<LandTypeDto>> GetListAsync(GetLandTypeListDto input)
    {
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = nameof(LandType.OrderIndex);
        }
        var filter = input.Keyword.ToUpper();
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
        var landTypes = await Repository.GetListAsync();

        return new ListResultDto<LandTypeLookupDto>(
            ObjectMapper.Map<List<LandType>, List<LandTypeLookupDto>>(landTypes)
        );
    }
    public async Task DeleteMultipleAsync(IEnumerable<int> ids)
    {
        await Repository.DeleteManyAsync(ids);
    }
}

