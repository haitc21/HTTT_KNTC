using KNTC.Complain;
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

namespace KNTC.Complain;

public class TepDinhKemHoSo : FullAuditedEntity<Guid>
{
    public TepDinhKemHoSo()
    {

    }
    public TepDinhKemHoSo(Guid id) : base(id)
    {

    }
    public TepDinhKemHoSo(Guid id, string tenTaiLieu) : base(id)
    {
        SetTenTaiLieu(tenTaiLieu);
    }
    public Guid IdHoSo { get; set; }
    public HoSo HoSo { get; set; }
    public string TenTaiLieu { get; private set; }
    public string HinhThuc { get; set; }
    public DateTime ThoiGianBanHanh { get; set; }
    public DateTime NgayNhan { get; set; }
    public string ThuTuButLuc { get; set; }
    public string NoiDungChinh { get; set; }
    public string FileName { get; set; }
    public string ContentType { get; set; }
    public long ContentLength { get; set; }
    private void SetTenTaiLieu([NotNull] string tenTaiLieu)
    {
        TenTaiLieu = Check.NotNullOrWhiteSpace(
            tenTaiLieu,
            nameof(tenTaiLieu),
            maxLength: HoSoConsts.MaxTenTaiLieuLength
        );
    }

    internal TepDinhKemHoSo ChangeTenTaiLieu([NotNull] string tenTaiLieu)
    {
        SetTenTaiLieu(tenTaiLieu);
        return this;
    }
}
