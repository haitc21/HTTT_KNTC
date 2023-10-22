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
                return string.Empty;
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
                return string.Empty;
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
                return string.Empty;
        }
    }

    public static string ToVNString(this TrangThai value)
    {
        if (value == null) return string.Empty;
        switch (value)
        {
            case TrangThai.TiepNhan:
                return "Tiếp nhận";

            case TrangThai.DangXuLy:
                return "Đang xử lý";

            case TrangThai.DaThuLy:
                return "Đã thụ lý";

            case TrangThai.DaKetLuan:
                return "Đã kết luận";

            case TrangThai.RutDon:
                return "Rút đơn";

            case TrangThai.TraLaiDon:
                return "Trả lại đơn";

            case TrangThai.ChuyenDon:
                return "Chuyển đơn";

            default:
                return string.Empty;
        }
    }
}