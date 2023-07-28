using NetTopologySuite.Geometries;
using System;
using Volo.Abp.Domain.Entities;

namespace KNTC.SpatialDatas;

public class SpatialData : Entity<int>
{
    public SpatialData()
    {
    }

    public SpatialData(int Id) : base(Id)
    {
    }
    public int IdHoSo { get; set; }

    public LoaiVuViec LoaiVuViec { get; set; }
    public string MaHoSo { get; private set; }
    public LinhVuc LinhVuc { get; set; }
    public string TieuDe { get; set; }
    public string NguoiNopDon { get; set; }
    public string CccdCmnd { get; set; }
    public string DienThoai { get; set; }
    public DateTime ThoiGianTiepNhan { get; set; }
    public int? MaTinhTP { get; set; }
    public int? MaQuanHuyen { get; set; }
    public int? MaXaPhuongTT { get; set; }
    public LoaiKetQua? KetQua { get; set; }
    public bool? CongKhai { get; set; }
    public NetTopologySuite.Geometries.Point? DuLieuToaDo { get; set; }
    public NetTopologySuite.Geometries.Geometry? DuLieuHinhHoc { get; set; }
}