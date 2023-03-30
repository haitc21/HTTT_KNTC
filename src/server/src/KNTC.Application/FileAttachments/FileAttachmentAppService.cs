using KNTC.Complains;
using KNTC.Denounces;
using KNTC.DocumentTypes;
using KNTC.Localization;
using KNTC.NPOI;
using KNTC.Permissions;
using KNTC.Units;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using NPOI.OpenXmlFormats.Wordprocessing;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.BlobStoring;
using Volo.Abp.Data;
using Volo.Abp.Domain.Repositories;

namespace KNTC.FileAttachments;

public class FileAttachmentAppService : CrudAppService<
            FileAttachment,
            FileAttachmentDto,
            Guid,
            GetFileAttachmentListDto,
            CreateAndUpdateFileAttachmentDto>, IFileAttachmentAppService
{
    private readonly FileAttachmentManager _fileAttachmentManager;
    private readonly IBlobContainer<FileAttachmentContainer> _blobContainer;
    private readonly IHostEnvironment _env;
    private readonly IRepository<DocumentType, int> _documentTypeRepo;
    private readonly IComplainRepository _complainRepo;
    private readonly IDenounceRepository _denounceRepo;

    public FileAttachmentAppService(IRepository<FileAttachment, Guid> repository,
        FileAttachmentManager fileAttachmentManager,
        IBlobContainer<FileAttachmentContainer> blobContainer,
        IHostEnvironment env,
        IRepository<DocumentType, int> documentRepo,
        IComplainRepository complainRepo)
        : base(repository)
    {
        LocalizationResource = typeof(KNTCResource);
        _fileAttachmentManager = fileAttachmentManager;
        _blobContainer = blobContainer;
        _env = env;
        _documentTypeRepo = documentRepo;
        _complainRepo = complainRepo;
    }
    public async override Task<PagedResultDto<FileAttachmentDto>> GetListAsync(GetFileAttachmentListDto input)
    {
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = nameof(FileAttachmentDto.TenTaiLieu);
        }

        var filter = !input.Keyword.IsNullOrEmpty() ? input.Keyword.ToUpper() : "";
        var queryable = await Repository.GetQueryableAsync();

        queryable = queryable
                    .WhereIf(input.ComplainId.HasValue, x => x.LoaiVuViec == LoaiVuViec.KhieuNai && x.ComplainId == input.ComplainId)
                    .WhereIf(input.DenounceId.HasValue, x => x.LoaiVuViec == LoaiVuViec.ToCao && x.DenounceId == input.DenounceId)
                    .WhereIf(!filter.IsNullOrEmpty(),
                             x => x.TenTaiLieu.ToUpper().Contains(filter)
                                 || x.FileName.ToUpper().Contains(filter)
                             )
                    .WhereIf(input.HinhThuc.HasValue, x => x.HinhThuc == input.HinhThuc)
                    .WhereIf(input.GiaiDoan.HasValue, x => x.GiaiDoan == input.GiaiDoan)
                    .OrderBy(input.Sorting)
                    .Skip(input.SkipCount)
                    .Take(input.MaxResultCount);

        var queryResult = await AsyncExecuter.ToListAsync(queryable);


        var totalCount = await Repository.CountAsync(
                x => (input.Keyword.IsNullOrEmpty()
                    || (x.TenTaiLieu.ToUpper().Contains(input.Keyword) || x.FileName.ToUpper().Contains(input.Keyword)))
                && (!input.HinhThuc.HasValue || x.HinhThuc == input.HinhThuc)
                && (!input.GiaiDoan.HasValue || x.HinhThuc == input.GiaiDoan)
                );

        return new PagedResultDto<FileAttachmentDto>(
            totalCount,
            ObjectMapper.Map<List<FileAttachment>, List<FileAttachmentDto>>(queryResult)
        );
    }

    public async override Task<FileAttachmentDto> CreateAsync(CreateAndUpdateFileAttachmentDto input)
    {
        var entity = await _fileAttachmentManager.CreateAsync(loaiVuViec: input.LoaiVuViec,
                                                             complainId: input.ComplainId,
                                                             denounceId: input.DenounceId,
                                                             giaiDoan: input.GiaiDoan,
                                                             tenTaiLieu: input.TenTaiLieu,
                                                             hinhThuc: input.HinhThuc,
                                                             thoiGianBanHanh: input.ThoiGianBanHanh,
                                                             ngayNhan: input.NgayNhan,
                                                             thuTuButLuc: input.ThuTuButLuc,
                                                             noiDungChinh: input.NoiDungChinh,
                                                             fileName: input.FileName,
                                                             contentType: input.ContentType,
                                                             contentLength: input.ContentLength);
        await Repository.InsertAsync(entity);
        return ObjectMapper.Map<FileAttachment, FileAttachmentDto>(entity);
    }

    public async override Task<FileAttachmentDto> UpdateAsync(Guid id, CreateAndUpdateFileAttachmentDto input)
    {
        var entity = await Repository.GetAsync(id, false);
        entity.SetConcurrencyStampIfNotNull(input.ConcurrencyStamp);
        await _fileAttachmentManager.UpdateAsync(fileAttachment: entity,
                                             loaiVuViec: input.LoaiVuViec,
                                             giaiDoan: input.GiaiDoan,
                                             tenTaiLieu: input.TenTaiLieu,
                                             hinhThuc: input.HinhThuc,
                                             thoiGianBanHanh: input.ThoiGianBanHanh,
                                             ngayNhan: input.NgayNhan,
                                             thuTuButLuc: input.ThuTuButLuc,
                                             noiDungChinh: input.NoiDungChinh,
                                             fileName: input.FileName,
                                             contentType: input.ContentType,
                                                             contentLength: input.ContentLength);
        await Repository.UpdateAsync(entity);
        return ObjectMapper.Map<FileAttachment, FileAttachmentDto>(entity);
    }
    public override async Task DeleteAsync(Guid id)
    {
        await Repository.DeleteAsync(id);
        await _blobContainer.DeleteAsync(id.ToString());
    }
    public async Task<Guid> UploadAsync(Guid fileAttachmentId, IFormFile file)
    {
        if (file == null) throw new UserFriendlyException("Vui lòng chọn tệp");
        try
        {
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream); // sao chép dữ liệu từ IFormFile vào MemoryStream
                await _blobContainer.SaveAsync(fileAttachmentId.ToString(), stream, overrideExisting: true);
            }
            return fileAttachmentId;

        }
        catch (Exception ex)
        {
            var entity = await Repository.GetAsync(fileAttachmentId);
            await Repository.DeleteAsync(fileAttachmentId);
            throw new UserFriendlyException($"Đã xảy ra lỗi khi tải tệp {entity.FileName}" + ex.Message);
        }
    }

    public async Task<byte[]> DownloadAsync(Guid fileAttachmentId)
    {
        var result = await _blobContainer.GetAllBytesOrNullAsync(fileAttachmentId.ToString());
        return result;
    }

    public async Task<byte[]> GetExcelAsync(GetFileAttachmentListDto input)
    {
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = nameof(FileAttachmentDto.TenTaiLieu);
        }

        var fileQuery = await Repository.GetQueryableAsync();
        fileQuery = fileQuery
                    .WhereIf(input.ComplainId.HasValue, x => x.LoaiVuViec == LoaiVuViec.KhieuNai && x.ComplainId == input.ComplainId)
                    .WhereIf(input.DenounceId.HasValue, x => x.LoaiVuViec == LoaiVuViec.ToCao && x.DenounceId == input.DenounceId);
        var query = from f in fileQuery
                    join dt in await _documentTypeRepo.GetQueryableAsync()
                    on f.HinhThuc equals dt.Id
                    select new FileAttachmentExcelDto
                    {
                        TenTaiLieu = f.TenTaiLieu,
                        HinhThuc = dt.DocumentTypeName,
                        NgayNhan = f.NgayNhan,
                        ThoiGianBanHanh = f.ThoiGianBanHanh,
                        ThuTuButLuc = f.ThuTuButLuc,
                        NoiDungChinh = f.NoiDungChinh
                    };
        query = query.OrderBy(input.Sorting);
        var fileAttachments = await AsyncExecuter.ToListAsync<FileAttachmentExcelDto>(query);
        if (fileAttachments.Any()) return null;

        var templatePath = Path.Combine(_env.ContentRootPath, "wwwroot", "Exceltemplate", "FileAttachment.xlsx");

        IWorkbook wb = ExcelNpoi.WriteExcelByTemp<FileAttachmentExcelDto>(fileAttachments, templatePath, 14, 0, true);
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

        string maHoSo = "";
        string tieuDe = "";
        if (input.ComplainId.HasValue)
        {
            var complain = await _complainRepo.GetAsync(input.ComplainId.Value);
            maHoSo = complain.MaHoSo;
            tieuDe= complain.TieuDe;
        }
        if (input.DenounceId.HasValue)
        {
            var denounce = await _denounceRepo.GetAsync(input.DenounceId.Value);
            maHoSo = denounce.MaHoSo;
            tieuDe = denounce.TieuDe;
        }

        IRow row = sheet.GetCreateRow(5);
        var cell = row.GetCreateCell(4);
        cell.SetCellValue(maHoSo);
        cell.CellStyle.WrapText = false;
        cell.CellStyle.SetFont(font);

        row = sheet.GetCreateRow(6);
        cell = row.GetCreateCell(4);
        cell.SetCellValue(tieuDe);
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

