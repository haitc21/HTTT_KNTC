using System;
using Volo.Abp.Application.Dtos;

namespace KNTC.FileAttachments;

public class FileAttachmentDto : AuditedEntityDto<Guid>
{
    public Guid? ComplainId { get; set; }
    public Guid? DenounceId { get; set; }
    public string TenTaiLieu { get; set; }
    public int GiaiDoan { get; set; }
    public int HinhThuc { get; set; }
    public DateTime ThoiGianBanHanh { get; set; }
    public DateTime NgayNhan { get; set; }
    public string ThuTuButLuc { get; set; }
    public string NoiDungChinh { get; set; }
    public string? FileName { get; set; }
    public string? ContentType { get; set; }
    public long? ContentLength { get; set; }
    public bool CongKhai { get; set; }
    public bool ChoPhepDownload { get; set; }
}