﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace KNTC.HoSos;

public interface IHoSoRepository : IRepository<HoSo, Guid>
{
    Task<List<HoSo>> GetListAsync(
                     int skipCount,
                     int maxResultCount,
                     string sorting,
                     string keyword,
                     LoaiVuViec? LoaiVuViec,
                     LinhVuc? LinhVuc,
                     LoaiKetQua? ketQua,
                     bool includeDetails = false);
}
