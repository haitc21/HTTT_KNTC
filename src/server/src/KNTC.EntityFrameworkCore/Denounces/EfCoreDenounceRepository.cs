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
                                               bool includeDetails = false)
    {
        var filter = !keyword.IsNullOrWhiteSpace() ? keyword.ToUpper() : keyword;
        var dbSet = await GetDbSetAsync();
        return await dbSet
            .WhereIf(
                !filter.IsNullOrWhiteSpace(),
                x => x.MaHoSo.ToUpper().Contains(filter)
                || x.TieuDe.ToUpper().Contains(filter)
             )
            .WhereIf(
                linhVuc.HasValue,
                x => x.LinhVuc == linhVuc
             )
             .WhereIf(
                ketQua.HasValue,
                x => (ketQua != LoaiKetQua.ChuaCoKQ && x.KetQua == ketQua) || x.KetQua == null
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
                                                   LinhVuc? linhVuc,
                                                   LoaiKetQua? ketQua,
                                                   int? maTinhTp,
                                                   int? maQuanHuyen,
                                                   int? maXaPhuongTT,
                                                   DateTime? fromDate,
                                                   DateTime? toDate,
                                                   bool? CongKhai)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet
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
            .OrderBy(sorting)
            .ToListAsync();
    }
}
