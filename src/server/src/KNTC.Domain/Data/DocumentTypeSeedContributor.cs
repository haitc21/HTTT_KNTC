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
using KNTC.DocumentTypes;

namespace KNTC.Data;

public class DocumentTypeSeedContributor : IDataSeedContributor, ITransientDependency
{
    public ILogger<UserInfoSeedContributor> Logger { get; set; }

    private readonly IRepository<DocumentType, int> _DocumentTypeRepo;
    public DocumentTypeSeedContributor(IRepository<DocumentType, int> DocumentTypeRepo)
    {
        _DocumentTypeRepo = DocumentTypeRepo;
        Logger = NullLogger<UserInfoSeedContributor>.Instance;
    }
    public async Task SeedAsync(DataSeedContext context)
    {

        Logger.LogInformation($"Seeding unit type start...");
        if (await _DocumentTypeRepo.GetCountAsync() > 0)
        {
            return;
        }
        List<DocumentType> DocumentTypes = new List<DocumentType>();
        var d1 = new DocumentType();
        d1.DocumentTypeCode = "1";
        d1.DocumentTypeName = "Bản gốc";
        d1.Description = "";
        d1.OrderIndex = 0;
        d1.Status = Status.Active;
        DocumentTypes.Add(d1);

        var d2 = new DocumentType();
        d2.DocumentTypeCode = "2";
        d2.DocumentTypeName = "Bản chính";
        d2.Description = "";
        d2.OrderIndex = 1;
        d1.Status = Status.Active;
        DocumentTypes.Add(d2);

        var d3 = new DocumentType();
        d3.DocumentTypeCode = "2";
        d3.DocumentTypeName = "Bản phô tô";
        d3.Description = "";
        d3.OrderIndex = 2;
        d3.Status = Status.Active;
        DocumentTypes.Add(d3);

        await _DocumentTypeRepo.InsertManyAsync(DocumentTypes);

        Logger.LogInformation($"Seeding unit type success!");
    }
}
