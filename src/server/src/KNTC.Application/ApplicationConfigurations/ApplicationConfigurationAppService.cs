using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
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
    private readonly AbpLocalizationOptions _localizationOptions;
    private readonly AbpMultiTenancyOptions _multiTenancyOptions;
    private readonly IServiceProvider _serviceProvider;
    private readonly IAbpAuthorizationPolicyProvider _abpAuthorizationPolicyProvider;
    private readonly IPermissionDefinitionManager _permissionDefinitionManager;
    private readonly DefaultAuthorizationPolicyProvider _defaultAuthorizationPolicyProvider;
    private readonly IPermissionChecker _permissionChecker;
    private readonly IAuthorizationService _authorizationService;
    private readonly ICurrentUser _currentUser;
    private readonly ISettingProvider _settingProvider;
    private readonly ISettingDefinitionManager _settingDefinitionManager;
    private readonly IFeatureDefinitionManager _featureDefinitionManager;
    private readonly ILanguageProvider _languageProvider;
    private readonly ITimezoneProvider _timezoneProvider;
    private readonly AbpClockOptions _abpClockOptions;
    private readonly ICachedObjectExtensionsDtoService _cachedObjectExtensionsDtoService;
    private readonly AbpApplicationConfigurationOptions _options;

    public ApplicationConfigurationAppService(IOptions<AbpLocalizationOptions> localizationOptions,
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
        _abpAuthorizationPolicyProvider = abpAuthorizationPolicyProvider;
        _permissionDefinitionManager = permissionDefinitionManager;
        _defaultAuthorizationPolicyProvider = defaultAuthorizationPolicyProvider;
        _permissionChecker = permissionChecker;
        _authorizationService = authorizationService;
        _currentUser = currentUser;
        _settingProvider = settingProvider;
        _settingDefinitionManager = settingDefinitionManager;
        _featureDefinitionManager = featureDefinitionManager;
        _languageProvider = languageProvider;
        _timezoneProvider = timezoneProvider;
        _abpClockOptions = abpClockOptions.Value;
        _cachedObjectExtensionsDtoService = cachedObjectExtensionsDtoService;
        _options = options.Value;
        _localizationOptions = localizationOptions.Value;
        _multiTenancyOptions = multiTenancyOptions.Value;
    }

    public override async Task<ApplicationConfigurationDto> GetAsync()
    {
        //TODO: Optimize & cache..?

        Logger.LogDebug("Executing AbpApplicationConfigurationAppService.GetAsync()...");

        var result = new ApplicationConfigurationDto
        {
            Auth = await GetAuthConfigAsync(),
            //Features = await GetFeaturesConfigAsync(),
            //GlobalFeatures = await GetGlobalFeaturesConfigAsync(),
            Localization = await GetLocalizationConfigAsync(),
            CurrentUser = GetCurrentUser(),
            Setting = await GetSettingConfigAsync(),
            //MultiTenancy = GetMultiTenancy(),
            //CurrentTenant = GetCurrentTenant(),
            Timing = await GetTimingConfigAsync(),
            Clock = GetClockConfig(),
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
