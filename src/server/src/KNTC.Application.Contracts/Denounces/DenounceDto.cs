using KNTC.FileAttachments;
using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace KNTC.Denounces;

public class DenounceDto : AuditedEntityDto<Guid>
{
    public string MaHoSo { get; private set; }
    public LoaiVuViec LoaiVuViec { get; set; }
    public string TieuDe { get; set; }
    public string NguoiDeNghi { get; set; }
    public string CccdCmnd { get; set; }
    public DateTime NgayCapCccdCmnd { get; set; }
    public string NoiCapCccdCmnd { get; set; }
    public DateTime NgaySinh { get; set; }
    public string DienThoai { get; set; }
    public string Email { get; set; }
    public string DiaChiThuongTru { get; set; }
    public string DiaChiLienHe { get; set; }
    public int maTinhTP { get; set; }
    public int maQuanHuyen { get; set; }
    public string maXaPhuongTT { get; set; }
    public DateTime ThoiGianTiepNhan { get; set; }
    public DateTime ThoiGianHenTraKQ { get; set; }
    public string NoiDungVuViec { get; set; }
    public string boPhanDangXL { get; set; }
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
    public DateTime? ngayKhieuNai1 { get; set; }
    public DateTime? NgayTraKQ1 { get; set; }
    public string ThamQuyen1 { get; set; }
    public string SoQD1 { get; set; }
    public LoaiKetQua? KetQua1 { get; set; }
    public DateTime? ngayKhieuNai2 { get; set; }
    public DateTime? NgayTraKQ2 { get; set; }
    public string ThamQuyen2 { get; set; }
    public string SoQDII { get; set; }
    public LoaiKetQua? KetQua2 { get; set; }
    public LoaiKetQua? KetQua { get; set; }
    public virtual List<FileAttachmentDto> FileAttachments { get; set; }
    public string ConcurrencyStamp { get; set; }
}
