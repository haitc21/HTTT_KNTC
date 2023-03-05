//using KNTC.Localization;
//using KNTC.Permissions;
//using KNTC.Users;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Mime;
//using System.Text;
//using System.Threading.Tasks;
//using System.Xml;
//using Volo.Abp;
//using Volo.Abp.Application.Dtos;
//using Volo.Abp.Application.Services;
//using Volo.Abp.BlobStoring;
//using Volo.Abp.Data;
//using Volo.Abp.Domain.Repositories;
//using Volo.Abp.ObjectMapping;
//using Volo.Abp.Threading;
//using Volo.Abp.Users;
//using static KNTC.Permissions.KNTCPermissions;
//using static Volo.Abp.Identity.Settings.IdentitySettingNames;

//namespace KNTC.HoSos;

////[Authorize(KNTCPermissions.HoSos.Default)]
//public class HoSoAppService : CrudAppService<
//            HoSo,
//            HoSoDto,
//            Guid,
//            GetHoSoListDto,
//            CreateHoSoDto,
//            UpdateHoSoDto>, IHoSoAppService
//{
//    private readonly IHoSoRepository _hoSoRepo;
//    private readonly HoSoManager _hoSoManager;
//    private readonly IRepository<KQGQHoSo, Guid> _kqgqRepo;
//    private readonly IRepository<TepDinhKemHoSo, Guid> _tepDinhKemRepo;
//    private readonly IBlobContainer<TepDinhKemHoSorContainer> _blobContainer;

//    public HoSoAppService(IRepository<HoSo, Guid> repository,
//        IHoSoRepository hoSoRepo,
//        IRepository<KQGQHoSo, Guid> kqgqRepo,
//        IRepository<TepDinhKemHoSo, Guid> tepDinhKemRepo,
//        HoSoManager hoSoManager,
//        IBlobContainer<TepDinhKemHoSorContainer> blobContainer) : base(repository)
//    {
//        LocalizationResource = typeof(KNTCResource);
//        //GetPolicyName = KNTCPermissions.HoSos.Default;

//        _hoSoRepo = hoSoRepo;
//        _kqgqRepo = kqgqRepo;
//        _tepDinhKemRepo = tepDinhKemRepo;
//        _hoSoManager = hoSoManager;
//        _blobContainer = blobContainer;
//    }

//    [Authorize(KNTCPermissions.HoSos.Default)]
//    public async override Task<PagedResultDto<HoSoDto>> GetListAsync(GetHoSoListDto input)
//    {
//        if (input.Sorting.IsNullOrWhiteSpace())
//        {
//            input.Sorting = nameof(HoSo.MaHoSo);
//        }

//        var hoSos = await _hoSoRepo.GetListAsync(
//            input.SkipCount,
//            input.MaxResultCount,
//            input.Sorting,
//            input.Keyword,
//            input.LoaiVuViec,
//            input.LinhVuc,
//            input.KetQua
//        );

//        var totalCount = input.Keyword == null
//            ? await _hoSoRepo.CountAsync()
//            : await _hoSoRepo.CountAsync(
//                x => x.MaHoSo.Contains(input.Keyword) || x.TieuDe.Contains(input.Keyword));

