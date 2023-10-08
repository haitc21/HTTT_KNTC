using KNTC.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace KNTC.SpatialDatas;

public class EfCoreSpatialDataRepository : EfCoreRepository<KNTCDbContext, SpatialData, int>, ISpatialDataRepository
{
    public EfCoreSpatialDataRepository(IDbContextProvider<KNTCDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }

    public async Task<SpatialData> FindByIdHoSoAsync(Guid idHoSo)
    {
        var dbSet = await GetDbSetAsync();
        var result = await dbSet.FirstOrDefaultAsync(x => x.IdHoSo == idHoSo);
        return result;
    }

    public async Task<List<SpatialData>> GetListByIdHoSoAsync(List<Guid> idHoSo)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet.Where(x => idHoSo.Contains(x.IdHoSo)).ToListAsync();
    }
}