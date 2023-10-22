//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Volo.Abp.Domain.Entities;

//namespace KNTC.Histories;

//public class History : Entity<int>
//{
//    public History()
//    {
//    }
//    public History(Guid idHoSo, LoaiVuViec loaiVuViec)
//    {
//        IdHoSo = idHoSo;
//        LoaiVuViec = loaiVuViec;
//    }
//    public Guid IdHoSo { get; private set; }
//    public LoaiVuViec LoaiVuViec { get; private set; }
//    public ThaoTac ThaoTac { get; set; }
//    public Guid NguoithucHien { get; set; }
//    public string? GhiChu { get; set; }
//}