//        return new PagedResultDto<HoSoDto>(
//        totalCount,
//            ObjectMapper.Map<List<HoSo>, List<HoSoDto>>(hoSos)
//        );
//    }
//    //[Authorize(KNTCPermissions.HoSos.Create)]
//    public override async Task<HoSoDto> CreateAsync(CreateHoSoDto input)
//    {
//        var hoSo = await _hoSoManager.CreateAsync(maHoSo: input.MaHoSo,
//                                                  yieuDe: input.TieuDe,
//                                                  nguoiDeNghi: input.NguoiDeNghi,
//                                                  cccdCmnd: input.CccdCmnd,
//                                                  ngayCapCccdCmnd: input.NgayCapCccdCmnd,
//                                                  noiCapCccdCmnd: input.NoiCapCccdCmnd,
//                                                  ngaySinh: input.NgaySinh,
//                                                  dienThaoi: input.DienThaoi,
//                                                  email: input.Email,
//                                                  diaChiThuongTru: input.DiaChiThuongTru,
//                                                  diaChiLienHe: input.DiaChiLienHe,
//                                                  maTinhTP: input.MaTinhTP,
//                                                  maQuanHuyen: input.MaQuanHuyen,
//                                                  maXaPhuongTT: input.MaXaPhuongTT,
//                                                  loaiVuViec: input.LoaiVuViec,
//                                                  linhVuc: input.LinhVuc,
//                                                  ngayTiepNhan: input.NgayTiepNhan,
//                                                  ngayHenTraKQ: input.NgayHenTraKQ,
//                                                  noiDungVuViec: input.NoiDungVuViec,
//                                                  soThua: input.SoThua,
//                                                  toBanDo: input.ToBanDo,
//                                                  dienTich: input.DienTich,
//                                                  loaiDat: input.LoaiDat,
//                                                  diaChiThuaDat: input.DiaChiThuaDat,
//                                                  tinhThuaDat: input.TinhThuaDat,
//                                                  huyenThuaDat: input.HuyenThuaDat,
//                                                  xaThuaDat: input.XaThuaDat,
//                                                  duLieuToaDo: input.DuLieuToaDo,
//                                                  duLieuHinhHoc: input.DuLieuHinhHoc);
//        if (input.KQGQHoSos.Count > 0)
//        {
//            hoSo.SoLanTraKQ = (short)input.KQGQHoSos.Count;
//            hoSo.KetQua = input.KQGQHoSos.Last().KetQua;
//            foreach (var item in input.KQGQHoSos)
//            {
//                var kqgq = await _hoSoManager.CreateKQGQHoSoAsync(hoSo: hoSo,
//                                                                  lanGQ: item.LanGQ,
//                                                                  ngayKhieuNai: item.ngayKhieuNai,
//                                                                  ngayTraKQ: item.NgayTraKQ,
//                                                                  thamQuyen: item.ThamQuyen,
//                                                                  soQD: item.SoQD,
//                                                                  ghiChu: item.GhiChu,
//                                                                  ketQua: item.KetQua);
//                hoSo.KQGQHoSos.Add(kqgq);
//            }
//        }
//        else
//        {
//            hoSo.SoLanTraKQ = 0;
//            hoSo.KetQua = null;
//        }
//        if (input.TepDinhKemHoSos.Count > 0)
//        {
//            foreach (var item in input.TepDinhKemHoSos)
//            {
//                var teDinhKem = await _hoSoManager.CreateTepDinhKemHoSoAsync(hoSo: hoSo,
//                                                                        tenTaiLieu: item.TenTaiLieu,
//                                                                        hinhThuc: item.HinhThuc,
//                                                                        thoiGianBanHanh: item.ThoiGianBanHanh,
//                                                                        ngayNhan: item.NgayNhan,
//                                                                        thuTuButLuc: item.ThuTuButLuc,
//                                                                        noiDungChinh: item.NoiDungChinh,
//                                                                        fileName: item.FileContent.FileName,
//                                                                        contentType: item.FileContent.ContentType,
//                                                                        contentLength: item.FileContent.Length
//                                                                        );
//                hoSo.TepDinhKemHoSos.Add(teDinhKem);
//                await UploadAsync(teDinhKem.Id.ToString(), item.FileContent);
//            }
//        }
//        await _hoSoRepo.InsertAsync(hoSo);
//        return ObjectMapper.Map<HoSo, HoSoDto>(hoSo);
//    }
//    //[Authorize(KNTCPermissions.HoSos.Default)]
//    public override async Task<HoSoDto> UpdateAsync(Guid id, UpdateHoSoDto input)
//    {
//        var hoSo = await _hoSoRepo.GetAsync(id, false);
//        hoSo.SetConcurrencyStampIfNotNull(input.ConcurrencyStamp);
//        if (input.ListKQGQHoSoDeleted.Count > 0)
//        {
//            await _kqgqRepo.DeleteManyAsync(input.ListKQGQHoSoDeleted);
//        }

//        if (input.ListTepDinhKemHoSosDeleted.Count > 0)
//        {
//            await _tepDinhKemRepo.DeleteManyAsync(input.ListTepDinhKemHoSosDeleted);
//            foreach (var item in input.ListTepDinhKemHoSosDeleted)
//            {
//                await _blobContainer.DeleteAsync(item.ToString());
//            }
//        }

