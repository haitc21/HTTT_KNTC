using Microsoft.AspNetCore.Authorization;
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
        IOptions<AbpApplicationConfigurationOptions> options)
        : base(localizationOptions, multiTenancyOptions, serviceProvider, abpAuthorizationPolicyProvider, permissionDefinitionManager, defaultAuthorizationPolicyProvider, permissionChecker, authorizationService, currentUser, settingProvider, settingDefinitionManager, featureDefinitionManager, languageProvider, timezoneProvider, abpClockOptions, cachedObjectExtensionsDtoService, options)
    {
        _serviceProvider = serviceProvider;
        _settingProvider = settingProvider;
        _settingDefinitionManager = settingDefinitionManager;
        _options = options.Value;
    }

    public override async Task<ApplicationConfigurationDto> GetAsync()
    {
        //TODO: Optimize & cache..?

        Logger.LogInformation("Executing AbpApplicationConfigurationAppService.GetAsync()...");
        var stopwatch = new Stopwatch();

        stopwatch.Start();
        var authConfig = await GetAuthConfigAsync();
        stopwatch.Stop();
        Logger.LogInformation($"GetAuthConfigAsync time: {stopwatch.Elapsed}");
        stopwatch.Reset();

        stopwatch.Start();
        var featureConfig = await GetFeaturesConfigAsync();
        stopwatch.Stop();
        Logger.LogInformation($"GetFeaturesConfigAsync time: {stopwatch.Elapsed}");
        stopwatch.Reset();

        stopwatch.Start();
        var globalFeatureConfig = await GetGlobalFeaturesConfigAsync();
        stopwatch.Stop();
        Logger.LogInformation($"GetGlobalFeaturesConfigAsync time: {stopwatch.Elapsed}");
        stopwatch.Reset();

        stopwatch.Start();
        var localizationConfig = await GetLocalizationConfigAsync();
        stopwatch.Stop();
        Logger.LogInformation($"GetLocalizationConfigAsync elapsed time: {stopwatch.Elapsed}");
        stopwatch.Reset();

        stopwatch.Start();
        var curUser = GetCurrentUser();
        stopwatch.Stop();
        Logger.LogInformation($"GetCurrentUser elapsed time: {stopwatch.Elapsed}");
        stopwatch.Reset();

        stopwatch.Start();
        var setting = await GetSettingConfigAsync();
        stopwatch.Stop();
        Logger.LogInformation($"GetSettingConfigAsync elapsed time: {stopwatch.Elapsed}");
        stopwatch.Reset();

        stopwatch.Start();
        var timingConfig = await GetTimingConfigAsync();
        stopwatch.Stop();
        Logger.LogInformation($"GetTimingConfigAsync elapsed time: {stopwatch.Elapsed}");
        stopwatch.Reset();

        stopwatch.Start();
        var clockConfig = GetClockConfig();
        stopwatch.Stop();
        Logger.LogInformation($"GetClockConfig elapsed time: {stopwatch.Elapsed}");
        stopwatch.Reset();

        var result = new ApplicationConfigurationDto
        {
            Auth = authConfig,
            Features = featureConfig,
            GlobalFeatures = globalFeatureConfig,
            Localization = localizationConfig,
            CurrentUser = curUser,
            Setting = setting,
            //MultiTenancy = GetMultiTenancy(),
            //CurrentTenant = GetCurrentTenant(),
            Timing = timingConfig,
            Clock = clockConfig,
            //ObjectExtensions = _cachedObjectExtensionsDtoService.Get(),
            //ExtraProperties = new ExtraPropertyDictionary()
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

        Logger.LogDebug("Executed AbpApplicationConfigurationAppService.GetAsync().");

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
