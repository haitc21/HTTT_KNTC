using KNTC.DocumentTypes;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
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
