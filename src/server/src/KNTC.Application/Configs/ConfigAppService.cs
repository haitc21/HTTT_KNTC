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

namespace KNTC.Configs;

public class ConfigAppService : CrudAppService<
            Config,
            ConfigDto,
            int,
            GetConfigListDto,
            CreateAndUpdateConfigDto>, IConfigAppService
{
    private readonly ConfigManager _configManager;
    public ConfigAppService(IRepository<Config, int> repository, ConfigManager configManager) : base(repository)
    {
        LocalizationResource = typeof(KNTCResource);
        CreatePolicyName = KNTCPermissions.UnitType.Create;
        UpdatePolicyName = KNTCPermissions.UnitType.Edit;
        DeletePolicyName = KNTCPermissions.UnitType.Delete;
        _configManager = configManager;
    }

    public async override Task<PagedResultDto<ConfigDto>> GetListAsync(GetConfigListDto input)
    {
        var filter = !input.Keyword.IsNullOrEmpty() ? input.Keyword.ToUpper() : "";
        var queryable = await Repository.GetQueryableAsync();

        queryable = queryable
                    .WhereIf(!filter.IsNullOrEmpty(),
                             x => x.OrganizationCode.ToUpper().Contains(filter)
                                 || x.OrganizationName.ToUpper().Contains(filter)
                             )
                    .WhereIf(input.Status.HasValue, x => x.Status == input.Status)
                    .OrderBy(input.Sorting)
                    .Skip(input.SkipCount)
                    .Take(input.MaxResultCount);

        var queryResult = await AsyncExecuter.ToListAsync(queryable);


        var totalCount = await Repository.CountAsync(
                x => (input.Keyword.IsNullOrEmpty()
                    || (x.OrganizationCode.ToUpper().Contains(input.Keyword) || x.OrganizationName.ToUpper().Contains(input.Keyword)))
                && (!input.Status.HasValue || x.Status == input.Status)
                );

        return new PagedResultDto<ConfigDto>(
            totalCount,
            ObjectMapper.Map<List<Config>, List<ConfigDto>>(queryResult)
        );
    }
    public async Task<ListResultDto<ConfigLookupDto>> GetLookupAsync()
    {
        var configs = await Repository.GetListAsync();

        return new ListResultDto<ConfigLookupDto>(
            ObjectMapper.Map<List<Config>, List<ConfigLookupDto>>(configs)
        );
    }

    public async override Task<ConfigDto> CreateAsync(CreateAndUpdateConfigDto input)
    {
        var entity = await _configManager.CreateAsync(input.OrganizationCode,
                                                          input.OrganizationName,
                                                          input.ToaDo,
                                                          input.Tel,
                                                          input.Address,
                                                          input.Description);
        await Repository.InsertAsync(entity);
        return ObjectMapper.Map<Config, ConfigDto>(entity);
    }

    public async override Task<ConfigDto> UpdateAsync(int id, CreateAndUpdateConfigDto input)
    {
        var entity = await Repository.GetAsync(id, false);
        entity.SetConcurrencyStampIfNotNull(input.ConcurrencyStamp);
        await _configManager.UpdateAsync(entity,
                                              input.OrganizationCode,
                                              input.OrganizationName,
                                              input.ToaDo,
                                              input.Tel,
                                              input.Address,
                                              input.Description);
        await Repository.UpdateAsync(entity);
        return ObjectMapper.Map<Config, ConfigDto>(entity);
    }

    [Authorize(KNTCPermissions.UnitType.Delete)]
    public async Task DeleteMultipleAsync(IEnumerable<int> ids)
    {
        await Repository.DeleteManyAsync(ids);
    }
}

