using KNTC.Complains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace KNTC.SpatialDatas;

public interface ISpatialDataRepository : IRepository<SpatialData, int>
{
    Task<SpatialData> GetByIdHoSoAsync(Guid idHoSo);
    Task<List<SpatialData>> GetListByIdHoSoAsync(List<Guid> idHoSo);
}
