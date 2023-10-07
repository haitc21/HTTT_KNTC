﻿using KNTC.DocumentTypes;
using System;
using System.Diagnostics.CodeAnalysis;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace KNTC.FileAttachments;

public class FileAttachment : AuditedAggregateRoot<Guid>
{
    public FileAttachment()
    {
    }

    public FileAttachment(Guid id) : base(id)
    {
    }

    public FileAttachment(Guid id, string tenTaiLieu, Guid idHoSo) : base(id)
    {
        SetTenTaiLieu(tenTaiLieu);
        IdHoSo = idHoSo;
    }

    public Guid IdHoSo { get; private set; }
    public LoaiVuViec LoaiVuViec { get; set; }
    public int GiaiDoan { get; set; }
    public string TenTaiLieu { get; private set; }
    public int HinhThuc { get; set; }
    public DateTime ThoiGianBanHanh { get; set; }
    public DateTime NgayNhan { get; set; }
    public string ThuTuButLuc { get; set; }
    public string NoiDungChinh { get; set; }
    public string FileName { get; set; }
    public string ContentType { get; set; }
    public long ContentLength { get; set; }
    public DocumentType DocumentType { get; set; }
    public bool CongKhai { get; set; }
    public bool ChoPhepDownload { get; set; }
    private void SetTenTaiLieu([NotNull] string tenTaiLieu)
    {
        TenTaiLieu = Check.NotNullOrWhiteSpace(
            tenTaiLieu,
            nameof(tenTaiLieu),
            maxLength: KNTCValidatorConsts.MaxTenTaiLieuLength
        );
    }

    internal FileAttachment ChangeTenTaiLieu([NotNull] string tenTaiLieu)
    {
        SetTenTaiLieu(tenTaiLieu);
        return this;
    }
}