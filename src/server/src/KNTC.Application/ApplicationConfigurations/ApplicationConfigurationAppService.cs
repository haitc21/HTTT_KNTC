using KNTC.SysConfigs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.ApplicationConfigurations;
using Volo.Abp.AspNetCore.Mvc.ApplicationConfigurations.ObjectExtending;
using Volo.Abp.Authorization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Caching;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Features;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Settings;
using Volo.Abp.Timing;
using Volo.Abp.Users;

namespace KNTC.ApplicationConfigurations;

[Dependency(ReplaceServices = true)]
[ExposeServices(typeof(IAbpApplicationConfigurationAppService), typeof(AbpApplicationConfigurationAppService), typeof(ApplicationConfigurationAppService))]
public class ApplicationConfigurationAppService : AbpApplicationConfigurationAppService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ISettingProvider _settingProvider;
    private readonly ISettingDefinitionManager _settingDefinitionManager;
    private readonly AbpApplicationConfigurationOptions _options;
    private readonly IDistributedCache<ApplicationLocalizationConfigurationDto, string> _cacheLocalization;
    private readonly IDistributedCache<ApplicationSettingConfigurationDto, string> _cacheSetting;

    public ApplicationConfigurationAppService(
        IOptions<AbpLocalizationOptions> localizationOptions,
        IOptions<AbpMultiTenancyOptions> multiTenancyOptions,
        IServiceProvider serviceProvider,
        IAbpAuthorizationPolicyProvider abpAuthorizationPolicyProvider,
        IPermissionDefinitionManager permissionDefinitionManager,
        DefaultAuthorizationPolicyProvider defaultAuthorizationPolicyProvider,
        IPermissionChecker permissionChecker,
        IAuthorizationService authorizationService,
        ICurrentUser currentUser,
        ISettingProvider settingProvider,
        ISettingDefinitionManager settingDefinitionManager,
        IFeatureDefinitionManager featureDefinitionManager,
        ILanguageProvider languageProvider,
        ITimezoneProvider timezoneProvider,
        IOptions<AbpClockOptions> abpClockOptions,
        ICachedObjectExtensionsDtoService cachedObjectExtensionsDtoService,
        IOptions<AbpApplicationConfigurationOptions> options,
        IDistributedCache<ApplicationLocalizationConfigurationDto, string> cacheLocalization,
        IDistributedCache<ApplicationSettingConfigurationDto, string> cacheSetting)
        : base(localizationOptions, multiTenancyOptions, serviceProvider, abpAuthorizationPolicyProvider, permissionDefinitionManager, defaultAuthorizationPolicyProvider, permissionChecker, authorizationService, currentUser, settingProvider, settingDefinitionManager, featureDefinitionManager, languageProvider, timezoneProvider, abpClockOptions, cachedObjectExtensionsDtoService, options)
    {
        _serviceProvider = serviceProvider;
        _settingProvider = settingProvider;
        _settingDefinitionManager = settingDefinitionManager;
        _options = options.Value;
        _cacheLocalization = cacheLocalization;
        _cacheSetting = cacheSetting;
    }

    public override async Task<ApplicationConfigurationDto> GetAsync()
    {
        var authConfig = await GetAuthConfigAsync();

        var localizationConfig = await _cacheLocalization.GetOrAddAsync(
        "AppConfigLocalization",
        async () => await GetLocalizationConfigAsync(),
        () => new DistributedCacheEntryOptions
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddHours(24)
        });

        var curUser = GetCurrentUser();

        var setting = await _cacheSetting.GetOrAddAsync(
        "AppConfigSetting",
        async () => await GetSettingConfigAsync(),
        () => new DistributedCacheEntryOptions
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddHours(24)
        });

        var result = new ApplicationConfigurationDto
        {
            Auth = authConfig,
            Localization = localizationConfig,
            CurrentUser = curUser,
            Setting = setting
        };

        if (_options.Contributors.Any())
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = new ApplicationConfigurationContributorContext(scope.ServiceProvider, result);
                foreach (var contributor in _options.Contributors)
                {
                    await contributor.ContributeAsync(context);
                }
            }
        }
        return result;
    }
    private async Task<ApplicationSettingConfigurationDto> GetSettingConfigAsync()
    {
        var result = new ApplicationSettingConfigurationDto
        {
            Values = new Dictionary<string, string>()
        };

        var settingDefinitions = _settingDefinitionManager.GetAll().Where(x => x.IsVisibleToClients);

        var settingValues = await _settingProvider.GetAllAsync(settingDefinitions.Select(x => x.Name).ToArray());

        foreach (var settingValue in settingValues)
        {
            result.Values[settingValue.Name] = settingValue.Value;
        }

        return result;
    }
}
