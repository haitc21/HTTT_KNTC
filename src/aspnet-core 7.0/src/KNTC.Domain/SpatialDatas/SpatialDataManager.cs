using KNTC;
using KNTC.Complains;
using KNTC.Denounces;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using Newtonsoft.Json;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Json;

namespace KNTC.SpatialDatas;

public class SpatialDataManager : DomainService
{
    private readonly IRepository<SpatialData, int> _spatialDataRepo;
    public IJsonSerializer JsonSerializer { get; }

    public SpatialDataManager(IRepository<SpatialData, int> spatialDataRepo, IJsonSerializer jsonSerializer)
    {
        _spatialDataRepo = spatialDataRepo;
        JsonSerializer = jsonSerializer;
    }

    public async Task<SpatialData> CreateAsync([NotNull] Guid idHoSo,
                                                [NotNull] LoaiVuViec loaiVuViec,
                                                [NotNull] LinhVuc linhVuc,
                                                [NotNull] string tieuDe,
                                                [NotNull] string nguoiNopDon,
                                                [NotNull] string cccdCmnd,
                                                [NotNull] string dienThoai,
                                                [NotNull] DateTime thoiGianTiepNhan,
                                                [NotNull] int maTinhTP,
                                                [NotNull] int maQuanHuyen,
                                                [NotNull] int maXaPhuongTT,
                                                [NotNull] LoaiKetQua ketQua,
                                                [NotNull] bool congKhai,
                                                string? duLieuToaDo,
                                                string? duLieuHinhHoc)
    {

        return new SpatialData()
        {
            IdHoSo = idHoSo,
            LoaiVuViec = loaiVuViec,
            LinhVuc = linhVuc,
            TieuDe = tieuDe,
            NguoiNopDon = nguoiNopDon,
            CccdCmnd = cccdCmnd,
            DienThoai = dienThoai,
            ThoiGianTiepNhan = thoiGianTiepNhan,
            MaTinhTP = maTinhTP,
            MaQuanHuyen = maQuanHuyen,
            MaXaPhuongTT = maXaPhuongTT,
            KetQua = ketQua,
            CongKhai = congKhai,
            DuLieuToaDo = SpatialDataHelper.ConvertStringToPoint(duLieuToaDo),
            DuLieuHinhHoc = SpatialDataHelper.ConvertJsonToGeometry(duLieuHinhHoc)
        };
    }
    public async Task<SpatialData> CreateAsync(Complain complain)
    {
        SpatialData spatialData = new SpatialData()
        {
            IdHoSo = complain.Id,
            LoaiVuViec = LoaiVuViec.KhieuNai,
            LinhVuc = complain.LinhVuc,
            TieuDe = complain.TieuDe,
            NguoiNopDon = complain.NguoiNopDon,
            CccdCmnd = complain.CccdCmnd,
            DienThoai = complain.DienThoai,
            ThoiGianTiepNhan = complain.ThoiGianTiepNhan,
            MaTinhTP = complain.MaTinhTP,
            MaQuanHuyen = complain.MaQuanHuyen,
            MaXaPhuongTT = complain.MaXaPhuongTT,
            KetQua = complain.KetQua,
            CongKhai = complain.CongKhai,
            DuLieuToaDo = SpatialDataHelper.ConvertStringToPoint(complain.DuLieuToaDo),
            DuLieuHinhHoc = SpatialDataHelper.ConvertJsonToGeometry(complain.DuLieuHinhHoc)
        };
        await _spatialDataRepo.InsertAsync(spatialData);
        return spatialData;
    }
    public async Task<SpatialData> CreateAsync(Denounce denouce)
    {
        SpatialData spatialData = new SpatialData()
        {
            IdHoSo = denouce.Id,
            LoaiVuViec = LoaiVuViec.KhieuNai,
            LinhVuc = denouce.LinhVuc,
            TieuDe = denouce.TieuDe,
            NguoiNopDon = denouce.NguoiNopDon,
            CccdCmnd = denouce.CccdCmnd,
            DienThoai = denouce.DienThoai,
            ThoiGianTiepNhan = denouce.ThoiGianTiepNhan,
            MaTinhTP = denouce.MaTinhTP,
            MaQuanHuyen = denouce.MaQuanHuyen,
            MaXaPhuongTT = denouce.MaXaPhuongTT,
            KetQua = denouce.KetQua,
            CongKhai = denouce.CongKhai,
            DuLieuToaDo = SpatialDataHelper.ConvertStringToPoint(denouce.DuLieuToaDo),
            DuLieuHinhHoc = SpatialDataHelper.ConvertJsonToGeometry(denouce.DuLieuHinhHoc)
        };
        await _spatialDataRepo.InsertAsync(spatialData);
        return spatialData;
    }
    public async Task UpdateAsync([NotNull] SpatialData spatialData,
                                  [NotNull] string tieuDe,
                                  [NotNull] string nguoiNopDon,
                                  [NotNull] string cccdCmnd,
                                  [NotNull] string dienThoai,
                                  [NotNull] DateTime thoiGianTiepNhan,
                                  [NotNull] int maTinhTP,
                                  [NotNull] int maQuanHuyen,
                                  [NotNull] int maXaPhuongTT,
                                  [NotNull] LoaiKetQua ketQua,
                                  [NotNull] bool congKhai,
                                  string? duLieuToaDo,
                                  string? duLieuHinhHoc)
    {

        spatialData.TieuDe = tieuDe;
        spatialData.NguoiNopDon = nguoiNopDon;
        spatialData.CccdCmnd = cccdCmnd;
        spatialData.DienThoai = dienThoai;
        spatialData.ThoiGianTiepNhan = thoiGianTiepNhan;
        spatialData.MaTinhTP = maTinhTP;
        spatialData.MaQuanHuyen = maQuanHuyen;
        spatialData.MaXaPhuongTT = maXaPhuongTT;
        spatialData.KetQua = ketQua;
        spatialData.CongKhai = congKhai;
        spatialData.DuLieuToaDo = SpatialDataHelper.ConvertStringToPoint(duLieuToaDo);
        spatialData.DuLieuHinhHoc = SpatialDataHelper.ConvertJsonToGeometry(duLieuHinhHoc);
    }

