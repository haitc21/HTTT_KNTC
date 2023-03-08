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

        Logger.LogInformation($"Seeding unit type start...");
        if (await _LandTypeRepo.GetCountAsync() > 0)
        {
            return;
        }
        List<LandType> LandTypes = new List<LandType>();
        var lt1 = new LandType(1);
        lt1.LandTypeCode = "NNP";
        lt1.LandTypeName = "Đất nông nghiệp";
        lt1.Description = "";
        lt1.OrderIndex = 1;
        lt1.Status = Status.Active;

        var lt2 = new LandType();
        lt2.LandTypeCode = "NTT";
        lt2.LandTypeName = "Đất nông thôn";
        lt2.Description = "";
        lt2.OrderIndex = 2;
        lt2.Status = Status.Active;

        var lt3 = new LandType();
        lt3.LandTypeCode = "DTTP";
        lt3.LandTypeName = "Đất thổ cư phía sau lưng tòa nhà";
        lt3.Description = "";
        lt3.OrderIndex = 3;
        lt3.Status = Status.Active;

        var lt4 = new LandType();
        lt4.LandTypeCode = "DTCN";
        lt4.LandTypeName = "Đất thổ cư ngoài trung tâm";
        lt4.Description = "";
        lt4.OrderIndex = 4;
        lt4.Status = Status.Active;

        var lt5 = new LandType();
        lt5.LandTypeCode = "DTCT";
        lt5.LandTypeName = "Đất thổ cư trung tâm";
        lt5.Description = "";
        lt5.OrderIndex = 5;
        lt5.Status = Status.Active;

        var lt6 = new LandType();
        lt6.LandTypeCode = "TMDV";
        lt6.LandTypeName = "Đất thương mại dịch vụ";
        lt6.Description = "";
        lt6.OrderIndex = 6;
        lt6.Status = Status.Active;

        var lt7 = new LandType();
        lt7.LandTypeCode = "CCXD";
        lt7.LandTypeName = "Đất chức năng xây dựng";
        lt7.Description = "";
        lt7.OrderIndex = 7;
        lt7.Status = Status.Active;

        await _LandTypeRepo.InsertManyAsync(LandTypes);

        Logger.LogInformation($"Seeding unit type success!");
    }
}
