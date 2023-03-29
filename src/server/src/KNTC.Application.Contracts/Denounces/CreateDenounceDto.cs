﻿using KNTC.FileAttachments;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KNTC.Denounces;

public class CreateDenounceDto
{
    [Required]
    [MaxLength(KNTCValidatorConsts.MaxMaHoSoLength)]
    public string MaHoSo { get; set; }
    [Required]
    public LinhVuc LinhVuc { get; set; }
    [Required]
    [MaxLength(KNTCValidatorConsts.MaxTieuDeLength)]
    public string TieuDe { get; set; }
    [Required]
    [MaxLength(KNTCValidatorConsts.MaxTenNguoiLength)]
    public string NguoiToCao { get; set; }
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
    public int maTinhTP { get; set; }
    [Required]
    public int maQuanHuyen { get; set; }
    public int maXaPhuongTT { get; set; }
    [Required]
    public DateTime ThoiGianTiepNhan { get; set; }
    [Required]
    public DateTime ThoiGianTraKQ { get; set; }
    [Required]
    public string NoiDungVuViec { get; set; }
    [Required]
    [MaxLength(KNTCValidatorConsts.MaxTenNguoiLength)]
    public string NguoiBiToCao { get; set; }
    [Required]
    [MaxLength(KNTCValidatorConsts.MaxBoPhanXLLength)]
    public string BoPhanDangXL { get; set; }

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
    public int TinhThuaDat { get; set; }
    [Required]
    public int HuyenThuaDat { get; set; }
    [Required]
    public int XaThuaDat { get; set; }
    [MaxLength(KNTCValidatorConsts.MaxToaDoLength)]
    public string DuLieuToaDo { get; set; }
    [MaxLength(KNTCValidatorConsts.MaxHinhHocLength)]
    public string DuLieuHinhHoc { get; set; }
    public string GhiChu { get; set; }
    [Required]
    public DateTime NgayGQTC { get; set; }
    [Required]
    [MaxLength(KNTCValidatorConsts.MaxTenNguoiLength)]
    public string NguoiGQTC { get; set; }
    [Required]
    [MaxLength(KNTCValidatorConsts.MaxSoQDLength)]
    public string QuyerDinhThuLyGQTC { get; set; }
    [Required]
    public DateTime NgayQDGQTC { get; set; }
    [MaxLength(KNTCValidatorConsts.MaxSoQDLength)]
    public string QuyetDinhDinhChiGQTC { get; set; }
    public DateTime? GiaHanGQTC1 { get; set; }
    public DateTime? GiaHanGQTC2 { get; set; }
    [Required]
    [MaxLength(KNTCValidatorConsts.MaxSoQDLength)]
    public string SoVBKLNDTC { get; set; }
    [Required]
    public DateTime NgayNhanTBKQXLKLTC { get; set; }
    [Required]
    public LoaiKetQua KetQua { get; set; }
    public bool CongKhaiKLGQTC { get; set; }
    public virtual List<CreateAndUpdateFileAttachmentDto> FileAttachments { get; set; }
}
