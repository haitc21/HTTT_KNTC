using KNTC.FileAttachments;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace KNTC.Complains;

public class UpdateComplainDto : EntityDto<Guid>, IHasConcurrencyStamp
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
    [MaxLength(KNTCValidatorConsts.MaxNguoiDeNghiLength)]
    public string NguoiDeNghi { get; set; }
    [Required]
    [MaxLength(KNTCValidatorConsts.MaxCccdCmndLength)]
    public string CccdCmnd { get; set; }

    /*
     * [Required]
    public DateTime NgayCapCccdCmnd { get; set; }
    [Required]
    [MaxLength(KNTCValidatorConsts.MaxNoiCapCccdCmnd)]
    public string NoiCapCccdCmnd { get; set; }
    */
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
    [Required]
    public int maXaPhuongTT { get; set; }
    [Required]
    public DateTime ThoiGianTiepNhan { get; set; }
    [Required]
    public DateTime ThoiGianHenTraKQ { get; set; }
    [Required]
    public string NoiDungVuViec { get; set; }
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
    public int tinhThuaDat { get; set; }
    [Required]
    public int huyenThuaDat { get; set; }
    public int xaThuaDat { get; set; }
    [MaxLength(KNTCValidatorConsts.MaxToaDoLength)]
    public string DuLieuToaDo { get; set; }
    [MaxLength(KNTCValidatorConsts.MaxHinhHocLength)]
    public string DuLieuHinhHoc { get; set; }
    public string GhiChu { get; set; }
    public LoaiKhieuNai? loaiKhieuNai1 { get; set; }
    public DateTime? NgayKhieuNai1 { get; set; }
    public DateTime? NgayTraKQ1 { get; set; }
    [MaxLength(KNTCValidatorConsts.MaxThamQuyenLength)]
    public string ThamQuyen1 { get; set; }
    [MaxLength(KNTCValidatorConsts.MaxSoQDLength)]
    public string SoQD1 { get; set; }
    public LoaiKetQua? KetQua1 { get; set; }
    public LoaiKhieuNai? loaiKhieuNai2 { get; set; }
    public DateTime? NgayKhieuNai2 { get; set; }
    public DateTime? NgayTraKQ2 { get; set; }
    [MaxLength(KNTCValidatorConsts.MaxThamQuyenLength)]
    public string ThamQuyen2 { get; set; }

    [MaxLength(KNTCValidatorConsts.MaxSoQDLength)]
    public string SoQD2 { get; set; }
    public LoaiKetQua? KetQua2 { get; set; }
    public string ConcurrencyStamp { get; set; }
}
