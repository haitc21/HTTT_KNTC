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

namespace KNTC.SysConfigs;

[Authorize(KNTCPermissions.SysConfigsPermission.Default)]
public class SysConfigAppService : CrudAppService<
            SysConfig,
            SysConfigDto,
            int,
            GetSysConfigListDto,
            CreateSysConfigDto,
            UpdateSysConfigDto>, ISysConfigAppService
{
    private readonly SysConfigManager _configManager;

    public SysConfigAppService(IRepository<SysConfig, int> repository, SysConfigManager configManager) : base(repository)
    {
        LocalizationResource = typeof(KNTCResource);
        CreatePolicyName = KNTCPermissions.SysConfigsPermission.Create;
        UpdatePolicyName = KNTCPermissions.SysConfigsPermission.Edit;
        DeletePolicyName = KNTCPermissions.SysConfigsPermission.Delete;
        _configManager = configManager;
    }

    [Authorize(KNTCPermissions.SysConfigsPermission.Default)]
    public override async Task<PagedResultDto<SysConfigDto>> GetListAsync(GetSysConfigListDto input)
    {
        var filter = !input.Keyword.IsNullOrEmpty() ? input.Keyword.ToUpper() : "";
        var queryable = await Repository.GetQueryableAsync();

        queryable = queryable
                    .WhereIf(!filter.IsNullOrEmpty(),
                             x => x.Name.ToUpper().Contains(filter)
                             )
                    .OrderBy(input.Sorting)
                    .Skip(input.SkipCount)
                    .Take(input.MaxResultCount);

        var queryResult = await AsyncExecuter.ToListAsync(queryable);

        var totalCount = await Repository.CountAsync(x => input.Keyword.IsNullOrEmpty() || (x.Name.ToUpper().Contains(input.Keyword)));

        return new PagedResultDto<SysConfigDto>(
            totalCount,
            ObjectMapper.Map<List<SysConfig>, List<SysConfigDto>>(queryResult)
        );
    }
    [Authorize(KNTCPermissions.SysConfigsPermission.Create)]
    public override async Task<SysConfigDto> CreateAsync(CreateSysConfigDto input)
    {
        var entity = await _configManager.CreateAsync(input.Name, input.Value, input.Description);
        await Repository.InsertAsync(entity);
        return ObjectMapper.Map<SysConfig, SysConfigDto>(entity);
    }

    [Authorize(KNTCPermissions.SysConfigsPermission.Edit)]
    public override async Task<SysConfigDto> UpdateAsync(int id, UpdateSysConfigDto input)
    {
        var entity = await Repository.GetAsync(id, false);
        entity.SetConcurrencyStampIfNotNull(input.ConcurrencyStamp);
        await _configManager.UpdateAsync(entity, input.Value, input.Description);
        await Repository.UpdateAsync(entity);
        return ObjectMapper.Map<SysConfig, SysConfigDto>(entity);
    }

    [Authorize(KNTCPermissions.SysConfigsPermission.Delete)]
    public async Task DeleteMultipleAsync(IEnumerable<int> ids)
    {
        await Repository.DeleteManyAsync(ids);
    }
}