using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace KNTC.DocumentTypes;

public class DocumentTypeManager : DomainService
{
    private readonly IRepository<DocumentType, int> _documentTypeRepo;
    public DocumentTypeManager(IRepository<DocumentType, int> documentTypeRepo)
    {
        _documentTypeRepo = documentTypeRepo;
    }
    public async Task<DocumentType> CreateAsync([NotNull] string code,
                                                [NotNull] string name,
                                                string description,
                                                int orderIndex)
    {

        Check.NotNullOrWhiteSpace(code, nameof(code));
        Check.NotNullOrWhiteSpace(name, nameof(name));
        await CheckCode(code);
        await CheckName(name);
        return new DocumentType(code, name)
        {
            Description = description,
            OrderIndex = orderIndex,
            Status = Status.Active
        };
    }
    public async Task UpdateAsync([NotNull] DocumentType documentType,
                                                 [NotNull] string code,
                                                 [NotNull] string name,
                                                 string description,
                                                 int orderIndex)
    {
        Check.NotNull(documentType, nameof(documentType));
        Check.NotNullOrWhiteSpace(code, nameof(code));
        Check.NotNullOrWhiteSpace(name, nameof(name));
        if(documentType.DocumentTypeCode != code)
        {
            await ChangeCode(documentType, code);
        }
        if (documentType.DocumentTypeName != name)
        {
            await ChangeName(documentType, name);
        }
        documentType.Description = description;
        documentType.OrderIndex = orderIndex;
    }
    private async Task ChangeName(DocumentType documentType, string name)
    {
        var existedName = await _documentTypeRepo.FindAsync(x => x.DocumentTypeName == name, false);
        if (existedName != null && existedName.Id != documentType.Id)
        {
            throw new BusinessException(KNTCDomainErrorCodes.NameAlreadyExist).WithData("name", name);
        }
        documentType.ChangeName(name);
    }
    private async Task CheckName(string name)
    {
        var existedName = await _documentTypeRepo.FindAsync(x => x.DocumentTypeName == name, false);
        if (existedName != null)
        {
            throw new BusinessException(KNTCDomainErrorCodes.NameAlreadyExist).WithData("name", name);
        }
    }
    private async Task ChangeCode(DocumentType documentType, string code)
    {
        var existedCode = await _documentTypeRepo.FindAsync(x => x.DocumentTypeCode == code, false);
        if (existedCode != null && existedCode.Id != documentType.Id)
        {
            throw new BusinessException(KNTCDomainErrorCodes.CodeAlreadyExist).WithData("code", code);
        }
        documentType.ChangeCode(code);
    }
    private async Task CheckCode(string code)
    {
        var existedCode = await _documentTypeRepo.FindAsync(x => x.DocumentTypeCode == code, false);
        if (existedCode != null)
        {
            throw new BusinessException(KNTCDomainErrorCodes.CodeAlreadyExist).WithData("code", code);
        }
    }
}
