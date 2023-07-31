using KNTC.Complains;
using KNTC.SpatialDatas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Distributed;

namespace KNTC.Complains;

public class UpdateComplainHandler
        : IDistributedEventHandler<UpdateComplainEto>,
          ITransientDependency
{
    private readonly IRepository<SpatialData, int> _spatialDataRepo;
    private readonly SpatialDataManager _spatialDataManager;
    public UpdateComplainHandler(IRepository<SpatialData, int> spatialDataRepo,
        SpatialDataManager spatialDataManager)
    {
        _spatialDataRepo = spatialDataRepo;
        _spatialDataManager = spatialDataManager;
    }
    public async Task HandleEventAsync(UpdateComplainEto eventData)
    {
        var spatialData = await _spatialDataRepo.GetAsync(x => x.IdHoSo == eventData.Id);
        await _spatialDataManager.UpdateAsync(spatialData, eventData);
        await _spatialDataRepo.InsertAsync(spatialData);
    }
}
