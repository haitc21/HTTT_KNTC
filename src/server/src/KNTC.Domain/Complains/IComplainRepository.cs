using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace KNTC.Complains;

public interface IComplainRepository : IRepository<Complain, Guid>
{
    Task<List<Complain>> GetListAsync(
                     int skipCount,
                     int maxResultCount,
                     string sorting,
                     string keyword,
                     LoaiVuViec? LoaiVuViec,
                     LoaiKetQua? ketQua,
                     bool includeDetails = false);
    Task<Complain> FindByMaHoSoAsync(string maHoSo, bool includeDetails = false);
}
