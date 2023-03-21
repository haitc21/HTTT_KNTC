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
using static KNTC.Permissions.KNTCPermissions;

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
        var filter = !input.Keyword.IsNullOrEmpty() ? input.Keyword.ToUpper() : "";
        var complains = await _complainRepo.GetListAsync(
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

        var totalCount = await _complainRepo.CountAsync(
                x => (input.Keyword.IsNullOrEmpty()
                    || (x.MaHoSo.ToUpper().Contains(input.Keyword) || x.TieuDe.ToUpper().Contains(input.Keyword)))
                && (!input.LinhVuc.HasValue || x.LinhVuc == input.LinhVuc)
                && (!input.KetQua.HasValue || x.KetQua == input.KetQua)
                && (!input.maTinhTP.HasValue || x.MaTinhTP == input.maTinhTP)
                && (!input.maQuanHuyen.HasValue || x.MaQuanHuyen == input.maQuanHuyen)
                && (!input.maXaPhuongTT.HasValue || x.MaXaPhuongTT == input.maXaPhuongTT)
                && (!input.GiaiDoan.HasValue || (input.GiaiDoan == 1 && x.NgayKhieuNai2 == null) || (input.GiaiDoan == 2 && x.NgayKhieuNai2 != null))
                && (!input.FromDate.HasValue || x.ThoiGianTiepNhan >= input.FromDate)
                && (!input.ToDate.HasValue || x.ThoiGianTiepNhan <= input.ToDate)
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
                                                  thoiGianyHenTraKQ: input.ThoiGianHenTraKQ,
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
                                                  loaiKhieuNai1: input.loaiKhieuNai1,
                                                  ngayKhieuNai1: input.NgayKhieuNai1,
                                                  NgayTraKQ1: input.NgayTraKQ1,
                                                  ThamQuyen1: input.ThamQuyen1,
                                                  SoQD1: input.SoQD1,
                                                  loaiKhieuNai2: input.loaiKhieuNai2,
                                                  ngayKhieuNai2: input.NgayKhieuNai2,
                                                  NgayTraKQ2: input.NgayTraKQ2,
                                                  ThamQuyen2: input.ThamQuyen2,
                                                  SoQD2: input.SoQD2,
                                                  KetQua1: input.KetQua1,
                                                  KetQua2: input.KetQua2);
        await _complainRepo.InsertAsync(complain);
        var result = ObjectMapper.Map<Complain, ComplainDto>(complain);
        if (input.FileAttachments != null && input.FileAttachments.Count > 0)
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
    [Authorize(KNTCPermissions.Complains.Default)]
    public override async Task<ComplainDto> UpdateAsync(Guid id, UpdateComplainDto input)
    {
        var complain = await _complainRepo.GetAsync(id, false);
        complain.SetConcurrencyStampIfNotNull(input.ConcurrencyStamp);
        await _complainManager.UpdateAsync(complain: complain,
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
                                          thoiGianyHenTraKQ: input.ThoiGianHenTraKQ,
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
                                          loaiKhieuNai1: input.loaiKhieuNai1,
                                          ngayKhieuNai1: input.NgayKhieuNai1,
                                          NgayTraKQ1: input.NgayTraKQ1,
                                          ThamQuyen1: input.ThamQuyen1,
                                          SoQD1: input.SoQD1,
                                          loaiKhieuNai2: input.loaiKhieuNai2,
                                          ngayKhieuNai2: input.NgayKhieuNai2,
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
        var idFileAttachs = (await _fileAttachmentRepo.GetListAsync(x => id == x.ComplainId)).Select(x => x.Id);
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
        var idFileAttachs = (await _fileAttachmentRepo.GetListAsync(x => ids.Any(i => i == x.ComplainId))).Select(x => x.Id);
        await _fileAttachmentRepo.DeleteManyAsync(idFileAttachs);
        foreach (var item in idFileAttachs)
        {
            await _blobContainer.DeleteAsync(item.ToString());
        }
        await _complainRepo.DeleteManyAsync(ids);
    }
}
