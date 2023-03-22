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

namespace KNTC.Denounces;
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

        _denounceRepo = denounceRepo;
        _fileAttachmentRepo = fileAttachmentRepo;
        _denounceManager = denounceManager;
        _blobContainer = blobContainer;
    }

    [AllowAnonymous]
    public override async Task<PagedResultDto<DenounceDto>> GetListAsync(GetDenounceListDto input)
    {
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = nameof(Denounce.MaHoSo);
        }
        var filter = !input.Keyword.IsNullOrEmpty() ? input.Keyword.ToUpper() : "";
        var denounces = await _denounceRepo.GetListAsync(
            input.SkipCount,
            input.MaxResultCount,
            input.Sorting,
            input.Keyword,
            input.LinhVuc,
            input.KetQua,
            input.maTinhTP,
            input.maQuanHuyen,
            input.maXaPhuongTT,
            input.GiaiDoan,
            input.FromDate,
            input.ToDate
        );

        var totalCount = await _denounceRepo.CountAsync(
                       x => (input.Keyword.IsNullOrEmpty()
                           || (x.MaHoSo.ToUpper().Contains(input.Keyword) || x.TieuDe.ToUpper().Contains(input.Keyword)))
                       && (!input.LinhVuc.HasValue || x.LinhVuc == input.LinhVuc)
                       && (!input.KetQua.HasValue || x.KetQua == input.KetQua)
                       && (!input.maTinhTP.HasValue || x.MaTinhTP == input.maTinhTP)
                       && (!input.maQuanHuyen.HasValue || x.MaQuanHuyen == input.maQuanHuyen)
                       && (!input.maXaPhuongTT.HasValue || x.MaXaPhuongTT == input.maXaPhuongTT)
                       && (!input.GiaiDoan.HasValue || (input.GiaiDoan == 1 && x.NgayKhieuNai1 != null && x.NgayKhieuNai2 == null) || (input.GiaiDoan == 2 && x.NgayKhieuNai2 != null))
                       && (!input.FromDate.HasValue || x.ThoiGianTiepNhan >= input.FromDate)
                       && (!input.ToDate.HasValue || x.ThoiGianTiepNhan <= input.ToDate)
                       );

        return new PagedResultDto<DenounceDto>(
        totalCount,
            ObjectMapper.Map<List<Denounce>, List<DenounceDto>>(denounces)
        );
    }
    [Authorize(KNTCPermissions.Denounces.Create)]
    public override async Task<DenounceDto> CreateAsync(CreateDenounceDto input)
    {
        var denounce = await _denounceManager.CreateAsync(maHoSo: input.MaHoSo,
                                                  linhVuc: input.LinhVuc,
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
                                                  thoiGianHenTraKQ: input.ThoiGianTraKQ,
                                                  noiDungVuViec: input.NoiDungVuViec,
                                                  boPhanDangXL: input.BoPhanDangXL,
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

                                                  ngayKhieuNai1: input.NgayKhieuNai1,
                                                  NgayTraKQ1: input.NgayTraKQ1,
                                                  ThamQuyen1: input.ThamQuyen1,
                                                  SoQD1: input.SoQD1,

                                                  ngayKhieuNai2: input.NgayKhieuNai2,
                                                  NgayTraKQ2: input.NgayTraKQ2,
                                                  ThamQuyen2: input.ThamQuyen2,
                                                  SoQD2: input.SoQD2,
                                                  KetQua1: input.KetQua1,
                                                  KetQua2: input.KetQua2);

        await _denounceRepo.InsertAsync(denounce);
        var result = ObjectMapper.Map<Denounce, DenounceDto>(denounce);
        if (input.FileAttachments != null && input.FileAttachments.Count > 0)
        {
            foreach (var item in input.FileAttachments)
            {
                var fileAttach = await _denounceManager.CreateFileAttachmentAsync(denounce: denounce,
                                                                        giaiDoan: item.GiaiDoan,
                                                                        tenTaiLieu: item.TenTaiLieu,
                                                                        hinhThuc: item.HinhThuc,
                                                                        thoiGianBanHanh: item.ThoiGianBanHanh,
                                                                        ngayNhan: item.NgayNhan,
                                                                        thuTuButLuc: item.ThuTuButLuc,
                                                                        noiDungChinh: item.NoiDungChinh,
                                                                        fileName: item.FileName,
                                                                        contentType: item.ContentType,
                                                                        contentLength: item.ContentLength
                                                                        );

                await _fileAttachmentRepo.InsertAsync(fileAttach);
                result.FileAttachments.Add(ObjectMapper.Map<FileAttachment, FileAttachmentDto>(fileAttach));
            }
        }
        return result;
    }
    [Authorize(KNTCPermissions.Denounces.Default)]
    public override async Task<DenounceDto> UpdateAsync(Guid id, UpdateDenounceDto input)
    {
        var denounce = await _denounceRepo.GetAsync(id, false);
        denounce.SetConcurrencyStampIfNotNull(input.ConcurrencyStamp);
        await _denounceManager.UpdateAsync(denounce: denounce,
                                           maHoSo: input.MaHoSo,
                                          linhVuc: input.LinhVuc,
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
                                                  thoiGianHenTraKQ: input.ThoiGianHenTraKQ,
                                                  noiDungVuViec: input.NoiDungVuViec,
                                                  boPhanDangXL: input.BoPhanDangXL,
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
                                                  ngayKhieuNai1: input.NgayKhieuNai1,
                                                  NgayTraKQ1: input.NgayTraKQ1,
                                                  ThamQuyen1: input.ThamQuyen1,
                                                  SoQD1: input.SoQD1,
                                                  ngayKhieuNai2: input.NgayKhieuNai2,
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
        var idFileAttachs = (await _fileAttachmentRepo.GetListAsync(x => id == x.DenounceId)).Select(x => x.Id);
        await _fileAttachmentRepo.DeleteManyAsync(idFileAttachs);
        foreach (var item in idFileAttachs)
        {
            await _blobContainer.DeleteAsync(item.ToString());
        }
        await _denounceRepo.DeleteAsync(id);
    }

    [Authorize(KNTCPermissions.Denounces.Delete)]
    public async Task DeleteMultipleAsync(IEnumerable<Guid> ids)
    {
        var idFileAttachs = (await _fileAttachmentRepo.GetListAsync(x => ids.Any(i => i == x.DenounceId))).Select(x => x.Id);
        await _fileAttachmentRepo.DeleteManyAsync(idFileAttachs);
        foreach (var item in idFileAttachs)
        {
            await _blobContainer.DeleteAsync(item.ToString());
        }
        await _denounceRepo.DeleteManyAsync(ids);
    }
}
