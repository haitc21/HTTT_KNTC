using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using System.Linq.Dynamic.Core;
using KNTC.DocumentTypes;
using KNTC.SpatialDatas;
using KNTC.Units;
using Microsoft.Extensions.Hosting;
using Volo.Abp.Domain.Repositories;
using KNTC.NPOI;
using NPOI.SS.UserModel;
using System.IO;
using Volo.Abp.ObjectMapping;
using Microsoft.AspNetCore.Authorization;
using KNTC.Complains;
using KNTC.Denounces;
using Volo.Abp.Caching;
using KNTC.CategoryUnitTypes;
using Microsoft.Extensions.Caching.Distributed;
using NPOI.POIFS.Properties;

namespace KNTC.Summaries;

public class SummaryAppService : KNTCAppService, ISummaryAppService
{
    private readonly ISummaryRepository _summaryRepo;
    private readonly IHostEnvironment _env;
    private readonly IRepository<Unit, int> _unitRepo;
    private readonly IComplainRepository _complainRepo;
    private readonly IDenounceRepository _denounceRepo;
    private readonly IDistributedCache<SummaryMapCache, GetSumaryMapDto> _cache;
    public SummaryAppService(ISummaryRepository summaryRepo,
        IHostEnvironment env,
        IRepository<Unit, int> unitRepo,
        IComplainRepository complainRepo,
        IDenounceRepository denounceRepo,
        IDistributedCache<SummaryMapCache, GetSumaryMapDto> cache)
    {
        _summaryRepo = summaryRepo;
        _env = env;
        _unitRepo = unitRepo;
        _complainRepo = complainRepo;
        _denounceRepo = denounceRepo;
        _cache = cache;
    }

