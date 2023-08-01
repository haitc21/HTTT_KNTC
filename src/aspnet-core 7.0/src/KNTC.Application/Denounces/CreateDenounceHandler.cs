using KNTC.SpatialDatas;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace KNTC.Denounces;

public class CreateDenounceHandler
        : IDistributedEventHandler<CreateDenounceEto>,
          ITransientDependency
{
    private readonly ISpatialDataRepository _spatialDataRepo;
    private readonly SpatialDataManager _spatialDataManager;

    public CreateDenounceHandler(ISpatialDataRepository spatialDataRepo,
        SpatialDataManager spatialDataManager)
    {
        _spatialDataRepo = spatialDataRepo;
        _spatialDataManager = spatialDataManager;
    }

    public async Task HandleEventAsync(CreateDenounceEto eventData)
    {
        var spatialData = await _spatialDataManager.CreateAsync(eventData);
        await _spatialDataRepo.InsertAsync(spatialData);
    }
}