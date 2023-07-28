//using KNTC.Localization;
//using KNTC.Permissions;
//using Microsoft.AspNetCore.Authorization;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Dynamic.Core;
//using System.Threading.Tasks;
//using Volo.Abp.Application.Dtos;
//using Volo.Abp.Application.Services;
//using Volo.Abp.Domain.Repositories;

//namespace KNTC.SpatialDatas;

//public class SpatialDataAppService : CrudAppService<
//            SpatialData,
//            SpatialDataDto,
//            int,
//            GetSpatialDataListDto,
//            CreateAndUpdateSpatialDataDto>, ISpatialDataAppService
//{
//    private readonly SpatialDataManager _spatialDataManager;

//    public SpatialDataAppService(IRepository<SpatialData, Guid> repository, SpatialDataManager spatialDataManager) : base(repository)
//    {
//        LocalizationResource = typeof(KNTCResource);
//        CreatePolicyName = KNTCPermissions.SpatialDatasPermission.Create;
//        UpdatePolicyName = KNTCPermissions.SpatialDatasPermission.Edit;
//        DeletePolicyName = KNTCPermissions.SpatialDatasPermission.Delete;
//        _spatialDataManager = spatialDataManager;
//    }

//    public override async Task<PagedResultDto<SpatialDataDto>> GetListAsync(GetSpatialDataListDto input)
//    {
//        if (input.Sorting.IsNullOrWhiteSpace())
//        {
//            input.Sorting = nameof(SpatialData.Id);
//        }

//        var filter = !input.Keyword.IsNullOrEmpty() ? input.Keyword.ToUpper() : "";
//        var queryable = await Repository.GetQueryableAsync();

//        queryable = queryable
//                    //.WhereIf(!filter.IsNullOrEmpty(), x => x.GeoJson.ToUpper().Contains(filter))
//                    .WhereIf(!filter.IsNullOrEmpty(),
//                             x => x.TenToChuc.ToUpper().Contains(filter)
//                             )
//                    .OrderBy(input.Sorting)
//                    .Skip(input.SkipCount)
//                    .Take(input.MaxResultCount);

//        var queryResult = await AsyncExecuter.ToListAsync(queryable);

//        var totalCount = await Repository.CountAsync();

//        var result = new PagedResultDto<SpatialDataDto>(
//            totalCount,
//            ObjectMapper.Map<List<SpatialData>, List<SpatialDataDto>>(queryResult)
//        );

//        return result;
//    }

//    public async Task<ListResultDto<SpatialDataLookupDto>> GetLookupAsync()
//    {
//        var spatialDatas = await Repository.GetListAsync();

//        return new ListResultDto<SpatialDataLookupDto>(
//            ObjectMapper.Map<List<SpatialData>, List<SpatialDataLookupDto>>(spatialDatas)
//        );
//    }

//    public override async Task<SpatialDataDto> CreateAsync(CreateAndUpdateSpatialDataDto input)
//    {
//        var entity = await _spatialDataManager.CreateAsync(input.GeoJson);
//        await Repository.InsertAsync(entity);
//        return ObjectMapper.Map<SpatialData, SpatialDataDto>(entity);
//    }

//    public override async Task<SpatialDataDto> UpdateAsync(int id, CreateAndUpdateSpatialDataDto input)
//    {
//        var entity = await Repository.GetAsync(id, false);
//        //entity.SetConcurrencyStampIfNotNull(input.ConcurrencyStamp);
//        await _spatialDataManager.UpdateAsync(entity, input.GeoJson);
//        await Repository.UpdateAsync(entity);
//        return ObjectMapper.Map<SpatialData, SpatialDataDto>(entity);
//    }

//    [Authorize(KNTCPermissions.SpatialDatasPermission.Delete)]
//    public async Task DeleteMultipleAsync(IEnumerable<int> ids)
//    {
//        await Repository.DeleteManyAsync(ids);
//    }

//    public async Task<List<string>> GetGeoJsonAsync()
//    {
//        var query = await Repository.GetQueryableAsync();
//        var query2 = query.Select(x => x.GeoJson);
//        var groJson = await AsyncExecuter.ToListAsync(query2);
//        return groJson;
//    }
//}