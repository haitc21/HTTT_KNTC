using KNTC.FileAttachments;
using KNTC.LandTypes;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace KNTC.Denounces;

public class Denounce : FullAuditedAggregateRoot<Guid>
{
    public Denounce()
    {
    }
    public Denounce(Guid id) : base(id)
    {
    }
    public Denounce(Guid id, string maHoSo) : base(id)
    {
        SetMaHoSo(maHoSo);
    }
    public string MaHoSo { get; private set; }
    public LinhVuc LinhVuc { get; set; }
    public string TieuDe { get; set; }
    public string NguoiDeNghi { get; set; }
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
    public string NguoiBiToCao { get; set; }
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
    public DateTime NgayGQTC { get; set; }
    public string NguoiGQTC { get; set; }
    public string QuyerDinhThuLyGQTC { get; set; }
    public DateTime NgayQDGQTC { get; set; }
    public string QuyetDinhDinhChiGQTC { get; set; }
    public DateTime? GiaHanGQTC1 { get; set; }
    public DateTime? GiaHanGQTC2 { get; set; }
    public string SoVBKLNDTC { get; set; }
    public DateTime NgayNhanTBKQXLKLTC { get; set; }
    public LoaiKetQua KetQua { get; set; }
    public bool CongKhaiKLGQTC { get; set; }
    public LandType LandType { get; set; }
    private void SetMaHoSo([NotNull] string maHoSo)
    {
        MaHoSo = Check.NotNullOrWhiteSpace(
            maHoSo,
            nameof(maHoSo),
            maxLength: KNTCValidatorConsts.MaxMaHoSoLength
        );
    }

    internal Denounce ChangeMaHoSo([NotNull] string maHoSo)
    {
        SetMaHoSo(maHoSo);
        return this;
    }
}
