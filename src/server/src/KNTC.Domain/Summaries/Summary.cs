using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNTC.Summaries;
[NotMapped]
public class Summary
{
    public Guid Id { get; set; }
    public string MaHoSo { get; set; }
    public LoaiVuViec LoaiVuViec { get; set; }
    public LinhVuc LinhVuc { get; set; }
    public string TieuDe { get; set; }
    public string NguoiNopDon { get; set; }
    public string DienThoai { get; set; }
    public DateTime ThoiGianTiepNhan { get; set; }
    public DateTime ThoiGianHenTraKQ { get; set; }
    public string BoPhanDangXL { get; set; }
    public LoaiKetQua? KetQua { get; set; }

}

