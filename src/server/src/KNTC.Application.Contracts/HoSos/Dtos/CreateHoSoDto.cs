﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace KNTC.HoSos;

public class CreateHoSoDto
{
    [Required]
    [MaxLength(HoSoConsts.MaxCodeLength)]
    public string MaHoSo { get; set; }
    [Required]
    [MaxLength(HoSoConsts.MaxTieuDeLength)]
    public string TieuDe { get; set; }
    [Required]
    [MaxLength(HoSoConsts.MaxNguoiDeNghiLength)]
    public string NguoiDeNghi { get; set; }
    [Required]
    [MaxLength(HoSoConsts.MaxCccdCmndLength)]
    public string CccdCmnd { get; set; }
    [Required]
    public DateTime NgayCapCccdCmnd { get; set; }
    [Required]
    [MaxLength(HoSoConsts.MaxNoiCapCccdCmnd)]
    public string NoiCapCccdCmnd { get; set; }
    [Required]
    public DateTime NgaySinh { get; set; }
    [Required]
    [Phone]
    [MaxLength(HoSoConsts.MaxSDTLength)]
    public string DienThaoi { get; set; }
    [EmailAddress]
    [MaxLength(HoSoConsts.MaxEmailLength)]
    public string Email { get; set; }
    [Required]
    [MaxLength(HoSoConsts.MaxDiaChiLength)]
    public string DiaChiThuongTru { get; set; }
    [Required]
    [MaxLength(HoSoConsts.MaxDiaChiLength)]
    public string DiaChiLienHe { get; set; }
    [Required]
    [MaxLength(HoSoConsts.MaxMaDiaDanhLength)]
    public string MaTinhTP { get; set; }
    [Required]
    [MaxLength(HoSoConsts.MaxMaDiaDanhLength)]
    public string MaQuanHuyen { get; set; }
    [Required]
    [MaxLength(HoSoConsts.MaxMaDiaDanhLength)]
    public string MaXaPhuongTT { get; set; }
    [Required]
    public LoaiVuViec LoaiVuViec { get; set; }
    [Required]
    public LinhVuc LinhVuc { get; set; }
    public DateTime NgayTiepNhan { get; set; }
    public DateTime NgayHenTraKQ { get; set; }
    [Required]
    public string NoiDungVuViec { get; set; }
    [Required]
    [MaxLength(HoSoConsts.MaxSoThuaLength)]
    public string SoThua { get; set; }
    [Required]
    [MaxLength(HoSoConsts.MaxToBanDoLength)]
    public string ToBanDo { get; set; }
    [Required]
    [MaxLength(HoSoConsts.MaxDienTichLength)]
    public string DienTich { get; set; }
    [Required]
    [MaxLength(HoSoConsts.MaxLoaiDatLength)]
    public string LoaiDat { get; set; }
    [Required]
    [MaxLength(HoSoConsts.MaxDiaChiLength)]
    public string DiaChiThuaDat { get; set; }
    [Required]
    [MaxLength(HoSoConsts.MaxMaDiaDanhLength)]
    public string TinhThuaDat { get; set; }
    [Required]
    [MaxLength(HoSoConsts.MaxMaDiaDanhLength)]
    public string HuyenThuaDat { get; set; }
    [Required]
    [MaxLength(HoSoConsts.MaxMaDiaDanhLength)]
    public string XaThuaDat { get; set; }
    [MaxLength(HoSoConsts.MaxToaDoLength)]
    public string DuLieuToaDo { get; set; }
    [MaxLength(HoSoConsts.MaxHinhHocLength)]
    public string DuLieuHinhHoc { get; set; }
    public IReadOnlyList<CreateAndUpdateKQGQHoSoDto> KQGQHoSos { get; set; }
    public IReadOnlyList<CreateAndUpdateTepDinhKemHoSoDto> TepDinhKemHoSos { get; set; }
}
