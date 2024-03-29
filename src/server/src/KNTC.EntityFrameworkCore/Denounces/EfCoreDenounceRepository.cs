﻿using KNTC.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace KNTC.Denounces;

public class EfCoreDenounceRepository : EfCoreRepository<KNTCDbContext, Denounce, Guid>, IDenounceRepository
{
    public EfCoreDenounceRepository(
       IDbContextProvider<KNTCDbContext> dbContextProvider)
       : base(dbContextProvider)
    {
    }

    public async Task<List<Denounce>> GetListAsync(int skipCount,
                                               int maxResultCount,
                                               string sorting,
                                               string keyword,
                                               LinhVuc? linhVuc,
                                               LoaiKetQua? ketQua,
                                               int? maTinhTp,
                                               int? maQuanHuyen,
                                               int? maXaPhuongTT,
                                               DateTime? fromDate,
                                               DateTime? toDate,
                                               bool? CongKhai,
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
                maTinhTp.HasValue,
                x => x.MaTinhTP == maTinhTp
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
                CongKhai.HasValue,
                x => x.CongKhai == CongKhai
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

    public async Task<Denounce> FindByMaHoSoAsync(string maHoSo, bool includeDetails = false)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet.FirstOrDefaultAsync(x => x.MaHoSo == maHoSo);
    }

    public async Task<List<Denounce>> GetDataExportAsync(string sorting,
                                                   string keyword,
                                                   LinhVuc? linhVuc,
                                                   LoaiKetQua? ketQua,
                                                   int? maTinhTP,
                                                   int? maQuanHuyen,
                                                   int? maXaPhuongTT,
                                                   DateTime? fromDate,
                                                   DateTime? toDate,
                                                   bool? CongKhai,
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
            .ToListAsync();
    }
}