using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace KNTC.FileAttachments;

public interface IFileAttachmentAppService : ICrudAppService<FileAttachmentDto,
        Guid,
        GetFileAttachmentListDto,
        CreateAndUpdateFileAttachmentDto>
{
    Task<Guid> UploadAsync(Guid fileAttachmentId, IFormFile file);

    Task<byte[]> DownloadAsync(Guid fileAttachmentId);
}