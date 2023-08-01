using KNTC.Denounces;
using KNTC.SpatialDatas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Distributed;

namespace KNTC.Denounces;

public class UpdateDenounceHandler
        : IDistributedEventHandler<UpdateDenounceEto>,
          ITransientDependency
{
    private readonly ISpatialDataRepository _spatialDataRepo;
    private readonly SpatialDataManager _spatialDataManager;
    public UpdateDenounceHandler(ISpatialDataRepository spatialDataRepo,
        SpatialDataManager spatialDataManager)
    {
        _spatialDataRepo = spatialDataRepo;
        _spatialDataManager = spatialDataManager;
    }
    public async Task HandleEventAsync(UpdateDenounceEto eventData)
    {
        var spatialData = await _spatialDataRepo.GetByIdHoSoAsync.Id);
        await _spatialDataManager.UpdateAsync(spatialData, eventData);
        await _spatialDataRepo.UpdateAsync(spatialData);
    }
}
