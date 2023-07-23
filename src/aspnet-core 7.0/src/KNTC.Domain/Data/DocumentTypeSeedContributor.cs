using KNTC.DocumentTypes;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace KNTC.Data;

public class DocumentTypeSeedContributor : IDataSeedContributor, ITransientDependency
{
    public ILogger<DocumentTypeSeedContributor> Logger { get; set; }

    private readonly IRepository<DocumentType, int> _DocumentTypeRepo;

    public DocumentTypeSeedContributor(IRepository<DocumentType, int> DocumentTypeRepo)
    {
        _DocumentTypeRepo = DocumentTypeRepo;
        Logger = NullLogger<DocumentTypeSeedContributor>.Instance;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        Logger.LogInformation($"Seeding document type start...");
        if (await _DocumentTypeRepo.GetCountAsync() > 0)
        {
            return;
        }
        List<DocumentType> DocumentTypes = new List<DocumentType>();
        var d1 = new DocumentType("1", "Bản gốc");
        d1.Description = "";
        d1.OrderIndex = 0;
        d1.Status = Status.Active;
        DocumentTypes.Add(d1);

        var d2 = new DocumentType("2", "Bản chính");
        d2.Description = "";
        d2.OrderIndex = 1;
        d1.Status = Status.Active;
        DocumentTypes.Add(d2);

        var d3 = new DocumentType("3", "Bản phô tô");
        d3.Description = "";
        d3.OrderIndex = 2;
        d3.Status = Status.Active;
        DocumentTypes.Add(d3);

        await _DocumentTypeRepo.InsertManyAsync(DocumentTypes);

        Logger.LogInformation($"Seeding document type success!");
    }
}