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
    public async Task<HoSo> CreateAsync([NotNull] string maHoSo,
                                        [NotNull] string yieuDe,
                                        [NotNull] string nguoiDeNghi,
                                        [NotNull] string cccdCmnd,
                                        [NotNull] DateTime ngayCapCccdCmnd,
                                        [NotNull] string noiCapCccdCmnd,
                                        [NotNull] DateTime ngaySinh,
                                        [NotNull] string dienThaoi,
                                        string email,
                                        [NotNull] string diaChiThuongTru,
                                        [NotNull] string diaChiLienHe,
                                        [NotNull] string maTinhTP,
                                        [NotNull] string maQuanHuyen,
                                        [NotNull] string maXaPhuongTT,
                                        [NotNull] LoaiVuViec loaiVuViec,
                                        [NotNull] LinhVuc linhVuc,
                                        [NotNull] DateTime ngayTiepNhan,
                                        [NotNull] DateTime ngayHenTraKQ,
                                        [NotNull] string noiDungVuViec,
                                        [NotNull] string soThua,
                                        [NotNull] string toBanDo,
                                        [NotNull] string dienTich,
                                        [NotNull] string loaiDat,
                                        [NotNull] string diaChiThuaDat,
                                        [NotNull] string tinhThuaDat,
                                        [NotNull] string huyenThuaDat,
                                        [NotNull] string xaThuaDat,
                                        string duLieuToaDo,
                                        string duLieuHinhHoc
        )
    {
        Check.NotNullOrWhiteSpace(maHoSo, nameof(maHoSo));
        Check.NotNullOrWhiteSpace(yieuDe, nameof(yieuDe));
        Check.NotNullOrWhiteSpace(nguoiDeNghi, nameof(nguoiDeNghi));
        Check.NotNullOrWhiteSpace(cccdCmnd, nameof(cccdCmnd));
        Check.NotNullOrWhiteSpace(noiCapCccdCmnd, nameof(noiCapCccdCmnd));
        Check.NotNull(ngayCapCccdCmnd, nameof(ngayCapCccdCmnd));
        Check.NotNull(ngaySinh, nameof(ngaySinh));
        Check.NotNullOrWhiteSpace(dienThaoi, nameof(dienThaoi));
        Check.NotNullOrWhiteSpace(diaChiThuongTru, nameof(diaChiThuongTru));
        Check.NotNullOrWhiteSpace(diaChiLienHe, nameof(diaChiLienHe));
        Check.NotNullOrWhiteSpace(maTinhTP, nameof(maTinhTP));
        Check.NotNullOrWhiteSpace(maQuanHuyen, nameof(maQuanHuyen));
        Check.NotNullOrWhiteSpace(maXaPhuongTT, nameof(maXaPhuongTT));
        Check.NotNull(loaiVuViec, nameof(loaiVuViec));
        Check.NotNull(linhVuc, nameof(linhVuc));
        Check.NotNullOrWhiteSpace(noiDungVuViec, nameof(noiDungVuViec));
        Check.NotNullOrWhiteSpace(soThua, nameof(soThua));
        Check.NotNullOrWhiteSpace(toBanDo, nameof(toBanDo));
        Check.NotNullOrWhiteSpace(dienThaoi, nameof(dienTich));
        Check.NotNullOrWhiteSpace(loaiDat, nameof(loaiDat));
        Check.NotNullOrWhiteSpace(diaChiThuaDat, nameof(diaChiThuaDat));
        Check.NotNullOrWhiteSpace(tinhThuaDat, nameof(tinhThuaDat));
        Check.NotNullOrWhiteSpace(huyenThuaDat, nameof(huyenThuaDat));
        Check.NotNullOrWhiteSpace(xaThuaDat, nameof(xaThuaDat));

        var existedHoSo = await _hoSoRepo.FindByMaHoSoAsync(maHoSo, false);
        if (existedHoSo != null)
        {
            throw new BusinessException(KNTCDomainErrorCodes.HoSoAlreadyExist).WithData("maHoSo", maHoSo);
        }
        return new HoSo(GuidGenerator.Create(), maHoSo)
        {
            TieuDe = yieuDe,
            NguoiDeNghi = nguoiDeNghi,
            CccdCmnd = cccdCmnd,
            NgayCapCccdCmnd = ngayCapCccdCmnd,
            NoiCapCccdCmnd = noiCapCccdCmnd,
            NgaySinh = ngaySinh,
            DienThaoi = dienThaoi,
            Email = email,
            DiaChiThuongTru = diaChiThuongTru,
            DiaChiLienHe = diaChiLienHe,
            MaTinhTP = maTinhTP,
            MaQuanHuyen = maQuanHuyen,
            MaXaPhuongTT = maXaPhuongTT,
            LoaiVuViec = loaiVuViec,
            LinhVuc = linhVuc,
            NgayTiepNhan = ngayTiepNhan,
            NgayHenTraKQ = ngayHenTraKQ,
            NoiDungVuViec = noiDungVuViec,
            SoThua = soThua,
            ToBanDo = toBanDo,
            DienTich = dienTich,
            LoaiDat = loaiDat,
            DiaChiThuaDat = diaChiThuaDat,
            TinhThuaDat = tinhThuaDat,
            HuyenThuaDat = huyenThuaDat,
            XaThuaDat = xaThuaDat,
            DuLieuToaDo = duLieuToaDo,
            DuLieuHinhHoc = duLieuHinhHoc
        };
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
    public async Task UpdateAsync([NotNull] HoSo hoSo,
                                  [NotNull] string maHoSo,
                                  [NotNull] string yieuDe,
                                  [NotNull] string nguoiDeNghi,
                                  [NotNull] string cccdCmnd,
                                  [NotNull] DateTime ngayCapCccdCmnd,
                                  [NotNull] string noiCapCccdCmnd,
                                  [NotNull] DateTime ngaySinh,
                                  [NotNull] string dienThaoi,
                                  string email,
                                  [NotNull] string diaChiThuongTru,
                                  [NotNull] string diaChiLienHe,
                                  [NotNull] string maTinhTP,
                                  [NotNull] string maQuanHuyen,
                                  [NotNull] string maXaPhuongTT,
                                  [NotNull] LoaiVuViec loaiVuViec,
                                  [NotNull] LinhVuc linhVuc,
                                  [NotNull] DateTime ngayTiepNhan,
                                  [NotNull] DateTime ngayHenTraKQ,
                                  [NotNull] string noiDungVuViec,
                                  [NotNull] string soThua,
                                  [NotNull] string toBanDo,
                                  [NotNull] string dienTich,
                                  [NotNull] string loaiDat,
                                  [NotNull] string diaChiThuaDat,
                                  [NotNull] string tinhThuaDat,
                                  [NotNull] string huyenThuaDat,
                                  [NotNull] string xaThuaDat,
                                  string duLieuToaDo,
                                  string duLieuHinhHoc
      )
    {
        Check.NotNull(hoSo, nameof(hoSo));
        Check.NotNullOrWhiteSpace(maHoSo, nameof(maHoSo));
        Check.NotNullOrWhiteSpace(yieuDe, nameof(yieuDe));
        Check.NotNullOrWhiteSpace(nguoiDeNghi, nameof(nguoiDeNghi));
        Check.NotNullOrWhiteSpace(cccdCmnd, nameof(cccdCmnd));
        Check.NotNullOrWhiteSpace(noiCapCccdCmnd, nameof(noiCapCccdCmnd));
        Check.NotNull(ngayCapCccdCmnd, nameof(ngayCapCccdCmnd));
        Check.NotNull(ngaySinh, nameof(ngaySinh));
        Check.NotNullOrWhiteSpace(dienThaoi, nameof(dienThaoi));
        Check.NotNullOrWhiteSpace(diaChiThuongTru, nameof(diaChiThuongTru));
        Check.NotNullOrWhiteSpace(diaChiLienHe, nameof(diaChiLienHe));
        Check.NotNullOrWhiteSpace(maTinhTP, nameof(maTinhTP));
        Check.NotNullOrWhiteSpace(maQuanHuyen, nameof(maQuanHuyen));
        Check.NotNullOrWhiteSpace(maXaPhuongTT, nameof(maXaPhuongTT));
        Check.NotNull(loaiVuViec, nameof(loaiVuViec));
        Check.NotNull(linhVuc, nameof(linhVuc));
        Check.NotNullOrWhiteSpace(noiDungVuViec, nameof(noiDungVuViec));
        Check.NotNullOrWhiteSpace(soThua, nameof(soThua));
        Check.NotNullOrWhiteSpace(toBanDo, nameof(toBanDo));
        Check.NotNullOrWhiteSpace(dienThaoi, nameof(dienTich));
        Check.NotNullOrWhiteSpace(loaiDat, nameof(loaiDat));
        Check.NotNullOrWhiteSpace(diaChiThuaDat, nameof(diaChiThuaDat));
        Check.NotNullOrWhiteSpace(tinhThuaDat, nameof(tinhThuaDat));
        Check.NotNullOrWhiteSpace(huyenThuaDat, nameof(huyenThuaDat));
        Check.NotNullOrWhiteSpace(xaThuaDat, nameof(xaThuaDat));

        if (hoSo.MaHoSo != maHoSo)
        {
            await ChangeMaHoSoAsync(hoSo, maHoSo);
        }
        hoSo.TieuDe = yieuDe;
        hoSo.NguoiDeNghi = nguoiDeNghi;
        hoSo.CccdCmnd = cccdCmnd;
        hoSo.NgayCapCccdCmnd = ngayCapCccdCmnd;
        hoSo.NoiCapCccdCmnd = noiCapCccdCmnd;
        hoSo.NgaySinh = ngaySinh;
        hoSo.DienThaoi = dienThaoi;
        hoSo.Email = email;
        hoSo.DiaChiThuongTru = diaChiThuongTru;
        hoSo.DiaChiLienHe = diaChiLienHe;
        hoSo.MaTinhTP = maTinhTP;
        hoSo.MaQuanHuyen = maQuanHuyen;
        hoSo.MaXaPhuongTT = maXaPhuongTT;
        hoSo.LoaiVuViec = loaiVuViec;
        hoSo.LinhVuc = linhVuc;
        hoSo.NgayTiepNhan = ngayTiepNhan;
        hoSo.NgayHenTraKQ = ngayHenTraKQ;
        hoSo.NoiDungVuViec = noiDungVuViec;
        hoSo.SoThua = soThua;
        hoSo.ToBanDo = toBanDo;
        hoSo.DienTich = dienTich;
        hoSo.LoaiDat = loaiDat;
        hoSo.DiaChiThuaDat = diaChiThuaDat;
        hoSo.TinhThuaDat = tinhThuaDat;
        hoSo.HuyenThuaDat = huyenThuaDat;
        hoSo.XaThuaDat = xaThuaDat;
        hoSo.DuLieuToaDo = duLieuToaDo;
        hoSo.DuLieuHinhHoc = duLieuHinhHoc;
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
        if (existLanGQ != null)
        {
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
            throw new BusinessException(KNTCDomainErrorCodes.KQGQHoSoNotExist);
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
            NoiDungChinh = noiDungChinh,
            FileName = fileName,
            ContentType = contentType,
            ContentLength = contentLength
        };
    }
    public async Task UpdateTepDinhKemHoSoAsync([NotNull] HoSo hoSo,
                                                [NotNull] TepDinhKemHoSo tepDinhKem,
                                                [NotNull] string tenTaiLieu,
                                                [NotNull] string hinhThuc,
                                                DateTime thoiGianBanHanh,
                                                DateTime ngayNhan,
                                                [NotNull] string thuTuButLuc,
                                                [NotNull] string noiDungChinh,
                                                string fileName,
                                                string contentType,
                                                long contentLength)
    {
        Check.NotNull(hoSo, nameof(hoSo));
        Check.NotNull(tepDinhKem, nameof(tepDinhKem));
        Check.NotNullOrWhiteSpace(tenTaiLieu, nameof(tenTaiLieu));
        Check.NotNullOrWhiteSpace(hinhThuc, nameof(hinhThuc));
        Check.NotNullOrWhiteSpace(thuTuButLuc, nameof(thuTuButLuc));
        Check.NotNullOrWhiteSpace(noiDungChinh, nameof(noiDungChinh));
        Check.NotNullOrWhiteSpace(fileName, nameof(fileName));
        Check.NotNullOrWhiteSpace(contentType, nameof(contentType));
        Check.NotNull(contentLength, nameof(contentLength));
        var tep = hoSo.TepDinhKemHoSos.FirstOrDefault(x => x.Id == tepDinhKem.Id);
        if (tep == null)
        {
            throw new BusinessException(KNTCDomainErrorCodes.KTepDinhKemMotExist);
        }
        var existTepDinhKem = hoSo.TepDinhKemHoSos.FirstOrDefault(x => x.TenTaiLieu == tenTaiLieu && x.Id != tepDinhKem.Id);
        if (existTepDinhKem != null)
        {
            throw new BusinessException(KNTCDomainErrorCodes.TepDinhKemAlreadyExist)
                .WithData("maHoSo", hoSo.MaHoSo)
                .WithData("maHoSo", hoSo.MaHoSo);
        }
        if (tepDinhKem.TenTaiLieu != tenTaiLieu)
        {
            tepDinhKem.ChangeTenTaiLieu(tenTaiLieu);
        }
        tepDinhKem.HinhThuc = hinhThuc;
        tepDinhKem.ThoiGianBanHanh = thoiGianBanHanh;
        tepDinhKem.NgayNhan = ngayNhan;
        tepDinhKem.ThuTuButLuc = thuTuButLuc;
        tepDinhKem.NoiDungChinh = noiDungChinh;
        if(!!string.IsNullOrEmpty(fileName))
        {
            tepDinhKem.FileName = fileName;
            tepDinhKem.ContentType = contentType;
            tepDinhKem.ContentLength = contentLength;
        }
    }
}
