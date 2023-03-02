using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace KNTC.HoSos;

public class HoSoManager : DomainService
{
    private readonly IHoSoRepository _hoSoRepo;
    public HoSoManager(IHoSoRepository hoSoRepo)
    {
        _hoSoRepo = hoSoRepo;
    }
    public async Task<HoSo> CreateAsync([NotNull] HoSo input)
    {
        Check.NotNullOrWhiteSpace(input.MaHoSo, nameof(input.MaHoSo));
        Check.NotNullOrWhiteSpace(input.TieuDe, nameof(input.TieuDe));
        Check.NotNullOrWhiteSpace(input.NguoiDeNghi, nameof(input.NguoiDeNghi));
        Check.NotNullOrWhiteSpace(input.CccdCmnd, nameof(input.CccdCmnd));
        Check.NotNullOrWhiteSpace(input.NoiCapCccdCmnd, nameof(input.NoiCapCccdCmnd));
        Check.NotNull(input.NgayCapCccdCmnd, nameof(input.NgayCapCccdCmnd));
        Check.NotNull(input.NgaySinh, nameof(input.NgaySinh));
        Check.NotNullOrWhiteSpace(input.DienThaoi, nameof(input.DienThaoi));
        Check.NotNullOrWhiteSpace(input.DiaChiThuongTru, nameof(input.DiaChiThuongTru));
        Check.NotNullOrWhiteSpace(input.DiaChiLienHe, nameof(input.DiaChiLienHe));
        Check.NotNullOrWhiteSpace(input.MaTinhTP, nameof(input.MaTinhTP));
        Check.NotNullOrWhiteSpace(input.MaQuanHuyen, nameof(input.MaQuanHuyen));
        Check.NotNullOrWhiteSpace(input.MaXaPhuongTT, nameof(input.MaXaPhuongTT));
        Check.NotNull(input.LoaiVuViec, nameof(input.LoaiVuViec));
        Check.NotNull(input.LinhVuc, nameof(input.LinhVuc));
        Check.NotNullOrWhiteSpace(input.NoiDungVuViec, nameof(input.NoiDungVuViec));
        Check.NotNullOrWhiteSpace(input.SoThua, nameof(input.SoThua));
        Check.NotNullOrWhiteSpace(input.ToBanDo, nameof(input.ToBanDo));
        Check.NotNullOrWhiteSpace(input.DienThaoi, nameof(input.DienTich));
        Check.NotNullOrWhiteSpace(input.LoaiDat, nameof(input.LoaiDat));
        Check.NotNullOrWhiteSpace(input.DiaChiThuaDat, nameof(input.DiaChiThuaDat));
        Check.NotNullOrWhiteSpace(input.TinhThuaDat, nameof(input.TinhThuaDat));
        Check.NotNullOrWhiteSpace(input.HuyenThuaDat, nameof(input.HuyenThuaDat));
        Check.NotNullOrWhiteSpace(input.XaThuaDat, nameof(input.XaThuaDat));

        return new HoSo();
    }
}
