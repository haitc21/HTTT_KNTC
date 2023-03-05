using KNTC.FileAttachments;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace KNTC.Complains;

public class ComplainManager : DomainService
{
    private readonly IComplainRepository _hoSoRepo;
    public ComplainManager(IComplainRepository hoSoRepo)
    {
        _hoSoRepo = hoSoRepo;
    }
    public async Task<Complain> CreateAsync([NotNull] string maHoSo,
                                              [NotNull] LoaiVuViec loaiVuViec,
                                              [NotNull] string tieuDe,
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
                                              string duLieuHinhHoc,
                                              string GhiChu,
                                              DateTime ngayKhieuNaiI,
                                              DateTime NgayTraKQI,
                                              string ThamQuyenI,
                                              string SoQDI,
                                              DateTime ngayKhieuNaiII,
                                              DateTime NgayTraKQII,
                                              string ThamQuyenII,
                                              string SoQDII,
                                              LoaiKetQua? KetQuaI = null,
                                              LoaiKetQua? KetQuaII = null
        )
    {
        Check.NotNullOrWhiteSpace(maHoSo, nameof(maHoSo));
        Check.NotNull(loaiVuViec, nameof(loaiVuViec));
        Check.NotNullOrWhiteSpace(tieuDe, nameof(tieuDe));
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
        return new Complain(GuidGenerator.Create(), maHoSo)
        {
            TieuDe = tieuDe,
            LoaiVuViec = loaiVuViec,
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
            DuLieuHinhHoc = duLieuHinhHoc,
            GhiChu = GhiChu,
            ngayKhieuNaiI = ngayKhieuNaiI,
            NgayTraKQI = NgayTraKQI,
            ThamQuyenI = ThamQuyenI,
            SoQDI = SoQDI,
            KetQuaI = KetQuaI,
            ngayKhieuNaiII = ngayKhieuNaiII,
            NgayTraKQII = NgayTraKQII,
            ThamQuyenII = ThamQuyenII,
            SoQDII = SoQDII,
            KetQuaII = KetQuaII,
            KetQua = KetQuaII ?? KetQuaI
        };
    }
    public async Task ChangeMaHoSoAsync([NotNull] Complain hoSo, [NotNull] string maHoSo)
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
    public async Task UpdateAsync([NotNull] Complain hoSo,
                                  [NotNull] string maHoSo,
                                  [NotNull] LoaiVuViec loaiVuViec,
                                   [NotNull] string tieuDe,
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
                                   string duLieuHinhHoc,
                                   string GhiChu,
                                   DateTime ngayKhieuNaiI,
                                   DateTime NgayTraKQI,
                                   string ThamQuyenI,
                                   string SoQDI,
                                   DateTime ngayKhieuNaiII,
                                   DateTime NgayTraKQII,
                                   string ThamQuyenII,
                                   string SoQDII,
                                   LoaiKetQua? KetQuaI = null,
                                   LoaiKetQua? KetQuaII = null
      )
    {
        Check.NotNull(hoSo, nameof(hoSo));
        Check.NotNullOrWhiteSpace(maHoSo, nameof(maHoSo));
        Check.NotNull(loaiVuViec, nameof(loaiVuViec));
        Check.NotNullOrWhiteSpace(tieuDe, nameof(tieuDe));
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
        hoSo.TieuDe = tieuDe;
        hoSo.LoaiVuViec = loaiVuViec;
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
        hoSo.GhiChu = GhiChu;
        hoSo.ngayKhieuNaiI = ngayKhieuNaiI;
        hoSo.NgayTraKQI = NgayTraKQI;
        hoSo.ThamQuyenI = ThamQuyenI;
        hoSo.SoQDI = SoQDI;
        hoSo.KetQuaI = KetQuaI;
        hoSo.ngayKhieuNaiII = ngayKhieuNaiII;
        hoSo.NgayTraKQII = NgayTraKQII;
        hoSo.ThamQuyenII= ThamQuyenII;
        hoSo.SoQDII= SoQDII;
        hoSo.KetQuaII= KetQuaII;
        hoSo.KetQua = KetQuaII ?? KetQuaI;
    }
    public async Task<FileAttachment> CreateFileAttachmentAsync([NotNull] Complain hoSo,
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
        var existTepDinhKem = hoSo.FileAttachments.FirstOrDefault(x => x.TenTaiLieu == tenTaiLieu);
        if (existTepDinhKem != null)
        {
            throw new BusinessException(KNTCDomainErrorCodes.TepDinhKemAlreadyExist)
                .WithData("maHoSo", hoSo.MaHoSo)
                .WithData("maHoSo", hoSo.MaHoSo);
        }
        return new FileAttachment(GuidGenerator.Create(), tenTaiLieu)
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
    public async Task UpdateFileAttachmentAsync([NotNull] Complain hoSo,
                                                [NotNull] FileAttachment tepDinhKem,
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
        var tep = hoSo.FileAttachments.FirstOrDefault(x => x.Id == tepDinhKem.Id);
        if (tep == null)
        {
            throw new BusinessException(KNTCDomainErrorCodes.KTepDinhKemMotExist);
        }
        var existTepDinhKem = hoSo.FileAttachments.FirstOrDefault(x => x.TenTaiLieu == tenTaiLieu && x.Id != tepDinhKem.Id);
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
        if (!!string.IsNullOrEmpty(fileName))
        {
            tepDinhKem.FileName = fileName;
            tepDinhKem.ContentType = contentType;
            tepDinhKem.ContentLength = contentLength;
        }
    }
}
