using KNTC.Complains;
using KNTC.Denounces;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace KNTC.FileAttachments;

public class FileAttachmentManager : DomainService
{
    private readonly IComplainRepository _complainRepo;
    private readonly IDenounceRepository _dunounceRepo;
    private readonly IRepository<FileAttachment, Guid> _fileAttachmentRepo;

    public FileAttachmentManager(IRepository<FileAttachment, Guid> fileAttachmentRepo,
        IDenounceRepository denounceRepo,
        IComplainRepository complainRepo)
    {
        _fileAttachmentRepo = fileAttachmentRepo;
        _dunounceRepo = denounceRepo;
        _complainRepo = complainRepo;
    }

    public async Task<FileAttachment> CreateAsync([NotNull] LoaiVuViec loaiVuViec,
                                                   Guid? complainId,
                                                   Guid? denounceId,
                                                   [NotNull] int giaiDoan,
                                                   [NotNull] string tenTaiLieu,
                                                   [NotNull] int hinhThuc,
                                                   [NotNull] DateTime thoiGianBanHanh,
                                                   [NotNull] DateTime ngayNhan,
                                                   [NotNull] string thuTuButLuc,
                                                   [NotNull] string noiDungChinh,
                                                   [NotNull] string fileName,
                                                   [NotNull] string contentType,
                                                   [NotNull] long contentLength,
                                                   [NotNull] bool congKhai)
    {
        Check.NotNull(loaiVuViec, nameof(loaiVuViec));
        Check.NotNull(giaiDoan, nameof(giaiDoan));
        Check.NotNullOrWhiteSpace(tenTaiLieu, nameof(tenTaiLieu));
        Check.NotNull(hinhThuc, nameof(hinhThuc));
        Check.NotNull(thoiGianBanHanh, nameof(thoiGianBanHanh));
        Check.NotNull(ngayNhan, nameof(ngayNhan));
        Check.NotNullOrWhiteSpace(thuTuButLuc, nameof(thuTuButLuc));
        Check.NotNullOrWhiteSpace(noiDungChinh, nameof(noiDungChinh));
        Check.NotNullOrWhiteSpace(fileName, nameof(fileName));
        Check.NotNullOrWhiteSpace(contentType, nameof(contentType));
        Check.NotNull(contentLength, nameof(contentLength));
        Check.NotNull(congKhai, nameof(congKhai));

        var existTepDinhKem = await _fileAttachmentRepo.FindAsync(x => x.TenTaiLieu == tenTaiLieu
                                                     && (
                                                     (loaiVuViec == LoaiVuViec.KhieuNai && x.ComplainId == complainId)
                                                     || (loaiVuViec == LoaiVuViec.ToCao && x.DenounceId == denounceId)
                                                     ));
        if (existTepDinhKem != null)
        {
            string maHoSo = "";
            if (loaiVuViec == LoaiVuViec.KhieuNai)
            {
                var complain = await _complainRepo.GetAsync(complainId.Value, false);
                maHoSo = complain.MaHoSo;
            }
            if (loaiVuViec == LoaiVuViec.ToCao)
            {
                var denounce = await _dunounceRepo.GetAsync(complainId.Value, false);
                maHoSo = denounce.MaHoSo;
            }
            throw new BusinessException(KNTCDomainErrorCodes.TepDinhKemAlreadyExist)
                .WithData("tenTaiLieu", tenTaiLieu)
                .WithData("maHoSo", maHoSo);
        }
        return new FileAttachment(GuidGenerator.Create(), tenTaiLieu)
        {
            ComplainId = complainId,
            DenounceId = denounceId,
            GiaiDoan = giaiDoan,
            HinhThuc = hinhThuc,
            ThoiGianBanHanh = thoiGianBanHanh,
            NgayNhan = ngayNhan,
            ThuTuButLuc = thuTuButLuc,
            NoiDungChinh = noiDungChinh,
            FileName = fileName,
            ContentType = contentType,
            ContentLength = contentLength,
            LoaiVuViec = complainId.HasValue ? LoaiVuViec.KhieuNai : LoaiVuViec.ToCao,
            CongKhai = congKhai
        };
    }

