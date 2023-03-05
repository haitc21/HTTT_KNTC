using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace KNTC.FileAttachments;

public class FileAttachmentDto : AuditedEntityDto<Guid>
{
    public Guid IdHoSo { get; set; }
    public string TenTaiLieu { get; private set; }
    public string HinhThuc { get; set; }
    public DateTime ThoiGianBanHanh { get; set; }
    public DateTime NgayNhan { get; set; }
    public string ThuTuButLuc { get; set; }
    public string NoiDungChinh { get; set; }
    public string FileName { get; set; }
}
