using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace KNTC.Histories;

public class History : Entity<int>
{
    public History()
    {

    }
    public History(Guid idHoSo, LoaiVuViec loaiVuViec, ThaoTac thaoTac, Guid mguoithucHien, string ghiChu)
    {
        IdHoSo = idHoSo;
        LoaiVuViec = loaiVuViec;
        ThaoTac = thaoTac;
        NguoithucHien = mguoithucHien;
        GhiChu = ghiChu;
    }
    public History(Guid idHoSo, LoaiVuViec loaiVuViec, TrangThai trangThai, Guid mguoithucHien, string ghiChu)
    {
        IdHoSo = idHoSo;
        LoaiVuViec = loaiVuViec;
        SetthaoTac(trangThai);
        NguoithucHien = mguoithucHien;
        GhiChu = ghiChu;
    }
    public Guid IdHoSo { get; private set; }
    public LoaiVuViec LoaiVuViec { get; private set; }
    public ThaoTac ThaoTac { get; set; }
    public Guid NguoithucHien { get; set; }
    public string? GhiChu { get; set; }
    private void SetthaoTac(TrangThai trangThai)
    {
        switch (trangThai)
        {
            case TrangThai.TiepNhan:
                ThaoTac = ThaoTac.TiepNhan;
                break;
            case TrangThai.ChuyenDon:
                ThaoTac = ThaoTac.ChuyenDon;
                break;
            case TrangThai.ThuLy:
                ThaoTac = ThaoTac.ThuLy;
                break;
            case TrangThai.KetLuan:
                ThaoTac = ThaoTac.KetLuan;
                break;
            case TrangThai.TraDpm:
                ThaoTac = ThaoTac.TraDon;
                break;
            case TrangThai.RutDon:
                ThaoTac = ThaoTac.RutDon;
                break;
            default:
                throw new NotImplementedException();

        }
    }
}
