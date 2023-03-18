using System;
using Volo.Abp.Application.Dtos;

namespace KNTC.FileAttachments;

public class FileAttachmentDto : AuditedEntityDto<Guid>
{
    public string TenTaiLieu { get; private set; }
    public short GiaiDoan { get; set; }
    public int HinhThuc { get; set; }
    public DateTime ThoiGianBanHanh { get; set; }
    public DateTime NgayNhan { get; set; }
    public string ThuTuButLuc { get; set; }
    public string NoiDungChinh { get; set; }
    public string FileName { get; set; }
}
