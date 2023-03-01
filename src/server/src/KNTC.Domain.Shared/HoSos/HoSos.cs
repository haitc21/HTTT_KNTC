using System;
using System.Collections.Generic;
using System.Text;

namespace KNTC.HoSos;
public enum LoaiVuViec : short
{
    KhieuNai,
    ToCao
}
public enum LinhVuc : short
{
    DataDai,
    MoiTruong,
    TaiNguyenNuoc,
    Khoángan
}
public enum LoaiKetQua : short
{
    Sai = -2,
    CoSai = -1,
    CoDung = 1,
    Dung = 2,
}