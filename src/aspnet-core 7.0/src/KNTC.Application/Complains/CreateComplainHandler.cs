using KNTC.Histories;
using KNTC.SpatialDatas;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Users;

namespace KNTC.Complains;

public class CreateComplainHandler
        : IDistributedEventHandler<CreateComplainEto>,
          ITransientDependency
{
    private readonly ISpatialDataRepository _spatialDataRepo;
    private readonly SpatialDataManager _spatialDataManager;
    private readonly IRepository<History, int> _historyRepo;
    private readonly ICurrentUser _currentUser;

    public CreateComplainHandler(ISpatialDataRepository spatialDataRepo,
        SpatialDataManager spatialDataManager,
        IRepository<History, int> historyRepo,
        ICurrentUser currentUser)
    {
        _spatialDataRepo = spatialDataRepo;
        _spatialDataManager = spatialDataManager;
        _historyRepo = historyRepo;
        _currentUser = currentUser;
    }

    public async Task HandleEventAsync(CreateComplainEto eventData)
    {
        // Tao spatial data
        var spatialData = await _spatialDataManager.CreateAsync(eventData);
        await _spatialDataRepo.InsertAsync(spatialData);
        // Ghi lich su
        var history = new History(eventData.Id,
                                 LoaiVuViec.KhieuNai,
                                 eventData.ThaoTac,
                                 _currentUser.Id.Value,
                                 eventData.GhiChu);
        await _historyRepo.InsertAsync(history);
    }
}