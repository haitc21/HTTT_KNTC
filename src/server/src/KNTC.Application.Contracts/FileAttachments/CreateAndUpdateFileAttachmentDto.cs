using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace KNTC.FileAttachments;

public class CreateAndUpdateFileAttachmentDto : EntityDto<Guid>
{
    public string TenTaiLieu { get; set; }
    [Required]
    public int GiaiDoan { get; set; }
    [Required]
    public int HinhThuc { get; set; }
    public DateTime ThoiGianBanHanh { get; set; }
    public DateTime NgayNhan { get; set; }
    [Required]
    [MaxLength(KNTCValidatorConsts.MaxThuTuButLucLength)]
    public string ThuTuButLuc { get; set; }
    public string NoiDungChinh { get; set; }
    public string FileName { get; set; }
    public string ContentType { get; set; }
    public long ContentLength { get; set; }
    public IFormFile FileContent { get; set; }
}
