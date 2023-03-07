using KNTC.Complains;
using KNTC.Denounces;
using KNTC.DocumentTypes;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace KNTC.FileAttachments;

public class FileAttachment : FullAuditedEntity<Guid>
{
    public FileAttachment()
    {

    }
    public FileAttachment(Guid id) : base(id)
    {

    }
    public FileAttachment(Guid id, string tenTaiLieu) : base(id)
    {
        SetTenTaiLieu(tenTaiLieu);
    }
    public Guid IdHoSo { get; set; }
    public int GiaiDoan { get; set; }
    public string TenTaiLieu { get; private set; }
    public int HinhThuc { get; set; }
    public DateTime ThoiGianBanHanh { get; set; }
    public DateTime NgayNhan { get; set; }
    public string ThuTuButLuc { get; set; }
    public string NoiDungChinh { get; set; }
    public string FileName { get; set; }
    public string ContentType { get; set; }
    public long ContentLength { get; set; }
    public Complain Complain { get; set; }
    public Denounce Denounce { get; set; }
    public DocumentType DocumentType { get; set; }
    private void SetTenTaiLieu([NotNull] string tenTaiLieu)
    {
        TenTaiLieu = Check.NotNullOrWhiteSpace(
            tenTaiLieu,
            nameof(tenTaiLieu),
            maxLength: KNTCValidatorConsts.MaxTenTaiLieuLength
        );
    }

    internal FileAttachment ChangeTenTaiLieu([NotNull] string tenTaiLieu)
    {
        SetTenTaiLieu(tenTaiLieu);
        return this;
    }
}
