using KNTC.SpatialDatas;
using KNTC.Summaries;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace KNTC.Complains;

public class CreateComplainHandler
        : IDistributedEventHandler<CreateComplainEto>,
          ITransientDependency
{
    private readonly ISpatialDataRepository _spatialDataRepo;
    private readonly SpatialDataManager _spatialDataManager;
    private readonly ISummaryDapperRepository _summaryDapperRepo;

    public CreateComplainHandler(ISpatialDataRepository spatialDataRepo,
        SpatialDataManager spatialDataManager,
        ISummaryDapperRepository summaryDapperRepo)
    {
        _spatialDataRepo = spatialDataRepo;
        _spatialDataManager = spatialDataManager;
        _summaryDapperRepo = summaryDapperRepo;
    }

    public async Task HandleEventAsync(CreateComplainEto eventData)
    {
        await _summaryDapperRepo.RefreshView();
        var spatialData = await _spatialDataManager.CreateAsync(eventData);
        await _spatialDataRepo.InsertAsync(spatialData);
    }
}