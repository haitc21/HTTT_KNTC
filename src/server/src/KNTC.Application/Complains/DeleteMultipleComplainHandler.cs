using KNTC.SpatialDatas;
using KNTC.Summaries;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace KNTC.Complains;

public class DeleteMultipleComplainHandler
        : IDistributedEventHandler<DeleteMultipleComplainEto>,
          ITransientDependency
{
    private readonly ISpatialDataRepository _spatialDataRepo;
    private readonly ISummaryDapperRepository _summaryDapperRepo;

    public DeleteMultipleComplainHandler(ISpatialDataRepository spatialDataRepo,
    ISummaryDapperRepository summaryDapperRepo)
    {
        _spatialDataRepo = spatialDataRepo;
        _summaryDapperRepo = summaryDapperRepo;
    }

    public async Task HandleEventAsync(DeleteMultipleComplainEto eventData)
    {
        await _summaryDapperRepo.RefreshView();
        var spatialDatas = await _spatialDataRepo.GetListByIdHoSoAsync(eventData.Ids);
        await _spatialDataRepo.DeleteManyAsync(spatialDatas);
    }
}