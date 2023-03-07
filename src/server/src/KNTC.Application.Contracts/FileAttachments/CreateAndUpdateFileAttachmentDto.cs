using KNTC.FileAttachments;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;
using static KNTC.Permissions.KNTCPermissions;

namespace KNTC.FileAttachments;

public class CreateAndUpdateFileAttachmentDto : EntityDto<Guid>
{
    [Required]
    [MaxLength(KNTCValidatorConsts.MaxTenTaiLieuLength)]
    public int GiaiDoan { get; set; }
    public string TenTaiLieu { get; set; }
    [Required]
    [MaxLength(KNTCValidatorConsts.MaxHinhThucLength)]
    public int HinhThuc { get; set; }
    public DateTime ThoiGianBanHanh { get; set; }
    public DateTime NgayNhan { get; set; }
    [Required]
    [MaxLength(KNTCValidatorConsts.MaxThuTuButLucLength)]
    public string ThuTuButLuc { get; set; }
    public string NoiDungChinh { get; set; }
    public IFormFile FileContent { get; set; }
}
