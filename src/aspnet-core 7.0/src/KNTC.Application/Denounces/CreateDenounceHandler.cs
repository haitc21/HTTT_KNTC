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

public class CreateDenounceHandler
        : IDistributedEventHandler<CreateDenounceEto>,
          ITransientDependency
{
    private readonly IRepository<SpatialData, int> _spatialDataRepo;
    private readonly SpatialDataManager _spatialDataManager;
    public CreateDenounceHandler(IRepository<SpatialData, int> spatialDataRepo,
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
