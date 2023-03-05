using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using KNTC.FileAttachments;

namespace KNTC.Denounces;

public class Denounce : FullAuditedAggregateRoot<Guid>
{
    public Denounce()
    {
        FileAttachments = new List<FileAttachment>();
    }
    public Denounce(Guid id) : base(id)
    {
        FileAttachments = new List<FileAttachment>();
    }
    public Denounce(Guid id, string maHoSo) : base(id)
    {
        SetMaHoSo(maHoSo);
        FileAttachments = new List<FileAttachment>();
    }
    public string MaHoSo { get; private set; }
    public LoaiVuViec LoaiVuViec { get; set; }
    public string TieuDe { get; set; }
    public string NguoiDeNghi { get; set; }
    public string CccdCmnd { get; set; }
    public DateTime NgayCapCccdCmnd { get; set; }
    public string NoiCapCccdCmnd { get; set; }
    public DateTime NgaySinh { get; set; }
    public string DienThaoi { get; set; }
    public string Email { get; set; }
    public string DiaChiThuongTru { get; set; }
    public string DiaChiLienHe { get; set; }
    public string MaTinhTP { get; set; }
    public string MaQuanHuyen { get; set; }
    public string MaXaPhuongTT { get; set; }
    public DateTime NgayTiepNhan { get; set; }
    public DateTime NgayHenTraKQ { get; set; }
    public string NoiDungVuViec { get; set; }
    public string SoThua { get; set; }
    public string ToBanDo { get; set; }
    public string DienTich { get; set; }
    public string LoaiDat { get; set; }
    public string DiaChiThuaDat { get; set; }
    public string TinhThuaDat { get; set; }
    public string HuyenThuaDat { get; set; }
    public string XaThuaDat { get; set; }
    public string DuLieuToaDo { get; set; }
    public string DuLieuHinhHoc { get; set; }
    public string GhiChu { get; set; }
    public DateTime? ngayKhieuNaiI { get; set; }
    public DateTime? NgayTraKQI { get; set; }
    public string ThamQuyenI { get; set; }
    public string SoQDI { get; set; }
    public LoaiKetQua? KetQuaI { get; set; }
    public DateTime? ngayKhieuNaiII { get; set; }
    public DateTime? NgayTraKQII { get; set; }
    public string ThamQuyenII { get; set; }
    public string SoQDII { get; set; }
    public LoaiKetQua? KetQuaII { get; set; }
    public LoaiKetQua? KetQua { get; set; }
    public virtual List<FileAttachment> FileAttachments { get; set; }
    private void SetMaHoSo([NotNull] string maHoSo)
    {
        MaHoSo = Check.NotNullOrWhiteSpace(
            maHoSo,
            nameof(maHoSo),
            maxLength: DenounceConsts.MaxMaHoSoLength
        );
    }

    internal Denounce ChangeMaHoSo([NotNull] string maHoSo)
    {
        SetMaHoSo(maHoSo);
        return this;
    }
}
