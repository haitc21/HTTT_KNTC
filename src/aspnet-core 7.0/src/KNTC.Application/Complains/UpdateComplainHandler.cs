using KNTC.Histories;
using KNTC.SpatialDatas;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Users;

namespace KNTC.Complains;

public class UpdateComplainHandler
        : IDistributedEventHandler<UpdateComplainEto>,
          ITransientDependency
{
    private readonly ISpatialDataRepository _spatialDataRepo;
    private readonly SpatialDataManager _spatialDataManager;
    private readonly IRepository<History, int> _historyRepo;
    private readonly ICurrentUser _currentUser;

    public UpdateComplainHandler(ISpatialDataRepository spatialDataRepo,
        SpatialDataManager spatialDataManager,
        IRepository<History, int> historyRepo,
        ICurrentUser currentUser)
    {
        _spatialDataRepo = spatialDataRepo;
        _spatialDataManager = spatialDataManager;
        _historyRepo = historyRepo;
        _currentUser = currentUser;
    }

    public async Task HandleEventAsync(UpdateComplainEto eventData)
    {
        // spatial data
        var spatialData = await _spatialDataRepo.FindByIdHoSoAsync(eventData.Id);
        if (spatialData != null)
        {
            await _spatialDataManager.UpdateAsync(spatialData, eventData);
            await _spatialDataRepo.UpdateAsync(spatialData);
        }
        else
        {
            var newSpatialData = await _spatialDataManager.CreateAsync(idHoSo: eventData.Id,
                                                                      maHoSo: eventData.MaHoSo,
                                                                      loaiVuViec: LoaiVuViec.KhieuNai,
                                                                      linhVuc: eventData.LinhVuc,
                                                                      tieuDe: eventData.TieuDe,
                                                                      nguoiNopDon: eventData.NguoiNopDon,
                                                                      cccdCmnd: eventData.CccdCmnd,
                                                                      dienThoai: eventData.DienThoai,
                                                                      thoiGianTiepNhan: eventData.ThoiGianTiepNhan,
                                                                      maTinhTP: eventData.MaTinhTP,
                                                                      maQuanHuyen: eventData.MaQuanHuyen,
                                                                      maXaPhuongTT: eventData.MaXaPhuongTT,
                                                                      ketQua: eventData.KetQua,
                                                                      congKhai: eventData.CongKhai,
                                                                      duLieuToaDo: eventData.DuLieuToaDo,
                                                                      duLieuHinhHoc: eventData.DuLieuHinhHoc);
            await _spatialDataRepo.InsertAsync(newSpatialData);
            // Ghi lich su
            var history = new History(eventData.Id, LoaiVuViec.KhieuNai,
                                     ThaoTac.ChinhSua,
                                     _currentUser.Id.Value,
                                     eventData.GhiChu);
            await _historyRepo.InsertAsync(history);
        }
    }
}