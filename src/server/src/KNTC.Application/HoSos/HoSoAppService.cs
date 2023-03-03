using KNTC.Localization;
using KNTC.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Threading;
using static KNTC.Permissions.KNTCPermissions;

namespace KNTC.HoSos;

//[Authorize(KNTCPermissions.HoSos.Default)]
public class HoSoAppService : CrudAppService<
            HoSo,
            HoSoDto,
            Guid,
            GetHoSoListDto,
            CreateHoSoDto,
            UpdateHoSoDto>, IHoSoAppService
{
    private readonly IHoSoRepository _hoSoRepo;
    private readonly HoSoManager _hoSoManager;
    private readonly IRepository<KQGQHoSo, Guid> _kqgqRepo;
    private readonly IRepository<TepDinhKemHoSo, Guid> _tepDinhKemRepo;
    public HoSoAppService(IRepository<HoSo, Guid> repository,
        IHoSoRepository hoSoRepo,
        IRepository<KQGQHoSo, Guid> kqgqRepo,
        IRepository<TepDinhKemHoSo, Guid> tepDinhKemRepo,
        HoSoManager hoSoManager) : base(repository)
    {
        LocalizationResource = typeof(KNTCResource);
        //GetPolicyName = KNTCPermissions.HoSos.Default;
        //GetListPolicyName = KNTCPermissions.HoSos.Default;
        //CreatePolicyName = KNTCPermissions.HoSos.Create;
        //UpdatePolicyName = KNTCPermissions.HoSos.Edit;
        //DeletePolicyName = KNTCPermissions.HoSos.Delete;
        _hoSoRepo = hoSoRepo;
        _kqgqRepo = kqgqRepo;
        _tepDinhKemRepo = tepDinhKemRepo;
        _hoSoManager = hoSoManager;
    }
    public async override Task<HoSoDto> GetAsync(Guid id)
    {
        var hoSo = await _hoSoRepo.GetAsync(id);
        var hoSoDto = ObjectMapper.Map<HoSo, HoSoDto>(hoSo);
        return hoSoDto;
    }
    public async override Task<PagedResultDto<HoSoDto>> GetListAsync(GetHoSoListDto input)
    {
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = nameof(HoSo.MaHoSo);
        }

        var hoSos = await _hoSoRepo.GetListAsync(
            input.SkipCount,
            input.MaxResultCount,
            input.Sorting,
            input.Keyword,
            input.LoaiVuViec,
            input.LinhVuc,
            input.KetQua
        );

        var totalCount = input.Keyword == null
            ? await _hoSoRepo.CountAsync()
            : await _hoSoRepo.CountAsync(
                x => x.MaHoSo.Contains(input.Keyword) || x.TieuDe.Contains(input.Keyword));

        return new PagedResultDto<HoSoDto>(
        totalCount,
            ObjectMapper.Map<List<HoSo>, List<HoSoDto>>(hoSos)
        );
    }
    public override async Task<HoSoDto> CreateAsync(CreateHoSoDto input)
    {
        var hoSo = await _hoSoManager.CreateAsync(maHoSo: input.MaHoSo,
                                                  yieuDe: input.TieuDe,
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
                                                  loaiVuViec: input.LoaiVuViec,
                                                  linhVuc: input.LinhVuc,
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
                                                  duLieuHinhHoc: input.DuLieuHinhHoc);
        if (input.KQGQHoSos.Count > 0)
        {
            hoSo.SoLanTraKQ = (short)input.KQGQHoSos.Count;
            hoSo.KetQua = input.KQGQHoSos.Last().KetQua;
            foreach (var item in input.KQGQHoSos)
            {
                var kqgq = await _hoSoManager.CreateKQGQHoSoAsync(hoSo: hoSo,
                                                                  lanGQ: item.LanGQ,
                                                                  ngayTraKQ: item.NgayTraKQ,
                                                                  thamQuyen: item.ThamQuyen,
                                                                  soQD: item.SoQD,
                                                                  ghiChu: item.GhiChu,
                                                                  ketQua: item.KetQua);
                hoSo.KQGQHoSos.Add(kqgq);
            }
        }
        if (input.TepDinhKemHoSos.Count > 0)
        {
            foreach (var item in input.TepDinhKemHoSos)
            {
                var kqgq = await _hoSoManager.CreateTepDinhKemHoSoAsync(hoSo: hoSo,
                                                                        tenTaiLieu: item.TenTaiLieu,
                                                                        hinhThuc: item.HinhThuc,
                                                                        thoiGianBanHanh: item.ThoiGianBanHanh,
                                                                        ngayNhan: item.NgayNhan,
                                                                        thuTuButLuc: item.ThuTuButLuc,
                                                                        noiDungChinh: item.NoiDungChinh,

                                                                        fileName: "test file",
                                                                        contentType: "Anh",
                                                                        contentLength: 10240
                                                                        //fileName: item.FileContent.FileName,
                                                                        //contentType: item.FileContent.ContentType,
                                                                        //contentLength: item.FileContent.Length
                                                                        );
                hoSo.TepDinhKemHoSos.Add(kqgq);
            }
        }
        await _hoSoRepo.InsertAsync(hoSo);
        return ObjectMapper.Map<HoSo, HoSoDto>(hoSo);
    }
}
