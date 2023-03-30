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
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace KNTC.Summaries;

public class SummaryRepository :  ISummaryRepository
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
                                                        int? maTinhTp,
                                                        int? maQuanHuyen,
                                                        int? maXaPhuongTT,
                                                        DateTime? fromDate,
                                                        DateTime? toDate)
    {

        var filter = !keyword.IsNullOrWhiteSpace() ? keyword.ToUpper() : keyword;
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
                        .Select(c => new Summary()
                        {
                            Id = c.Id,
                            MaHoSo = c.MaHoSo,
                            LoaiVuViec = LoaiVuViec.KhieuNai,
                            LinhVuc = c.LinhVuc,
                            NguoiNopDon = c.NguoiDeNghi,
                            TieuDe = c.TieuDe,
                            ThoiGianTiepNhan = c.ThoiGianTiepNhan,
                            ThoiGianHenTraKQ = c.ThoiGianHenTraKQ,
                            BoPhanDangXL = c.BoPhanDangXL,
                            KetQua = c.KetQua
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
                        .Select(d => new Summary()
                        {
                            Id = d.Id,
                            MaHoSo = d.MaHoSo,
                            LoaiVuViec = LoaiVuViec.ToCao,
                            LinhVuc = d.LinhVuc,
                            NguoiNopDon = d.NguoiToCao,
                            TieuDe = d.TieuDe,
                            ThoiGianTiepNhan = d.ThoiGianTiepNhan,
                            ThoiGianHenTraKQ = d.ThoiGianHenTraKQ,
                            BoPhanDangXL = d.BoPhanDangXL,
                            KetQua = d.KetQua
                        });
        var query = complainQuery.Union(denounceQuery);
        return query;
    }
}
