using KNTC.FileAttachments;
using KNTC.Localization;
using KNTC.Permissions;
using KNTC.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.BlobStoring;
using Volo.Abp.Data;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Threading;
using Volo.Abp.Users;
using static KNTC.Permissions.KNTCPermissions;
using static Volo.Abp.Identity.Settings.IdentitySettingNames;

namespace KNTC.Denounces;

[Authorize(KNTCPermissions.Denounces.Default)]
public class DenounceAppService : CrudAppService<
            Denounce,
            DenounceDto,
            Guid,
            GetDenounceListDto,
            CreateDenounceDto,
            UpdateDenounceDto>, IDenounceAppService
{
    private readonly IDenounceRepository _denounceRepo;
    private readonly DenounceManager _denounceManager;
    private readonly IRepository<FileAttachment, Guid> _fileAttachmentRepo;
    private readonly IBlobContainer<FileAttachmentContainer> _blobContainer;

    public DenounceAppService(IRepository<Denounce, Guid> repository,
        IDenounceRepository denounceRepo,
        DenounceManager denounceManager,
        IRepository<FileAttachment, Guid> fileAttachmentRepo,
        IBlobContainer<FileAttachmentContainer> blobContainer) : base(repository)
    {
        LocalizationResource = typeof(KNTCResource);
        GetPolicyName = KNTCPermissions.Denounces.Default;

        _denounceRepo = denounceRepo;
        _fileAttachmentRepo = fileAttachmentRepo;
        _denounceManager = denounceManager;
        _blobContainer = blobContainer;
    }

    [Authorize(KNTCPermissions.Denounces.Default)]
    public override async Task<PagedResultDto<DenounceDto>> GetListAsync(GetDenounceListDto input)
    {
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = nameof(Denounce.MaHoSo);
        }

        var denounces = await _denounceRepo.GetListAsync(
            input.SkipCount,
            input.MaxResultCount,
            input.Sorting,
            input.Keyword,
            input.LoaiVuViec,
            input.KetQua
        );

        var totalCount = input.Keyword == null
            ? await _denounceRepo.CountAsync()
            : await _denounceRepo.CountAsync(
                x => x.MaHoSo.Contains(input.Keyword) || x.TieuDe.Contains(input.Keyword));

        return new PagedResultDto<DenounceDto>(
        totalCount,
            ObjectMapper.Map<List<Denounce>, List<DenounceDto>>(denounces)
        );
    }
    [Authorize(KNTCPermissions.Denounces.Create)]
    public override async Task<DenounceDto> CreateAsync(CreateDenounceDto input)
    {
        var denounce = await _denounceManager.CreateAsync(maHoSo: input.MaHoSo,
                                                  loaiVuViec: input.LoaiVuViec,
                                                  tieuDe: input.TieuDe,
                                                  nguoiDeNghi: input.NguoiDeNghi,
                                                  cccdCmnd: input.CccdCmnd,
                                                  ngayCapCccdCmnd: input.NgayCapCccdCmnd,
                                                  noiCapCccdCmnd: input.NoiCapCccdCmnd,
                                                  ngaySinh: input.NgaySinh,
                                                  dienThaoi: input.DienThaoi,
                                                  email: input.Email,
                                                  diaChiThuongTru: input.DiaChiThuongTru,
                                                  diaChiLienHe: input.DiaChiLienHe,
                                                  maTinhTP: input.MaTinhTP,
                                                  maQuanHuyen: input.MaQuanHuyen,
                                                  maXaPhuongTT: input.MaXaPhuongTT,
                                                  ngayTiepNhan: input.NgayTiepNhan,
                                                  ngayHenTraKQ: input.NgayHenTraKQ,
                                                  noiDungVuViec: input.NoiDungVuViec,
                                                  soThua: input.SoThua,
                                                  toBanDo: input.ToBanDo,
                                                  dienTich: input.DienTich,
                                                  loaiDat: input.LoaiDat,
                                                  diaChiThuaDat: input.DiaChiThuaDat,
                                                  tinhThuaDat: input.TinhThuaDat,
                                                  huyenThuaDat: input.HuyenThuaDat,
                                                  xaThuaDat: input.XaThuaDat,
                                                  duLieuToaDo: input.DuLieuToaDo,
                                                  duLieuHinhHoc: input.DuLieuHinhHoc,
                                                  GhiChu: input.GhiChu,
                                                  ngayKhieuNai1: input.ngayKhieuNai1,
                                                  NgayTraKQ1: input.NgayTraKQ1,
                                                  ThamQuyen1: input.ThamQuyen1,
                                                  SoQD1: input.SoQD1,
                                                  ngayKhieuNai2: input.ngayKhieuNai2,
                                                  NgayTraKQ2: input.NgayTraKQ2,
                                                  ThamQuyen2: input.ThamQuyen2,
                                                  SoQD2: input.SoQD2,
                                                  KetQua1: input.KetQua1,
                                                  KetQua2: input.KetQua2);
        if (input.FileAttachments.Count > 0)
        {
            foreach (var item in input.FileAttachments)
            {
                var fileAttach = await _denounceManager.CreateFileAttachmentAsync(denounce: denounce,
                                                                        tenTaiLieu: item.TenTaiLieu,
                                                                        hinhThuc: item.HinhThuc,
                                                                        thoiGianBanHanh: item.ThoiGianBanHanh,
                                                                        ngayNhan: item.NgayNhan,
                                                                        thuTuButLuc: item.ThuTuButLuc,
                                                                        noiDungChinh: item.NoiDungChinh,
                                                                        fileName: item.FileContent.FileName,
                                                                        contentType: item.FileContent.ContentType,
                                                                        contentLength: item.FileContent.Length
                                                                        );
                denounce.FileAttachments.Add(fileAttach);
                await UploadAsync(fileAttach.Id.ToString(), item.FileContent);
            }
        }
        await _denounceRepo.InsertAsync(denounce);
        return ObjectMapper.Map<Denounce, DenounceDto>(denounce);
    }
    [Authorize(KNTCPermissions.Denounces.Default)]
    public override async Task<DenounceDto> UpdateAsync(Guid id, UpdateDenounceDto input)
    {
        var denounce = await _denounceRepo.GetAsync(id, false);
        denounce.SetConcurrencyStampIfNotNull(input.ConcurrencyStamp);

        if (input.ListTepDinhKemHoSosDeleted.Count > 0)
        {
            foreach (var idFileAttach in input.ListTepDinhKemHoSosDeleted)
            {
                await _blobContainer.DeleteAsync(idFileAttach.ToString());
            }
        }
        if (input.FileAttachments.Count > 0)
        {
            foreach (var fileAttach in input.FileAttachments)
            {
                if (fileAttach.Id == null)
                {
                    var tepDinhKem = await _denounceManager.CreateFileAttachmentAsync(denounce: denounce,
                                                        tenTaiLieu: fileAttach.TenTaiLieu,
                                                        hinhThuc: fileAttach.HinhThuc,
                                                        thoiGianBanHanh: fileAttach.ThoiGianBanHanh,
                                                        ngayNhan: fileAttach.NgayNhan,
                                                        thuTuButLuc: fileAttach.ThuTuButLuc,
                                                        noiDungChinh: fileAttach.NoiDungChinh,
                                                        fileName: fileAttach.FileContent.FileName,
                                                        contentType: fileAttach.FileContent.ContentType,
                                                        contentLength: fileAttach.FileContent.Length
                                                        );
                    await _fileAttachmentRepo.InsertAsync(tepDinhKem);
                    await UploadAsync(tepDinhKem.Id.ToString(), fileAttach.FileContent);
                }
                else
                {
                    var tepDinhKem = await _fileAttachmentRepo.GetAsync(fileAttach.Id);
                    await _denounceManager.UpdateFileAttachmentAsync(denounce: denounce,
                                                        tepDinhKem: tepDinhKem,
                                                        tenTaiLieu: fileAttach.TenTaiLieu,
                                                        hinhThuc: fileAttach.HinhThuc,
                                                        thoiGianBanHanh: fileAttach.ThoiGianBanHanh,
                                                        ngayNhan: fileAttach.NgayNhan,
                                                        thuTuButLuc: fileAttach.ThuTuButLuc,
                                                        noiDungChinh: fileAttach.NoiDungChinh,
                                                        fileName: fileAttach.FileContent != null ? fileAttach.FileContent.FileName : string.Empty,
                                                        contentType: fileAttach.FileContent != null ? fileAttach.FileContent.ContentType : string.Empty,
                                                        contentLength: fileAttach.FileContent != null ? fileAttach.FileContent.Length : 0
                                                        );
                    await _fileAttachmentRepo.UpdateAsync(tepDinhKem);
                    // FileContent == null => Chỉ thay đổi thông tin không thay đổi file
                    if (fileAttach.FileContent != null)
                    {
                        await UploadAsync(tepDinhKem.Id.ToString(), fileAttach.FileContent);
                    }
                }

            }
        }
        await _denounceManager.UpdateAsync(denounce: denounce,
                                           maHoSo: input.MaHoSo,
                                          loaiVuViec: input.LoaiVuViec,
                                                  tieuDe: input.TieuDe,
                                                  nguoiDeNghi: input.NguoiDeNghi,
                                                  cccdCmnd: input.CccdCmnd,
                                                  ngayCapCccdCmnd: input.NgayCapCccdCmnd,
                                                  noiCapCccdCmnd: input.NoiCapCccdCmnd,
                                                  ngaySinh: input.NgaySinh,
                                                  dienThaoi: input.DienThaoi,
                                                  email: input.Email,
                                                  diaChiThuongTru: input.DiaChiThuongTru,
                                                  diaChiLienHe: input.DiaChiLienHe,
                                                  maTinhTP: input.MaTinhTP,
                                                  maQuanHuyen: input.MaQuanHuyen,
                                                  maXaPhuongTT: input.MaXaPhuongTT,
                                                  ngayTiepNhan: input.NgayTiepNhan,
                                                  ngayHenTraKQ: input.NgayHenTraKQ,
                                                  noiDungVuViec: input.NoiDungVuViec,
                                                  soThua: input.SoThua,
                                                  toBanDo: input.ToBanDo,
                                                  dienTich: input.DienTich,
                                                  loaiDat: input.LoaiDat,
                                                  diaChiThuaDat: input.DiaChiThuaDat,
                                                  tinhThuaDat: input.TinhThuaDat,
                                                  huyenThuaDat: input.HuyenThuaDat,
                                                  xaThuaDat: input.XaThuaDat,
                                                  duLieuToaDo: input.DuLieuToaDo,
                                                  duLieuHinhHoc: input.DuLieuHinhHoc,
                                                  GhiChu: input.GhiChu,
                                                  ngayKhieuNai1: input.ngayKhieuNai1,
                                                  NgayTraKQ1: input.NgayTraKQ1,
                                                  ThamQuyen1: input.ThamQuyen1,
                                                  SoQD1: input.SoQD1,
                                                  ngayKhieuNai2: input.ngayKhieuNai2,
                                                  NgayTraKQ2: input.NgayTraKQ2,
                                                  ThamQuyen2: input.ThamQuyen2,
                                                  SoQD2: input.SoQD2,
                                                  KetQua1: input.KetQua1,
                                                  KetQua2: input.KetQua2);
        await _denounceRepo.UpdateAsync(denounce);
        return ObjectMapper.Map<Denounce, DenounceDto>(denounce);
    }

    [Authorize(KNTCPermissions.Denounces.Delete)]
    public override async Task DeleteAsync(Guid id)
    {
        var idFileAttachs = (await _fileAttachmentRepo.GetListAsync(x => id == x.IdHoSo)).Select(x => x.Id);
        await _fileAttachmentRepo.DeleteManyAsync(idFileAttachs);
        foreach (var item in idFileAttachs)
        {
            await _blobContainer.DeleteAsync(item.ToString());
        }
        await _denounceRepo.DeleteAsync(id);
    }

    //[Authorize(KNTCPermissions.Denounces.Delete)]
    public async Task DeleteMultipleAsync(IEnumerable<Guid> ids)
    {
        var idFileAttachs = (await _fileAttachmentRepo.GetListAsync(x => ids.Contains(x.IdHoSo))).Select(x => x.Id);
        await _fileAttachmentRepo.DeleteManyAsync(idFileAttachs);
        foreach (var item in idFileAttachs)
        {
            await _blobContainer.DeleteAsync(item.ToString());
        }
        await _denounceRepo.DeleteManyAsync(ids);
    }

    private async Task UploadAsync(string idTepDinhKem, IFormFile file)
    {
        if (file == null) throw new UserFriendlyException("Vui lòng chọn tệp đính kèm cho hồ sơ");
        try
        {
            var stream = file.OpenReadStream();
            await _blobContainer.SaveAsync(idTepDinhKem, stream, overrideExisting: true);
        }
        catch (Exception ex)
        {
            throw new UserFriendlyException(ex.Message);
        }

    }

    [Authorize(KNTCPermissions.Denounces.Default)]
    public async Task<byte[]> DowloadAsync(string idTepDinhKem)
    {
        return await _blobContainer.GetAllBytesOrNullAsync(idTepDinhKem);
    }
}
