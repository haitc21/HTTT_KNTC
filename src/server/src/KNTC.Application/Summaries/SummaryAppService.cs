using KNTC.Complains;
using KNTC.Denounces;
using KNTC.NPOI;
using KNTC.Units;
using KNTC.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Hosting;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Caching;
using Volo.Abp.Domain.Repositories;

namespace KNTC.Summaries;

public class SummaryAppService : KNTCAppService, ISummaryAppService
{
    private readonly ISummaryRepository _summaryRepo;
    private readonly IHostEnvironment _env;
    private readonly IRepository<Unit, int> _unitRepo;
    private readonly IComplainRepository _complainRepo;
    private readonly IDenounceRepository _denounceRepo;
    private readonly IRepository<UserInfo> _userInfoRepo;
    private readonly IDistributedCache<SummaryMapCache, GetSumaryMapDto> _cache;
    private readonly IDistributedCache<SummaryChartDto> _cacheChart;

    public SummaryAppService(ISummaryRepository summaryRepo,
        IHostEnvironment env,
        IRepository<Unit, int> unitRepo,
        IComplainRepository complainRepo,
        IDenounceRepository denounceRepo,
        IRepository<UserInfo> userInfoRepo,
        IDistributedCache<SummaryMapCache, GetSumaryMapDto> cache,
        IDistributedCache<SummaryChartDto> cacheChart = null)
    {
        _summaryRepo = summaryRepo;
        _env = env;
        _unitRepo = unitRepo;
        _complainRepo = complainRepo;
        _denounceRepo = denounceRepo;
        _userInfoRepo = userInfoRepo;
        _cache = cache;
        _cacheChart = cacheChart;
    }

    [AllowAnonymous]
    public async Task<PagedResultDto<SummaryDto>> GetListAsync(GetSummaryListDto input)
    {
        int[]? managedUnitIds = null;
        UserType? userType = null;
        var userId = CurrentUser.Id;
        if (userId == null)
        //var hasPermission = await AuthorizationService.AuthorizeAsync(KNTCPermissions.ComplainsPermission.Default);
        //if (hasPermission.Succeeded == false)
        {
            input.CongKhai = true;
        }
        else
        {
            //Nếu là user trong hệ thống -> Chỉ cho phép xem các đơn vị người đó được quản lý
            var userInfo = await _userInfoRepo.FindAsync(x => x.UserId == CurrentUser.Id);
            if (userInfo != null)
            {
                userType = userInfo.UserType ?? null;
                managedUnitIds = userInfo.ManagedUnitIds;
            }
        }
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = $"{nameof(SummaryDto.ThoiGianTiepNhan)} DESC, {nameof(SummaryDto.MaHoSo)}";
        }
        var query = await _summaryRepo.GetListAsync(input.loaiVuViec,
                                                    input.linhVuc,
                                                    input.LandComplain,
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
                                                    input.CongKhai,
                                                    input.TrangThai,
                                                    input.NguoiNopDon,
                                                    userType,
                                                    managedUnitIds);
        var totalCount = await AsyncExecuter.LongCountAsync(query);
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
        Random random = new Random();
        int randomNumber = random.Next(1, 11);

