using KNTC.CategoryUnitTypes;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace KNTC.Data;

public class UnitTypeSeedContributor : IDataSeedContributor, ITransientDependency
{
    public ILogger<UserInfoSeedContributor> Logger { get; set; }

    private readonly IRepository<UnitType, int> _unitTypeRepo;
    public UnitTypeSeedContributor(IRepository<UnitType, int> unitTypeRepo)
    {
        _unitTypeRepo = unitTypeRepo;
        Logger = NullLogger<UserInfoSeedContributor>.Instance;
    }
    public async Task SeedAsync(DataSeedContext context)
    {

        Logger.LogInformation($"Seeding unit type start...");
        if (await _unitTypeRepo.GetCountAsync() > 0)
        {
            return;
        }
        List<UnitType> unitTypes = new List<UnitType>();
        var u1 = new UnitType("1", "Tỉnh / Thành phố");
        u1.Description = "";
        u1.OrderIndex = 0;
        u1.Status = Status.Active;
        unitTypes.Add(u1);

        var u2 = new UnitType("2", "Quận / Huyện");
        u2.Description = "";
        u2.OrderIndex = 1;
        u1.Status = Status.Active;
        unitTypes.Add(u2);

        var u3 = new UnitType("3", "Phường / Xã");
        u3.Description = "";
        u3.OrderIndex = 2;
        u3.Status = Status.Active;
        unitTypes.Add(u3);

        var u4 = new UnitType("4", "Đường / Phố");
        u4.Description = "";
        u4.OrderIndex = 3;
        u4.Status = Status.Active;
        unitTypes.Add(u4);

        await _unitTypeRepo.InsertManyAsync(unitTypes);

        Logger.LogInformation($"Seeding unit type success!");
    }
}
