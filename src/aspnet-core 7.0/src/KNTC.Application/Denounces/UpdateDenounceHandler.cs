
using KNTC.Histories;
using KNTC.SpatialDatas;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Users;

namespace KNTC.Denounces;

public class UpdateDenounceHandler
        : IDistributedEventHandler<UpdateDenounceEto>,
          ITransientDependency
{
    private readonly ISpatialDataRepository _spatialDataRepo;
    private readonly SpatialDataManager _spatialDataManager;
    private readonly IRepository<History, int> _historyRepo;
    private readonly ICurrentUser _currentUser;

    public UpdateDenounceHandler(ISpatialDataRepository spatialDataRepo,
        SpatialDataManager spatialDataManager,
        IRepository<History, int> historyRepo,
        ICurrentUser currentUser)
    {
        _spatialDataRepo = spatialDataRepo;
        _spatialDataManager = spatialDataManager;
        _historyRepo = historyRepo;
        _currentUser = currentUser;
    }

    public async Task HandleEventAsync(UpdateDenounceEto eventData)
    {
        // spatial data
        var spatialData = await _spatialDataRepo.FindByIdHoSoAsync(eventData.Id);
        await _spatialDataManager.UpdateAsync(spatialData, eventData);
        await _spatialDataRepo.UpdateAsync(spatialData);
        // Ghi lich su
        var history = new History(eventData.Id,
                                 LoaiVuViec.KhieuNai,
                                 eventData.ThaoTac,
                                 _currentUser.Id.Value,
                                 eventData.GhiChu);
        await _historyRepo.InsertAsync(history);
    }
}