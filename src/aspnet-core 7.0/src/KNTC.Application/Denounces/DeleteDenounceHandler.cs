using KNTC.SpatialDatas;
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

    public DeleteDenounceHandler(ISpatialDataRepository spatialDataRepo,
        SpatialDataManager spatialDataManager)
    {
        _spatialDataRepo = spatialDataRepo;
        _spatialDataManager = spatialDataManager;
    }

    public async Task HandleEventAsync(DeleteDenounceEto eventData)
    {
        var spatialData = await _spatialDataRepo.FindByIdHoSoAsync(eventData.Id);
        if (spatialData != null)
        {
            await _spatialDataRepo.DeleteAsync(spatialData);
        }
    }
}