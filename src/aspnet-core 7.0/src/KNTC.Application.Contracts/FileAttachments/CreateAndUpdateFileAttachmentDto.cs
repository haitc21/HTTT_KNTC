using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace KNTC.FileAttachments;

public class CreateAndUpdateFileAttachmentDto : IHasConcurrencyStamp
{
    public Guid? Id { get; set; }
    public LoaiVuViec LoaiVuViec { get; set; }
    public Guid IdHoSo { get; set; }

    [Required]
    public string TenTaiLieu { get; set; }

    [Required]
    public int GiaiDoan { get; set; }

    [Required]
    public int HinhThuc { get; set; }

    [Required]
    public DateTime ThoiGianBanHanh { get; set; }

    [Required]
    public DateTime NgayNhan { get; set; }

    [Required]
    [MaxLength(KNTCValidatorConsts.MaxThuTuButLucLength)]
    public string ThuTuButLuc { get; set; }

    [Required]
    public string NoiDungChinh { get; set; }

    [Required]
    public string FileName { get; set; }

    public string ContentType { get; set; }
    public long ContentLength { get; set; }
    public string? ConcurrencyStamp { get; set; }

    [Required]
    public bool CongKhai { get; set; }
}