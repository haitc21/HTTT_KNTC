using KNTC.SpatialDatas;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace KNTC.Denounces;

public class DeleteMultipleDenounceHandler
        : IDistributedEventHandler<DeleteMultipleDenounceEto>,
          ITransientDependency
{
    private readonly ISpatialDataRepository _spatialDataRepo;
    private readonly SpatialDataManager _spatialDataManager;

    public DeleteMultipleDenounceHandler(ISpatialDataRepository spatialDataRepo,
        SpatialDataManager spatialDataManager)
    {
        _spatialDataRepo = spatialDataRepo;
        _spatialDataManager = spatialDataManager;
    }

    public async Task HandleEventAsync(DeleteMultipleDenounceEto eventData)
    {
        var spatialDatas = await _spatialDataRepo.GetListByIdHoSoAsync(eventData.Ids);
        await _spatialDataRepo.DeleteManyAsync(spatialDatas);
    }
}