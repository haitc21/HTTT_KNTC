using KNTC.Extenssions;
using KNTC.FileAttachments;
using KNTC.Localization;
using KNTC.NPOI;
using KNTC.Permissions;
using KNTC.RedisCache;
using KNTC.Summaries;
using KNTC.Units;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Hosting;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.BlobStoring;
using Volo.Abp.Data;
using Volo.Abp.Domain.Repositories;

namespace KNTC.Denounces;

public class DenounceAppService : CrudAppService<
            Denounce,
            DenounceDto,
            Guid,
            GetDenounceListDto,
            CreateDenounceDto,
            UpdateDenounceDto>, IDenounceAppService
{
    private readonly IDenounceRepository _denounceRepo;
    private readonly DenounceManager _denounceManager;
    private readonly IRepository<FileAttachment, Guid> _fileAttachmentRepo;
    private readonly FileAttachmentManager _fileAttachmentManager;
    private readonly IBlobContainer<FileAttachmentContainer> _blobContainer;
    private readonly IHostEnvironment _env;
    private readonly IRepository<Unit, int> _unitRepo;
    private readonly IRedisCacheService _cacheService;

    public DenounceAppService(IRepository<Denounce, Guid> repository,
        IDenounceRepository denounceRepo,
        DenounceManager denounceManager,
        IRepository<FileAttachment, Guid> fileAttachmentRepo,
        IBlobContainer<FileAttachmentContainer> blobContainer,
        FileAttachmentManager fileAttachmentManager,
        IHostEnvironment env,
        IRepository<Unit, int> unitRepo,
        IRedisCacheService cacheService = null) : base(repository)
    {
        LocalizationResource = typeof(KNTCResource);

        _denounceRepo = denounceRepo;
        _fileAttachmentRepo = fileAttachmentRepo;
        _denounceManager = denounceManager;
        _blobContainer = blobContainer;
        _fileAttachmentManager = fileAttachmentManager;
        _env = env;
        _unitRepo = unitRepo;
        _cacheService = cacheService;
    }

    [AllowAnonymous]
    public override async Task<PagedResultDto<DenounceDto>> GetListAsync(GetDenounceListDto input)
    {
        var hasPermission = await AuthorizationService.AuthorizeAsync(KNTCPermissions.DenouncesPermission.Default);
        if (hasPermission.Succeeded == false)
        {
            input.CongKhai = true;
        }
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = $"{nameof(Denounce.ThoiGianTiepNhan)} DESC, {nameof(Denounce.MaHoSo)}";
        }
        string filter = !input.Keyword.IsNullOrEmpty() ? input.Keyword.ToUpper().Trim() : "";
        string nguoiNopDon = !input.NguoiNopDon.IsNullOrEmpty() ? input.NguoiNopDon.ToUpper().Trim() : "";
        var denounces = await _denounceRepo.GetListAsync(
            input.SkipCount,
            input.MaxResultCount,
            input.Sorting,
            filter,
            input.LinhVuc,
            input.KetQua,
            input.maTinhTP,
            input.maQuanHuyen,
            input.maXaPhuongTT,
            input.FromDate,
            input.ToDate,
            input.CongKhai,
            nguoiNopDon
        );

        var totalCount = await _denounceRepo.CountAsync(
                       x => (input.Keyword.IsNullOrEmpty()
                           || (x.MaHoSo.ToUpper().Contains(filter) || x.TieuDe.ToUpper().Contains(filter)))
                       && (!input.LinhVuc.HasValue || x.LinhVuc == input.LinhVuc)
                       && (!input.KetQua.HasValue || x.KetQua == input.KetQua)
                       && (!input.maTinhTP.HasValue || x.MaTinhTP == input.maTinhTP)
                       && (!input.maQuanHuyen.HasValue || x.MaQuanHuyen == input.maQuanHuyen)
                       && (!input.maXaPhuongTT.HasValue || x.MaXaPhuongTT == input.maXaPhuongTT)
                       && (!input.FromDate.HasValue || x.ThoiGianTiepNhan >= input.FromDate)
                       && (!input.ToDate.HasValue || x.ThoiGianTiepNhan <= input.ToDate)
                       && (!input.CongKhai.HasValue || x.CongKhai == input.CongKhai)
                       && (input.NguoiNopDon.IsNullOrEmpty()
                           || (x.NguoiNopDon.ToUpper().Contains(nguoiNopDon) || x.CccdCmnd == nguoiNopDon || x.DienThoai == nguoiNopDon))
                       );

        return new PagedResultDto<DenounceDto>(
        totalCount,
            ObjectMapper.Map<List<Denounce>, List<DenounceDto>>(denounces)
        );
    }

    [Authorize(KNTCPermissions.DenouncesPermission.Create)]
    public override async Task<DenounceDto> CreateAsync(CreateDenounceDto input)
    {
        var denounce = await _denounceManager.CreateAsync(maHoSo: input.MaHoSo,
                                                  linhVuc: input.LinhVuc,
                                                  tieuDe: input.TieuDe,
                                                  nguoiNopDon: input.NguoiNopDon,
                                                  cccdCmnd: input.CccdCmnd,
                                                  //ngayCapCccdCmnd: input.NgayCapCccdCmnd,
                                                  //noiCapCccdCmnd: input.NoiCapCccdCmnd,
                                                  ngaySinh: input.NgaySinh,
                                                  DienThoai: input.DienThoai,
                                                  email: input.Email,
                                                  diaChiThuongTru: input.DiaChiThuongTru,
                                                  diaChiLienHe: input.DiaChiLienHe,
                                                  maTinhTP: input.maTinhTP,
                                                  maQuanHuyen: input.maQuanHuyen,
                                                  maXaPhuongTT: input.maXaPhuongTT,
                                                  thoiGianTiepNhan: input.ThoiGianTiepNhan,
                                                  thoiGianHenTraKQ: input.ThoiGianHenTraKQ,
                                                  noiDungVuViec: input.NoiDungVuViec,
                                                  nguoiBiToCao: input.NguoiBiToCao,
                                                  boPhanDangXL: input.BoPhanDangXL,
                                                  soThua: input.SoThua,
                                                  toBanDo: input.ToBanDo,
                                                  dienTich: input.DienTich,
                                                  loaiDat: input.LoaiDat,
                                                  diaChiThuaDat: input.DiaChiThuaDat,
                                                  tinhThuaDat: input.TinhThuaDat,
                                                  huyenThuaDat: input.HuyenThuaDat,
                                                  xaThuaDat: input.XaThuaDat,
                                                  duLieuToaDo: input.DuLieuToaDo,
                                                  duLieuHinhHoc: input.DuLieuHinhHoc,
                                                  GhiChu: input.GhiChu,
                                                  ngayGQTC: input.NgayGQTC,
                                                  nguoiGQTC: input.NguoiGQTC,
                                                  quyerDinhThuLyGQTC: input.QuyerDinhThuLyGQTC,
                                                  ngayQDGQTC: input.NgayQDGQTC,
                                                  quyetDinhDinhChiGQTC: input.QuyetDinhDinhChiGQTC,
                                                  giaHanGQTC1: input.GiaHanGQTC1,
                                                  giaHanGQTC2: input.GiaHanGQTC2,
                                                  soVBKLNDTC: input.SoVBKLNDTC,
                                                  ngayNhanTBKQXLKLTC: input.NgayNhanTBKQXLKLTC,
                                                  ketQua: input.KetQua,
                                                  congKhai: input.CongKhai
                                                  );

        await _denounceRepo.InsertAsync(denounce);
        var result = ObjectMapper.Map<Denounce, DenounceDto>(denounce);
        if (input.FileAttachments != null && input.FileAttachments.Count > 0)
        {
            foreach (var item in input.FileAttachments)
            {
                var fileAttach = await _fileAttachmentManager.CreateAsync(loaiVuViec: LoaiVuViec.ToCao,
                                                                     complainId: null,
                                                                     denounceId: denounce.Id,
                                                                     giaiDoan: item.GiaiDoan,
                                                                     tenTaiLieu: item.TenTaiLieu,
                                                                     hinhThuc: item.HinhThuc,
                                                                     thoiGianBanHanh: item.ThoiGianBanHanh,
                                                                     ngayNhan: item.NgayNhan,
                                                                     thuTuButLuc: item.ThuTuButLuc,
                                                                     noiDungChinh: item.NoiDungChinh,
                                                                     fileName: item.FileName,
                                                                     contentType: item.ContentType,
                                                                     contentLength: item.ContentLength.Value,
                                                                     congKhai: item.CongKhai);
                await _fileAttachmentRepo.InsertAsync(fileAttach);
                result.FileAttachments.Add(ObjectMapper.Map<FileAttachment, FileAttachmentDto>(fileAttach));
            }
        }
        await _cacheService.DeleteCacheKeysSContainAsync(nameof(Summary));
        return result;
    }

    [Authorize(KNTCPermissions.DenouncesPermission.Default)]
    public override async Task<DenounceDto> UpdateAsync(Guid id, UpdateDenounceDto input)
    {
        var denounce = await _denounceRepo.GetAsync(id, false);
        denounce.SetConcurrencyStampIfNotNull(input.ConcurrencyStamp);
        await _denounceManager.UpdateAsync(denounce: denounce,
                                           maHoSo: input.MaHoSo,
                                          linhVuc: input.LinhVuc,
                                          tieuDe: input.TieuDe,
                                          nguoiNopDon: input.NguoiNopDon,
                                          cccdCmnd: input.CccdCmnd,
                                          //ngayCapCccdCmnd: input.NgayCapCccdCmnd,
                                          //noiCapCccdCmnd: input.NoiCapCccdCmnd,
                                          ngaySinh: input.NgaySinh,
                                          DienThoai: input.DienThoai,
                                          email: input.Email,
                                          diaChiThuongTru: input.DiaChiThuongTru,
                                          diaChiLienHe: input.DiaChiLienHe,
                                          maTinhTP: input.maTinhTP,
                                          maQuanHuyen: input.maQuanHuyen,
                                          maXaPhuongTT: input.maXaPhuongTT,
                                          thoiGianTiepNhan: input.ThoiGianTiepNhan,
                                          thoiGianHenTraKQ: input.ThoiGianHenTraKQ,
                                          noiDungVuViec: input.NoiDungVuViec,
                                          nguoiBiToCao: input.NguoiBiToCao,
                                          boPhanDangXL: input.BoPhanDangXL,
                                          soThua: input.SoThua,
                                          toBanDo: input.ToBanDo,
                                          dienTich: input.DienTich,
                                          loaiDat: input.LoaiDat,
                                          diaChiThuaDat: input.DiaChiThuaDat,
                                          tinhThuaDat: input.TinhThuaDat,
                                          huyenThuaDat: input.HuyenThuaDat,
                                          xaThuaDat: input.XaThuaDat,
                                          duLieuToaDo: input.DuLieuToaDo,
                                          duLieuHinhHoc: input.DuLieuHinhHoc,
                                          GhiChu: input.GhiChu,
                                          ngayGQTC: input.NgayGQTC,
                                          nguoiGQTC: input.NguoiGQTC,
                                          quyerDinhThuLyGQTC: input.QuyerDinhThuLyGQTC,
                                          ngayQDGQTC: input.NgayQDGQTC,
                                          quyetDinhDinhChiGQTC: input.QuyetDinhDinhChiGQTC,
                                          giaHanGQTC1: input.GiaHanGQTC1,
                                          giaHanGQTC2: input.GiaHanGQTC2,
                                          soVBKLNDTC: input.SoVBKLNDTC,
                                          ngayNhanTBKQXLKLTC: input.NgayNhanTBKQXLKLTC,
                                          ketQua: input.KetQua,
                                          congKhai: input.CongKhai);
        await _denounceRepo.UpdateAsync(denounce);
        await _cacheService.DeleteCacheKeysSContainAsync(nameof(Summary));
        return ObjectMapper.Map<Denounce, DenounceDto>(denounce);
    }

    [Authorize(KNTCPermissions.DenouncesPermission.Delete)]
    public override async Task DeleteAsync(Guid id)
    {
        var idFileAttachs = (await _fileAttachmentRepo.GetListAsync(x => id == x.DenounceId)).Select(x => x.Id);
        await _fileAttachmentRepo.DeleteManyAsync(idFileAttachs);
        foreach (var item in idFileAttachs)
        {
            await _blobContainer.DeleteAsync(item.ToString());
        }
        await _cacheService.DeleteCacheKeysSContainAsync(nameof(Summary));
        await _denounceRepo.DeleteAsync(id);
    }

    [Authorize(KNTCPermissions.DenouncesPermission.Delete)]
    public async Task DeleteMultipleAsync(IEnumerable<Guid> ids)
    {
        var idFileAttachs = (await _fileAttachmentRepo.GetListAsync(x => ids.Any(i => i == x.DenounceId))).Select(x => x.Id);
        await _fileAttachmentRepo.DeleteManyAsync(idFileAttachs);
        foreach (var item in idFileAttachs)
        {
            await _blobContainer.DeleteAsync(item.ToString());
        }
        await _cacheService.DeleteCacheKeysSContainAsync(nameof(Summary));
        await _denounceRepo.DeleteManyAsync(ids);
    }

    //[Authorize(KNTCPermissions.DenouncesPermission.Default)]
    public async Task<byte[]> GetExcelAsync(GetDenounceListDto input)
    {
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = $"{nameof(Denounce.ThoiGianTiepNhan)} DESC, {nameof(Denounce.MaHoSo)}";
        }
        var denounces = await _denounceRepo.GetDataExportAsync(
            input.Sorting,
            input.Keyword,
            input.LinhVuc,
            input.KetQua,
            input.maTinhTP,
            input.maQuanHuyen,
            input.maXaPhuongTT,
            input.FromDate,
            input.ToDate,
            input.CongKhai,
            input.NguoiNopDon
        );
        if (denounces == null) return null;

        var denounceDto = ObjectMapper.Map<List<Denounce>, List<DenounceExcelDto>>(denounces);

        var templatePath = Path.Combine(_env.ContentRootPath, "wwwroot", "Exceltemplate", "Denounce.xlsx");
        IWorkbook wb = ExcelNpoi.WriteExcelByTemp<DenounceExcelDto>(denounceDto, templatePath, 17, 0, true);
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

        IRow row = sheet.GetCreateRow(4);
        var cell = row.GetCreateCell(4);
        cell.SetCellValue(input.Keyword);
        cell.CellStyle.WrapText = false;
        cell.CellStyle.SetFont(font);

        row = sheet.GetCreateRow(5);
        cell = row.GetCreateCell(4);
        cell.SetCellValue(input.NguoiNopDon);
        cell.CellStyle.WrapText = false;
        cell.CellStyle.SetFont(font);

        string linhVuc = "Tất cả";
        if (input.LinhVuc.HasValue)
        {
            linhVuc = input.LinhVuc.Value.ToVNString();
        }
        row = sheet.GetCreateRow(6);
        cell = row.GetCreateCell(4);
        cell.SetCellValue(linhVuc);
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
            ketQua = input.KetQua.Value.ToVNString();
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
}