//        if (input.KQGQHoSos.Count > 0)
//        {
//            hoSo.SoLanTraKQ = (short)input.KQGQHoSos.Count;
//            hoSo.KetQua = input.KQGQHoSos.Last().KetQua;
//            foreach (var item in input.KQGQHoSos)
//            {
//                if (item.Id == null)
//                {
//                    var kqgq = await _hoSoManager.CreateKQGQHoSoAsync(hoSo: hoSo,
//                                                  lanGQ: item.LanGQ,
//                                                  ngayKhieuNai: item.ngayKhieuNai,
//                                                  ngayTraKQ: item.NgayTraKQ,
//                                                  thamQuyen: item.ThamQuyen,
//                                                  soQD: item.SoQD,
//                                                  ghiChu: item.GhiChu,
//                                                  ketQua: item.KetQua);
//                    await _kqgqRepo.InsertAsync(kqgq);
//                }
//                else
//                {
//                    var kqgqHoSo = await _kqgqRepo.GetAsync(item.Id);
//                    await _hoSoManager.UpdateeKQGQHoSoAsync(hoSo: hoSo,
//                                                            kqgqHoSo: kqgqHoSo,
//                                                            lanGQ: item.LanGQ,
//                                                            ngayKhieuNai: item.ngayKhieuNai,
//                                                            ngayTraKQ: item.NgayTraKQ,
//                                                            thamQuyen: item.ThamQuyen,
//                                                            soQD: item.SoQD,
//                                                            ghiChu: item.GhiChu,
//                                                            ketQua: item.KetQua);
//                    await _kqgqRepo.UpdateAsync(kqgqHoSo);
//                }
//            }
//        }
//        else
//        {
//            hoSo.SoLanTraKQ = 0;
//            hoSo.KetQua = null;
//        }

//        if (input.TepDinhKemHoSos.Count > 0)
//        {
//            foreach (var item in input.TepDinhKemHoSos)
//            {
//                if (item.Id == null)
//                {
//                    var tepDinhKem = await _hoSoManager.CreateTepDinhKemHoSoAsync(hoSo: hoSo,
//                                                        tenTaiLieu: item.TenTaiLieu,
//                                                        hinhThuc: item.HinhThuc,
//                                                        thoiGianBanHanh: item.ThoiGianBanHanh,
//                                                        ngayNhan: item.NgayNhan,
//                                                        thuTuButLuc: item.ThuTuButLuc,
//                                                        noiDungChinh: item.NoiDungChinh,
//                                                        fileName: item.FileContent.FileName,
//                                                        contentType: item.FileContent.ContentType,
//                                                        contentLength: item.FileContent.Length
//                                                        );
//                    await _tepDinhKemRepo.InsertAsync(tepDinhKem);
//                    await UploadAsync(tepDinhKem.Id.ToString(), item.FileContent);
//                }
//                else
//                {
//                    var tepDinhKem = await _tepDinhKemRepo.GetAsync(item.Id);
//                    await _hoSoManager.UpdateTepDinhKemHoSoAsync(hoSo: hoSo,
//                                                        tepDinhKem: tepDinhKem,
//                                                        tenTaiLieu: item.TenTaiLieu,
//                                                        hinhThuc: item.HinhThuc,
//                                                        thoiGianBanHanh: item.ThoiGianBanHanh,
//                                                        ngayNhan: item.NgayNhan,
//                                                        thuTuButLuc: item.ThuTuButLuc,
//                                                        noiDungChinh: item.NoiDungChinh,
//                                                        fileName: item.FileContent != null ? item.FileContent.FileName : string.Empty,
//                                                        contentType: item.FileContent != null ? item.FileContent.ContentType : string.Empty,
//                                                        contentLength: item.FileContent != null ? item.FileContent.Length : 0
//                                                        );
//                    await _tepDinhKemRepo.UpdateAsync(tepDinhKem);
//                    // FileContent == null => Chỉ thay đổi thông tin không thay đổi file
//                    if (item.FileContent != null)
//                    {
//                        await UploadAsync(tepDinhKem.Id.ToString(), item.FileContent);
//                    }
//                }

