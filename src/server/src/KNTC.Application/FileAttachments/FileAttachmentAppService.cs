using KNTC.Localization;
using KNTC.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    public FileAttachmentAppService(IRepository<FileAttachment, Guid> repository, FileAttachmentManager fileAttachmentManager, IBlobContainer<FileAttachmentContainer> blobContainer)
        : base(repository)
    {
        LocalizationResource = typeof(KNTCResource);
        _fileAttachmentManager = fileAttachmentManager;
        _blobContainer = blobContainer;
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
                    .WhereIf(input.ComplainId.HasValue, x => x.ComplainId == input.ComplainId)
                    .WhereIf(input.DenounceId.HasValue, x => x.DenounceId == input.DenounceId)
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
                                                             DenounceId: input.DenounceId,
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

    [AllowAnonymous]
    public async Task<byte[]> DownloadAsync(Guid fileAttachmentId)
    {
        var result = await _blobContainer.GetAllBytesOrNullAsync(fileAttachmentId.ToString());
        return result;
    }
}

