using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.Identity;
using Volo.Abp.Users;
using KNTC.Users;
using KNTC.LandTypes;

namespace KNTC.Data;

public class LandTypeSeedContributor : IDataSeedContributor, ITransientDependency
{
    public ILogger<UserInfoSeedContributor> Logger { get; set; }

    private readonly IRepository<LandType, int> _LandTypeRepo;
    public LandTypeSeedContributor(IRepository<LandType, int> LandTypeRepo)
    {
        _LandTypeRepo = LandTypeRepo;
        Logger = NullLogger<UserInfoSeedContributor>.Instance;
    }
    public async Task SeedAsync(DataSeedContext context)
    {

        Logger.LogInformation($"Seeding land type start...");
        if (await _LandTypeRepo.GetCountAsync() > 0)
        {
            return;
        }
        List<LandType> LandTypes = new List<LandType>();
        var lt1 = new LandType("NNP", "Đất nông nghiệp");
        lt1.Description = "";
        lt1.OrderIndex = 1;
        lt1.Status = Status.Active;
        LandTypes.Add(lt1);

        var lt2 = new LandType("PNN", "Đất phi nông nghiệp");
        lt2.Description = "";
        lt2.OrderIndex = 2;
        lt2.Status = Status.Active;
        LandTypes.Add(lt2);

        var lt3 = new LandType("CSD", "Nhóm đất chưa sử dụng");
        lt3.Description = "";
        lt3.OrderIndex = 3;
        lt3.Status = Status.Active;
        LandTypes.Add(lt3);

        var lt4 = new LandType("MVB", "Đất có mặt nước ven biển");
        lt4.Description = "";
        lt4.OrderIndex = 4;
        lt4.Status = Status.Active;
        LandTypes.Add(lt4);

        await _LandTypeRepo.InsertManyAsync(LandTypes);

        Logger.LogInformation($"Seeding land type success!");
    }
}
