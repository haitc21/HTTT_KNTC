using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNTC.Extenssions;

public static class LoaiKetQuaExtensions
{
    public static string ToVietnameseString(this LoaiKetQua value)
    {
        if(value == null) return string.Empty;
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
}

