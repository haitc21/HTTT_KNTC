using KNTC.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
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
    public override async Task<IQueryable<Denounce>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).Include(nameof(Denounce.FileAttachments));
    }
    public async Task<List<Denounce>> GetListAsync(int skipCount,
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

    public async Task<Denounce> FindByMaHoSoAsync(string maHoSo, bool includeDetails = false)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet.IncludeIf(includeDetails, a => a.FileAttachments)
                          .FirstOrDefaultAsync(x => x.MaHoSo == maHoSo);
    }
}
