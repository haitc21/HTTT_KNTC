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
                                              DateTime? ngayKhieuNai1,
                                              DateTime? NgayTraKQ1,
                                              string ThamQuyen1,
                                              string SoQD1,
                                              DateTime? ngayKhieuNai2,
                                              DateTime? NgayTraKQ2,
                                              string ThamQuyen2,
                                              string SoQD2,
                                              LoaiKetQua? KetQua1 = null,
                                              LoaiKetQua? KetQua2 = null
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
            ngayKhieuNai1 = ngayKhieuNai1,
            NgayTraKQ1 = NgayTraKQ1,
            ThamQuyen1 = ThamQuyen1,
            SoQD1 = SoQD1,
            KetQua1 = KetQua1,
            ngayKhieuNai2 = ngayKhieuNai2,
            NgayTraKQ2 = NgayTraKQ2,
            ThamQuyen2 = ThamQuyen2,
            SoQD2 = SoQD2,
            KetQua2 = KetQua2,
            KetQua = KetQua2 ?? KetQua1
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
    public async Task UpdateAsync([NotNull] Complain complain,
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
                                   DateTime? ngayKhieuNai1,
                                   DateTime? NgayTraKQ1,
                                   string ThamQuyen1,
                                   string SoQD1,
                                   DateTime? ngayKhieuNai2,
                                   DateTime? NgayTraKQ2,
                                   string ThamQuyen2,
                                   string SoQD2,
                                   LoaiKetQua? KetQua1 = null,
                                   LoaiKetQua? KetQua2 = null
      )
    {
        Check.NotNull(complain, nameof(complain));
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

        if (complain.MaHoSo != maHoSo)
        {
            await ChangeMaHoSoAsync(complain, maHoSo);
        }
        complain.TieuDe = tieuDe;
        complain.LoaiVuViec = loaiVuViec;
        complain.NguoiDeNghi = nguoiDeNghi;
        complain.CccdCmnd = cccdCmnd;
        complain.NgayCapCccdCmnd = ngayCapCccdCmnd;
        complain.NoiCapCccdCmnd = noiCapCccdCmnd;
        complain.NgaySinh = ngaySinh;
        complain.DienThaoi = dienThaoi;
        complain.Email = email;
        complain.DiaChiThuongTru = diaChiThuongTru;
        complain.DiaChiLienHe = diaChiLienHe;
        complain.MaTinhTP = maTinhTP;
        complain.MaQuanHuyen = maQuanHuyen;
        complain.MaXaPhuongTT = maXaPhuongTT;
        complain.NgayTiepNhan = ngayTiepNhan;
        complain.NgayHenTraKQ = ngayHenTraKQ;
        complain.NoiDungVuViec = noiDungVuViec;
        complain.SoThua = soThua;
        complain.ToBanDo = toBanDo;
        complain.DienTich = dienTich;
        complain.LoaiDat = loaiDat;
        complain.DiaChiThuaDat = diaChiThuaDat;
        complain.TinhThuaDat = tinhThuaDat;
        complain.HuyenThuaDat = huyenThuaDat;
        complain.XaThuaDat = xaThuaDat;
        complain.DuLieuToaDo = duLieuToaDo;
        complain.DuLieuHinhHoc = duLieuHinhHoc;
        complain.GhiChu = GhiChu;
        complain.ngayKhieuNai1 = ngayKhieuNai1;
        complain.NgayTraKQ1 = NgayTraKQ1;
        complain.ThamQuyen1 = ThamQuyen1;
        complain.SoQD1 = SoQD1;
        complain.KetQua1 = KetQua1;
        complain.ngayKhieuNai2 = ngayKhieuNai2;
        complain.NgayTraKQ2 = NgayTraKQ2;
        complain.ThamQuyen2= ThamQuyen2;
        complain.SoQD2= SoQD2;
        complain.KetQua2= KetQua2;
        complain.KetQua = KetQua2 ?? KetQua1;
    }
    public async Task<FileAttachment> CreateFileAttachmentAsync([NotNull] Complain complain,
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
        Check.NotNull(complain, nameof(complain));
        Check.NotNullOrWhiteSpace(tenTaiLieu, nameof(tenTaiLieu));
        Check.NotNullOrWhiteSpace(hinhThuc, nameof(hinhThuc));
        Check.NotNullOrWhiteSpace(thuTuButLuc, nameof(thuTuButLuc));
        Check.NotNullOrWhiteSpace(noiDungChinh, nameof(noiDungChinh));
        Check.NotNullOrWhiteSpace(fileName, nameof(fileName));
        Check.NotNullOrWhiteSpace(contentType, nameof(contentType));
        Check.NotNull(contentLength, nameof(contentLength));
        var existTepDinhKem = complain.FileAttachments.FirstOrDefault(x => x.TenTaiLieu == tenTaiLieu);
        if (existTepDinhKem != null)
        {
            throw new BusinessException(KNTCDomainErrorCodes.TepDinhKemAlreadyExist)
                .WithData("maHoSo", complain.MaHoSo)
                .WithData("maHoSo", complain.MaHoSo);
        }
        return new FileAttachment(GuidGenerator.Create(), tenTaiLieu)
        {
            IdHoSo = complain.Id,
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
    public async Task UpdateFileAttachmentAsync([NotNull] Complain complain,
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
        Check.NotNull(complain, nameof(complain));
        Check.NotNull(tepDinhKem, nameof(tepDinhKem));
        Check.NotNullOrWhiteSpace(tenTaiLieu, nameof(tenTaiLieu));
        Check.NotNullOrWhiteSpace(hinhThuc, nameof(hinhThuc));
        Check.NotNullOrWhiteSpace(thuTuButLuc, nameof(thuTuButLuc));
        Check.NotNullOrWhiteSpace(noiDungChinh, nameof(noiDungChinh));
        Check.NotNullOrWhiteSpace(fileName, nameof(fileName));
        Check.NotNullOrWhiteSpace(contentType, nameof(contentType));
        Check.NotNull(contentLength, nameof(contentLength));
        var tep = complain.FileAttachments.FirstOrDefault(x => x.Id == tepDinhKem.Id);
        if (tep == null)
        {
            throw new BusinessException(KNTCDomainErrorCodes.KTepDinhKemMotExist);
        }
        var existTepDinhKem = complain.FileAttachments.FirstOrDefault(x => x.TenTaiLieu == tenTaiLieu && x.Id != tepDinhKem.Id);
        if (existTepDinhKem != null)
        {
            throw new BusinessException(KNTCDomainErrorCodes.TepDinhKemAlreadyExist)
                .WithData("maHoSo", complain.MaHoSo)
                .WithData("maHoSo", complain.MaHoSo);
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
