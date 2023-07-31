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

public class DeleteMultipleComplainHandler
        : IDistributedEventHandler<DeleteMultipleComplainEto>,
          ITransientDependency
{
    private readonly IRepository<SpatialData, int> _spatialDataRepo;
    private readonly SpatialDataManager _spatialDataManager;
    public DeleteMultipleComplainHandler(IRepository<SpatialData, int> spatialDataRepo,
        SpatialDataManager spatialDataManager)
    {
        _spatialDataRepo = spatialDataRepo;
        _spatialDataManager = spatialDataManager;
    }
    public async Task HandleEventAsync(DeleteMultipleComplainEto eventData)
    {
        var spatialDatas = await _spatialDataRepo.GetListAsync(x => eventData.Ids.Contains(x.IdHoSo));
        await _spatialDataRepo.DeleteManyAsync(spatialDatas);
    }
}
