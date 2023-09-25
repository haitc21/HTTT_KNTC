using KNTC.Complains;
using KNTC.Denounces;
using KNTC.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EntityFrameworkCore;

namespace KNTC.Summaries;

public class SummaryRepository : ISummaryRepository, ITransientDependency
{
    private readonly IDbContextProvider<KNTCDbContext> _dbContextProvider;

    public SummaryRepository(IDbContextProvider<KNTCDbContext> dbContextProvider)
    {
        _dbContextProvider = dbContextProvider;
    }
    public async Task<IQueryable<Summary>> GetListAsync(LoaiVuViec? loaiVuViec,
                                                    LinhVuc? linhVuc,
                                                    bool landComplain,
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
                                                    TrangThai? TrangThai,
                                                    string nguoiNopDon,
                                                    int? userType,
                                                    int[]? managedUnitIds
                                                    )
    {
        var filter = !keyword.IsNullOrWhiteSpace() ? keyword.ToUpper().Trim() : keyword;
        nguoiNopDon = !nguoiNopDon.IsNullOrEmpty() ? nguoiNopDon.ToUpper().Trim() : "";
        var dbContext = await _dbContextProvider.GetDbContextAsync();
        var query = dbContext.Set<Summary>().AsNoTracking()
            .WhereIf(
                loaiVuViec.HasValue && loaiVuViec != LoaiVuViec.TatCa,
                x => x.LoaiVuViec == loaiVuViec)
            .WhereIf(!landComplain, x => !(x.LinhVuc == LinhVuc.DatDai && x.LoaiVuViec == LoaiVuViec.KhieuNai))
            .WhereIf(!enviromentComplain, x => !(x.LinhVuc == LinhVuc.MoiTruong && x.LoaiVuViec == LoaiVuViec.KhieuNai))
            .WhereIf(!waterComplain, x => !(x.LinhVuc == LinhVuc.TaiNguyenNuoc && x.LoaiVuViec == LoaiVuViec.KhieuNai))
            .WhereIf(!mineralComplain, x => !(x.LinhVuc == LinhVuc.KhoangSan && x.LoaiVuViec == LoaiVuViec.KhieuNai))
            .WhereIf(!landDenounce, x => !(x.LinhVuc == LinhVuc.DatDai && x.LoaiVuViec == LoaiVuViec.ToCao))
            .WhereIf(!enviromentDenounce, x => !(x.LinhVuc == LinhVuc.MoiTruong && x.LoaiVuViec == LoaiVuViec.ToCao))
            .WhereIf(!waterDenounce, x => !(x.LinhVuc == LinhVuc.TaiNguyenNuoc && x.LoaiVuViec == LoaiVuViec.ToCao))
            .WhereIf(!mineralDenounce, x => !(x.LinhVuc == LinhVuc.KhoangSan && x.LoaiVuViec == LoaiVuViec.ToCao))
            .WhereIf(
                !filter.IsNullOrWhiteSpace(),
                x => x.MaHoSo.ToUpper().Contains(filter)
                || x.TieuDe.ToUpper().Contains(filter)
             )
            .WhereIf(
               ketQua.HasValue,
               x => x.KetQua == ketQua
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
               TrangThai.HasValue,
               x => x.TrangThai == TrangThai
            )
            .WhereIf(
               !string.IsNullOrEmpty(nguoiNopDon),
               x => (x.NguoiNopDon.ToUpper().Contains(nguoiNopDon) || x.CccdCmnd == nguoiNopDon || x.DienThoai == nguoiNopDon)
            )
            .WhereIf(
               (userType == 2 && !managedUnitIds.IsNullOrEmpty()),
               x => (managedUnitIds.Contains(x.MaQuanHuyen)) || x.CongKhai
            )
            .WhereIf(
               (userType == 3 && !managedUnitIds.IsNullOrEmpty()),
               x => (managedUnitIds.Contains(x.MaXaPhuongTT)) || x.CongKhai
            );
        return query;
    }
}