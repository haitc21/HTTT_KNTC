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

namespace KNTC.LandTypes;

public class LandTypeAppService : CrudAppService<
            LandType,
            LandTypeDto,
            int,
            GetLandTypeListDto,
            CreateAndUpdateLandTypeDto>, ILandTypeAppService
{
    private readonly LandTypeManager _landTypeManager;
    public LandTypeAppService(IRepository<LandType, int> repository, LandTypeManager landTypeManager) : base(repository)
    {
        LocalizationResource = typeof(KNTCResource);
        CreatePolicyName = KNTCPermissions.LandType.Create;
        UpdatePolicyName = KNTCPermissions.LandType.Edit;
        DeletePolicyName = KNTCPermissions.LandType.Delete;
        _landTypeManager = landTypeManager;
    }
    public async override Task<PagedResultDto<LandTypeDto>> GetListAsync(GetLandTypeListDto input)
    {
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = nameof(LandType.OrderIndex);
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
        var landTypes = await Repository.GetListAsync();

        return new ListResultDto<LandTypeLookupDto>(
            ObjectMapper.Map<List<LandType>, List<LandTypeLookupDto>>(landTypes)
        );
    }

    public async override Task<LandTypeDto> CreateAsync(CreateAndUpdateLandTypeDto input)
    {
        var entity = await _landTypeManager.CreateAsync(input.LandTypeCode,
                                                          input.LandTypeName,
                                                          input.Description,
                                                          input.OrderIndex);
        await Repository.InsertAsync(entity);
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
                                              input.OrderIndex);
        await Repository.UpdateAsync(entity);
        return ObjectMapper.Map<LandType, LandTypeDto>(entity);
    }

    [Authorize(KNTCPermissions.LandType.Delete)]
    public async Task DeleteMultipleAsync(IEnumerable<int> ids)
    {
        await Repository.DeleteManyAsync(ids);
    }
}

