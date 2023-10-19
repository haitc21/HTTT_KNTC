using KNTC.SpatialDatas;
using KNTC.Summaries;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace KNTC.Denounces;

public class DeleteMultipleDenounceHandler
        : IDistributedEventHandler<DeleteMultipleDenounceEto>,
          ITransientDependency
{
    private readonly ISpatialDataRepository _spatialDataRepo;
    private readonly ISummaryDapperRepository _summaryDapperRepo;
    public DeleteMultipleDenounceHandler(ISpatialDataRepository spatialDataRepo, ISummaryDapperRepository summaryDapperRepo)
    {
        _spatialDataRepo = spatialDataRepo;
        _summaryDapperRepo = summaryDapperRepo;
    }

    public async Task HandleEventAsync(DeleteMultipleDenounceEto eventData)
    {
        await _summaryDapperRepo.RefreshView();
        var spatialDatas = await _spatialDataRepo.GetListByIdHoSoAsync(eventData.Ids);
        await _spatialDataRepo.DeleteManyAsync(spatialDatas);
    }
}