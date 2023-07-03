using KNTC.DocumentTypes;
using KNTC.SysConfigs;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace KNTC.Data;

public class SysConfigSeedContributor : IDataSeedContributor, ITransientDependency
{
    public ILogger<SysConfigSeedContributor> Logger { get; set; }

    private readonly IRepository<SysConfig, int> _sysConfigRepo;

    public SysConfigSeedContributor(IRepository<SysConfig, int> sysConfigRepo)
    {
        _sysConfigRepo = sysConfigRepo;
        Logger = NullLogger<SysConfigSeedContributor>.Instance;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        Logger.LogInformation($"Seeding sys config start...");
        if (await _sysConfigRepo.GetCountAsync() > 0)
        {
            return;
        }
        List<SysConfig> sysConfigs = new List<SysConfig>()
        {
            new(SysConfigConsts.Prefix + nameof(SysConfigConsts.TITLE),SysConfigConsts.TITLE){
                Description = "Tiêu đề Website"
            },
            new(SysConfigConsts.Prefix + nameof(SysConfigConsts.GEOSERVER_DOMAIN),SysConfigConsts.GEOSERVER_DOMAIN){
                Description = "Địa chỉ trang quản lý bản đồ quy hoạch"
            },
            new(SysConfigConsts.Prefix + nameof(SysConfigConsts.MAP_CENTER),SysConfigConsts.MAP_CENTER){
                Description = "Tọa độ Tỉnh/TP"
            }
        };

        await _sysConfigRepo.InsertManyAsync(sysConfigs);

        Logger.LogInformation($"Seeding sys config  success!");
    }
}