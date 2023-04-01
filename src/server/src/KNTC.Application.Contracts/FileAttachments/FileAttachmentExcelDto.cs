using System;
using Volo.Abp.Application.Dtos;

namespace KNTC.FileAttachments;

public class FileAttachmentExcelDto
{
    public string TenTaiLieu { get; set; }
    public string HinhThuc { get; set; }
    public DateTime NgayNhan { get; set; }
    public DateTime ThoiGianBanHanh { get; set; }
    public string ThuTuButLuc { get; set; }
    public string NoiDungChinh { get; set; }
}
