using KNTC.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace KNTC.Complains;

public class EfCoreComplainRepository : EfCoreRepository<KNTCDbContext, Complain, Guid>, IComplainRepository
{
    public EfCoreComplainRepository(
       IDbContextProvider<KNTCDbContext> dbContextProvider)
       : base(dbContextProvider)
    {
    }

    public async Task<List<Complain>> GetListAsync(int skipCount,
                                               int maxResultCount,
                                               string sorting,
                                               string keyword,
                                               LinhVuc? linhVuc,
                                               int[]? mangLinhVuc,
                                               LoaiKetQua? ketQua,
                                               int? maTinhTP,
                                               int? maQuanHuyen,
                                               int? maXaPhuongTT,
                                               int? giaiDoan,
                                               DateTime? fromDate,
                                               DateTime? toDate,
                                               bool? congKhai,
                                               bool? luuTru,
                                               TrangThai? trangThai,
                                               string nguoiNopDon,
                                               UserType? userType,
                                               int[]? managedUnitIds
                                               )
    {
        keyword = !keyword.IsNullOrWhiteSpace() ? keyword.ToUpper() : "";
        nguoiNopDon = !nguoiNopDon.IsNullOrWhiteSpace() ? nguoiNopDon.ToUpper() : "";

        var dbSet = await GetDbSetAsync();
        return await dbSet
            .WhereIf(
                !keyword.IsNullOrWhiteSpace(),
                x => x.MaHoSo.ToUpper().Contains(keyword)
                || x.TieuDe.ToUpper().Contains(keyword)
             )
            .WhereIf(
                linhVuc.HasValue,
                x => x.LinhVuc == linhVuc
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
                giaiDoan.HasValue && giaiDoan != 0,
                x => (giaiDoan == 1 && x.NgayKhieuNai1 != null && x.NgayKhieuNai2 == null) ||
                (giaiDoan == 2 && x.NgayKhieuNai2 != null)
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
                luuTru.HasValue,
                x => x.LuuTru == luuTru
             )
             .WhereIf(
                trangThai.HasValue,
                x => x.TrangThai == trangThai
             )
             .WhereIf(
                !string.IsNullOrEmpty(nguoiNopDon),
                x => (x.NguoiNopDon.ToUpper().Contains(nguoiNopDon) || x.CccdCmnd == nguoiNopDon || x.DienThoai == nguoiNopDon)
             )
             .WhereIf(
                (userType == UserType.QuanLyHuyen && !managedUnitIds.IsNullOrEmpty()),
                x => managedUnitIds.Contains(x.MaQuanHuyen) || x.CongKhai
             )
             .WhereIf(
                (userType == UserType.QuanLyXa && !managedUnitIds.IsNullOrEmpty()),
                x => managedUnitIds.Contains(x.MaXaPhuongTT) || x.CongKhai
             )
            .OrderBy(sorting)
            .Skip(skipCount)
            .Take(maxResultCount)
            .ToListAsync();
    }

    public async Task<Complain> FindByMaHoSoAsync(string maHoSo, bool includeDetails = false)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet.FirstOrDefaultAsync(x => x.MaHoSo == maHoSo);
    }

    public async Task<List<Complain>> GetDataExportAsync(
                                               string sorting,
                                               string keyword,
                                               LinhVuc? linhVuc,
                                               LoaiKetQua? ketQua,
                                               int? maTinhTP,
                                               int? maQuanHuyen,
                                               int? maXaPhuongTT,
                                               int? giaiDoan,
                                               DateTime? fromDate,
                                               DateTime? toDate,
                                               bool? CongKhai,
                                               bool? LuuTru,
                                               TrangThai? TrangThai,
                                               string? nguoiNopDon,
                                               UserType? userType,
                                               int[]? managedUnitIds
                                               )
    {
        keyword = !keyword.IsNullOrWhiteSpace() ? keyword.ToUpper() : "";
        nguoiNopDon = !nguoiNopDon.IsNullOrWhiteSpace() ? nguoiNopDon.ToUpper() : "";
        var dbSet = await GetDbSetAsync();
        return await dbSet
            .WhereIf(
                !keyword.IsNullOrWhiteSpace(),
                x => x.MaHoSo.ToUpper().Contains(keyword)
                || x.TieuDe.ToUpper().Contains(keyword)
             )
            .WhereIf(
                linhVuc.HasValue,
                x => x.LinhVuc == linhVuc
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
                giaiDoan.HasValue && giaiDoan != 0,
                x => (giaiDoan == 1 && x.NgayKhieuNai1 != null && x.NgayKhieuNai2 == null) ||
                (giaiDoan == 2 && x.NgayKhieuNai2 != null)
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
                CongKhai.HasValue,
                x => x.CongKhai == CongKhai
             )
             .WhereIf(
                LuuTru.HasValue,
                x => x.LuuTru == LuuTru
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
                (userType == UserType.QuanLyHuyen && !managedUnitIds.IsNullOrEmpty()),
                x => managedUnitIds.Contains(x.MaQuanHuyen) || x.CongKhai
             )
             .WhereIf(
                (userType == UserType.QuanLyXa && !managedUnitIds.IsNullOrEmpty()),
                x => managedUnitIds.Contains(x.MaXaPhuongTT) || x.CongKhai
             )
            .OrderBy(sorting)
            .ToListAsync();
    }
}