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

namespace KNTC.SpatialDatas;

public class SpatialDataAppService : CrudAppService<
            SpatialData,
            SpatialDataDto,
            int,
            GetSpatialDataListDto,
            CreateAndUpdateSpatialDataDto>, ISpatialDataAppService
{
    private readonly SpatialDataManager _spatialDataManager;
    public SpatialDataAppService(IRepository<SpatialData, int> repository, SpatialDataManager spatialDataManager) : base(repository)
    {
        LocalizationResource = typeof(KNTCResource);
        CreatePolicyName = KNTCPermissions.SpatialDatas.Create;
        UpdatePolicyName = KNTCPermissions.SpatialDatas.Edit;
        DeletePolicyName = KNTCPermissions.SpatialDatas.Delete;
        _spatialDataManager = spatialDataManager;
    }

    public async override Task<PagedResultDto<SpatialDataDto>> GetListAsync(GetSpatialDataListDto input)
    {
        var filter = !input.Keyword.IsNullOrEmpty() ? input.Keyword.ToUpper() : "";
        var queryable = await Repository.GetQueryableAsync();

        queryable = queryable
                    //.WhereIf(!filter.IsNullOrEmpty())
                    .OrderBy(input.Sorting)
                    .Skip(input.SkipCount)
                    .Take(input.MaxResultCount);

        var queryResult = await AsyncExecuter.ToListAsync(queryable);


        var totalCount = await Repository.CountAsync();

        return new PagedResultDto<SpatialDataDto>(
            totalCount,
            ObjectMapper.Map<List<SpatialData>, List<SpatialDataDto>>(queryResult)
        );
    }
    public async Task<ListResultDto<SpatialDataLookupDto>> GetLookupAsync()
    {
        var spatialDatas = await Repository.GetListAsync();

        return new ListResultDto<SpatialDataLookupDto>(
            ObjectMapper.Map<List<SpatialData>, List<SpatialDataLookupDto>>(spatialDatas)
        );
    }

    public async override Task<SpatialDataDto> CreateAsync(CreateAndUpdateSpatialDataDto input)
    {
        var entity = await _spatialDataManager.CreateAsync(input.geoJson);
        await Repository.InsertAsync(entity);
        return ObjectMapper.Map<SpatialData, SpatialDataDto>(entity);
    }

    public async override Task<SpatialDataDto> UpdateAsync(int id, CreateAndUpdateSpatialDataDto input)
    {
        var entity = await Repository.GetAsync(id, false);
        //entity.SetConcurrencyStampIfNotNull(input.ConcurrencyStamp);
        await _spatialDataManager.UpdateAsync(entity, input.geoJson);
        await Repository.UpdateAsync(entity);
        return ObjectMapper.Map<SpatialData, SpatialDataDto>(entity);
    }

    [Authorize(KNTCPermissions.SpatialDatas.Delete)]
    public async Task DeleteMultipleAsync(IEnumerable<int> ids)
    {
        await Repository.DeleteManyAsync(ids);
    }
}

