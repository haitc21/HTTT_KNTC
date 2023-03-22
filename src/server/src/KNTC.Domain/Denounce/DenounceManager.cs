using KNTC.Complains;
using KNTC.FileAttachments;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace KNTC.Denounces;

public class DenounceManager : DomainService
{
    private readonly IDenounceRepository _hoSoRepo;
    private readonly IRepository<FileAttachment, Guid> _fileAttachmentRepo;
    public DenounceManager(IDenounceRepository hoSoRepo, IRepository<FileAttachment, Guid> fileAttachmentRepo)
    {
        _hoSoRepo = hoSoRepo;
        _fileAttachmentRepo = fileAttachmentRepo;
    }
    public async Task<Denounce> CreateAsync([NotNull] string maHoSo,
                                              [NotNull] LinhVuc linhVuc,
                                              [NotNull] string tieuDe,
                                              [NotNull] string nguoiDeNghi,
                                              [NotNull] string cccdCmnd,
                                              //[NotNull] DateTime ngayCapCccdCmnd,
                                              //[NotNull] string noiCapCccdCmnd,
                                              [NotNull] DateTime ngaySinh,
                                              [NotNull] string DienThoai,
                                              string email,
                                              [NotNull] string diaChiThuongTru,
                                              [NotNull] string diaChiLienHe,
                                              [NotNull] int maTinhTP,
                                              [NotNull] int maQuanHuyen,
                                              [NotNull] int maXaPhuongTT,
                                              [NotNull] DateTime thoiGianTiepNhan,
                                              [NotNull] DateTime thoiGianHenTraKQ,
                                              [NotNull] string noiDungVuViec,
                                              [NotNull] string boPhanDangXL,
                                              [NotNull] string soThua,
                                              [NotNull] string toBanDo,
                                              [NotNull] decimal dienTich,
                                              [NotNull] int loaiDat,
                                              [NotNull] string diaChiThuaDat,
                                              [NotNull] int tinhThuaDat,
                                              [NotNull] int huyenThuaDat,
                                              [NotNull] int xaThuaDat,
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
        Check.NotNull(linhVuc, nameof(linhVuc));
        Check.NotNullOrWhiteSpace(tieuDe, nameof(tieuDe));
        Check.NotNullOrWhiteSpace(nguoiDeNghi, nameof(nguoiDeNghi));
        Check.NotNullOrWhiteSpace(cccdCmnd, nameof(cccdCmnd));
        //Check.NotNullOrWhiteSpace(noiCapCccdCmnd, nameof(noiCapCccdCmnd));
        //Check.NotNull(ngayCapCccdCmnd, nameof(ngayCapCccdCmnd));
        Check.NotNull(ngaySinh, nameof(ngaySinh));
        Check.NotNullOrWhiteSpace(DienThoai, nameof(DienThoai));
        Check.NotNullOrWhiteSpace(diaChiThuongTru, nameof(diaChiThuongTru));
        Check.NotNullOrWhiteSpace(diaChiLienHe, nameof(diaChiLienHe));
        Check.NotNull(maTinhTP, nameof(maTinhTP));
        Check.NotNull(maQuanHuyen, nameof(maQuanHuyen));
        Check.NotNull(maXaPhuongTT, nameof(maXaPhuongTT));
        Check.NotNullOrWhiteSpace(noiDungVuViec, nameof(noiDungVuViec));
        Check.NotNullOrWhiteSpace(boPhanDangXL, nameof(boPhanDangXL));
        Check.NotNullOrWhiteSpace(soThua, nameof(soThua));
        Check.NotNullOrWhiteSpace(toBanDo, nameof(toBanDo));
        Check.NotNull(dienTich, nameof(dienTich));
        Check.NotNull(loaiDat, nameof(loaiDat));
        Check.NotNullOrWhiteSpace(diaChiThuaDat, nameof(diaChiThuaDat));
        Check.NotNull(tinhThuaDat, nameof(tinhThuaDat));
        Check.NotNull(huyenThuaDat, nameof(huyenThuaDat));
        Check.NotNull(xaThuaDat, nameof(xaThuaDat));

        var existedHoSo = await _hoSoRepo.FindByMaHoSoAsync(maHoSo, false);
        if (existedHoSo != null)
        {
            throw new BusinessException(KNTCDomainErrorCodes.HoSoAlreadyExist).WithData("maHoSo", maHoSo);
        }
        return new Denounce(GuidGenerator.Create(), maHoSo)
        {
            TieuDe = tieuDe,
            LinhVuc = linhVuc,
            NguoiDeNghi = nguoiDeNghi,
            CccdCmnd = cccdCmnd,
            //NgayCapCccdCmnd = ngayCapCccdCmnd,
            //NoiCapCccdCmnd = noiCapCccdCmnd,
            NgaySinh = ngaySinh,
            DienThoai = DienThoai,
            Email = email,
            DiaChiThuongTru = diaChiThuongTru,
            DiaChiLienHe = diaChiLienHe,
            MaTinhTP = maTinhTP,
            MaQuanHuyen = maQuanHuyen,
            MaXaPhuongTT = maXaPhuongTT,
            ThoiGianTiepNhan = thoiGianTiepNhan,
            ThoiGianHenTraKQ = thoiGianHenTraKQ,
            NoiDungVuViec = noiDungVuViec,
            BoPhanDangXL = boPhanDangXL,
            GhiChu = GhiChu,
            SoThua = soThua,
            ToBanDo = toBanDo,
            DienTich = dienTich,
            LoaiDat = loaiDat,
            DiaChiThuaDat = diaChiThuaDat,
            tinhThuaDat = tinhThuaDat,
            huyenThuaDat = huyenThuaDat,
            xaThuaDat = xaThuaDat,
            DuLieuToaDo = duLieuToaDo,
            DuLieuHinhHoc = duLieuHinhHoc,
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
                                  [NotNull] LinhVuc linhVuc,
                                   [NotNull] string tieuDe,
                                   [NotNull] string nguoiDeNghi,
                                   [NotNull] string cccdCmnd,
                                   //[NotNull] DateTime ngayCapCccdCmnd,
                                   //[NotNull] string noiCapCccdCmnd,
                                   [NotNull] DateTime ngaySinh,
                                   [NotNull] string DienThoai,
                                   string email,
                                   [NotNull] string diaChiThuongTru,
                                   [NotNull] string diaChiLienHe,
                                   [NotNull] int maTinhTP,
                                   [NotNull] int maQuanHuyen,
                                   [NotNull] int maXaPhuongTT,
                                   [NotNull] DateTime thoiGianTiepNhan,
                                   [NotNull] DateTime thoiGianHenTraKQ,
                                   [NotNull] string noiDungVuViec,
                                   [NotNull] string boPhanDangXL,
                                   [NotNull] string soThua,
                                   [NotNull] string toBanDo,
                                   [NotNull] decimal dienTich,
                                   [NotNull] int loaiDat,
                                   [NotNull] string diaChiThuaDat,
                                   [NotNull] int tinhThuaDat,
                                   [NotNull] int huyenThuaDat,
                                   [NotNull] int xaThuaDat,
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
        Check.NotNull(linhVuc, nameof(linhVuc));
        Check.NotNullOrWhiteSpace(tieuDe, nameof(tieuDe));
        Check.NotNullOrWhiteSpace(nguoiDeNghi, nameof(nguoiDeNghi));
        Check.NotNullOrWhiteSpace(cccdCmnd, nameof(cccdCmnd));
        //Check.NotNullOrWhiteSpace(noiCapCccdCmnd, nameof(noiCapCccdCmnd));
        //Check.NotNull(ngayCapCccdCmnd, nameof(ngayCapCccdCmnd));
        Check.NotNull(ngaySinh, nameof(ngaySinh));
        Check.NotNullOrWhiteSpace(DienThoai, nameof(DienThoai));
        Check.NotNullOrWhiteSpace(diaChiThuongTru, nameof(diaChiThuongTru));
        Check.NotNullOrWhiteSpace(diaChiLienHe, nameof(diaChiLienHe));
        Check.NotNull(maTinhTP, nameof(maTinhTP));
        Check.NotNull(maQuanHuyen, nameof(maQuanHuyen));
        Check.NotNull(maXaPhuongTT, nameof(maXaPhuongTT));
        Check.NotNullOrWhiteSpace(noiDungVuViec, nameof(noiDungVuViec));
        Check.NotNullOrWhiteSpace(noiDungVuViec, nameof(boPhanDangXL));

        Check.NotNullOrWhiteSpace(soThua, nameof(soThua));
        Check.NotNullOrWhiteSpace(toBanDo, nameof(toBanDo));
        Check.NotNull(dienTich, nameof(dienTich));
        Check.NotNull(loaiDat, nameof(loaiDat));
        Check.NotNullOrWhiteSpace(diaChiThuaDat, nameof(diaChiThuaDat));
        Check.NotNull(tinhThuaDat, nameof(tinhThuaDat));
        Check.NotNull(huyenThuaDat, nameof(huyenThuaDat));
        Check.NotNull(xaThuaDat, nameof(xaThuaDat));

        if (denounce.MaHoSo != maHoSo)
        {
            await ChangeMaHoSoAsync(denounce, maHoSo);
        }
        denounce.TieuDe = tieuDe;
        //denounce.LinhVuc = linhVuc;
        denounce.NguoiDeNghi = nguoiDeNghi;
        denounce.CccdCmnd = cccdCmnd;
        //denounce.NgayCapCccdCmnd = ngayCapCccdCmnd;
        //denounce.NoiCapCccdCmnd = noiCapCccdCmnd;
        denounce.NgaySinh = ngaySinh;
        denounce.DienThoai = DienThoai;
        denounce.Email = email;
        denounce.DiaChiThuongTru = diaChiThuongTru;
        denounce.DiaChiLienHe = diaChiLienHe;
        denounce.MaTinhTP = maTinhTP;
        denounce.MaQuanHuyen = maQuanHuyen;
        denounce.MaXaPhuongTT = maXaPhuongTT;
        denounce.ThoiGianTiepNhan = thoiGianTiepNhan;
        denounce.ThoiGianHenTraKQ = thoiGianHenTraKQ;
        denounce.NoiDungVuViec = noiDungVuViec;
        denounce.BoPhanDangXL = boPhanDangXL;
        denounce.GhiChu = GhiChu;

        denounce.SoThua = soThua;
        denounce.ToBanDo = toBanDo;
        denounce.DienTich = dienTich;
        denounce.LoaiDat = loaiDat;
        denounce.DiaChiThuaDat = diaChiThuaDat;
        denounce.tinhThuaDat = tinhThuaDat;
        denounce.huyenThuaDat = huyenThuaDat;
        denounce.xaThuaDat = xaThuaDat;
        denounce.DuLieuToaDo = duLieuToaDo;
        denounce.DuLieuHinhHoc = duLieuHinhHoc;
        denounce.KetQua = KetQua2;
    }
    public async Task<FileAttachment> CreateFileAttachmentAsync([NotNull] Denounce denounce,
                                                                [NotNull] int giaiDoan,
                                                                [NotNull] string tenTaiLieu,
                                                                [NotNull] int hinhThuc,
                                                                DateTime thoiGianBanHanh,
                                                                DateTime ngayNhan,
                                                                [NotNull] string thuTuButLuc,
                                                                [NotNull] string noiDungChinh,
                                                                [NotNull] string fileName,
                                                                [NotNull] string contentType,
                                                                [NotNull] long contentLength)
    {
        Check.NotNull(denounce, nameof(denounce));
        Check.NotNull(denounce, nameof(giaiDoan));
        Check.NotNullOrWhiteSpace(tenTaiLieu, nameof(tenTaiLieu));
        Check.NotNull(hinhThuc, nameof(hinhThuc));
        Check.NotNullOrWhiteSpace(thuTuButLuc, nameof(thuTuButLuc));
        Check.NotNullOrWhiteSpace(noiDungChinh, nameof(noiDungChinh));
        Check.NotNullOrWhiteSpace(fileName, nameof(fileName));
        Check.NotNullOrWhiteSpace(contentType, nameof(contentType));
        Check.NotNull(contentLength, nameof(contentLength));
        var existTepDinhKem = await _fileAttachmentRepo.FindAsync(x => x.TenTaiLieu == tenTaiLieu
                                                     && x.DenounceId == denounce.Id);
        if (existTepDinhKem != null)
        {
            throw new BusinessException(KNTCDomainErrorCodes.TepDinhKemAlreadyExist)
                .WithData("tenTaiLieu", tenTaiLieu)
                .WithData("maHoSo", denounce.MaHoSo);
        }
        return new FileAttachment(GuidGenerator.Create(), tenTaiLieu)
        {
            DenounceId = denounce.Id,
            GiaiDoan = giaiDoan,
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
}