    public async Task UpdateAsync([NotNull] FileAttachment fileAttachment,
                                   [NotNull] LoaiVuViec loaiVuViec,
                                   [NotNull] int giaiDoan,
                                   [NotNull] string tenTaiLieu,
                                   [NotNull] int hinhThuc,
                                   [NotNull] DateTime thoiGianBanHanh,
                                   [NotNull] DateTime ngayNhan,
                                   [NotNull] string thuTuButLuc,
                                   [NotNull] string noiDungChinh,
                                   string fileName,
                                   string contentType,
                                   long contentLength,
                                   [NotNull] bool congKhai)
    {
        Check.NotNull(loaiVuViec, nameof(loaiVuViec));
        Check.NotNull(giaiDoan, nameof(giaiDoan));
        Check.NotNull(fileAttachment, nameof(fileAttachment));
        Check.NotNullOrWhiteSpace(tenTaiLieu, nameof(tenTaiLieu));
        Check.NotNull(hinhThuc, nameof(hinhThuc));
        Check.NotNull(thoiGianBanHanh, nameof(thoiGianBanHanh));
        Check.NotNull(ngayNhan, nameof(ngayNhan));
        Check.NotNullOrWhiteSpace(thuTuButLuc, nameof(thuTuButLuc));
        Check.NotNullOrWhiteSpace(noiDungChinh, nameof(noiDungChinh));
        Check.NotNullOrWhiteSpace(fileName, nameof(fileName));
        Check.NotNullOrWhiteSpace(contentType, nameof(contentType));
        Check.NotNull(contentLength, nameof(contentLength));
        Check.NotNull(congKhai, nameof(congKhai));

        if (fileAttachment.TenTaiLieu != tenTaiLieu)
        {
            var existTepDinhKem = await _fileAttachmentRepo.FindAsync(x => x.TenTaiLieu == tenTaiLieu
                                                     && (
                                                     (loaiVuViec == LoaiVuViec.KhieuNai && x.ComplainId == fileAttachment.ComplainId)
                                                     || (loaiVuViec == LoaiVuViec.ToCao && x.DenounceId == fileAttachment.DenounceId)
                                                     ));
            if (existTepDinhKem != null)
            {
                string maHoSo = "";
                if (loaiVuViec == LoaiVuViec.KhieuNai)
                {
                    var complain = await _complainRepo.GetAsync(fileAttachment.ComplainId.Value, false);
                    maHoSo = complain.MaHoSo;
                }
                if (loaiVuViec == LoaiVuViec.ToCao)
                {
                    var denounce = await _dunounceRepo.GetAsync(fileAttachment.ComplainId.Value, false);
                    maHoSo = denounce.MaHoSo;
                }
                throw new BusinessException(KNTCDomainErrorCodes.TepDinhKemAlreadyExist)
                    .WithData("tenTaiLieu", tenTaiLieu)
                    .WithData("maHoSo", maHoSo);
            }
            fileAttachment.ChangeTenTaiLieu(tenTaiLieu);
        }
        fileAttachment.GiaiDoan = giaiDoan;
        fileAttachment.HinhThuc = hinhThuc;
        fileAttachment.ThoiGianBanHanh = thoiGianBanHanh;
        fileAttachment.NgayNhan = ngayNhan;
        fileAttachment.ThuTuButLuc = thuTuButLuc;
        fileAttachment.NoiDungChinh = noiDungChinh;
        fileAttachment.CongKhai = congKhai;
        if (!!string.IsNullOrEmpty(fileName))
        {
            fileAttachment.FileName = fileName;
            fileAttachment.ContentType = contentType;
            fileAttachment.ContentLength = contentLength;
        }
    }
}