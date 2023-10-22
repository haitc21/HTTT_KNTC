using KNTC.SpatialDatas;
using KNTC.Summaries;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace KNTC.Denounces;

public class UpdateDenounceHandler
        : IDistributedEventHandler<UpdateDenounceEto>,
          ITransientDependency
{
    private readonly ISpatialDataRepository _spatialDataRepo;
    private readonly SpatialDataManager _spatialDataManager;
    private readonly ISummaryDapperRepository _summaryDapperRepo;

    public UpdateDenounceHandler(ISpatialDataRepository spatialDataRepo,
        SpatialDataManager spatialDataManager,
        ISummaryDapperRepository summaryDapperRepo)
    {
        _spatialDataRepo = spatialDataRepo;
        _spatialDataManager = spatialDataManager;
        _summaryDapperRepo = summaryDapperRepo;
    }

    public async Task HandleEventAsync(UpdateDenounceEto eventData)
    {
        await _summaryDapperRepo.RefreshView();
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
                                                                       loaiVuViec: LoaiVuViec.ToCao,
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
        }
    }
}