using NetTopologySuite.Geometries;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel.DataAnnotations.Schema;
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

    public Guid IdHoSo { get; set; }
    public LoaiVuViec LoaiVuViec { get; set; }
    public string MaHoSo { get; set; }
    public LinhVuc LinhVuc { get; set; }
    public string TieuDe { get; set; }
    public string NguoiNopDon { get; set; }
    public string CccdCmnd { get; set; }
    public string DienThoai { get; set; }
    public DateTime ThoiGianTiepNhan { get; set; }
    public int MaTinhTP { get; set; }
    public int MaQuanHuyen { get; set; }
    public int MaXaPhuongTT { get; set; }
    public LoaiKetQua? KetQua { get; set; }
    public bool CongKhai { get; set; }

    [JsonConverter(typeof(NetTopologySuite.IO.Converters.GeometryConverter))]
    public Point? Point { get; set; }

    [JsonConverter(typeof(NetTopologySuite.IO.Converters.GeometryConverter))]
    public Geometry? Geometry { get; set; }

    public JObject? Properties { get; set; }
    public string? Type { get; set; }
}

[NotMapped]
public class GeoJsonData
{
    public string? type { get; set; }
    public JObject? properties { get; set; }

    [JsonConverter(typeof(NetTopologySuite.IO.Converters.GeometryConverter))]
    public Geometry? geometry { get; set; }
}