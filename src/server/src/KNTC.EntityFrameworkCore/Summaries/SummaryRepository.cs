using KNTC.Complains;
using KNTC.Denounces;
using KNTC.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore;

namespace KNTC.Summaries;

public class SummaryRepository : ISummaryRepository
{
    private readonly IDbContextProvider<KNTCDbContext> _dbContextProvider;

    public SummaryRepository(IDbContextProvider<KNTCDbContext> dbContextProvider)
    {
        _dbContextProvider = dbContextProvider;
    }

    public async Task<IQueryable<Summary>> GetListAsync(bool landComplain,
                                                        bool enviromentComplain,
                                                        bool waterComplain,
                                                        bool mineralComplain,
                                                        bool landDenounce,
                                                        bool enviromentDenounce,
                                                        bool waterDenounce,
                                                        bool mineralDenounce,
                                                        string keyword,
                                                        LoaiKetQua? ketQua,
                                                        int? maTinhTP,
                                                        int? maQuanHuyen,
                                                        int? maXaPhuongTT,
                                                        DateTime? fromDate,
                                                        DateTime? toDate,
                                                        bool? congKhai,
                                                        string nguoiNopDon)
    {
        var filter = !keyword.IsNullOrWhiteSpace() ? keyword.ToUpper().Trim() : keyword;
        nguoiNopDon = !nguoiNopDon.IsNullOrEmpty() ? nguoiNopDon.ToUpper().Trim() : "";
        var dbContext = await _dbContextProvider.GetDbContextAsync();
        var complainQuery = dbContext.Set<Complain>()
                        .WhereIf(landComplain == false, x => x.LinhVuc != LinhVuc.DatDai)
                        .WhereIf(enviromentComplain == false, x => x.LinhVuc != LinhVuc.MoiTruong)
                        .WhereIf(waterComplain == false, x => x.LinhVuc != LinhVuc.TaiNguyenNuoc)
                        .WhereIf(mineralComplain == false, x => x.LinhVuc != LinhVuc.KhoangSan)
                        .WhereIf(
                            !filter.IsNullOrWhiteSpace(),
                            x => x.MaHoSo.ToUpper().Contains(filter)
                            || x.TieuDe.ToUpper().Contains(filter)
                         )
                         .WhereIf(
                            ketQua.HasValue,
                            x => (ketQua != LoaiKetQua.ChuaCoKQ && x.KetQua == ketQua) || x.KetQua == null
                         )
                         .WhereIf(
                            maTinhTP.HasValue,
                            x => x.MaTinhTP == maTinhTP
                         )
                         .WhereIf(
                            maQuanHuyen.HasValue,
                            x => x.MaQuanHuyen == maQuanHuyen
                         )
                         .WhereIf(
                            maXaPhuongTT.HasValue,
                            x => x.MaXaPhuongTT == maXaPhuongTT
                         )
                         .WhereIf(
                            fromDate.HasValue,
                            x => x.ThoiGianTiepNhan >= fromDate
                         )
                         .WhereIf(
                            toDate.HasValue,
                            x => x.ThoiGianTiepNhan <= toDate
                         )
                         .WhereIf(
                            congKhai.HasValue,
                            x => x.CongKhai == congKhai
                         )
                         .WhereIf(
                            !string.IsNullOrEmpty(nguoiNopDon),
                            x => (x.NoiDungVuViec.ToUpper().Contains(nguoiNopDon) || x.CccdCmnd == nguoiNopDon)
                         )
                        .Select(c => new Summary()
                        {
                            Id = c.Id,
                            MaHoSo = c.MaHoSo,
                            LoaiVuViec = LoaiVuViec.KhieuNai,
                            LinhVuc = c.LinhVuc,
                            NguoiNopDon = c.NguoiNopDon,
                            DienThoai = c.DienThoai,
                            DiaChiLienHe = c.DiaChiLienHe,
                            TieuDe = c.TieuDe,
                            ThoiGianTiepNhan = c.ThoiGianTiepNhan,
                            ThoiGianHenTraKQ = c.ThoiGianHenTraKQ,
                            BoPhanDangXL = c.BoPhanDangXL,
                            KetQua = c.KetQua,
                            DuLieuToaDo = c.DuLieuToaDo,
                            DuLieuHinhHoc = c.DuLieuHinhHoc,
                            SoThua = c.SoThua,
                            ToBanDo= c.ToBanDo
                        });

        var denounceQuery = dbContext.Set<Denounce>()
                        .WhereIf(landDenounce == false, x => x.LinhVuc != LinhVuc.DatDai)
                        .WhereIf(enviromentDenounce == false, x => x.LinhVuc != LinhVuc.MoiTruong)
                        .WhereIf(waterDenounce == false, x => x.LinhVuc != LinhVuc.TaiNguyenNuoc)
                        .WhereIf(mineralDenounce == false, x => x.LinhVuc != LinhVuc.KhoangSan)
                        .WhereIf(
                            !filter.IsNullOrWhiteSpace(),
                            x => x.MaHoSo.ToUpper().Contains(filter)
                            || x.TieuDe.ToUpper().Contains(filter)
                         )
                         .WhereIf(
                            ketQua.HasValue,
                            x => (ketQua != LoaiKetQua.ChuaCoKQ && x.KetQua == ketQua) || x.KetQua == null
                         )
                         .WhereIf(
                            maTinhTP.HasValue,
                            x => x.MaTinhTP == maTinhTP
                         )
                         .WhereIf(
                            maQuanHuyen.HasValue,
                            x => x.MaQuanHuyen == maQuanHuyen
                         )
                         .WhereIf(
                            maXaPhuongTT.HasValue,
                            x => x.MaXaPhuongTT == maXaPhuongTT
                         )
                         .WhereIf(
                            fromDate.HasValue,
                            x => x.ThoiGianTiepNhan >= fromDate
                         )
                         .WhereIf(
                            toDate.HasValue,
                            x => x.ThoiGianTiepNhan <= toDate
                         )
                         .WhereIf(
                            congKhai.HasValue,
                            x => x.CongKhai == congKhai
                         )
                         .WhereIf(
                            !string.IsNullOrEmpty(nguoiNopDon),
                            x => (x.NoiDungVuViec.ToUpper().Contains(nguoiNopDon) || x.CccdCmnd == nguoiNopDon)
                         )
                        .Select(d => new Summary()
                        {
                            Id = d.Id,
                            MaHoSo = d.MaHoSo,
                            LoaiVuViec = LoaiVuViec.ToCao,
                            LinhVuc = d.LinhVuc,
                            NguoiNopDon = d.NguoiNopDon,
                            DienThoai = d.DienThoai,
                            DiaChiLienHe = d.DiaChiLienHe,
                            TieuDe = d.TieuDe,
                            ThoiGianTiepNhan = d.ThoiGianTiepNhan,
                            ThoiGianHenTraKQ = d.ThoiGianHenTraKQ,
                            BoPhanDangXL = d.BoPhanDangXL,
                            KetQua = d.KetQua,
                            DuLieuToaDo = d.DuLieuToaDo,
                            DuLieuHinhHoc = d.DuLieuHinhHoc,
                            SoThua = d.SoThua,
                            ToBanDo = d.ToBanDo
                        });
        var query = complainQuery.Union(denounceQuery);
        return query;
    }
}