using KNTC.FileAttachments;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace KNTC.Complains;

public class ComplainDto : AuditedEntityDto<Guid>
{
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
}
