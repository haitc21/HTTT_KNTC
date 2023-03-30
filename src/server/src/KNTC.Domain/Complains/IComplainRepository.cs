using System;
using System.Collections.Generic;
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
                     LinhVuc? linhVuc,
                     LoaiKetQua? ketQua,
                     int? maTinhTp,
                     int? maQuanHuyen,
                     int? maXaPhuongTT,
                     int? giaiDoan,
                     DateTime? fromDate,
                     DateTime? toDate,
                     bool? congKhai,
                     bool includeDetails = false);
    Task<Complain> FindByMaHoSoAsync(string maHoSo, bool includeDetails = false);
    Task<List<Complain>> GetDataExportAsync(
                 string sorting,
                 LinhVuc? linhVuc,
                 LoaiKetQua? ketQua,
                 int? maTinhTp,
                 int? maQuanHuyen,
                 int? maXaPhuongTT,
                 int? giaiDoan,
                 DateTime? FromDate,
                 DateTime? ToDate);
}
