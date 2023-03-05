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

namespace KNTC.Complain;

public class EfCoreHoSoRepository : EfCoreRepository<KNTCDbContext, HoSo, Guid>, IHoSoRepository
{
    public EfCoreHoSoRepository(
       IDbContextProvider<KNTCDbContext> dbContextProvider)
       : base(dbContextProvider)
    {
    }
    public override async Task<IQueryable<HoSo>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).Include(nameof(HoSo.KQGQHoSos))
                .Include(nameof(HoSo.TepDinhKemHoSos));
    }
    public async Task<List<HoSo>> GetListAsync(int skipCount,
                                               int maxResultCount,
                                               string sorting,
                                               string keyword,
                                               LoaiVuViec? LoaiVuViec,
                                               LinhVuc? LinhVuc,
                                               LoaiKetQua? ketQua,
                                               bool includeDetails = false)
    {
        var filter = !keyword.IsNullOrWhiteSpace() ? keyword.ToUpper() : keyword;
        var dbSet = await GetDbSetAsync();
        return await dbSet.IncludeIf(includeDetails, a => a.KQGQHoSos)
            .IncludeIf(includeDetails, a => a.TepDinhKemHoSos)
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
                LinhVuc.HasValue,
                table => table.LinhVuc == LinhVuc
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

    public async Task<HoSo> FindByMaHoSoAsync(string maHoSo, bool includeDetails = false)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet.IncludeIf(includeDetails, a => a.KQGQHoSos)
                          .IncludeIf(includeDetails, a => a.TepDinhKemHoSos)
                          .FirstOrDefaultAsync(x => x.MaHoSo == maHoSo);
    }
}
