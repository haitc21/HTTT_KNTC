using KNTC.SpatialDatas;
using KNTC.Summaries;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace KNTC.Denounces;

public class DeleteDenounceHandler
        : IDistributedEventHandler<DeleteDenounceEto>,
          ITransientDependency
{
    private readonly ISpatialDataRepository _spatialDataRepo;
    private readonly SpatialDataManager _spatialDataManager;
    private readonly ISummaryDapperRepository _summaryDapperRepo;

    public DeleteDenounceHandler(ISpatialDataRepository spatialDataRepo,
        SpatialDataManager spatialDataManager,
        ISummaryDapperRepository summaryDapperRepo)
    {
        _spatialDataRepo = spatialDataRepo;
        _spatialDataManager = spatialDataManager;
        _summaryDapperRepo = summaryDapperRepo;
    }

    public async Task HandleEventAsync(DeleteDenounceEto eventData)
    {
        await _summaryDapperRepo.RefreshView();
        var spatialData = await _spatialDataRepo.FindByIdHoSoAsync(eventData.Id);
        if (spatialData != null)
        {
            await _spatialDataRepo.DeleteAsync(spatialData);
        }
    }
}