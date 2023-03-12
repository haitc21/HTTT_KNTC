using System;
using System.Collections.Generic;
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
                     LinhVuc? linhVuc,
                     LoaiKetQua? ketQua,
                     int? maTinhTp,
                     int? maQuanHuyen,
                     int? maXaPhuongTT,
                     int? giaiDoan,
                     DateTime? FromDate,
                     DateTime? ToDate,
                     bool includeDetails = false);
    Task<Denounce> FindByMaHoSoAsync(string maHoSo, bool includeDetails = false);
}
