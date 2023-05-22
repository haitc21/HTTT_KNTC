using KNTC.LandTypes;
using System;
using System.Diagnostics.CodeAnalysis;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace KNTC.Complains;

public class Complain : AuditedAggregateRoot<Guid>
{
    public Complain()
    {
    }

    public Complain(Guid id) : base(id)
    {
    }

    public Complain(Guid id, string maHoSo) : base(id)
    {
        SetMaHoSo(maHoSo);
    }

    public string MaHoSo { get; private set; }
    public LinhVuc LinhVuc { get; set; }
    public string TieuDe { get; set; }
    public string NguoiNopDon { get; set; }
    public string CccdCmnd { get; set; }

    //public DateTime NgayCapCccdCmnd { get; set; }
    //public string NoiCapCccdCmnd { get; set; }
    public DateTime NgaySinh { get; set; }

    public string DienThoai { get; set; }
    public string Email { get; set; }
    public string DiaChiThuongTru { get; set; }
    public string DiaChiLienHe { get; set; }
    public int MaTinhTP { get; set; }
    public int MaQuanHuyen { get; set; }
    public int MaXaPhuongTT { get; set; }
    public DateTime ThoiGianTiepNhan { get; set; }
    public DateTime ThoiGianHenTraKQ { get; set; }
    public string NoiDungVuViec { get; set; }
    public string BoPhanDangXL { get; set; }
    public string SoThua { get; set; }
    public string ToBanDo { get; set; }
    public decimal DienTich { get; set; }
    public int LoaiDat { get; set; }
    public string DiaChiThuaDat { get; set; }
    public int tinhThuaDat { get; set; }
    public int huyenThuaDat { get; set; }
    public int xaThuaDat { get; set; }
    public string DuLieuToaDo { get; set; }
    public string DuLieuHinhHoc { get; set; }
    public string GhiChu { get; set; }
    public LoaiKhieuNai? loaiKhieuNai1 { get; set; }
    public DateTime? NgayKhieuNai1 { get; set; }
    public DateTime? NgayTraKQ1 { get; set; }
    public string ThamQuyen1 { get; set; }
    public string SoQD1 { get; set; }
    public LoaiKetQua? KetQua1 { get; set; }
    public LoaiKhieuNai? loaiKhieuNai2 { get; set; }
    public DateTime? NgayKhieuNai2 { get; set; }
    public DateTime? NgayTraKQ2 { get; set; }
    public string ThamQuyen2 { get; set; }
    public string SoQD2 { get; set; }
    public LoaiKetQua? KetQua2 { get; set; }
    public LoaiKetQua? KetQua { get; set; }
    public bool CongKhai { get; set; }
    public LandType LandType { get; set; }

    private void SetMaHoSo([NotNull] string maHoSo)
    {
        MaHoSo = Check.NotNullOrWhiteSpace(
            maHoSo,
            nameof(maHoSo),
            maxLength: KNTCValidatorConsts.MaxMaHoSoLength
        );
    }

    internal Complain ChangeMaHoSo([NotNull] string maHoSo)
    {
        SetMaHoSo(maHoSo);
        return this;
    }
}