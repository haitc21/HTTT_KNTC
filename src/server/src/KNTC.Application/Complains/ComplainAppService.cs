using KNTC.Extenssions;
using KNTC.FileAttachments;
using KNTC.Localization;
using KNTC.NPOI;
using KNTC.Permissions;
using KNTC.Units;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using NPOI.SS.UserModel;
using NPOI.XWPF.UserModel;
using Polly;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.BlobStoring;
using Volo.Abp.Data;
using Volo.Abp.Domain.Repositories;
using static KNTC.Permissions.KNTCPermissions;

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

    public ComplainAppService(IRepository<Complain, Guid> repository,
        IComplainRepository complainRepo,
        ComplainManager complainManager,
        IRepository<FileAttachment, Guid> fileAttachmentRepo,
        IBlobContainer<FileAttachmentContainer> blobContainer,
        FileAttachmentManager fileAttachmentManager,
        IHostEnvironment env,
        IRepository<Unit, int> unitRepo) : base(repository)
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
    }
    [AllowAnonymous]
    public override async Task<PagedResultDto<ComplainDto>> GetListAsync(GetComplainListDto input)
    {
        var hasPermission = await AuthorizationService.AuthorizeAsync(KNTCPermissions.ComplainsPermission.Default);
        if(hasPermission.Succeeded == false)
        {
            input.CongKhai = true;
        }
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = nameof(Complain.MaHoSo);
        }
        var filter = !input.Keyword.IsNullOrEmpty() ? input.Keyword.ToUpper() : "";
        var complains = await _complainRepo.GetListAsync(
            input.SkipCount,
            input.MaxResultCount,
            input.Sorting,
            input.Keyword,
            input.LinhVuc,
            input.mangLinhVuc,
            input.KetQua,
            input.maTinhTP,
            input.maQuanHuyen,
            input.maXaPhuongTT,
            input.GiaiDoan,
            input.FromDate,
            input.ToDate,
            input.CongKhai
        );

        var totalCount = await _complainRepo.CountAsync(
                x => (input.Keyword.IsNullOrEmpty()
                    || (x.MaHoSo.ToUpper().Contains(input.Keyword) || x.TieuDe.ToUpper().Contains(input.Keyword)))
                && (!input.LinhVuc.HasValue || x.LinhVuc == input.LinhVuc)
                && (input.mangLinhVuc.IsNullOrEmpty() || input.mangLinhVuc.Contains((int) x.LinhVuc))
                && (!input.KetQua.HasValue || x.KetQua == input.KetQua)
                && (!input.maTinhTP.HasValue || x.MaTinhTP == input.maTinhTP)
                && (!input.maQuanHuyen.HasValue || x.MaQuanHuyen == input.maQuanHuyen)
                && (!input.maXaPhuongTT.HasValue || x.MaXaPhuongTT == input.maXaPhuongTT)
                && (!input.GiaiDoan.HasValue ||
                    (input.GiaiDoan == 1 && x.NgayKhieuNai1 != null && x.NgayKhieuNai2 == null) ||
                    (input.GiaiDoan == 2 && x.NgayKhieuNai2 != null))
                && (!input.FromDate.HasValue || x.ThoiGianTiepNhan >= input.FromDate)
                && (!input.ToDate.HasValue || x.ThoiGianTiepNhan <= input.ToDate)
                && (!input.CongKhai.HasValue || x.CongKhai == input.CongKhai)
                );

        return new PagedResultDto<ComplainDto>(
        totalCount,
            ObjectMapper.Map<List<Complain>, List<ComplainDto>>(complains)
        );
    }
    [Authorize(KNTCPermissions.ComplainsPermission.Create)]
    public override async Task<ComplainDto> CreateAsync(CreateComplainDto input)
    {
        var complain = await _complainManager.CreateAsync(maHoSo: input.MaHoSo,
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
                                                  thoiGianyHenTraKQ: input.ThoiGianHenTraKQ,
                                                  noiDungVuViec: input.NoiDungVuViec,
                                                  boPhanDangXL: input.BoPhanDangXL,
                                                  soThua: input.SoThua,
                                                  toBanDo: input.ToBanDo,
                                                  dienTich: input.DienTich,
                                                  loaiDat: input.LoaiDat,
                                                  diaChiThuaDat: input.DiaChiThuaDat,
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
                                                                     complainId: complain.Id,
                                                                     denounceId: null,
                                                                     giaiDoan: item.GiaiDoan,
                                                                     tenTaiLieu: item.TenTaiLieu,
                                                                     hinhThuc: item.HinhThuc,
                                                                     thoiGianBanHanh: item.ThoiGianBanHanh,
                                                                     ngayNhan: item.NgayNhan,
                                                                     thuTuButLuc: item.ThuTuButLuc,
                                                                     noiDungChinh: item.NoiDungChinh,
                                                                     fileName: item.FileName,
                                                                     contentType: item.ContentType,
                                                                     contentLength: item.ContentLength,
                                                                     congKhai: item.CongKhai);
                await _fileAttachmentRepo.InsertAsync(fileAttach);
                result.FileAttachments.Add(ObjectMapper.Map<FileAttachment, FileAttachmentDto>(fileAttach));
            }
        }
        return result;
    }
    [Authorize(KNTCPermissions.ComplainsPermission.Default)]
    public override async Task<ComplainDto> UpdateAsync(Guid id, UpdateComplainDto input)
    {
        var complain = await _complainRepo.GetAsync(id, false);
        complain.SetConcurrencyStampIfNotNull(input.ConcurrencyStamp);
        await _complainManager.UpdateAsync(complain: complain,
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
                                          thoiGianyHenTraKQ: input.ThoiGianHenTraKQ,
                                          noiDungVuViec: input.NoiDungVuViec,
                                          boPhanDangXL: input.BoPhanDangXL,
                                          soThua: input.SoThua,
                                          toBanDo: input.ToBanDo,
                                          dienTich: input.DienTich,
                                          loaiDat: input.LoaiDat,
                                          diaChiThuaDat: input.DiaChiThuaDat,
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
        return ObjectMapper.Map<Complain, ComplainDto>(complain);
    }

    [Authorize(KNTCPermissions.ComplainsPermission.Delete)]
    public override async Task DeleteAsync(Guid id)
    {
        var idFileAttachs = (await _fileAttachmentRepo.GetListAsync(x => id == x.ComplainId)).Select(x => x.Id);
        await _fileAttachmentRepo.DeleteManyAsync(idFileAttachs);
        foreach (var item in idFileAttachs)
        {
            await _blobContainer.DeleteAsync(item.ToString());
        }
        await _complainRepo.DeleteAsync(id);
    }

    [Authorize(KNTCPermissions.ComplainsPermission.Delete)]
    public async Task DeleteMultipleAsync(IEnumerable<Guid> ids)
    {
        var idFileAttachs = (await _fileAttachmentRepo.GetListAsync(x => ids.Any(i => i == x.ComplainId))).Select(x => x.Id);
        await _fileAttachmentRepo.DeleteManyAsync(idFileAttachs);
        foreach (var item in idFileAttachs)
        {
            await _blobContainer.DeleteAsync(item.ToString());
        }
        await _complainRepo.DeleteManyAsync(ids);
    }


    [Authorize(KNTCPermissions.ComplainsPermission.Default)]
    public async Task<byte[]> GetExcelAsync(GetComplainListDto input)
    {
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = nameof(Complain.MaHoSo);
        }
        var complains = await _complainRepo.GetDataExportAsync(
            input.Sorting,
            input.LinhVuc,
            input.KetQua,
            input.maTinhTP,
            input.maQuanHuyen,
            input.maXaPhuongTT,
            input.GiaiDoan,
            input.FromDate,
            input.ToDate
        );
        if (complains == null) return null;

        var complainDto = ObjectMapper.Map<List<Complain>, List<ComplainExcelDto>>(complains);

        var templatePath = Path.Combine(_env.ContentRootPath, "wwwroot", "Exceltemplate", "Complain.xlsx");
        IWorkbook wb = ExcelNpoi.WriteExcelByTemp<ComplainExcelDto>(complainDto, templatePath, 14, 0, true);
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

        string linhVuc = "Tất cả";
        if (input.LinhVuc.HasValue)
        {
            linhVuc = input.LinhVuc.Value.ToVNString();
        }
        IRow row = sheet.GetCreateRow(4);
        var cell = row.GetCreateCell(4);
        cell.SetCellValue(linhVuc);
        cell.CellStyle.WrapText = false;
        cell.CellStyle.SetFont(font);

        string tenTinh = "Tất cả";
        if (input.maTinhTP != null)
        {
            var tinh = await _unitRepo.GetAsync(x => x.Id == input.maTinhTP);
            tenTinh = tinh.UnitName;
        }
        row = sheet.GetCreateRow(5);
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

        if(!tuNgay.IsNullOrEmpty() && !denNgay.IsNullOrEmpty())
        {
            row = sheet.GetCreateRow(8);
            cell = row.GetCreateCell(4);
            cell.SetCellValue(tuNgay + " - " + denNgay);
            cell.CellStyle.WrapText = false;
            cell.CellStyle.SetFont(font);
        }

        string giaiDoan = "Tất cả";
        if (input.GiaiDoan.HasValue)
        {
            if (input.GiaiDoan == 1)
                giaiDoan = "Khiếu nại/Khiếu kiện lần 1";
            if (input.GiaiDoan == 2)
                giaiDoan = "Khiếu nại/Khiếu kiện lần 2";
        }
        row = sheet.GetCreateRow(9);
        cell = row.GetCreateCell(4);
        cell.SetCellValue(giaiDoan);
        cell.CellStyle.WrapText = false;
        cell.CellStyle.SetFont(font);

        string ketQua = "Tất cả";
        if (input.KetQua.HasValue)
        {
            ketQua = input.KetQua.Value.ToVNString();
        }
        row = sheet.GetCreateRow(10);
        cell = row.GetCreateCell(4);
        cell.SetCellValue(ketQua);
        cell.CellStyle.WrapText = false;
        cell.CellStyle.SetFont(font);

        string congKhai = "Tất cả";
        if (input.CongKhai.HasValue)
        {
            congKhai = input.CongKhai.Value == true ? "Công khai" : "Không công khai";

        }
        row = sheet.GetCreateRow(11);
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
