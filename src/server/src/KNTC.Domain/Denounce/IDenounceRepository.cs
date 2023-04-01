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
                     DateTime? fromDate,
                     DateTime? toDate,
                     bool? CongKhai,
                     bool includeDetails = false);
    Task<Denounce> FindByMaHoSoAsync(string maHoSo, bool includeDetails = false);

    Task<List<Denounce>> GetDataExportAsync(
                 string sorting,
                 LinhVuc? linhVuc,
                 LoaiKetQua? ketQua,
                 int? maTinhTp,
                 int? maQuanHuyen,
                 int? maXaPhuongTT,
                 DateTime? FromDate,
                 DateTime? ToDate,
                 bool? CongKhai);
}