    public async Task UpdateAsync([NotNull] Complain complain)
    {
        var spatialData = await _spatialDataRepo.GetAsync(x => x.IdHoSo == complain.Id);
        spatialData.TieuDe = complain.TieuDe;
        spatialData.NguoiNopDon = complain.NguoiNopDon;
        spatialData.CccdCmnd = complain.CccdCmnd;
        spatialData.DienThoai = complain.DienThoai;
        spatialData.ThoiGianTiepNhan = complain.ThoiGianTiepNhan;
        spatialData.MaTinhTP = complain.MaTinhTP;
        spatialData.MaQuanHuyen = complain.MaQuanHuyen;
        spatialData.MaXaPhuongTT = complain.MaXaPhuongTT;
        spatialData.KetQua = complain.KetQua;
        spatialData.CongKhai = complain.CongKhai;
        spatialData.DuLieuToaDo = SpatialDataHelper.ConvertStringToPoint(complain.DuLieuToaDo);
        spatialData.DuLieuHinhHoc = SpatialDataHelper.ConvertJsonToGeometry(complain.DuLieuHinhHoc);
        await _spatialDataRepo.UpdateAsync(spatialData);
    }
    public async Task UpdateAsync([NotNull] Denounce denounce)
    {
        var spatialData = await _spatialDataRepo.GetAsync(x => x.IdHoSo == denounce.Id);
        spatialData.TieuDe = denounce.TieuDe;
        spatialData.NguoiNopDon = denounce.NguoiNopDon;
        spatialData.CccdCmnd = denounce.CccdCmnd;
        spatialData.DienThoai = denounce.DienThoai;
        spatialData.ThoiGianTiepNhan = denounce.ThoiGianTiepNhan;
        spatialData.MaTinhTP = denounce.MaTinhTP;
        spatialData.MaQuanHuyen = denounce.MaQuanHuyen;
        spatialData.MaXaPhuongTT = denounce.MaXaPhuongTT;
        spatialData.KetQua = denounce.KetQua;
        spatialData.CongKhai = denounce.CongKhai;
        spatialData.DuLieuToaDo = SpatialDataHelper.ConvertStringToPoint(denounce.DuLieuToaDo);
        spatialData.DuLieuHinhHoc = SpatialDataHelper.ConvertJsonToGeometry(denounce.DuLieuHinhHoc);
        await _spatialDataRepo.UpdateAsync(spatialData);
    }
}