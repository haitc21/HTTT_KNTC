using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace KNTC.SpatialDatas;

public interface ISpatialDataRepository : IRepository<SpatialData, int>
{
    Task<SpatialData> FindByIdHoSoAsync(Guid idHoSo);

    Task<List<SpatialData>> GetListByIdHoSoAsync(List<Guid> idHoSo);
}