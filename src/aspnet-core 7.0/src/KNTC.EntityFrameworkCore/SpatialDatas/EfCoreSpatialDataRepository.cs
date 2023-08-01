using KNTC.Complains;
using KNTC.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace KNTC.SpatialDatas;

public class EfCoreSpatialDataRepository : EfCoreRepository<KNTCDbContext, SpatialData, int>, ISpatialDataRepository
{
    public EfCoreSpatialDataRepository(IDbContextProvider<KNTCDbContext> dbContextProvider) 
        : base(dbContextProvider)
    {
    }

    public async Task<SpatialData> GetByIdHoSoAsync(Guid idHoSo)
    {
        var dbSet = await GetDbSetAsync();
        var result =  await dbSet.FirstOrDefaultAsync(x => x.IdHoSo == idHoSo);
        if (result == null)
        {
            throw new UserFriendlyException("Không tìm thấy dữ liệu bản đồ");
        }
        return result;
    }

    public async Task<List<SpatialData>> GetListByIdHoSoAsync(List<Guid> idHoSo)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet.Where(x => idHoSo.Contains(x.IdHoSo)).ToListAsync();
    }
}
