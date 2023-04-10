using KNTC.LandTypes;
using KNTC.Localization;
using KNTC.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Caching;
using Volo.Abp.Data;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;

namespace KNTC.DocumentTypes;

public class DocumentTypeAppService : CrudAppService<
            DocumentType,
            DocumentTypeDto,
            int,
            GetDocumentTypesListDto,
            CreateAndUpdateDocumentTypeDto>, IDocumentTypeAppService
{
    private readonly DocumentTypeManager _documentTypeManager;
    private readonly IDistributedCache<DocumentTypeLookupCache> _cache;
    public DocumentTypeAppService(IRepository<DocumentType, int> repository, DocumentTypeManager documentTypeManager, IDistributedCache<DocumentTypeLookupCache> cache) : base(repository)
    {
        LocalizationResource = typeof(KNTCResource);
        CreatePolicyName = KNTCPermissions.DocumentTypePermission.Create;
        UpdatePolicyName = KNTCPermissions.DocumentTypePermission.Edit;
        DeletePolicyName = KNTCPermissions.DocumentTypePermission.Delete;
        _documentTypeManager = documentTypeManager;
        _cache = cache;
    }

    public async override Task<PagedResultDto<DocumentTypeDto>> GetListAsync(GetDocumentTypesListDto input)
    {
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = $"{nameof(DocumentType.OrderIndex)}, {nameof(DocumentType.DocumentTypeName)}";
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
        var cacheItem = await _cache.GetOrAddAsync(
        "All",
        async () =>
        {
            var entities = await Repository.GetListAsync(x => x.Status == Status.Active);
            var dtos = ObjectMapper.Map<List<DocumentType>, List<DocumentTypeLookupDto>>(entities);
            return new DocumentTypeLookupCache() { Items = dtos };
        },
        () => new DistributedCacheEntryOptions
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddHours(12)
        });

        return new ListResultDto<DocumentTypeLookupDto>(cacheItem.Items);
    }

    public async override Task<DocumentTypeDto> CreateAsync(CreateAndUpdateDocumentTypeDto input)
    {
        var entity = await _documentTypeManager.CreateAsync(input.DocumentTypeCode,
                                                            input.DocumentTypeName,
                                                            input.Description,
                                                            input.OrderIndex,
                                                            input.Status);
        await Repository.InsertAsync(entity);
        await _cache.RemoveAsync("All");
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
                                              input.OrderIndex,
                                              input.Status);
        await Repository.UpdateAsync(entity);
        await _cache.RemoveAsync("All");
        return ObjectMapper.Map<DocumentType, DocumentTypeDto>(entity);
    }

    public override async Task DeleteAsync(int id)
    {
        await Repository.DeleteAsync(id);
        await _cache.RemoveAsync("All");
    }
    [Authorize(KNTCPermissions.DocumentTypePermission.Delete)]
    public async Task DeleteMultipleAsync(IEnumerable<int> ids)
    {
        await Repository.DeleteManyAsync(ids);
        await _cache.RemoveAsync("All");
    }
}

