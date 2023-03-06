using System;
using System.Collections.Generic;
using System.Text;

namespace KNTC;
public enum LoaiVuViec : short
{
    KhieuNai = 1,
    ToCao = 2
}
public enum LinhVuc : short
{
    DataDai = 1,
    MoiTruong = 2,
    TaiNguyenNuoc = 3,
    KhoangSan = 4
}
public enum LoaiKetQua : short
{
    Dung = 1,
    Sai = -1,
    CoDungCoSai = 2,
}

public enum LoaiKhieuNai : short
{
    KhieuNai = 1,
    KhieuKien = 2,
}