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
                                               LoaiVuViec? LoaiVuViec,
                                               LoaiKetQua? ketQua,
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
                LoaiVuViec.HasValue,
                x => x.LoaiVuViec == LoaiVuViec
             )
             .WhereIf(
                ketQua.HasValue,
                table => table.KetQua == ketQua
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
