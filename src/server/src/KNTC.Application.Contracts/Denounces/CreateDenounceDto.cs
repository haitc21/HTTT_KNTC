﻿using KNTC.Denounces;
using KNTC.FileAttachments;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace KNTC.Denounces;

public class CreateDenounceDto
{
    [Required]
    [MaxLength(KNTCValidatorConsts.MaxMaHoSoLength)]
    public string MaHoSo { get; set; }
    [Required]
    public LoaiVuViec LoaiVuViec { get; set; }
    [Required]
    [MaxLength(KNTCValidatorConsts.MaxTieuDeLength)]
    public string TieuDe { get; set; }
    [Required]
    [MaxLength(KNTCValidatorConsts.MaxNguoiDeNghiLength)]
    public string NguoiDeNghi { get; set; }
    [Required]
    [MaxLength(KNTCValidatorConsts.MaxCccdCmndLength)]
    public string CccdCmnd { get; set; }
    /*
    [Required]
    public DateTime NgayCapCccdCmnd { get; set; }
    [Required]
    [MaxLength(KNTCValidatorConsts.MaxNoiCapCccdCmnd)]
    */
    public string NoiCapCccdCmnd { get; set; }
    [Required]
    public DateTime NgaySinh { get; set; }
    [Required]
    [Phone]
    [MaxLength(KNTCValidatorConsts.MaxSDTLength)]
    public string DienThoai { get; set; }
    [EmailAddress]
    [MaxLength(KNTCValidatorConsts.MaxEmailLength)]
    public string Email { get; set; }
    [Required]
    [MaxLength(KNTCValidatorConsts.MaxDiaChiLength)]
    public string DiaChiThuongTru { get; set; }
    [Required]
    [MaxLength(KNTCValidatorConsts.MaxDiaChiLength)]
    public string DiaChiLienHe { get; set; }
    [Required]
    [MaxLength(KNTCValidatorConsts.MaxMaDiaDanhLength)]
    public int maTinhTP { get; set; }
    [Required]
    [MaxLength(KNTCValidatorConsts.MaxMaDiaDanhLength)]
    public int maQuanHuyen { get; set; }
    [Required]
    [MaxLength(KNTCValidatorConsts.MaxMaDiaDanhLength)]
    public int maXaPhuongTT { get; set; }
    [Required]
    public DateTime ThoiGianTiepNhan { get; set; }
    [Required]
    public DateTime ThoiGianTraKQ { get; set; }
    [Required]
    public string NoiDungVuViec { get; set; }
    [Required]
    [MaxLength(KNTCValidatorConsts.MaxBoPhanXLLength)]
    public string boPhanDangXL { get; set; }
    
    [Required]
    [MaxLength(KNTCValidatorConsts.MaxSoThuaLength)]
    public string SoThua { get; set; }
    [Required]
    [MaxLength(KNTCValidatorConsts.MaxToBanDoLength)]
    public string ToBanDo { get; set; }
    [Required]
    public decimal DienTich { get; set; }
    [Required]
    public int LoaiDat { get; set; }
    [Required]
    [MaxLength(KNTCValidatorConsts.MaxDiaChiLength)]
    public string DiaChiThuaDat { get; set; }
    [Required]
    [MaxLength(KNTCValidatorConsts.MaxMaDiaDanhLength)]
    public int tinhThuaDat { get; set; }
    [Required]
    [MaxLength(KNTCValidatorConsts.MaxMaDiaDanhLength)]
    public int huyenThuaDat { get; set; }
    [Required]
    [MaxLength(KNTCValidatorConsts.MaxMaDiaDanhLength)]
    public int xaThuaDat { get; set; }
    [MaxLength(KNTCValidatorConsts.MaxToaDoLength)]
    public string DuLieuToaDo { get; set; }
    [MaxLength(KNTCValidatorConsts.MaxHinhHocLength)]
    public string DuLieuHinhHoc { get; set; }
    public string GhiChu { get; set; }
    public DateTime? ngayKhieuNai1 { get; set; }
    public DateTime? NgayTraKQ1 { get; set; }
    [MaxLength(KNTCValidatorConsts.MaxThamQuyenLength)]
    public string ThamQuyen1 { get; set; }
    [MaxLength(KNTCValidatorConsts.MaxSoQDLength)]
    public string SoQD1 { get; set; }
    public LoaiKetQua? KetQua1 { get; set; }
    public DateTime? ngayKhieuNai2 { get; set; }
    public DateTime? NgayTraKQ2 { get; set; }
    [MaxLength(KNTCValidatorConsts.MaxThamQuyenLength)]
    public string ThamQuyen2 { get; set; }

    [MaxLength(KNTCValidatorConsts.MaxSoQDLength)]
    public string SoQD2 { get; set; }
    public LoaiKetQua? KetQua2 { get; set; }
    public virtual List<CreateAndUpdateFileAttachmentDto> FileAttachments { get; set; }
}
