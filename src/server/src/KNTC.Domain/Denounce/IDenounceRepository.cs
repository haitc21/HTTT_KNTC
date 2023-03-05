using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace KNTC.Denounces;

public interface IDenounceRepository : IRepository<Denounce, Guid>
{
    Task<List<Denounce>> GetListAsync(
                     int skipCount,
                     int maxResultCount,
                     string sorting,
                     string keyword,
                     LoaiVuViec? LoaiVuViec,
                     LoaiKetQua? ketQua,
                     bool includeDetails = false);
    Task<Denounce> FindByMaHoSoAsync(string maHoSo, bool includeDetails = false);
}
