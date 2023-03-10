using KNTC.FileAttachments;
using KNTC.Localization;
using KNTC.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.BlobStoring;
using Volo.Abp.Data;
using Volo.Abp.Domain.Repositories;

namespace KNTC.Complains;

public class ComplainAppService : CrudAppService<
            Complain,
            ComplainDto,
            Guid,
            GetComplainListDto,
            CreateComplainDto,
            UpdateComplainDto>, IComplainAppService
{
    private readonly IComplainRepository _complainRepo;
    private readonly ComplainManager _complainManager;
    private readonly IRepository<FileAttachment, Guid> _fileAttachmentRepo;
    private readonly IBlobContainer<FileAttachmentContainer> _blobContainer;

    public ComplainAppService(IRepository<Complain, Guid> repository,
        IComplainRepository complainRepo,
        ComplainManager complainManager,
        IRepository<FileAttachment, Guid> fileAttachmentRepo,
        IBlobContainer<FileAttachmentContainer> blobContainer) : base(repository)
    {
        LocalizationResource = typeof(KNTCResource);

        _complainRepo = complainRepo;
        _fileAttachmentRepo = fileAttachmentRepo;
        _complainManager = complainManager;
        _blobContainer = blobContainer;
    }

    [AllowAnonymous]
    public override async Task<PagedResultDto<ComplainDto>> GetListAsync(GetComplainListDto input)
    {
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = nameof(Complain.MaHoSo);
        }
        input.Keyword = input.Keyword.ToUpper();
        var complains = await _complainRepo.GetListAsync(
            input.SkipCount,
            input.MaxResultCount,
            input.Sorting,
            input.Keyword,
            input.LoaiVuViec,
            input.KetQua
        );

        var totalCount = await _complainRepo.CountAsync(
                x => (input.Keyword.IsNullOrEmpty()
                    || (x.MaHoSo.ToUpper().Contains(input.Keyword) || x.TieuDe.ToUpper().Contains(input.Keyword)))
                && (!input.LoaiVuViec.HasValue || x.LoaiVuViec == input.LoaiVuViec)
                && (!input.KetQua.HasValue || x.KetQua == input.KetQua)
                );

        return new PagedResultDto<ComplainDto>(
        totalCount,
            ObjectMapper.Map<List<Complain>, List<ComplainDto>>(complains)
        );
    }
    [Authorize(KNTCPermissions.Complains.Create)]
    public override async Task<ComplainDto> CreateAsync(CreateComplainDto input)
    {
        var complain = await _complainManager.CreateAsync(maHoSo: input.MaHoSo,
                                                  loaiVuViec: input.LoaiVuViec,
                                                  tieuDe: input.TieuDe,
                                                  nguoiDeNghi: input.NguoiDeNghi,
                                                  cccdCmnd: input.CccdCmnd,
                                                  //ngayCapCccdCmnd: input.NgayCapCccdCmnd,
                                                  //noiCapCccdCmnd: input.NoiCapCccdCmnd,
                                                  ngaySinh: input.NgaySinh,
                                                  DienThoai: input.DienThoai,
                                                  email: input.Email,
                                                  diaChiThuongTru: input.DiaChiThuongTru,
                                                  diaChiLienHe: input.DiaChiLienHe,
                                                  maTinhTP: input.maTinhTP,
                                                  maQuanHuyen: input.maQuanHuyen,
                                                  maXaPhuongTT: input.maXaPhuongTT,
                                                  thoiGianTiepNhan: input.ThoiGianTiepNhan,
                                                  thoiGianyHenTraKQ: input.ThoiGianHenTraKQ,
                                                  noiDungVuViec: input.NoiDungVuViec,
                                                  boPhanDangXL: input.boPhanDangXL,
                                                  soThua: input.SoThua,
                                                  toBanDo: input.ToBanDo,
                                                  dienTich: input.DienTich,
                                                  loaiDat: input.LoaiDat,
                                                  diaChiThuaDat: input.DiaChiThuaDat,
                                                  tinhThuaDat: input.tinhThuaDat,
                                                  huyenThuaDat: input.huyenThuaDat,
                                                  xaThuaDat: input.xaThuaDat,
                                                  duLieuToaDo: input.DuLieuToaDo,
                                                  duLieuHinhHoc: input.DuLieuHinhHoc,
                                                  GhiChu: input.GhiChu,
                                                  loaiKhieuNai1: input.loaiKhieuNai1,
                                                  ngayKhieuNai1: input.ngayKhieuNai1,
                                                  NgayTraKQ1: input.NgayTraKQ1,
                                                  ThamQuyen1: input.ThamQuyen1,
                                                  SoQD1: input.SoQD1,
                                                  loaiKhieuNai2: input.loaiKhieuNai2,
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
                var fileAttach = await _complainManager.CreateFileAttachmentAsync(complain: complain,
                                                                        giaiDoan: item.GiaiDoan,
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
                complain.FileAttachments.Add(fileAttach);
                await UploadAsync(fileAttach.Id.ToString(), item.FileContent);
            }
        }
        await _complainRepo.InsertAsync(complain);
        return ObjectMapper.Map<Complain, ComplainDto>(complain);
    }
    [Authorize(KNTCPermissions.Complains.Default)]
    public override async Task<ComplainDto> UpdateAsync(Guid id, UpdateComplainDto input)
    {
        var complain = await _complainRepo.GetAsync(id, false);
        complain.SetConcurrencyStampIfNotNull(input.ConcurrencyStamp);

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
                    var tepDinhKem = await _complainManager.CreateFileAttachmentAsync(complain: complain,
                                                                                      giaiDoan: fileAttach.GiaiDoan,
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
                    await _complainManager.UpdateFileAttachmentAsync(complain: complain,
                                                                     tepDinhKem: tepDinhKem,
                                                                     giaiDoan: fileAttach.GiaiDoan,
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
        await _complainManager.UpdateAsync(complain: complain,
                                           maHoSo: input.MaHoSo,
                                          loaiVuViec: input.LoaiVuViec,
                                          tieuDe: input.TieuDe,
                                          nguoiDeNghi: input.NguoiDeNghi,
                                          cccdCmnd: input.CccdCmnd,
                                          //ngayCapCccdCmnd: input.NgayCapCccdCmnd,
                                          //noiCapCccdCmnd: input.NoiCapCccdCmnd,
                                          ngaySinh: input.NgaySinh,
                                          DienThoai: input.DienThoai,
                                          email: input.Email,
                                          diaChiThuongTru: input.DiaChiThuongTru,
                                          diaChiLienHe: input.DiaChiLienHe,
                                          maTinhTP: input.maTinhTP,
                                          maQuanHuyen: input.maQuanHuyen,
                                          maXaPhuongTT: input.maXaPhuongTT,
                                          thoiGianTiepNhan: input.ThoiGianTiepNhan,
                                          thoiGianyHenTraKQ: input.ThoiGianHenTraKQ,
                                          noiDungVuViec: input.NoiDungVuViec,
                                          boPhanDangXL: input.boPhanDangXL,
                                          soThua: input.SoThua,
                                          toBanDo: input.ToBanDo,
                                          dienTich: input.DienTich,
                                          loaiDat: input.LoaiDat,
                                          diaChiThuaDat: input.DiaChiThuaDat,
                                          tinhThuaDat: input.tinhThuaDat,
                                          huyenThuaDat: input.huyenThuaDat,
                                          xaThuaDat: input.xaThuaDat,
                                          duLieuToaDo: input.DuLieuToaDo,
                                          duLieuHinhHoc: input.DuLieuHinhHoc,
                                          GhiChu: input.GhiChu,
                                          loaiKhieuNai1: input.loaiKhieuNai1,
                                          ngayKhieuNai1: input.ngayKhieuNai1,
                                          NgayTraKQ1: input.NgayTraKQ1,
                                          ThamQuyen1: input.ThamQuyen1,
                                          SoQD1: input.SoQD1,
                                          loaiKhieuNai2: input.loaiKhieuNai2,
                                          ngayKhieuNai2: input.ngayKhieuNai2,
                                          NgayTraKQ2: input.NgayTraKQ2,
                                          ThamQuyen2: input.ThamQuyen2,
                                          SoQD2: input.SoQD2,
                                          KetQua1: input.KetQua1,
                                          KetQua2: input.KetQua2);
        await _complainRepo.UpdateAsync(complain);
        return ObjectMapper.Map<Complain, ComplainDto>(complain);
    }

    [Authorize(KNTCPermissions.Complains.Delete)]
    public override async Task DeleteAsync(Guid id)
    {
        var idFileAttachs = (await _fileAttachmentRepo.GetListAsync(x => id == x.IdHoSo)).Select(x => x.Id);
        await _fileAttachmentRepo.DeleteManyAsync(idFileAttachs);
        foreach (var item in idFileAttachs)
        {
            await _blobContainer.DeleteAsync(item.ToString());
        }
        await _complainRepo.DeleteAsync(id);
    }

    [Authorize(KNTCPermissions.Complains.Delete)]
    public async Task DeleteMultipleAsync(IEnumerable<Guid> ids)
    {
        var idFileAttachs = (await _fileAttachmentRepo.GetListAsync(x => ids.Contains(x.IdHoSo))).Select(x => x.Id);
        await _fileAttachmentRepo.DeleteManyAsync(idFileAttachs);
        foreach (var item in idFileAttachs)
        {
            await _blobContainer.DeleteAsync(item.ToString());
        }
        await _complainRepo.DeleteManyAsync(ids);
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

    [AllowAnonymous]
    public async Task<byte[]> DowloadAsync(string idTepDinhKem)
    {
        return await _blobContainer.GetAllBytesOrNullAsync(idTepDinhKem);
    }
}