    public async Task<PagedResultDto<SummaryDto>> GetListAsync(GetSummaryListDto input)
    {
        var userId = CurrentUser.Id;
        if (userId == null)
        {
            input.CongKhai = true;
        }
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = $"{nameof(SummaryDto.MaHoSo)}";
        }
        var query = await _summaryRepo.GetListAsync(input.LandComplain,
                                                    input.EnviromentComplain,
                                                    input.WaterComplain,
                                                    input.MineralComplain,
                                                    input.LandDenounce,
                                                    input.EnviromentDenounce,
                                                    input.WaterDenounce,
                                                    input.MineralDenounce,
                                                    input.Keyword,
                                                    input.KetQua,
                                                    input.maTinhTP,
                                                    input.maQuanHuyen,
                                                    input.maXaPhuongTT,
                                                    input.FromDate,
                                                    input.ToDate,
                                                    input.CongKhai);
        var totalCount = await AsyncExecuter.CountAsync(query);
        query = query.OrderBy(input.Sorting)
                    .Skip(input.SkipCount)
                    .Take(input.MaxResultCount);
        var result = await AsyncExecuter.ToListAsync(query);
        return new PagedResultDto<SummaryDto>(
            totalCount,
            ObjectMapper.Map<List<Summary>, List<SummaryDto>>(result)
       );
    }

    public async Task<List<SummaryMapDto>> GetMapAsync(GetSumaryMapDto input)
    {
        var userId = CurrentUser.Id;
        if (userId == null)
        {
            input.CongKhai = true;
        }
        var cacheItem = await _cache.GetOrAddAsync(
        input,
        async () => await GetDataMap(input),
        () => new DistributedCacheEntryOptions
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddHours(12)
        });
        return cacheItem.Items;
    }
    private async Task<SummaryMapCache> GetDataMap(GetSumaryMapDto input)
    {
        var query = await _summaryRepo.GetListAsync(input.LandComplain,
                                                    input.EnviromentComplain,
                                                    input.WaterComplain,
                                                    input.MineralComplain,
                                                    input.LandDenounce,
                                                    input.EnviromentDenounce,
                                                    input.WaterDenounce,
                                                    input.MineralDenounce,
                                                    input.Keyword,
                                                    input.KetQua,
                                                    input.MaTinhTP,
                                                    input.MaQuanHuyen,
                                                    input.MaXaPhuongTT,
                                                    input.FromDate,
                                                    input.ToDate,
                                                    input.CongKhai);
        var entities = await AsyncExecuter.ToListAsync(query);
        return new SummaryMapCache()
        {
            Items = ObjectMapper.Map<List<Summary>, List<SummaryMapDto>>(entities)
        };
    }

    public async Task<byte[]> GetExcelAsync(GetSummaryListDto input)
    {
        var userId = CurrentUser.Id;
        if (userId == null)
        {
            input.CongKhai = true;
        }
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = $"{nameof(SummaryDto.MaHoSo)}";
        }
        var query = await _summaryRepo.GetListAsync(input.LandComplain,
                                                    input.EnviromentComplain,
                                                    input.WaterComplain,
                                                    input.MineralComplain,
                                                    input.LandDenounce,
                                                    input.EnviromentDenounce,
                                                    input.WaterDenounce,
                                                    input.MineralDenounce,
                                                    input.Keyword,
                                                    input.KetQua,
                                                    input.maTinhTP,
                                                    input.maQuanHuyen,
                                                    input.maXaPhuongTT,
                                                    input.FromDate,
                                                    input.ToDate,
                                                    input.CongKhai);
        var summaries = await AsyncExecuter.ToListAsync(query);
        if (summaries == null) return null;
        var summaryDto = ObjectMapper.Map<List<Summary>, List<SummaryExcelDto>>(summaries);

        var templatePath = Path.Combine(_env.ContentRootPath, "wwwroot", "Exceltemplate", "Summary.xlsx");
        IWorkbook wb = ExcelNpoi.WriteExcelByTemp<SummaryExcelDto>(summaryDto, templatePath, 14, 0, true);
        if (wb == null) return null;

        ISheet sheet = wb.GetSheetAt(0);
        ICellStyle cellStyle = wb.CreateCellStyle();
        cellStyle.BorderLeft = BorderStyle.Thin;
        cellStyle.BorderBottom = BorderStyle.Thin;
        cellStyle.BorderRight = BorderStyle.Thin;
        var font = wb.CreateFont();
        font.IsBold = false;
        font.FontName = "Times New Roman";
        cellStyle.SetFont(font);



        string tenTinh = "Tất cả";
        if (input.maTinhTP != null)
        {
            var tinh = await _unitRepo.GetAsync(x => x.Id == input.maTinhTP);
            tenTinh = tinh.UnitName;
        }
        IRow row = sheet.GetCreateRow(5);
        var cell = row.GetCreateCell(4);
        cell.SetCellValue(tenTinh);
        cell.CellStyle.WrapText = false;
        cell.CellStyle.SetFont(font);

        string tenHuyen = "Tất cả";
        if (input.maQuanHuyen != null)
        {
            var huyen = await _unitRepo.GetAsync(x => x.Id == input.maQuanHuyen);
            tenHuyen = huyen.UnitName;
        }
        row = sheet.GetCreateRow(6);
        cell = row.GetCreateCell(4);
        cell.SetCellValue(tenHuyen);
        cell.CellStyle.WrapText = false;
        cell.CellStyle.SetFont(font);

        string tenXa = "Tất cả";
        if (input.maXaPhuongTT != null)
        {
            var xa = await _unitRepo.GetAsync(x => x.Id == input.maXaPhuongTT);
            tenXa = xa.UnitName;
        }
        row = sheet.GetCreateRow(7);
        cell = row.GetCreateCell(4);
        cell.SetCellValue(tenXa);
        cell.CellStyle.WrapText = false;
        cell.CellStyle.SetFont(font);

        string tuNgay = "";
        if (input.FromDate.HasValue)
        {
            var fromDateGmt7 = TimeZoneInfo.ConvertTimeFromUtc(input.FromDate.Value, TimeZoneInfo.Local);
            tuNgay = fromDateGmt7.ToString(FormatType.FormatDateVN);
        }


        string denNgay = "";
        if (input.ToDate.HasValue)
        {
            var toDateGmt7 = TimeZoneInfo.ConvertTimeFromUtc(input.ToDate.Value, TimeZoneInfo.Local);
            denNgay = toDateGmt7.ToString(FormatType.FormatDateVN);
        }
        if (!tuNgay.IsNullOrEmpty() && !denNgay.IsNullOrEmpty())
        {
            row = sheet.GetCreateRow(8);
            cell = row.GetCreateCell(4);
            cell.SetCellValue(tuNgay + " - " + denNgay);
            cell.CellStyle.WrapText = false;
            cell.CellStyle.SetFont(font);
        }

        string ketQua = "Tất cả";
        if (input.KetQua.HasValue)
        {
            switch (input.KetQua)
            {
                case LoaiKetQua.Dung:
                    ketQua = "Đúng";
                    break;
                case LoaiKetQua.Sai:
                    ketQua = "Sai";
                    break;
                case LoaiKetQua.CoDungCoSai:
                    ketQua = "Có đúng có sai";
                    break;
                default:
                    ketQua = "";
                    break;
            }
        }
        row = sheet.GetCreateRow(9);
        cell = row.GetCreateCell(4);
        cell.SetCellValue(ketQua);
        cell.CellStyle.WrapText = false;
        cell.CellStyle.SetFont(font);

        string congKhai = "Tất cả";
        if (input.CongKhai.HasValue)
        {
            congKhai = input.CongKhai.Value == true ? "Công khai" : "Không công khai";

        }
        row = sheet.GetCreateRow(10);
        cell = row.GetCreateCell(4);
        cell.SetCellValue(congKhai);
        cell.CellStyle.WrapText = false;
        cell.CellStyle.SetFont(font);

        using (var stream = new MemoryStream())
        {
            wb.Write(stream);
            wb.Close();
            return stream.ToArray();
        }
    }
    public async Task<SummaryChartDto> GetChartAsync()
    {
        var result = new SummaryChartDto();

        result.LandComplain = await _complainRepo.CountAsync(x => x.LinhVuc == LinhVuc.DatDai);
        result.EnviromentComplain = await _complainRepo.CountAsync(x => x.LinhVuc == LinhVuc.MoiTruong);
        result.WaterComplain = await _complainRepo.CountAsync(x => x.LinhVuc == LinhVuc.TaiNguyenNuoc);
        result.MineralComplain = await _complainRepo.CountAsync(x => x.LinhVuc == LinhVuc.KhoangSan);

        result.LandDenounce = await _denounceRepo.CountAsync(x => x.LinhVuc == LinhVuc.DatDai);
        result.EnviromentDenounce = await _denounceRepo.CountAsync(x => x.LinhVuc == LinhVuc.MoiTruong);
        result.WaterDenounce = await _denounceRepo.CountAsync(x => x.LinhVuc == LinhVuc.TaiNguyenNuoc);
        result.MineralDenounce = await _denounceRepo.CountAsync(x => x.LinhVuc == LinhVuc.KhoangSan);

        return result;
    }
}
