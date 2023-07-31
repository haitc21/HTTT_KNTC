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
    public IJsonSerializer JsonSerializer { get; }

    public SpatialDataManager(IJsonSerializer jsonSerializer)
    {
        JsonSerializer = jsonSerializer;
    }

    public async Task<SpatialData> CreateAsync([NotNull] Guid idHoSo,
                                                [NotNull] string maHoSo,
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
            MaHoSo = maHoSo,
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
    public async Task<SpatialData> CreateAsync(CreateComplainEto complainEto)
    {
        var gf = NetTopologySuite.NtsGeometryServices.Instance.CreateGeometryFactory(4326);
        string json = complainEto.DuLieuHinhHoc;
        var serializer = GeoJsonSerializer.Create();
        using (var stringReader = new StringReader(json))
        using (var jsonReader = new JsonTextReader(stringReader))
        {
            Geometry result = serializer.Deserialize<Geometry>(jsonReader);
        }
        SpatialData spatialData = new SpatialData()
        {
            IdHoSo = complainEto.Id,
            MaHoSo = complainEto.MaHoSo,
            LoaiVuViec = LoaiVuViec.KhieuNai,
            LinhVuc = complainEto.LinhVuc,
            TieuDe = complainEto.TieuDe,
            NguoiNopDon = complainEto.NguoiNopDon,
            CccdCmnd = complainEto.CccdCmnd,
            DienThoai = complainEto.DienThoai,
            ThoiGianTiepNhan = complainEto.ThoiGianTiepNhan,
            MaTinhTP = complainEto.MaTinhTP,
            MaQuanHuyen = complainEto.MaQuanHuyen,
            MaXaPhuongTT = complainEto.MaXaPhuongTT,
            KetQua = complainEto.KetQua,
            CongKhai = complainEto.CongKhai,
            DuLieuToaDo = SpatialDataHelper.ConvertStringToPoint(complainEto.DuLieuToaDo),
            DuLieuHinhHoc = SpatialDataHelper.ConvertJsonToGeometry(complainEto.DuLieuHinhHoc)
        };
        return spatialData;
    }
    public async Task<SpatialData> CreateAsync(CreateDenounceEto denouceEto)
    {
        SpatialData spatialData = new SpatialData()
        {
            IdHoSo = denouceEto.Id,
            MaHoSo = denouceEto.MaHoSo,
            LoaiVuViec = LoaiVuViec.ToCao,
            LinhVuc = denouceEto.LinhVuc,
            TieuDe = denouceEto.TieuDe,
            NguoiNopDon = denouceEto.NguoiNopDon,
            CccdCmnd = denouceEto.CccdCmnd,
            DienThoai = denouceEto.DienThoai,
            ThoiGianTiepNhan = denouceEto.ThoiGianTiepNhan,
            MaTinhTP = denouceEto.MaTinhTP,
            MaQuanHuyen = denouceEto.MaQuanHuyen,
            MaXaPhuongTT = denouceEto.MaXaPhuongTT,
            KetQua = denouceEto.KetQua,
            CongKhai = denouceEto.CongKhai,
            DuLieuToaDo = SpatialDataHelper.ConvertStringToPoint(denouceEto.DuLieuToaDo),
            DuLieuHinhHoc = SpatialDataHelper.ConvertJsonToGeometry(denouceEto.DuLieuHinhHoc)
        };
        return spatialData;
    }
    public async Task UpdateAsync([NotNull] SpatialData spatialData,
                                  [NotNull] string maHoSo,
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
        spatialData.MaHoSo = maHoSo;
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

    public async Task UpdateAsync([NotNull] SpatialData spatialData,[NotNull] UpdateComplainEto complainEto)
    {
        spatialData.MaHoSo = complainEto.MaHoSo;
        spatialData.TieuDe = complainEto.TieuDe;
        spatialData.NguoiNopDon = complainEto.NguoiNopDon;
        spatialData.CccdCmnd = complainEto.CccdCmnd;
        spatialData.DienThoai = complainEto.DienThoai;
        spatialData.ThoiGianTiepNhan = complainEto.ThoiGianTiepNhan;
        spatialData.MaTinhTP = complainEto.MaTinhTP;
        spatialData.MaQuanHuyen = complainEto.MaQuanHuyen;
        spatialData.MaXaPhuongTT = complainEto.MaXaPhuongTT;
        spatialData.KetQua = complainEto.KetQua;
        spatialData.CongKhai = complainEto.CongKhai;
        spatialData.DuLieuToaDo = SpatialDataHelper.ConvertStringToPoint(complainEto.DuLieuToaDo);
        spatialData.DuLieuHinhHoc = SpatialDataHelper.ConvertJsonToGeometry(complainEto.DuLieuHinhHoc);
    }
    public async Task UpdateAsync([NotNull] SpatialData spatialData, [NotNull] UpdateDenounceEto denounceEto)
    {
        spatialData.MaHoSo = denounceEto.MaHoSo;
        spatialData.TieuDe = denounceEto.TieuDe;
        spatialData.NguoiNopDon = denounceEto.NguoiNopDon;
        spatialData.CccdCmnd = denounceEto.CccdCmnd;
        spatialData.DienThoai = denounceEto.DienThoai;
        spatialData.ThoiGianTiepNhan = denounceEto.ThoiGianTiepNhan;
        spatialData.MaTinhTP = denounceEto.MaTinhTP;
        spatialData.MaQuanHuyen = denounceEto.MaQuanHuyen;
        spatialData.MaXaPhuongTT = denounceEto.MaXaPhuongTT;
        spatialData.KetQua = denounceEto.KetQua;
        spatialData.CongKhai = denounceEto.CongKhai;
        spatialData.DuLieuToaDo = SpatialDataHelper.ConvertStringToPoint(denounceEto.DuLieuToaDo);
        spatialData.DuLieuHinhHoc = SpatialDataHelper.ConvertJsonToGeometry(denounceEto.DuLieuHinhHoc);
    }
}