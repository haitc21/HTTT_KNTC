using KNTC.Localization;
using KNTC.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Data;

namespace KNTC.DocumentTypes;

public class DocumentTypeAppService : CrudAppService<
            DocumentType,
            DocumentTypeDto,
            int,
            GetDocumentTypesListDto,
            CreateAndUpdateDocumentTypeDto>, IDocumentTypeAppService
{
    private readonly DocumentTypeManager _documentTypeManager;
    public DocumentTypeAppService(IRepository<DocumentType, int> repository, DocumentTypeManager documentTypeManager) : base(repository)
    {
        LocalizationResource = typeof(KNTCResource);
        CreatePolicyName = KNTCPermissions.DocumentType.Create;
        UpdatePolicyName = KNTCPermissions.DocumentType.Edit;
        DeletePolicyName = KNTCPermissions.DocumentType.Create;
        _documentTypeManager = documentTypeManager;
    }

    public async override Task<PagedResultDto<DocumentTypeDto>> GetListAsync(GetDocumentTypesListDto input)
    {
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = nameof(DocumentType.OrderIndex);
        }
        var filter = !input.Keyword.IsNullOrEmpty() ? input.Keyword.ToUpper() : "";
        var queryable = await Repository.GetQueryableAsync();

        queryable = queryable
                    .WhereIf(!filter.IsNullOrEmpty(),
                             x => x.DocumentTypeCode.ToUpper().Contains(filter)
                                 || x.DocumentTypeName.ToUpper().Contains(filter)
                             )
                    .WhereIf(input.Status.HasValue, x => x.Status == input.Status)
                    .OrderBy(input.Sorting)
                    .Skip(input.SkipCount)
                    .Take(input.MaxResultCount);

        var queryResult = await AsyncExecuter.ToListAsync(queryable);


        var totalCount = await Repository.CountAsync(
                x => (input.Keyword.IsNullOrEmpty()
                    || (x.DocumentTypeCode.ToUpper().Contains(input.Keyword) || x.DocumentTypeName.ToUpper().Contains(input.Keyword)))
                && (!input.Status.HasValue || x.Status == input.Status)
                );

        return new PagedResultDto<DocumentTypeDto>(
            totalCount,
            ObjectMapper.Map<List<DocumentType>, List<DocumentTypeDto>>(queryResult)
        );

    }
    public async Task<ListResultDto<DocumentTypeLookupDto>> GetLookupAsync()
    {
        var documentTypes = await Repository.GetListAsync();

        return new ListResultDto<DocumentTypeLookupDto>(
            ObjectMapper.Map<List<DocumentType>, List<DocumentTypeLookupDto>>(documentTypes)
        );
    }

    public async override Task<DocumentTypeDto> CreateAsync(CreateAndUpdateDocumentTypeDto input)
    {
        var entity = await _documentTypeManager.CreateAsync(input.DocumentTypeCode,
                                                            input.DocumentTypeName,
                                                            input.Description,
                                                            input.OrderIndex);
        await Repository.InsertAsync(entity);
        return ObjectMapper.Map<DocumentType, DocumentTypeDto>(entity);

    }

    public async override Task<DocumentTypeDto> UpdateAsync(int id, CreateAndUpdateDocumentTypeDto input)
    {
        var entity = await Repository.GetAsync(id, false);
        entity.SetConcurrencyStampIfNotNull(input.ConcurrencyStamp);
        await _documentTypeManager.UpdateAsync(entity,
                                              input.DocumentTypeCode,
                                              input.DocumentTypeName,
                                              input.Description,
                                              input.OrderIndex);
        await Repository.UpdateAsync(entity);
        return ObjectMapper.Map<DocumentType, DocumentTypeDto>(entity);
    }


    [Authorize(KNTCPermissions.DocumentType.Delete)]
    public async Task DeleteMultipleAsync(IEnumerable<int> ids)
    {
        await Repository.DeleteManyAsync(ids);
    }
}

