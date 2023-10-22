using KNTC.SpatialDatas;
using KNTC.Summaries;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace KNTC.Complains;

public class DeleteComplainHandler
        : IDistributedEventHandler<DeleteComplainEto>,
          ITransientDependency
{
    private readonly ISpatialDataRepository _spatialDataRepo;
    private readonly ISummaryDapperRepository _summaryDapperRepo;

    public DeleteComplainHandler(ISpatialDataRepository spatialDataRepo,
        ISummaryDapperRepository summaryDapperRepo)
    {
        _spatialDataRepo = spatialDataRepo;
        _summaryDapperRepo = summaryDapperRepo;
    }

    public async Task HandleEventAsync(DeleteComplainEto eventData)
    {
        await _summaryDapperRepo.RefreshView();
        var spatialData = await _spatialDataRepo.FindByIdHoSoAsync(eventData.Id);
        if (spatialData != null)
        {
            await _spatialDataRepo.DeleteAsync(spatialData);
        }
    }
}