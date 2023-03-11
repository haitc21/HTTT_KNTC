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
    public override async Task<IQueryable<Complain>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).Include(nameof(Complain.FileAttachments));
    }
    public async Task<List<Complain>> GetListAsync(int skipCount,
                                               int maxResultCount,
                                               string sorting,
                                               string keyword,
                                               LinhVuc? linhVuc,
                                               LoaiKetQua? ketQua,
                                               int? maTinhTp,
                                               int? maQuanHuyen,
                                               int? maXaPhuongTT,
                                               int? giaiDoan,
                                               DateTime? fromDate,
                                               DateTime? toDate,
                                               bool includeDetails = false)
    {
        var filter = !keyword.IsNullOrWhiteSpace() ? keyword.ToUpper() : keyword;
        var dbSet = await GetDbSetAsync();
        return await dbSet
            .IncludeIf(includeDetails, a => a.FileAttachments)
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
                giaiDoan.HasValue,
                x => (giaiDoan == 1 && x.ngayKhieuNai2 == null) || (giaiDoan == 1 && x.ngayKhieuNai2 != null)
             )
             .WhereIf(
                fromDate.HasValue,
                x => x.ThoiGianTiepNhan >= fromDate
             )
             .WhereIf(
                toDate.HasValue,
                x => x.ThoiGianTiepNhan <= toDate
             )
            .OrderBy(sorting)
            .Skip(skipCount)
            .Take(maxResultCount)
            .ToListAsync();
    }

    public async Task<Complain> FindByMaHoSoAsync(string maHoSo, bool includeDetails = false)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet.IncludeIf(includeDetails, a => a.FileAttachments)
                          .FirstOrDefaultAsync(x => x.MaHoSo == maHoSo);
    }
}
