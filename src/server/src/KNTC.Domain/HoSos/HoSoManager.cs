using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
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
    public async Task<HoSo> CreateAsync([NotNull] HoSo hoSo)
    {
        Check.NotNullOrWhiteSpace(hoSo.MaHoSo, nameof(hoSo.MaHoSo));
        Check.NotNullOrWhiteSpace(hoSo.TieuDe, nameof(hoSo.TieuDe));
        Check.NotNullOrWhiteSpace(hoSo.NguoiDeNghi, nameof(hoSo.NguoiDeNghi));
        Check.NotNullOrWhiteSpace(hoSo.CccdCmnd, nameof(hoSo.CccdCmnd));
        Check.NotNullOrWhiteSpace(hoSo.NoiCapCccdCmnd, nameof(hoSo.NoiCapCccdCmnd));
        Check.NotNull(hoSo.NgayCapCccdCmnd, nameof(hoSo.NgayCapCccdCmnd));
        Check.NotNull(hoSo.NgaySinh, nameof(hoSo.NgaySinh));
        Check.NotNullOrWhiteSpace(hoSo.DienThaoi, nameof(hoSo.DienThaoi));
        Check.NotNullOrWhiteSpace(hoSo.DiaChiThuongTru, nameof(hoSo.DiaChiThuongTru));
        Check.NotNullOrWhiteSpace(hoSo.DiaChiLienHe, nameof(hoSo.DiaChiLienHe));
        Check.NotNullOrWhiteSpace(hoSo.MaTinhTP, nameof(hoSo.MaTinhTP));
        Check.NotNullOrWhiteSpace(hoSo.MaQuanHuyen, nameof(hoSo.MaQuanHuyen));
        Check.NotNullOrWhiteSpace(hoSo.MaXaPhuongTT, nameof(hoSo.MaXaPhuongTT));
        Check.NotNull(hoSo.LoaiVuViec, nameof(hoSo.LoaiVuViec));
        Check.NotNull(hoSo.LinhVuc, nameof(hoSo.LinhVuc));
        Check.NotNullOrWhiteSpace(hoSo.NoiDungVuViec, nameof(hoSo.NoiDungVuViec));
        Check.NotNullOrWhiteSpace(hoSo.SoThua, nameof(hoSo.SoThua));
        Check.NotNullOrWhiteSpace(hoSo.ToBanDo, nameof(hoSo.ToBanDo));
        Check.NotNullOrWhiteSpace(hoSo.DienThaoi, nameof(hoSo.DienTich));
        Check.NotNullOrWhiteSpace(hoSo.LoaiDat, nameof(hoSo.LoaiDat));
        Check.NotNullOrWhiteSpace(hoSo.DiaChiThuaDat, nameof(hoSo.DiaChiThuaDat));
        Check.NotNullOrWhiteSpace(hoSo.TinhThuaDat, nameof(hoSo.TinhThuaDat));
        Check.NotNullOrWhiteSpace(hoSo.HuyenThuaDat, nameof(hoSo.HuyenThuaDat));
        Check.NotNullOrWhiteSpace(hoSo.XaThuaDat, nameof(hoSo.XaThuaDat));

        var existedHoSo = await _hoSoRepo.FindByMaHoSoAsync(hoSo.MaHoSo, false);
        if (existedHoSo != null)
        {
            throw new BusinessException(KNTCDomainErrorCodes.HoSoAlreadyExist).WithData("maHoSo", hoSo.MaHoSo);
        }
        return hoSo;
    }
    public async Task ChangeMaHoSoAsync([NotNull] HoSo hoSo, [NotNull] string maHoSo)
    {
        Check.NotNull(hoSo, nameof(hoSo));
        Check.NotNullOrWhiteSpace(maHoSo, nameof(maHoSo));
        var existedHoSo = await _hoSoRepo.FindByMaHoSoAsync(maHoSo, false);
        if (existedHoSo != null && existedHoSo.Id != hoSo.Id)
        {
            throw new BusinessException(KNTCDomainErrorCodes.HoSoAlreadyExist).WithData("maHoSo", hoSo.MaHoSo);
        }
        hoSo.ChangeMaHoSo(maHoSo);
    }
    public async Task<KQGQHoSo> CreateKQGQHoSoAsync([NotNull] HoSo hoSo,
                                                    [NotNull] short lanGQ,
                                                    [NotNull] DateTime ngayTraKQ,
                                                    [NotNull] string thamQuyen,
                                                    [NotNull] string soQD,
                                                    [NotNull] string ghiChu,
                                                    [NotNull] LoaiKetQua ketQua)
    {
        Check.NotNull(hoSo, nameof(hoSo));
        Check.NotNull(lanGQ, nameof(lanGQ));
        Check.NotNull(ngayTraKQ, nameof(ngayTraKQ));
        Check.NotNullOrWhiteSpace(thamQuyen, nameof(thamQuyen));
        Check.NotNullOrWhiteSpace(soQD, nameof(soQD));
        Check.NotNullOrWhiteSpace(ghiChu, nameof(ghiChu));
        Check.NotNull(ketQua, nameof(ketQua));
        var existLanGQ = hoSo.KQGQHoSos.FirstOrDefault(x => x.LanGQ == lanGQ);
        if (existLanGQ != null) {
            throw new BusinessException(KNTCDomainErrorCodes.LanGQAlreadyExist);
        }
        return new KQGQHoSo(GuidGenerator.Create())
        {
            IdHoSo = hoSo.Id,
            LanGQ = lanGQ,
            NgayTraKQ = ngayTraKQ,
            ThamQuyen = thamQuyen,
            SoQD = soQD,
            GhiChu = ghiChu,
            KetQua = ketQua
        };
    }
    public async Task UpdateeKQGQHoSoAsync([NotNull] HoSo hoSo,
                                                 [NotNull] KQGQHoSo kqgqHoSo,
                                                 [NotNull] short lanGQ,
                                                 [NotNull] DateTime ngayTraKQ,
                                                 [NotNull] string thamQuyen,
                                                 [NotNull] string soQD,
                                                 [NotNull] string ghiChu,
                                                 [NotNull] LoaiKetQua ketQua)
    {
        Check.NotNull(hoSo, nameof(hoSo));
        Check.NotNull(kqgqHoSo, nameof(kqgqHoSo));
        Check.NotNull(lanGQ, nameof(lanGQ));
        Check.NotNull(ngayTraKQ, nameof(ngayTraKQ));
        Check.NotNullOrWhiteSpace(thamQuyen, nameof(thamQuyen));
        Check.NotNullOrWhiteSpace(soQD, nameof(soQD));
        Check.NotNullOrWhiteSpace(ghiChu, nameof(ghiChu));
        Check.NotNull(ketQua, nameof(ketQua));
        var kqgq = hoSo.KQGQHoSos.FirstOrDefault(x => x.Id == kqgqHoSo.Id);
        if (kqgq == null)
        {
            throw new BusinessException(KNTCDomainErrorCodes.KQGQHóNotExist);
        }
        var existLanGQ = hoSo.KQGQHoSos.FirstOrDefault(x => x.LanGQ == lanGQ);
        if (existLanGQ != null)
        {
            throw new BusinessException(KNTCDomainErrorCodes.LanGQAlreadyExist);
        }
        kqgqHoSo.LanGQ = lanGQ;
        kqgqHoSo.NgayTraKQ = ngayTraKQ;
        kqgqHoSo.ThamQuyen = thamQuyen;
        kqgqHoSo.SoQD = soQD;
        kqgqHoSo.GhiChu = ghiChu;
        kqgqHoSo.KetQua = ketQua;
    }
    public async Task<TepDinhKemHoSo> CreateTepDinhKemHoSoAsync([NotNull] HoSo hoSo,
                                                                [NotNull] string tenTaiLieu,
                                                                [NotNull] string hinhThuc,
                                                                DateTime thoiGianBanHanh,
                                                                DateTime ngayNhan,
                                                                [NotNull] string thuTuButLuc,
                                                                [NotNull] string noiDungChinh,
                                                                [NotNull] string fileName,
                                                                [NotNull] string contentType,
                                                                [NotNull] long contentLength)
    {
        Check.NotNull(hoSo, nameof(hoSo));
        Check.NotNullOrWhiteSpace(tenTaiLieu, nameof(tenTaiLieu));
        Check.NotNullOrWhiteSpace(hinhThuc, nameof(hinhThuc));
        Check.NotNullOrWhiteSpace(thuTuButLuc, nameof(thuTuButLuc));
        Check.NotNullOrWhiteSpace(noiDungChinh, nameof(noiDungChinh));
        Check.NotNullOrWhiteSpace(fileName, nameof(fileName));
        Check.NotNullOrWhiteSpace(contentType, nameof(contentType));
        Check.NotNull(contentLength, nameof(contentLength));
        var existTepDinhKem = hoSo.TepDinhKemHoSos.FirstOrDefault(x => x.TenTaiLieu == tenTaiLieu);
        if (existTepDinhKem != null)
        {
            throw new BusinessException(KNTCDomainErrorCodes.TepDinhKemAlreadyExist)
                .WithData("maHoSo", hoSo.MaHoSo)
                .WithData("maHoSo", hoSo.MaHoSo);
        }
        return new TepDinhKemHoSo(GuidGenerator.Create(), tenTaiLieu)
        {
            IdHoSo = hoSo.Id,
            HinhThuc = hinhThuc,
            ThoiGianBanHanh = thoiGianBanHanh,
            NgayNhan = ngayNhan,
            ThuTuButLuc = thuTuButLuc,
            NoiDungChinh= noiDungChinh,
            FileName = fileName,
            ContentType = contentType,
            ContentLength = contentLength
        };
    }
}
