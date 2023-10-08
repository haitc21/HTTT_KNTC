using KNTC.FileAttachments;
using System;
using System.Diagnostics.CodeAnalysis;
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
                                              [NotNull] string nguoiNopDon,
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
                                              [NotNull] string nguoiBiToCao,
                                              [NotNull] string boPhanDangXL,
                                              [NotNull] string soThua,
                                              [NotNull] string toBanDo,
                                              [NotNull] decimal dienTich,
                                              [NotNull] int loaiDat,
                                              [NotNull] string diaChiThuaDat,
                                              [NotNull] int tinhThuaDat,
                                              [NotNull] int huyenThuaDat,
                                              [NotNull] int xaThuaDat,
                                              string? duLieuToaDo,
                                              string? duLieuHinhHoc,
                                              string? GhiChu,
                                              DateTime? ngayGQTC,
                                              string? nguoiGQTC,
                                              string? quyerDinhThuLyGQTC,
                                              DateTime? ngayQDGQTC,
                                              string? quyetDinhDinhChiGQTC,
                                              DateTime? giaHanGQTC1,
                                              DateTime? giaHanGQTC2,
                                              string? soVBKLNDTC,
                                              DateTime? ngayNhanTBKQXLKLTC,
                                              LoaiKetQua? ketQua,
                                              bool congKhai,
                                              bool luuTru,
                                              [NotNull] TrangThai TrangThai
        )
    {
        Check.NotNullOrWhiteSpace(maHoSo, nameof(maHoSo));
        Check.NotNull(linhVuc, nameof(linhVuc));
        Check.NotNullOrWhiteSpace(tieuDe, nameof(tieuDe));
        Check.NotNullOrWhiteSpace(nguoiNopDon, nameof(nguoiNopDon));
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
        Check.NotNullOrWhiteSpace(nguoiBiToCao, nameof(nguoiBiToCao));
        Check.NotNullOrWhiteSpace(boPhanDangXL, nameof(boPhanDangXL));
        Check.NotNullOrWhiteSpace(soThua, nameof(soThua));
        Check.NotNullOrWhiteSpace(toBanDo, nameof(toBanDo));
        Check.NotNull(dienTich, nameof(dienTich));
        Check.NotNull(loaiDat, nameof(loaiDat));
        Check.NotNullOrWhiteSpace(diaChiThuaDat, nameof(diaChiThuaDat));
        Check.NotNull(tinhThuaDat, nameof(tinhThuaDat));
        Check.NotNull(huyenThuaDat, nameof(huyenThuaDat));
        Check.NotNull(xaThuaDat, nameof(xaThuaDat));
        Check.NotNull(TrangThai, nameof(TrangThai));

        var existedHoSo = await _hoSoRepo.FindByMaHoSoAsync(maHoSo, false);
        if (existedHoSo != null)
        {
            throw new BusinessException(KNTCDomainErrorCodes.HoSoAlreadyExist).WithData("maHoSo", maHoSo);
        }
        return new Denounce(GuidGenerator.Create(), maHoSo)
        {
            TieuDe = tieuDe,
            LinhVuc = linhVuc,
            NguoiNopDon = nguoiNopDon,
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
            NguoiBiToCao = noiDungVuViec,
            BoPhanDangXL = boPhanDangXL,
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
            NgayGQTC = ngayGQTC,
            NguoiGQTC = nguoiGQTC,
            QuyerDinhThuLyGQTC = quyerDinhThuLyGQTC,
            NgayQDGQTC = ngayQDGQTC,
            QuyetDinhDinhChiGQTC = quyetDinhDinhChiGQTC,
            GiaHanGQTC1 = giaHanGQTC1,
            GiaHanGQTC2 = giaHanGQTC2,
            SoVBKLNDTC = soVBKLNDTC,
            NgayNhanTBKQXLKLTC = ngayNhanTBKQXLKLTC,
            KetQua = ketQua,
            CongKhai = congKhai,
            LuuTru = luuTru,
            TrangThai = TrangThai
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
                                   [NotNull] string nguoiNopDon,
                                   [NotNull] string cccdCmnd,
                                   //[NotNull] DateTime ngayCapCccdCmnd,
                                   //[NotNull] string noiCapCccdCmnd,
                                   [NotNull] DateTime ngaySinh,
                                   [NotNull] string DienThoai,
                                   string? email,
                                   [NotNull] string diaChiThuongTru,
                                   [NotNull] string diaChiLienHe,
                                   [NotNull] int maTinhTP,
                                   [NotNull] int maQuanHuyen,
                                   [NotNull] int maXaPhuongTT,
                                   [NotNull] DateTime thoiGianTiepNhan,
                                   [NotNull] DateTime thoiGianHenTraKQ,
                                   [NotNull] string noiDungVuViec,
                                   [NotNull] string nguoiBiToCao,
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
                                   string? GhiChu,
                                   DateTime? ngayGQTC,
                                   string? nguoiGQTC,
                                   string? quyerDinhThuLyGQTC,
                                   DateTime? ngayQDGQTC,
                                   string? quyetDinhDinhChiGQTC,
                                   DateTime? giaHanGQTC1,
                                   DateTime? giaHanGQTC2,
                                   string? soVBKLNDTC,
                                   DateTime? ngayNhanTBKQXLKLTC,
                                   LoaiKetQua? ketQua,
                                   bool congKhai,
                                   bool luutru,
                                   [NotNull] TrangThai TrangThai
      )
    {
        Check.NotNull(denounce, nameof(denounce));
        Check.NotNullOrWhiteSpace(maHoSo, nameof(maHoSo));
        Check.NotNull(linhVuc, nameof(linhVuc));
        Check.NotNullOrWhiteSpace(tieuDe, nameof(tieuDe));
        Check.NotNullOrWhiteSpace(nguoiNopDon, nameof(nguoiNopDon));
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
        Check.NotNullOrWhiteSpace(nguoiBiToCao, nameof(nguoiBiToCao));
        Check.NotNullOrWhiteSpace(noiDungVuViec, nameof(boPhanDangXL));

        Check.NotNullOrWhiteSpace(soThua, nameof(soThua));
        Check.NotNullOrWhiteSpace(toBanDo, nameof(toBanDo));
        Check.NotNull(dienTich, nameof(dienTich));
        Check.NotNull(loaiDat, nameof(loaiDat));
        Check.NotNullOrWhiteSpace(diaChiThuaDat, nameof(diaChiThuaDat));
        Check.NotNull(tinhThuaDat, nameof(tinhThuaDat));
        Check.NotNull(huyenThuaDat, nameof(huyenThuaDat));
        Check.NotNull(xaThuaDat, nameof(xaThuaDat));
        Check.NotNull(TrangThai, nameof(TrangThai));

        if (denounce.MaHoSo != maHoSo)
        {
            await ChangeMaHoSoAsync(denounce, maHoSo);
        }
        denounce.TieuDe = tieuDe;
        //denounce.LinhVuc = linhVuc;
        denounce.NguoiNopDon = nguoiNopDon;
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
        denounce.NguoiBiToCao = nguoiBiToCao;
        denounce.BoPhanDangXL = boPhanDangXL;
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
        denounce.NgayGQTC = ngayGQTC;
        denounce.NguoiGQTC = nguoiGQTC;
        denounce.QuyerDinhThuLyGQTC = quyerDinhThuLyGQTC;
        denounce.NgayQDGQTC = ngayQDGQTC;
        denounce.QuyetDinhDinhChiGQTC = quyetDinhDinhChiGQTC;
        denounce.GiaHanGQTC1 = giaHanGQTC1;
        denounce.GiaHanGQTC2 = giaHanGQTC2;
        denounce.SoVBKLNDTC = soVBKLNDTC;
        denounce.NgayNhanTBKQXLKLTC = ngayNhanTBKQXLKLTC;
        denounce.KetQua = ketQua;
        denounce.CongKhai = congKhai;
        denounce.LuuTru = luutru;
        denounce.TrangThai = TrangThai;
    }
}