using KNTC.SpatialDatas;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace KNTC.Complains;

public class DeleteMultipleComplainHandler
        : IDistributedEventHandler<DeleteMultipleComplainEto>,
          ITransientDependency
{
    private readonly ISpatialDataRepository _spatialDataRepo;
    private readonly SpatialDataManager _spatialDataManager;

    public DeleteMultipleComplainHandler(ISpatialDataRepository spatialDataRepo,
        SpatialDataManager spatialDataManager)
    {
        _spatialDataRepo = spatialDataRepo;
        _spatialDataManager = spatialDataManager;
    }

    public async Task HandleEventAsync(DeleteMultipleComplainEto eventData)
    {
        var spatialDatas = await _spatialDataRepo.GetListByIdHoSoAsync(eventData.Ids);
        await _spatialDataRepo.DeleteManyAsync(spatialDatas);
    }
}