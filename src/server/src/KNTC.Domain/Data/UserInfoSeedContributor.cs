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

namespace KNTC.Data;

public class UserInfoSeedContributor : IDataSeedContributor, ITransientDependency
{
    public ILogger<UserInfoSeedContributor> Logger { get; set; }

    private readonly IRepository<UserInfo, Guid> _userInfoRepo;
    private readonly IRepository<IdentityUser, Guid> _userRepo;
    private readonly IGuidGenerator _guidGenerator;
    public UserInfoSeedContributor(IRepository<UserInfo, Guid> userInfoRepo,
        IGuidGenerator guidGenerator,
        IRepository<IdentityUser, Guid> userRepo)
    {
        _userInfoRepo = userInfoRepo;
        _guidGenerator = guidGenerator;
        _userRepo = userRepo;
        Logger = NullLogger<UserInfoSeedContributor>.Instance;
    }
    public async Task SeedAsync(DataSeedContext context)
    {

        Logger.LogInformation($"Seeding user info start...");
        if (await _userInfoRepo.GetCountAsync() > 0)
        {
            return;
        }
        var user = await _userRepo.GetAsync(x => x.UserName == "admin");
        var userInfo = new UserInfo(_guidGenerator.Create())
        {
            UserId = user.Id,
            Dob = new DateTime(1990, 1, 1)
        };
        await _userInfoRepo.InsertAsync(userInfo);
        Logger.LogInformation($"Seeding user info success!");
    }
}
