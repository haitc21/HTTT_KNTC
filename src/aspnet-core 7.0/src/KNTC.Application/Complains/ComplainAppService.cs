using KNTC.Extenssions;
using KNTC.FileAttachments;
using KNTC.Helpers;
using KNTC.Histories;
using KNTC.Localization;
using KNTC.NPOI;
using KNTC.Permissions;
using KNTC.RedisCache;
using KNTC.SpatialDatas;
using KNTC.Summaries;
using KNTC.Units;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Hosting;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.BlobStoring;
using Volo.Abp.Data;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Distributed;

namespace KNTC.Complains;

public class ComplainAppService : CrudAppService<
            Complain,
            ComplainDto,
            Guid,
            GetComplainListDto,
            CreateComplainDto,
            UpdateComplainDto>, IComplainAppService
{
    private readonly IComplainRepository _complainRepo;
    private readonly ComplainManager _complainManager;
    private readonly IRepository<FileAttachment, Guid> _fileAttachmentRepo;
    private readonly FileAttachmentManager _fileAttachmentManager;
    private readonly IBlobContainer<FileAttachmentContainer> _blobContainer;
    private readonly IHostEnvironment _env;
    private readonly IRepository<Unit, int> _unitRepo;
    private readonly IRedisCacheService _cacheService;
    private readonly IDistributedEventBus _distributedEventBus;
    private readonly ISpatialDataRepository _spatialDataRepo;
    private readonly IRepository<History, int> _historyRepo;

    public ComplainAppService(IRepository<Complain, Guid> repository,
        IComplainRepository complainRepo,
        ComplainManager complainManager,
        IRepository<FileAttachment, Guid> fileAttachmentRepo,
        IBlobContainer<FileAttachmentContainer> blobContainer,
        FileAttachmentManager fileAttachmentManager,
        IHostEnvironment env,
        IRepository<Unit, int> unitRepo,
        IRedisCacheService cacheService,
        IDistributedEventBus distributedEventBus,
        ISpatialDataRepository spatialDataRepo,
        IRepository<History, int> historyRepo) : base(repository)
    {
        LocalizationResource = typeof(KNTCResource);

        _complainRepo = complainRepo;
        _fileAttachmentRepo = fileAttachmentRepo;
        _complainManager = complainManager;
        _blobContainer = blobContainer;
        _fileAttachmentManager = fileAttachmentManager;
        _env = env;
        _unitRepo = unitRepo;
        _unitRepo = unitRepo;
        _cacheService = cacheService;
        _distributedEventBus = distributedEventBus;
        _spatialDataRepo = spatialDataRepo;
        _historyRepo = historyRepo;
    }

    [AllowAnonymous]
    public override async Task<ComplainDto> GetAsync(Guid id)
    {
        var complain = await _complainRepo.GetAsync(id);
        var hasPermission = await AuthorizationService.AuthorizeAsync(KNTCPermissions.ComplainsPermission.Default);
        if (!hasPermission.Succeeded && !complain.CongKhai)
        {
            throw new UserFriendlyException("Bạn không có quyền xem thông tin này");
        }
        var result = ObjectMapper.Map<Complain, ComplainDto>(complain);
        var spatialData = await _spatialDataRepo.FindByIdHoSoAsync(id);
        if (spatialData != null)
        {
            result.DuLieuToaDo = SpatialDataHelper.ConvertPointToString(spatialData.Point);
            if (spatialData.Geometry != null)
            {
                result.DuLieuHinhHoc = SpatialDataHelper.ConvertGeoDataToJson(new GeoJsonData()
                {
                    type = spatialData.Type,
                    properties = spatialData.Properties,
                    geometry = spatialData.Geometry
                });
            }

        }
        return result;
    }

    [AllowAnonymous]
    public override async Task<PagedResultDto<ComplainDto>> GetListAsync(GetComplainListDto input)
    {
        var hasPermission = await AuthorizationService.AuthorizeAsync(KNTCPermissions.ComplainsPermission.Default);
        if (hasPermission.Succeeded == false)
        {
            input.CongKhai = true;
        }
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = $"{nameof(Complain.ThoiGianTiepNhan)} DESC, {nameof(Complain.MaHoSo)}";
        }
        string filter = !input.Keyword.IsNullOrEmpty() ? input.Keyword.ToUpper().Trim() : "";
        string nguoiNopDon = !input.NguoiNopDon.IsNullOrEmpty() ? input.NguoiNopDon.ToUpper().Trim() : "";
        var complains = await _complainRepo.GetListAsync(
            input.SkipCount,
            input.MaxResultCount,
            input.Sorting,
            filter,
            input.LinhVuc,
            input.mangLinhVuc,
            input.KetQua,
            input.maTinhTP,
            input.maQuanHuyen,
            input.maXaPhuongTT,
            input.GiaiDoan,
            input.FromDate,
            input.ToDate,
            input.CongKhai,
            nguoiNopDon
        );

        var totalCount = await _complainRepo.CountAsync(
                x => (input.Keyword.IsNullOrEmpty()
                    || (x.MaHoSo.ToUpper().Contains(filter) || x.TieuDe.ToUpper().Contains(filter)))
                && (!input.LinhVuc.HasValue || x.LinhVuc == input.LinhVuc)
                && (input.mangLinhVuc.IsNullOrEmpty() || input.mangLinhVuc.Contains((int)x.LinhVuc))
                && (!input.KetQua.HasValue || x.KetQua == input.KetQua)
                && (!input.maTinhTP.HasValue || x.MaTinhTP == input.maTinhTP)
                && (!input.maQuanHuyen.HasValue || x.MaQuanHuyen == input.maQuanHuyen)
                && (!input.maXaPhuongTT.HasValue || x.MaXaPhuongTT == input.maXaPhuongTT)
                && (!input.GiaiDoan.HasValue || input.GiaiDoan == 0 ||
                    (input.GiaiDoan == 1 && x.NgayKhieuNai1 != null && x.NgayKhieuNai2 == null) ||
                    (input.GiaiDoan == 2 && x.NgayKhieuNai2 != null))
                && (!input.FromDate.HasValue || x.ThoiGianTiepNhan >= input.FromDate)
                && (!input.ToDate.HasValue || x.ThoiGianTiepNhan <= input.ToDate)
                && (!input.CongKhai.HasValue || x.CongKhai == input.CongKhai)
                && (input.NguoiNopDon.IsNullOrEmpty()
                    || (x.NguoiNopDon.ToUpper().Contains(nguoiNopDon) || x.CccdCmnd == nguoiNopDon || x.DienThoai == nguoiNopDon))
                );

        return new PagedResultDto<ComplainDto>(
        totalCount,
            ObjectMapper.Map<List<Complain>, List<ComplainDto>>(complains)
        );
    }

    [Authorize(KNTCPermissions.ComplainsPermission.Create)]
    public override async Task<ComplainDto> CreateAsync(CreateComplainDto input)
    {
        var complain = await _complainManager.CreateAsync(maHoSo: input.MaHoSo.Trim(),
                                                  linhVuc: input.LinhVuc,
                                                  tieuDe: input.TieuDe.Trim(),
                                                  nguoiNopDon: input.NguoiNopDon.Trim(),
                                                  cccdCmnd: input.CccdCmnd.Trim(),
                                                  //ngayCapCccdCmnd: input.NgayCapCccdCmnd,
                                                  //noiCapCccdCmnd: input.NoiCapCccdCmnd,
                                                  ngaySinh: input.NgaySinh,
                                                  DienThoai: input.DienThoai.Trim(),
                                                  email: input.Email,
                                                  diaChiThuongTru: input.DiaChiThuongTru.Trim(),
                                                  diaChiLienHe: input.DiaChiLienHe.Trim(),
                                                  maTinhTP: input.maTinhTP,
                                                  maQuanHuyen: input.maQuanHuyen,
                                                  maXaPhuongTT: input.maXaPhuongTT,
                                                  thoiGianTiepNhan: input.ThoiGianTiepNhan,
                                                  thoiGianyHenTraKQ: input.ThoiGianHenTraKQ,
                                                  noiDungVuViec: input.NoiDungVuViec.Trim(),
                                                  boPhanDangXL: input.BoPhanDangXL.Trim(),
                                                  soThua: input.SoThua.Trim(),
                                                  toBanDo: input.ToBanDo.Trim(),
                                                  dienTich: input.DienTich,
                                                  loaiDat: input.LoaiDat,
                                                  diaChiThuaDat: input.DiaChiThuaDat.Trim(),
                                                  tinhThuaDat: input.tinhThuaDat,
                                                  huyenThuaDat: input.huyenThuaDat,
                                                  xaThuaDat: input.xaThuaDat,
                                                  duLieuToaDo: input.DuLieuToaDo,
                                                  duLieuHinhHoc: input.DuLieuHinhHoc,
                                                  GhiChu: input.GhiChu,
                                                  loaiKhieuNai1: input.loaiKhieuNai1,
                                                  ngayKhieuNai1: input.NgayKhieuNai1,
                                                  NgayTraKQ1: input.NgayTraKQ1,
                                                  ThamQuyen1: input.ThamQuyen1,
                                                  SoQD1: input.SoQD1,
                                                  loaiKhieuNai2: input.loaiKhieuNai2,
                                                  ngayKhieuNai2: input.NgayKhieuNai2,
                                                  NgayTraKQ2: input.NgayTraKQ2,
                                                  ThamQuyen2: input.ThamQuyen2,
                                                  SoQD2: input.SoQD2,
                                                  congKhai: input.CongKhai,
                                                  KetQua1: input.KetQua1,
                                                  KetQua2: input.KetQua2);
        await _complainRepo.InsertAsync(complain);
        var result = ObjectMapper.Map<Complain, ComplainDto>(complain);
        if (input.FileAttachments != null && input.FileAttachments.Count > 0)
        {
            foreach (var item in input.FileAttachments)
            {
                var fileAttach = await _fileAttachmentManager.CreateAsync(loaiVuViec: LoaiVuViec.KhieuNai,
                                                                     idHoSo: complain.Id,
                                                                     giaiDoan: item.GiaiDoan,
                                                                     tenTaiLieu: item.TenTaiLieu.Trim(),
                                                                     hinhThuc: item.HinhThuc,
                                                                     thoiGianBanHanh: item.ThoiGianBanHanh,
                                                                     ngayNhan: item.NgayNhan,
                                                                     thuTuButLuc: item.ThuTuButLuc.Trim(),
                                                                     noiDungChinh: item.NoiDungChinh.Trim(),
                                                                     fileName: item.FileName.Trim(),
                                                                     contentType: item.ContentType,
                                                                     contentLength: item.ContentLength,
                                                                     congKhai: item.CongKhai);
                await _fileAttachmentRepo.InsertAsync(fileAttach);
                result.FileAttachments.Add(ObjectMapper.Map<FileAttachment, FileAttachmentDto>(fileAttach));
            }
        }
        await _cacheService.DeleteCacheKeysSContainAsync(nameof(Summary));
        var createEto = ObjectMapper.Map<CreateComplainDto, CreateComplainEto>(input);
        createEto.Id = complain.Id;
        await _distributedEventBus.PublishAsync(createEto);
        return result;
    }

    [Authorize(KNTCPermissions.ComplainsPermission.Default)]
    public override async Task<ComplainDto> UpdateAsync(Guid id, UpdateComplainDto input)
    {
        var complain = await _complainRepo.GetAsync(id, false);
        complain.SetConcurrencyStampIfNotNull(input.ConcurrencyStamp);
        await _complainManager.UpdateAsync(complain: complain,
                                           maHoSo: input.MaHoSo.Trim(),
                                          linhVuc: input.LinhVuc,
                                          tieuDe: input.TieuDe.Trim(),
                                          nguoiNopDon: input.NguoiNopDon.Trim(),
                                          cccdCmnd: input.CccdCmnd.Trim(),
                                          //ngayCapCccdCmnd: input.NgayCapCccdCmnd,
                                          //noiCapCccdCmnd: input.NoiCapCccdCmnd,
                                          ngaySinh: input.NgaySinh,
                                          DienThoai: input.DienThoai.Trim(),
                                          email: input.Email,
                                          diaChiThuongTru: input.DiaChiThuongTru.Trim(),
                                          diaChiLienHe: input.DiaChiLienHe.Trim(),
                                          maTinhTP: input.maTinhTP,
                                          maQuanHuyen: input.maQuanHuyen,
                                          maXaPhuongTT: input.maXaPhuongTT,
                                          thoiGianTiepNhan: input.ThoiGianTiepNhan,
                                          thoiGianyHenTraKQ: input.ThoiGianHenTraKQ,
                                          noiDungVuViec: input.NoiDungVuViec.Trim(),
                                          boPhanDangXL: input.BoPhanDangXL.Trim(),
                                          soThua: input.SoThua.Trim(),
                                          toBanDo: input.ToBanDo.Trim(),
                                          dienTich: input.DienTich,
                                          loaiDat: input.LoaiDat,
                                          diaChiThuaDat: input.DiaChiThuaDat.Trim(),
                                          tinhThuaDat: input.tinhThuaDat,
                                          huyenThuaDat: input.huyenThuaDat,
                                          xaThuaDat: input.xaThuaDat,
                                          duLieuToaDo: input.DuLieuToaDo,
                                          duLieuHinhHoc: input.DuLieuHinhHoc,
                                          GhiChu: input.GhiChu,
                                          loaiKhieuNai1: input.loaiKhieuNai1,
                                          ngayKhieuNai1: input.NgayKhieuNai1,
                                          NgayTraKQ1: input.NgayTraKQ1,
                                          ThamQuyen1: input.ThamQuyen1,
                                          SoQD1: input.SoQD1,
                                          loaiKhieuNai2: input.loaiKhieuNai2,
                                          ngayKhieuNai2: input.NgayKhieuNai2,
                                          NgayTraKQ2: input.NgayTraKQ2,
                                          ThamQuyen2: input.ThamQuyen2,
                                          SoQD2: input.SoQD2,
                                          congKhai: input.CongKhai,
                                          KetQua1: input.KetQua1,
                                          KetQua2: input.KetQua2);
        await _complainRepo.UpdateAsync(complain);
        await _cacheService.DeleteCacheKeysSContainAsync(nameof(Summary));
        var updateEto = ObjectMapper.Map<UpdateComplainDto, UpdateComplainEto>(input);
        updateEto.Id = complain.Id;
        await _distributedEventBus.PublishAsync(updateEto);
        return ObjectMapper.Map<Complain, ComplainDto>(complain);
    }

    [Authorize(KNTCPermissions.ComplainsPermission.Delete)]
    public override async Task DeleteAsync(Guid id)
    {
        var idFileAttachs = (await _fileAttachmentRepo.GetListAsync(x => id == x.IdHoSo)).Select(x => x.Id);
        await _fileAttachmentRepo.DeleteManyAsync(idFileAttachs);
        foreach (var item in idFileAttachs)
        {
            await _blobContainer.DeleteAsync(item.ToString());
        }
        await _cacheService.DeleteCacheKeysSContainAsync(nameof(Summary));
        await _distributedEventBus.PublishAsync(new DeleteComplainEto(id));
        await _complainRepo.DeleteAsync(id);
    }

    [Authorize(KNTCPermissions.ComplainsPermission.Delete)]
    public async Task DeleteMultipleAsync(IEnumerable<Guid> ids)
    {
        var idFileAttachs = (await _fileAttachmentRepo.GetListAsync(x => ids.Any(i => i == x.IdHoSo))).Select(x => x.Id);
        await _fileAttachmentRepo.DeleteManyAsync(idFileAttachs);
        foreach (var item in idFileAttachs)
        {
            await _blobContainer.DeleteAsync(item.ToString());
        }
        await _cacheService.DeleteCacheKeysSContainAsync(nameof(Summary));
        await _distributedEventBus.PublishAsync(new DeleteMultipleComplainEto(ids.ToList()));
        await _complainRepo.DeleteManyAsync(ids);
    }

    //[Authorize(KNTCPermissions.ComplainsPermission.Default)]
    public async Task<byte[]> GetExcelAsync(GetComplainListDto input)
    {
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = $"{nameof(Complain.ThoiGianTiepNhan)} DESC, {nameof(Complain.MaHoSo)}";
        }
        var complains = await _complainRepo.GetDataExportAsync(
            input.Sorting,
            input.Keyword,
            input.LinhVuc,
            input.KetQua,
            input.maTinhTP,
            input.maQuanHuyen,
            input.maXaPhuongTT,
            input.GiaiDoan,
            input.FromDate,
            input.ToDate,
            input.CongKhai,
            input.NguoiNopDon
        );
        if (complains == null) return null;

        var complainDto = ObjectMapper.Map<List<Complain>, List<ComplainExcelDto>>(complains);

        var templatePath = Path.Combine(_env.ContentRootPath, "wwwroot", "Exceltemplate", "Complain.xlsx");
        IWorkbook wb = ExcelNpoi.WriteExcelByTemp<ComplainExcelDto>(complainDto, templatePath, 17, 0, true);
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

        string giaiDoan = "Tất cả";
        if (input.GiaiDoan.HasValue && input.GiaiDoan != 0)
        {
            if (input.GiaiDoan == 1)
                giaiDoan = "Khiếu nại/Khiếu kiện lần 1";
            if (input.GiaiDoan == 2)
                giaiDoan = "Khiếu nại/Khiếu kiện lần 2";
        }
        row = sheet.GetCreateRow(11);
        cell = row.GetCreateCell(4);
        cell.SetCellValue(giaiDoan);
        cell.CellStyle.WrapText = false;
        cell.CellStyle.SetFont(font);

        string ketQua = "Tất cả";
        if (input.KetQua.HasValue)
        {
            ketQua = input.KetQua.Value.ToVNString();
        }
        row = sheet.GetCreateRow(12);
        cell = row.GetCreateCell(4);
        cell.SetCellValue(ketQua);
        cell.CellStyle.WrapText = false;
        cell.CellStyle.SetFont(font);

        string congKhai = "Tất cả";
        if (input.CongKhai.HasValue)
        {
            congKhai = input.CongKhai.Value == true ? "Công khai" : "Không công khai";
        }
        row = sheet.GetCreateRow(13);
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

    [Authorize(KNTCPermissions.ComplainsPermission.Edit)]
    public async Task<ComplainDto> UpdateStageAsync(UpdateStageComplainDto input)
    {
        var complain = await _complainRepo.GetAsync(input.Id, false);
        complain.SetConcurrencyStampIfNotNull(input.ConcurrencyStamp);
        complain.TrangThai = input.TrangThai;
        _complainManager.SettinhTrang(complain);
        await _complainRepo.UpdateAsync(complain);
        // Ghi lai lich su thao tac
        var history = new History(input.Id, LoaiVuViec.KhieuNai, input.TrangThai, CurrentUser.Id.Value, input.GhiChu);
        await _historyRepo.InsertAsync(history);
        return ObjectMapper.Map<Complain, ComplainDto>(complain);
    }
}