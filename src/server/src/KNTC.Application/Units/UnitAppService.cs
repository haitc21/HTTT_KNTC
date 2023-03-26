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

namespace KNTC.Units;

public class UnitAppService : CrudAppService<
            Unit,
            UnitDto,
            int,
            GetUnitListDto,
            CreateAndUpdateUnitDto>, IUnitAppService
{
    private readonly UnitManager _unitManager;
    public UnitAppService(IRepository<Unit, int> repository, UnitManager unitManager) : base(repository)
    {
        LocalizationResource = typeof(KNTCResource);
        CreatePolicyName = KNTCPermissions.Unit.Create;
        UpdatePolicyName = KNTCPermissions.Unit.Edit;
        DeletePolicyName = KNTCPermissions.Unit.Delete;
        _unitManager = unitManager;
    }
    public async override Task<PagedResultDto<UnitDto>> GetListAsync(GetUnitListDto input)
    {
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = nameof(Unit.OrderIndex);
        }
        var filter = !input.Keyword.IsNullOrEmpty() ? input.Keyword.ToUpper() : "";
        var queryable = await Repository.GetQueryableAsync();

        queryable = queryable
                    .WhereIf(!filter.IsNullOrEmpty(),
                             x => x.UnitCode.ToUpper().Contains(filter)
                                 || x.UnitName.ToUpper().Contains(filter)
                             )
                    .WhereIf(input.UnitTypeId.HasValue, x => x.UnitTypeId == input.UnitTypeId)
                    .WhereIf(input.ParentId.HasValue, x => x.ParentId == input.ParentId)
                    .WhereIf(input.Status.HasValue, x => x.Status == input.Status)
                    .OrderBy(input.Sorting)
                    .Skip(input.SkipCount)
                    .Take(input.MaxResultCount);

        var queryResult = await AsyncExecuter.ToListAsync(queryable);


        var totalCount = await Repository.CountAsync(
                x => (input.Keyword.IsNullOrEmpty()
                    || (x.UnitCode.ToUpper().Contains(input.Keyword) || x.UnitName.ToUpper().Contains(input.Keyword)))
                && (!input.UnitTypeId.HasValue || x.UnitTypeId == input.UnitTypeId)
                && (!input.ParentId.HasValue || x.ParentId == input.ParentId)
                && (!input.Status.HasValue || x.Status == input.Status)
                );

        return new PagedResultDto<UnitDto>(
            totalCount,
            ObjectMapper.Map<List<Unit>, List<UnitDto>>(queryResult)
        );
    }
    public async Task<ListResultDto<UnitLookupDto>> GetLookupAsync(int unitTypeId, int? parentId = null)
    {
        var queryable = await Repository.GetQueryableAsync();

        queryable = queryable
                    .Where(x => x.Status == Status.Active)
                    .Where(x => x.UnitTypeId == unitTypeId)
                    .WhereIf(parentId.HasValue, x => x.ParentId == parentId)
                    .OrderBy(nameof(UnitLookupDto.UnitName));
        var queryResult = await AsyncExecuter.ToListAsync(queryable);

        return new ListResultDto<UnitLookupDto>(
            ObjectMapper.Map<List<Unit>, List<UnitLookupDto>>(queryResult)
        );
    }

    public async override Task<UnitDto> CreateAsync(CreateAndUpdateUnitDto input)
    {
        var entity = await _unitManager.CreateAsync(input.UnitCode,
                                                   input.UnitName,
                                                   input.ShortName,
                                                   input.UnitTypeId,
                                                   input.Description,
                                                   input.OrderIndex,
                                                   input.Status);
        await Repository.InsertAsync(entity);
        return ObjectMapper.Map<Unit, UnitDto>(entity);
    }

    public async override Task<UnitDto> UpdateAsync(int id, CreateAndUpdateUnitDto input)
    {
        var entity = await Repository.GetAsync(id, false);
        entity.SetConcurrencyStampIfNotNull(input.ConcurrencyStamp);
        await _unitManager.UpdateAsync(entity,
                                       input.UnitCode,
                                       input.UnitName,
                                       input.ShortName,
                                       input.UnitTypeId,
                                       input.Description,
                                       input.OrderIndex,
                                       input.Status);
        await Repository.UpdateAsync(entity);
        return ObjectMapper.Map<Unit, UnitDto>(entity);
    }

    [Authorize(KNTCPermissions.Unit.Delete)]
    public async Task DeleteMultipleAsync(IEnumerable<int> ids)
    {
        await Repository.DeleteManyAsync(ids);
    }
}

