﻿using KNTC.Complains;
using KNTC.FileAttachments;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace KNTC.Complains;

public class CreateComplainDto
{
    [Required]
    [MaxLength(ComplainConsts.MaxMaHoSoLength)]
    public string MaHoSo { get; set; }
    [Required]
    public LoaiVuViec LoaiVuViec { get; set; }
    [Required]
    [MaxLength(ComplainConsts.MaxTieuDeLength)]
    public string TieuDe { get; set; }
    [Required]
    [MaxLength(ComplainConsts.MaxNguoiDeNghiLength)]
    public string NguoiDeNghi { get; set; }
    [Required]
    [MaxLength(ComplainConsts.MaxCccdCmndLength)]
    public string CccdCmnd { get; set; }
    [Required]
    public DateTime NgayCapCccdCmnd { get; set; }
    [Required]
    [MaxLength(ComplainConsts.MaxNoiCapCccdCmnd)]
    public string NoiCapCccdCmnd { get; set; }
    [Required]
    public DateTime NgaySinh { get; set; }
    [Required]
    [Phone]
    [MaxLength(ComplainConsts.MaxSDTLength)]
    public string DienThaoi { get; set; }
    [EmailAddress]
    [MaxLength(ComplainConsts.MaxEmailLength)]
    public string Email { get; set; }
    [Required]
    [MaxLength(ComplainConsts.MaxDiaChiLength)]
    public string DiaChiThuongTru { get; set; }
    [Required]
    [MaxLength(ComplainConsts.MaxDiaChiLength)]
    public string DiaChiLienHe { get; set; }
    [Required]
    [MaxLength(ComplainConsts.MaxMaDiaDanhLength)]
    public string MaTinhTP { get; set; }
    [Required]
    [MaxLength(ComplainConsts.MaxMaDiaDanhLength)]
    public string MaQuanHuyen { get; set; }
    [Required]
    [MaxLength(ComplainConsts.MaxMaDiaDanhLength)]
    public string MaXaPhuongTT { get; set; }
    [Required]
    public DateTime NgayTiepNhan { get; set; }
    [Required]
    public DateTime NgayHenTraKQ { get; set; }
    [Required]
    public string NoiDungVuViec { get; set; }
    [Required]
    [MaxLength(ComplainConsts.MaxSoThuaLength)]
    public string SoThua { get; set; }
    [Required]
    [MaxLength(ComplainConsts.MaxToBanDoLength)]
    public string ToBanDo { get; set; }
    [Required]
    [MaxLength(ComplainConsts.MaxDienTichLength)]
    public string DienTich { get; set; }
    [Required]
    [MaxLength(ComplainConsts.MaxLoaiDatLength)]
    public string LoaiDat { get; set; }
    [Required]
    [MaxLength(ComplainConsts.MaxDiaChiLength)]
    public string DiaChiThuaDat { get; set; }
    [Required]
    [MaxLength(ComplainConsts.MaxMaDiaDanhLength)]
    public string TinhThuaDat { get; set; }
    [Required]
    [MaxLength(ComplainConsts.MaxMaDiaDanhLength)]
    public string HuyenThuaDat { get; set; }
    [Required]
    [MaxLength(ComplainConsts.MaxMaDiaDanhLength)]
    public string XaThuaDat { get; set; }
    [MaxLength(ComplainConsts.MaxToaDoLength)]
    public string DuLieuToaDo { get; set; }
    [MaxLength(ComplainConsts.MaxHinhHocLength)]
    public string DuLieuHinhHoc { get; set; }
    public string GhiChu { get; set; }
    public DateTime? ngayKhieuNai1 { get; set; }
    public DateTime? NgayTraKQ1 { get; set; }
    [MaxLength(ComplainConsts.MaxThamQuyenLength)]
    public string ThamQuyen1 { get; set; }
    [MaxLength(ComplainConsts.MaxSoQDLength)]
    public string SoQD1 { get; set; }
    public LoaiKetQua? KetQua1 { get; set; }
    public DateTime? ngayKhieuNai2 { get; set; }
    public DateTime? NgayTraKQ2 { get; set; }
    [MaxLength(ComplainConsts.MaxThamQuyenLength)]
    public string ThamQuyen2 { get; set; }

    [MaxLength(ComplainConsts.MaxSoQDLength)]
    public string SoQD2 { get; set; }
    public LoaiKetQua? KetQua2 { get; set; }
    public virtual List<CreateAndUpdateFileAttachmentDto> FileAttachments { get; set; }
}