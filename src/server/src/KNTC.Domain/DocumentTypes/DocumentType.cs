using KNTC.FileAttachments;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace KNTC.DocumentTypes;

public class DocumentType : AuditedAggregateRoot<int>
{
    public DocumentType()
    {
    }

    public DocumentType(int id) : base(id)
    {
    }

    public DocumentType(string code, string name)
    {
        ChangeCode(code);
        ChangeName(name);
    }

    public DocumentType(int id, string code, string name) : base(id)
    {
        ChangeCode(code);
        ChangeName(name);
    }

    public string DocumentTypeCode { get; private set; }
    public string DocumentTypeName { get; private set; }
    public string? Description { get; set; }
    public int? OrderIndex { get; set; }
    public Status Status { get; set; }
    public virtual List<FileAttachment> FileAttachments { get; set; }

    private void SetCode([NotNull] string code)
    {
        DocumentTypeCode = Check.NotNullOrWhiteSpace(
            code,
            nameof(code),
            maxLength: KNTCValidatorConsts.MaxCodeLength
        );
    }

    internal DocumentType ChangeCode([NotNull] string code)
    {
        SetCode(code);
        return this;
    }

    private void SetName([NotNull] string name)
    {
        DocumentTypeName = Check.NotNullOrWhiteSpace(
            name,
            nameof(name),
            maxLength: KNTCValidatorConsts.MaxNameLength
        );
    }

    internal DocumentType ChangeName([NotNull] string name)
    {
        SetName(name);
        return this;
    }
}