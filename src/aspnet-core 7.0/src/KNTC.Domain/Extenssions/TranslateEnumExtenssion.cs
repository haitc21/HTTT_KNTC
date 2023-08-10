using System;

namespace KNTC.Extenssions;

public static class EnumExtensions
{
    public static string ToVNString(this LoaiKetQua value)
    {
        if (value == null) return string.Empty;
        switch (value)
        {
            case LoaiKetQua.Dung:
                return "Đúng";

            case LoaiKetQua.Sai:
                return "Sai";

            case LoaiKetQua.CoDungCoSai:
                return "Có đúng, có sai";

            default:
                throw new ArgumentException("Giá trị không hợp lệ");
        }
    }

    public static string ToVNString(this LinhVuc linhVuc)
    {
        if (linhVuc == null) return string.Empty;
        switch (linhVuc)
        {
            case LinhVuc.DatDai:
                return "Đất đai";

            case LinhVuc.MoiTruong:
                return "Môi trường";

            case LinhVuc.TaiNguyenNuoc:
                return "Tài nguyên nước";

            case LinhVuc.KhoangSan:
                return "Khoáng sản";

            default:
                throw new ArgumentException("Giá trị không hợp lệ");
        }
    }

    public static string ToVNString(this LoaiVuViec loaiVuViec)
    {
        if (loaiVuViec == null) return string.Empty;
        switch (loaiVuViec)
        {
            case LoaiVuViec.KhieuNai:
                return "Khiếu nại/Khiếu kiện";

            case LoaiVuViec.ToCao:
                return "Tố cáo";

            default:
                throw new ArgumentException("Giá trị không hợp lệ");
        }
    }
}