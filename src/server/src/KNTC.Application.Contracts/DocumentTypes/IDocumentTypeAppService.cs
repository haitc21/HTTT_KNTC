using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace KNTC.DocumentTypes;

public interface IDocumentTypeAppService
    : ICrudAppService<DocumentTypeDto,
        int,
        GetDocumentTypesListDto,
        CreateAndUpdateDocumentTypeDto>
{
    Task<ListResultDto<DocumentTypeLookupDto>> GetLookupAsync();
    Task DeleteMultipleAsync(IEnumerable<int> ids);
}
