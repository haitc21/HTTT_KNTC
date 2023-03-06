﻿using KNTC.FileAttachments;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace KNTC.Denounces;

public class DenounceManager : DomainService
{
    private readonly IDenounceRepository _hoSoRepo;
    public DenounceManager(IDenounceRepository hoSoRepo)
    {
        _hoSoRepo = hoSoRepo;
    }
    public async Task<Denounce> CreateAsync([NotNull] string maHoSo,
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
        return new Denounce(GuidGenerator.Create(), maHoSo)
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
    public async Task ChangeMaHoSoAsync([NotNull] Denounce hoSo, [NotNull] string maHoSo)
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
    public async Task UpdateAsync([NotNull] Denounce denounce,
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
        Check.NotNull(denounce, nameof(denounce));
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

        if (denounce.MaHoSo != maHoSo)
        {
            await ChangeMaHoSoAsync(denounce, maHoSo);
        }
        denounce.TieuDe = tieuDe;
        denounce.LoaiVuViec = loaiVuViec;
        denounce.NguoiDeNghi = nguoiDeNghi;
        denounce.CccdCmnd = cccdCmnd;
        denounce.NgayCapCccdCmnd = ngayCapCccdCmnd;
        denounce.NoiCapCccdCmnd = noiCapCccdCmnd;
        denounce.NgaySinh = ngaySinh;
        denounce.DienThaoi = dienThaoi;
        denounce.Email = email;
        denounce.DiaChiThuongTru = diaChiThuongTru;
        denounce.DiaChiLienHe = diaChiLienHe;
        denounce.MaTinhTP = maTinhTP;
        denounce.MaQuanHuyen = maQuanHuyen;
        denounce.MaXaPhuongTT = maXaPhuongTT;
        denounce.NgayTiepNhan = ngayTiepNhan;
        denounce.NgayHenTraKQ = ngayHenTraKQ;
        denounce.NoiDungVuViec = noiDungVuViec;
        denounce.SoThua = soThua;
        denounce.ToBanDo = toBanDo;
        denounce.DienTich = dienTich;
        denounce.LoaiDat = loaiDat;
        denounce.DiaChiThuaDat = diaChiThuaDat;
        denounce.TinhThuaDat = tinhThuaDat;
        denounce.HuyenThuaDat = huyenThuaDat;
        denounce.XaThuaDat = xaThuaDat;
        denounce.DuLieuToaDo = duLieuToaDo;
        denounce.DuLieuHinhHoc = duLieuHinhHoc;
        denounce.GhiChu = GhiChu;
        denounce.ngayKhieuNai1 = ngayKhieuNai1;
        denounce.NgayTraKQ1 = NgayTraKQ1;
        denounce.ThamQuyen1 = ThamQuyen1;
        denounce.SoQD1 = SoQD1;
        denounce.KetQua1 = KetQua1;
        denounce.ngayKhieuNai2 = ngayKhieuNai2;
        denounce.NgayTraKQ2 = NgayTraKQ2;
        denounce.ThamQuyen2 = ThamQuyen2;
        denounce.SoQD2 = SoQD2;
        denounce.KetQua2 = KetQua2;
        denounce.KetQua = KetQua2 ?? KetQua1;
    }
    public async Task<FileAttachment> CreateFileAttachmentAsync([NotNull] Denounce denounce,
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
        Check.NotNull(denounce, nameof(denounce));
        Check.NotNullOrWhiteSpace(tenTaiLieu, nameof(tenTaiLieu));
        Check.NotNullOrWhiteSpace(hinhThuc, nameof(hinhThuc));
        Check.NotNullOrWhiteSpace(thuTuButLuc, nameof(thuTuButLuc));
        Check.NotNullOrWhiteSpace(noiDungChinh, nameof(noiDungChinh));
        Check.NotNullOrWhiteSpace(fileName, nameof(fileName));
        Check.NotNullOrWhiteSpace(contentType, nameof(contentType));
        Check.NotNull(contentLength, nameof(contentLength));
        var existTepDinhKem = denounce.FileAttachments.FirstOrDefault(x => x.TenTaiLieu == tenTaiLieu);
        if (existTepDinhKem != null)
        {
            throw new BusinessException(KNTCDomainErrorCodes.TepDinhKemAlreadyExist)
                .WithData("maHoSo", denounce.MaHoSo)
                .WithData("maHoSo", denounce.MaHoSo);
        }
        return new FileAttachment(GuidGenerator.Create(), tenTaiLieu)
        {
            IdHoSo = denounce.Id,
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
    public async Task UpdateFileAttachmentAsync([NotNull] Denounce denounce,
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
        Check.NotNull(denounce, nameof(denounce));
        Check.NotNull(tepDinhKem, nameof(tepDinhKem));
        Check.NotNullOrWhiteSpace(tenTaiLieu, nameof(tenTaiLieu));
        Check.NotNullOrWhiteSpace(hinhThuc, nameof(hinhThuc));
        Check.NotNullOrWhiteSpace(thuTuButLuc, nameof(thuTuButLuc));
        Check.NotNullOrWhiteSpace(noiDungChinh, nameof(noiDungChinh));
        Check.NotNullOrWhiteSpace(fileName, nameof(fileName));
        Check.NotNullOrWhiteSpace(contentType, nameof(contentType));
        Check.NotNull(contentLength, nameof(contentLength));
        var tep = denounce.FileAttachments.FirstOrDefault(x => x.Id == tepDinhKem.Id);
        if (tep == null)
        {
            throw new BusinessException(KNTCDomainErrorCodes.KTepDinhKemMotExist);
        }
        var existTepDinhKem = denounce.FileAttachments.FirstOrDefault(x => x.TenTaiLieu == tenTaiLieu && x.Id != tepDinhKem.Id);
        if (existTepDinhKem != null)
        {
            throw new BusinessException(KNTCDomainErrorCodes.TepDinhKemAlreadyExist)
                .WithData("maHoSo", denounce.MaHoSo)
                .WithData("maHoSo", denounce.MaHoSo);
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