//            }
//        }
//        await _hoSoManager.UpdateAsync(hoSo: hoSo,
//                                       maHoSo: input.MaHoSo,
//                                       yieuDe: input.TieuDe,
//                                       nguoiDeNghi: input.NguoiDeNghi,
//                                       cccdCmnd: input.CccdCmnd,
//                                       ngayCapCccdCmnd: input.NgayCapCccdCmnd,
//                                       noiCapCccdCmnd: input.NoiCapCccdCmnd,
//                                       ngaySinh: input.NgaySinh,
//                                       dienThaoi: input.DienThaoi,
//                                       email: input.Email,
//                                       diaChiThuongTru: input.DiaChiThuongTru,
//                                       diaChiLienHe: input.DiaChiLienHe,
//                                       maTinhTP: input.MaTinhTP,
//                                       maQuanHuyen: input.MaQuanHuyen,
//                                       maXaPhuongTT: input.MaXaPhuongTT,
//                                       loaiVuViec: input.LoaiVuViec,
//                                       linhVuc: input.LinhVuc,
//                                       ngayTiepNhan: input.NgayTiepNhan,
//                                       ngayHenTraKQ: input.NgayHenTraKQ,
//                                       noiDungVuViec: input.NoiDungVuViec,
//                                       soThua: input.SoThua,
//                                       toBanDo: input.ToBanDo,
//                                       dienTich: input.DienTich,
//                                       loaiDat: input.LoaiDat,
//                                       diaChiThuaDat: input.DiaChiThuaDat,
//                                       tinhThuaDat: input.TinhThuaDat,
//                                       huyenThuaDat: input.HuyenThuaDat,
//                                       xaThuaDat: input.XaThuaDat,
//                                       duLieuToaDo: input.DuLieuToaDo,
//                                                  duLieuHinhHoc: input.DuLieuHinhHoc);
//        await _hoSoRepo.UpdateAsync(hoSo);
//        return ObjectMapper.Map<HoSo, HoSoDto>(hoSo);
//    }

//    //[Authorize(KNTCPermissions.HoSos.Delete)]
//    public override async Task DeleteAsync(Guid id)
//    {
//        var idKqgqs = (await _kqgqRepo.GetListAsync(x => id == x.IdHoSo)).Select(x => x.Id);
//        await _kqgqRepo.DeleteManyAsync(idKqgqs);
//        var idTepDinhKems = (await _tepDinhKemRepo.GetListAsync(x => id == x.IdHoSo)).Select(x => x.Id);
//        await _tepDinhKemRepo.DeleteManyAsync(idTepDinhKems);
//        foreach (var item in idTepDinhKems)
//        {
//            await _blobContainer.DeleteAsync(item.ToString());
//        }
//        await _hoSoRepo.DeleteAsync(id);
//    }

//    //[Authorize(KNTCPermissions.HoSos.Delete)]
//    public async Task DeleteMultipleAsync(IEnumerable<Guid> ids)
//    {
//        var idKqgqs = (await _kqgqRepo.GetListAsync(x => ids.Contains(x.IdHoSo))).Select(x => x.Id);
//        await _kqgqRepo.DeleteManyAsync(idKqgqs);
//        var idTepDinhKems = (await _tepDinhKemRepo.GetListAsync(x => ids.Contains(x.IdHoSo))).Select(x => x.Id);
//        await _tepDinhKemRepo.DeleteManyAsync(idTepDinhKems);
//        foreach (var item in idTepDinhKems)
//        {
//            await _blobContainer.DeleteAsync(item.ToString());
//        }
//        await _hoSoRepo.DeleteManyAsync(ids);
//    }

//    private async Task UploadAsync(string idTepDinhKem, IFormFile file)
//    {
//        if (file == null) throw new UserFriendlyException("Vui lòng chọn tệp đính kèm cho hồ sơ");
//        try
//        {
//            var stream = file.OpenReadStream();
//            await _blobContainer.SaveAsync(idTepDinhKem, stream, overrideExisting: true);
//        }
//        catch (Exception ex)
//        {
//            throw new UserFriendlyException(ex.Message);
//        }

//    }

//    [Authorize(KNTCPermissions.HoSos.Default)]
//    public async Task<byte[]> DowloadAsync(string idTepDinhKem)
//    {
//        return await _blobContainer.GetAllBytesOrNullAsync(idTepDinhKem);
//    }
//}
