﻿using KNTC.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NPOI.OpenXmlFormats.Vml.Office;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
                                               string nguoiNopDon)
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
                !string.IsNullOrEmpty(nguoiNopDon),
                x => (x.NguoiNopDon.ToUpper().Contains(nguoiNopDon) || x.CccdCmnd == nguoiNopDon || x.DienThoai == nguoiNopDon)
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
                                               string nguoiNopDon)
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
                !string.IsNullOrEmpty(nguoiNopDon),
                x => (x.NguoiNopDon.ToUpper().Contains(nguoiNopDon) || x.CccdCmnd == nguoiNopDon || x.DienThoai == nguoiNopDon)
             )
            .OrderBy(sorting)
            .ToListAsync();
    }
}