        var cacheItem = await _cache.GetOrAddAsync(
        input,
        async () => await GetMapFromDBAsync(input),
        () => new DistributedCacheEntryOptions
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(10).AddSeconds(randomNumber),
        });
        return cacheItem.Items;
    }

    private async Task<SummaryMapCache> GetMapFromDBAsync(GetSumaryMapDto input)
    {
        int[] managedUnitIds = null;
        UserType? userType = null;
        var userId = CurrentUser.Id;
        if (userId == null)
        {
            input.CongKhai = true;
        }
        else
        {
            //Nếu là user trong hệ thống -> Chỉ cho phép xem các đơn vị người đó được quản lý
            var userInfo = await _userInfoRepo.FindAsync(x => x.UserId == CurrentUser.Id);
            if (userInfo != null)
            {
                userType = userInfo.UserType.Value;
                managedUnitIds = userInfo.ManagedUnitIds;
            }
        }
        var query = await _summaryRepo.GetListAsync(input.loaiVuViec,
                                                    input.linhVuc,
                                                    input.LandComplain,
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
                                                    input.CongKhai,
                                                    input.TrangThai,
                                                    input.NguoiNopDon,
                                                    userType,
                                                    managedUnitIds);
        var querDtoy = query.Select(x => new SummaryMapDto()
        {
            Id = x.Id,
            LoaiVuViec = x.LoaiVuViec,
            DuLieuToaDo = x.DuLieuToaDo,
            DuLieuHinhHoc = x.DuLieuHinhHoc,
        });
        var entities = await AsyncExecuter.ToListAsync(querDtoy);
        return new SummaryMapCache() { Items = entities };
    }

    public async Task<byte[]> GetLogBookExcelAsync(GetSummaryListDto input)
    {
        int[] managedUnitIds = null;
        UserType? userType = null;
        var userId = CurrentUser.Id;
        if (userId == null)
        {
            input.CongKhai = true;
        }
        else
        {
            //Nếu là user trong hệ thống -> Chỉ cho phép xem các đơn vị người đó được quản lý
            var userInfo = await _userInfoRepo.FindAsync(x => x.UserId == CurrentUser.Id);
            if (userInfo != null)
            {
                userType = userInfo.UserType.Value;
                managedUnitIds = userInfo.ManagedUnitIds;
            }
        }
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = $"{nameof(SummaryDto.ThoiGianTiepNhan)} DESC, {nameof(SummaryDto.MaHoSo)}";
        }
        var query = await _summaryRepo.GetListAsync(input.loaiVuViec,
                                                    input.linhVuc,
                                                    input.LandComplain,
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
                                                    input.CongKhai,
                                                    input.TrangThai,
                                                    input.NguoiNopDon,
                                                    userType,
                                                    managedUnitIds);
        var summaries = await AsyncExecuter.ToListAsync(query);
        if (summaries == null) return null;

        var summaryDto = ObjectMapper.Map<List<Summary>, List<LogBookExcelDto>>(summaries);

        var templatePath = Path.Combine(_env.ContentRootPath, "wwwroot", "Exceltemplate", "LogBook.xlsx");
        IWorkbook wb = ExcelNpoi.WriteExcelByTemp<LogBookExcelDto>(summaryDto, templatePath, 13, 0, true);
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

        /*
        IRow row = sheet.GetCreateRow(5);
        var cell = row.GetCreateCell(4);
        cell.SetCellValue(input.Keyword);
        cell.CellStyle.WrapText = false;
        cell.CellStyle.SetFont(font);

        row = sheet.GetCreateRow(6);
        cell = row.GetCreateCell(4);
        if (input.loaiVuViec == LoaiVuViec.TatCa)
            cell.SetCellValue("Tất cả");
        else if (input.loaiVuViec == LoaiVuViec.KhieuNai)
            cell.SetCellValue("Khiếu nại");
        else
            cell.SetCellValue("Tố cáo");
        cell.CellStyle.WrapText = false;
        cell.CellStyle.SetFont(font);

        row = sheet.GetCreateRow(7);
        cell = row.GetCreateCell(4);
        if (input.linhVuc == LinhVuc.TatCa)
            cell.SetCellValue("Tất cả");
        else if (input.linhVuc == LinhVuc.DatDai)
            cell.SetCellValue("Đất đai");
        else if (input.linhVuc == LinhVuc.KhoangSan)
            cell.SetCellValue("Khoáng sản");
        else if (input.linhVuc == LinhVuc.MoiTruong)
            cell.SetCellValue("Môi trường");
        else
            cell.SetCellValue("Tài nguyên nước");
        cell.CellStyle.WrapText = false;
        cell.CellStyle.SetFont(font);

        //
        row = sheet.GetCreateRow(8);
        cell = row.GetCreateCell(4);
        cell.SetCellValue(input.NguoiNopDon);
        cell.CellStyle.WrapText = false;
        cell.CellStyle.SetFont(font);

        string tenTinh = "Tất cả";
        if (input.maTinhTP != null)
        {
            var tinh = await _unitRepo.GetAsync(x => x.Id == input.maTinhTP);
            tenTinh = tinh.UnitName;
        }
        row = sheet.GetCreateRow(9);
        cell = row.GetCreateCell(4);
        cell.SetCellValue(tenTinh);
        cell.CellStyle.WrapText = false;
        cell.CellStyle.SetFont(font);

        string tenHuyen = "Tất cả";
        if (input.maQuanHuyen != null)
        {
            var huyen = await _unitRepo.GetAsync(x => x.Id == input.maQuanHuyen);
            tenHuyen = huyen.UnitName;
        }
        row = sheet.GetCreateRow(10);
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
        row = sheet.GetCreateRow(11);
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
            row = sheet.GetCreateRow(12);
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
        row = sheet.GetCreateRow(13);
        cell = row.GetCreateCell(4);
        cell.SetCellValue(ketQua);
        cell.CellStyle.WrapText = false;
        cell.CellStyle.SetFont(font);

        string congKhai = "Tất cả";
        if (input.CongKhai.HasValue)
        {
            congKhai = input.CongKhai.Value == true ? "Công khai" : "Không công khai";
        }
        row = sheet.GetCreateRow(14);
        cell = row.GetCreateCell(4);
        cell.SetCellValue(congKhai);
        cell.CellStyle.WrapText = false;
        cell.CellStyle.SetFont(font);
        */
        using (var stream = new MemoryStream())
        {
            wb.Write(stream);
            wb.Close();
            return stream.ToArray();
        }
    }

    public async Task<byte[]> GetReportExcelAsync(GetSummaryListDto input)
    {
        int[] managedUnitIds = null;
        UserType? userType = null;
        var userId = CurrentUser.Id;
        if (userId == null)
        {
            input.CongKhai = true;
        }
        else
        {
            //Nếu là user trong hệ thống -> Chỉ cho phép xem các đơn vị người đó được quản lý
            var userInfo = await _userInfoRepo.FindAsync(x => x.UserId == CurrentUser.Id);
            if (userInfo != null)
            {
                userType = userInfo.UserType.Value;
                managedUnitIds = userInfo.ManagedUnitIds;
            }
        }
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = $"{nameof(SummaryDto.ThoiGianTiepNhan)} DESC, {nameof(SummaryDto.MaHoSo)}";
        }
        var query = await _summaryRepo.GetListAsync(input.loaiVuViec,
                                                    input.linhVuc,
                                                    input.LandComplain,
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
                                                    input.CongKhai,
                                                    input.TrangThai,
                                                    input.NguoiNopDon,
                                                    userType,
                                                    managedUnitIds);
        var summaries = await AsyncExecuter.ToListAsync(query);
        if (summaries == null) return null;
        var summaryDto = ObjectMapper.Map<List<Summary>, List<SummaryExcelDto>>(summaries);

        var templatePath = Path.Combine(_env.ContentRootPath, "wwwroot", "Exceltemplate", "Report.xlsx");
        IWorkbook wb = ExcelNpoi.WriteExcelByTemp<SummaryExcelDto>(summaryDto, templatePath, 18, 0, true);
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

        IRow row = sheet.GetCreateRow(5);
        var cell = row.GetCreateCell(4);
        cell.SetCellValue(input.Keyword);
        cell.CellStyle.WrapText = false;
        cell.CellStyle.SetFont(font);

        row = sheet.GetCreateRow(6);
        cell = row.GetCreateCell(4);
        if (input.loaiVuViec == LoaiVuViec.TatCa)
            cell.SetCellValue("Tất cả");
        else if (input.loaiVuViec == LoaiVuViec.KhieuNai)
            cell.SetCellValue("Khiếu nại");
        else
            cell.SetCellValue("Tố cáo");
        cell.CellStyle.WrapText = false;
        cell.CellStyle.SetFont(font);

        row = sheet.GetCreateRow(7);
        cell = row.GetCreateCell(4);
        if (input.linhVuc == LinhVuc.TatCa)
            cell.SetCellValue("Tất cả");
        else if (input.linhVuc == LinhVuc.DatDai)
            cell.SetCellValue("Đất đai");
        else if (input.linhVuc == LinhVuc.KhoangSan)
            cell.SetCellValue("Khoáng sản");
        else if (input.linhVuc == LinhVuc.MoiTruong)
            cell.SetCellValue("Môi trường");
        else
            cell.SetCellValue("Tài nguyên nước");
        cell.CellStyle.WrapText = false;
        cell.CellStyle.SetFont(font);

        //
        row = sheet.GetCreateRow(8);
        cell = row.GetCreateCell(4);
        cell.SetCellValue(input.NguoiNopDon);
        cell.CellStyle.WrapText = false;
        cell.CellStyle.SetFont(font);

        string tenTinh = "Tất cả";
        if (input.maTinhTP != null)
        {
            var tinh = await _unitRepo.GetAsync(x => x.Id == input.maTinhTP);
            tenTinh = tinh.UnitName;
        }
        row = sheet.GetCreateRow(9);
        cell = row.GetCreateCell(4);
        cell.SetCellValue(tenTinh);
        cell.CellStyle.WrapText = false;
        cell.CellStyle.SetFont(font);

        string tenHuyen = "Tất cả";
        if (input.maQuanHuyen != null)
        {
            var huyen = await _unitRepo.GetAsync(x => x.Id == input.maQuanHuyen);
            tenHuyen = huyen.UnitName;
        }
        row = sheet.GetCreateRow(10);
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
        row = sheet.GetCreateRow(11);
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
            row = sheet.GetCreateRow(12);
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
        row = sheet.GetCreateRow(13);
        cell = row.GetCreateCell(4);
        cell.SetCellValue(ketQua);
        cell.CellStyle.WrapText = false;
        cell.CellStyle.SetFont(font);

        string congKhai = "Tất cả";
        if (input.CongKhai.HasValue)
        {
            congKhai = input.CongKhai.Value == true ? "Công khai" : "Không công khai";
        }
        row = sheet.GetCreateRow(14);
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

    public async Task<byte[]> GetExcelAsync(GetSummaryListDto input)
    {
        int[] managedUnitIds = null;
        UserType? userType = null;
        var userId = CurrentUser.Id;
        if (userId == null)
        {
            input.CongKhai = true;
        }
        else
        {
            //Nếu là user trong hệ thống -> Chỉ cho phép xem các đơn vị người đó được quản lý
            var userInfo = await _userInfoRepo.FindAsync(x => x.UserId == CurrentUser.Id);
            if (userInfo != null)
            {
                userType = userInfo.UserType.Value;
                managedUnitIds = userInfo.ManagedUnitIds;
            }
        }
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = $"{nameof(SummaryDto.ThoiGianTiepNhan)} DESC, {nameof(SummaryDto.MaHoSo)}";
        }
        var query = await _summaryRepo.GetListAsync(input.loaiVuViec,
                                                    input.linhVuc,
                                                    input.LandComplain,
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
                                                    input.CongKhai,
                                                    input.TrangThai,
                                                    input.NguoiNopDon,
                                                    userType,
                                                    managedUnitIds);
        var summaries = await AsyncExecuter.ToListAsync(query);
        if (summaries == null) return null;
        var summaryDto = ObjectMapper.Map<List<Summary>, List<SummaryExcelDto>>(summaries);

        var templatePath = Path.Combine(_env.ContentRootPath, "wwwroot", "Exceltemplate", "Summary.xlsx");
        IWorkbook wb = ExcelNpoi.WriteExcelByTemp<SummaryExcelDto>(summaryDto, templatePath, 16, 0, true);
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

        IRow row = sheet.GetCreateRow(5);
        var cell = row.GetCreateCell(4);
        cell.SetCellValue(input.Keyword);
        cell.CellStyle.WrapText = false;
        cell.CellStyle.SetFont(font);

        row = sheet.GetCreateRow(6);
        cell = row.GetCreateCell(4);
        cell.SetCellValue(input.NguoiNopDon);
        cell.CellStyle.WrapText = false;
        cell.CellStyle.SetFont(font);

        string tenTinh = "Tất cả";
        if (input.maTinhTP != null)
        {
            var tinh = await _unitRepo.GetAsync(x => x.Id == input.maTinhTP);
            tenTinh = tinh.UnitName;
        }
        row = sheet.GetCreateRow(7);
        cell = row.GetCreateCell(4);
        cell.SetCellValue(tenTinh);
        cell.CellStyle.WrapText = false;
        cell.CellStyle.SetFont(font);

        string tenHuyen = "Tất cả";
        if (input.maQuanHuyen != null)
        {
            var huyen = await _unitRepo.GetAsync(x => x.Id == input.maQuanHuyen);
            tenHuyen = huyen.UnitName;
        }
        row = sheet.GetCreateRow(8);
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
        row = sheet.GetCreateRow(9);
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
            row = sheet.GetCreateRow(10);
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
        row = sheet.GetCreateRow(11);
        cell = row.GetCreateCell(4);
        cell.SetCellValue(ketQua);
        cell.CellStyle.WrapText = false;
        cell.CellStyle.SetFont(font);

        string congKhai = "Tất cả";
        if (input.CongKhai.HasValue)
        {
            congKhai = input.CongKhai.Value == true ? "Công khai" : "Không công khai";
        }
        row = sheet.GetCreateRow(12);
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

    //
    public async Task<SummaryChartDto> GetChartAsync()
    {
        Random random = new Random();
        int randomNumber = random.Next(1, 11);
        var result = await _cacheChart.GetOrAddAsync(
        $"{nameof(Summary)}_Chart",
        async () => await GetDataChartFromDbAsync(),
        () => new DistributedCacheEntryOptions
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(10).AddSeconds(randomNumber)
        });
        return result;
    }

    private async Task<SummaryChartDto> GetDataChartFromDbAsync()
    {
        var result = new SummaryChartDto();

        //1.ChartByType
        //LandComplain
        result.LandComplain = await _complainRepo.CountAsync(x => x.LinhVuc == LinhVuc.DatDai);
        result.LandComplain_Dung = await _complainRepo.CountAsync(x => x.LinhVuc == LinhVuc.DatDai && (x.KetQua == LoaiKetQua.Dung || x.KetQua2 == LoaiKetQua.Dung || x.KetQua1 == LoaiKetQua.Dung));
        result.LandComplain_CoDungCoSai = await _complainRepo.CountAsync(x => x.LinhVuc == LinhVuc.DatDai && (x.KetQua == LoaiKetQua.CoDungCoSai || x.KetQua2 == LoaiKetQua.CoDungCoSai || x.KetQua1 == LoaiKetQua.CoDungCoSai));
        result.LandComplain_Sai = await _complainRepo.CountAsync(x => x.LinhVuc == LinhVuc.DatDai && (x.KetQua == LoaiKetQua.Sai || x.KetQua2 == LoaiKetQua.Sai || x.KetQua1 == LoaiKetQua.Sai));
        //result.LandComplain_ChuaCoKQ = result.LandComplain - result.LandComplain_Dung - result.LandComplain_CoDungCoSai - result.LandComplain_Sai;

        //EnviromentComplain
        result.EnviromentComplain = await _complainRepo.CountAsync(x => x.LinhVuc == LinhVuc.MoiTruong);
        result.EnviromentComplain_Dung = await _complainRepo.CountAsync(x => x.LinhVuc == LinhVuc.MoiTruong && (x.KetQua == LoaiKetQua.Dung || x.KetQua2 == LoaiKetQua.Dung || x.KetQua1 == LoaiKetQua.Dung));
        result.EnviromentComplain_CoDungCoSai = await _complainRepo.CountAsync(x => x.LinhVuc == LinhVuc.MoiTruong && (x.KetQua == LoaiKetQua.CoDungCoSai || x.KetQua2 == LoaiKetQua.CoDungCoSai || x.KetQua1 == LoaiKetQua.CoDungCoSai));
        result.EnviromentComplain_Sai = await _complainRepo.CountAsync(x => x.LinhVuc == LinhVuc.MoiTruong && (x.KetQua == LoaiKetQua.Sai || x.KetQua2 == LoaiKetQua.Sai || x.KetQua1 == LoaiKetQua.Sai));
        //result.EnviromentComplain_ChuaCoKQ = result.EnviromentComplain - result.EnviromentComplain_Dung - result.EnviromentComplain_CoDungCoSai - result.EnviromentComplain_Sai;

        //WaterComplain
        result.WaterComplain = await _complainRepo.CountAsync(x => x.LinhVuc == LinhVuc.TaiNguyenNuoc);
        result.WaterComplain_Dung = await _complainRepo.CountAsync(x => x.LinhVuc == LinhVuc.TaiNguyenNuoc && (x.KetQua == LoaiKetQua.Dung || x.KetQua2 == LoaiKetQua.Dung || x.KetQua1 == LoaiKetQua.Dung));
        result.WaterComplain_CoDungCoSai = await _complainRepo.CountAsync(x => x.LinhVuc == LinhVuc.TaiNguyenNuoc && (x.KetQua == LoaiKetQua.CoDungCoSai || x.KetQua2 == LoaiKetQua.CoDungCoSai || x.KetQua1 == LoaiKetQua.CoDungCoSai));
        result.WaterComplain_Sai = await _complainRepo.CountAsync(x => x.LinhVuc == LinhVuc.TaiNguyenNuoc && (x.KetQua == LoaiKetQua.Sai || x.KetQua2 == LoaiKetQua.Sai || x.KetQua1 == LoaiKetQua.Sai));
        //result.WaterComplain_ChuaCoKQ = result.WaterComplain - result.WaterComplain_Dung - result.WaterComplain_CoDungCoSai - result.WaterComplain_Sai;

        //MineralComplain
        result.MineralComplain = await _complainRepo.CountAsync(x => x.LinhVuc == LinhVuc.KhoangSan);
        result.MineralComplain_Dung = await _complainRepo.CountAsync(x => x.LinhVuc == LinhVuc.KhoangSan && (x.KetQua == LoaiKetQua.Dung || x.KetQua2 == LoaiKetQua.Dung || x.KetQua1 == LoaiKetQua.Dung));
        result.MineralComplain_CoDungCoSai = await _complainRepo.CountAsync(x => x.LinhVuc == LinhVuc.KhoangSan && (x.KetQua == LoaiKetQua.CoDungCoSai || x.KetQua2 == LoaiKetQua.CoDungCoSai || x.KetQua1 == LoaiKetQua.CoDungCoSai));
        result.MineralComplain_Sai = await _complainRepo.CountAsync(x => x.LinhVuc == LinhVuc.KhoangSan && (x.KetQua == LoaiKetQua.Sai || x.KetQua2 == LoaiKetQua.Sai || x.KetQua1 == LoaiKetQua.Sai));
        //result.MineralComplain_ChuaCoKQ = result.MineralComplain - result.MineralComplain_Dung - result.MineralComplain_CoDungCoSai - result.MineralComplain_Sai;

        //LandDenounce
        result.LandDenounce = await _denounceRepo.CountAsync(x => x.LinhVuc == LinhVuc.DatDai);
        result.LandDenounce_Dung = await _denounceRepo.CountAsync(x => (x.LinhVuc == LinhVuc.DatDai) && (x.KetQua == LoaiKetQua.Dung));
        result.LandDenounce_CoDungCoSai = await _denounceRepo.CountAsync(x => x.LinhVuc == LinhVuc.DatDai && (x.KetQua == LoaiKetQua.CoDungCoSai));
        result.LandDenounce_Sai = await _denounceRepo.CountAsync(x => x.LinhVuc == LinhVuc.DatDai && (x.KetQua == LoaiKetQua.Sai));
        //result.LandDenounce_ChuaCoKQ = result.LandDenounce - result.LandDenounce_Dung - result.LandDenounce_CoDungCoSai - result.LandDenounce_Sai;

        //EnviromentDenounce
        result.EnviromentDenounce = await _denounceRepo.CountAsync(x => x.LinhVuc == LinhVuc.MoiTruong);
        result.EnviromentDenounce_Dung = await _denounceRepo.CountAsync(x => x.LinhVuc == LinhVuc.MoiTruong && (x.KetQua == LoaiKetQua.Dung));
        result.EnviromentDenounce_CoDungCoSai = await _denounceRepo.CountAsync(x => x.LinhVuc == LinhVuc.MoiTruong && (x.KetQua == LoaiKetQua.CoDungCoSai));
        result.EnviromentDenounce_Sai = await _denounceRepo.CountAsync(x => x.LinhVuc == LinhVuc.MoiTruong && (x.KetQua == LoaiKetQua.Sai));
        //result.EnviromentDenounce_ChuaCoKQ = result.EnviromentDenounce - result.EnviromentDenounce_Dung - result.EnviromentDenounce_CoDungCoSai - result.EnviromentDenounce_Sai;

        //WaterDenounce
        result.WaterDenounce = await _denounceRepo.CountAsync(x => x.LinhVuc == LinhVuc.TaiNguyenNuoc);
        result.WaterDenounce_Dung = await _denounceRepo.CountAsync(x => x.LinhVuc == LinhVuc.TaiNguyenNuoc && (x.KetQua == LoaiKetQua.Dung));
        result.WaterDenounce_CoDungCoSai = await _denounceRepo.CountAsync(x => x.LinhVuc == LinhVuc.TaiNguyenNuoc && (x.KetQua == LoaiKetQua.CoDungCoSai));
        result.WaterDenounce_Sai = await _denounceRepo.CountAsync(x => x.LinhVuc == LinhVuc.TaiNguyenNuoc && (x.KetQua == LoaiKetQua.Sai));
        //result.WaterDenounce_ChuaCoKQ = result.WaterDenounce - result.WaterDenounce_Dung - result.WaterDenounce_CoDungCoSai - result.WaterDenounce_Sai;

        //MineralDenounce
        result.MineralDenounce = await _denounceRepo.CountAsync(x => x.LinhVuc == LinhVuc.KhoangSan);
        result.MineralDenounce_Dung = await _denounceRepo.CountAsync(x => x.LinhVuc == LinhVuc.KhoangSan && (x.KetQua == LoaiKetQua.Dung));
        result.MineralDenounce_CoDungCoSai = await _denounceRepo.CountAsync(x => x.LinhVuc == LinhVuc.KhoangSan && (x.KetQua == LoaiKetQua.CoDungCoSai));
        result.MineralDenounce_Sai = await _denounceRepo.CountAsync(x => x.LinhVuc == LinhVuc.KhoangSan && (x.KetQua == LoaiKetQua.Sai));
        //result.MineralDenounce_ChuaCoKQ = result.MineralDenounce - result.MineralDenounce_Dung - result.MineralDenounce_CoDungCoSai - result.MineralDenounce_Sai;

        //2.ChartByStatus
        var Count = Enum.GetNames(typeof(TrangThai)).Length;
        int i = 0;
        result.ComplainByStatus = new int[Count];
        result.DenounceByStatus = new int[Count];
        foreach (TrangThai t in Enum.GetValues(typeof(TrangThai)))
        {
            result.ComplainByStatus[i] = await _complainRepo.CountAsync(x => x.TrangThai == t);
            result.DenounceByStatus[i] = await _denounceRepo.CountAsync(x => x.TrangThai == t);
            i++;
        }

        return result;
    }
}