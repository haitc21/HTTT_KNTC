using KNTC.Denounces;
using KNTC.FileAttachments;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace KNTC.Denounces;

public class CreateUnitTypeDto
{
    [Required]
    [MaxLength(DenounceConsts.MaxMaHoSoLength)]
    public string MaHoSo { get; set; }
    [Required]
    public LoaiVuViec LoaiVuViec { get; set; }
    [Required]
    [MaxLength(DenounceConsts.MaxTieuDeLength)]
    public string TieuDe { get; set; }
    [Required]
    [MaxLength(DenounceConsts.MaxNguoiDeNghiLength)]
    public string NguoiDeNghi { get; set; }
    [Required]
    [MaxLength(DenounceConsts.MaxCccdCmndLength)]
    public string CccdCmnd { get; set; }
    /*
    [Required]
    public DateTime NgayCapCccdCmnd { get; set; }
    [Required]
    [MaxLength(DenounceConsts.MaxNoiCapCccdCmnd)]
    */
    public string NoiCapCccdCmnd { get; set; }
    [Required]
    public DateTime NgaySinh { get; set; }
    [Required]
    [Phone]
    [MaxLength(DenounceConsts.MaxSDTLength)]
    public string DienThoai { get; set; }
    [EmailAddress]
    [MaxLength(DenounceConsts.MaxEmailLength)]
    public string Email { get; set; }
    [Required]
    [MaxLength(DenounceConsts.MaxDiaChiLength)]
    public string DiaChiThuongTru { get; set; }
    [Required]
    [MaxLength(DenounceConsts.MaxDiaChiLength)]
    public string DiaChiLienHe { get; set; }
    [Required]
    [MaxLength(DenounceConsts.MaxMaDiaDanhLength)]
    public int maTinhTP { get; set; }
    [Required]
    [MaxLength(DenounceConsts.MaxMaDiaDanhLength)]
    public int maQuanHuyen { get; set; }
    [Required]
    [MaxLength(DenounceConsts.MaxMaDiaDanhLength)]
    public int maXaPhuongTT { get; set; }
    [Required]
    public DateTime ThoiGianTiepNhan { get; set; }
    [Required]
    public DateTime ThoiGianTraKQ { get; set; }
    [Required]
    public string NoiDungVuViec { get; set; }
    [Required]
    [MaxLength(DenounceConsts.MaxBoPhanXLLength)]
    public string boPhanDangXL { get; set; }
    
    [Required]
    [MaxLength(DenounceConsts.MaxSoThuaLength)]
    public string SoThua { get; set; }
    [Required]
    [MaxLength(DenounceConsts.MaxToBanDoLength)]
    public string ToBanDo { get; set; }
    [Required]
    public decimal DienTich { get; set; }
    [Required]
    [MaxLength(DenounceConsts.MaxLoaiDatLength)]
    public string LoaiDat { get; set; }
    [Required]
    [MaxLength(DenounceConsts.MaxDiaChiLength)]
    public string DiaChiThuaDat { get; set; }
    [Required]
    [MaxLength(DenounceConsts.MaxMaDiaDanhLength)]
    public int tinhThuaDat { get; set; }
    [Required]
    [MaxLength(DenounceConsts.MaxMaDiaDanhLength)]
    public int huyenThuaDat { get; set; }
    [Required]
    [MaxLength(DenounceConsts.MaxMaDiaDanhLength)]
    public int xaThuaDat { get; set; }
    [MaxLength(DenounceConsts.MaxToaDoLength)]
    public string DuLieuToaDo { get; set; }
    [MaxLength(DenounceConsts.MaxHinhHocLength)]
    public string DuLieuHinhHoc { get; set; }
    public string GhiChu { get; set; }
    public DateTime? ngayKhieuNai1 { get; set; }
    public DateTime? NgayTraKQ1 { get; set; }
    [MaxLength(DenounceConsts.MaxThamQuyenLength)]
    public string ThamQuyen1 { get; set; }
    [MaxLength(DenounceConsts.MaxSoQDLength)]
    public string SoQD1 { get; set; }
    public LoaiKetQua? KetQua1 { get; set; }
    public DateTime? ngayKhieuNai2 { get; set; }
    public DateTime? NgayTraKQ2 { get; set; }
    [MaxLength(DenounceConsts.MaxThamQuyenLength)]
    public string ThamQuyen2 { get; set; }

    [MaxLength(DenounceConsts.MaxSoQDLength)]
    public string SoQD2 { get; set; }
    public LoaiKetQua? KetQua2 { get; set; }
    public virtual List<CreateAndUpdateFileAttachmentDto> FileAttachments { get; set; }
}
