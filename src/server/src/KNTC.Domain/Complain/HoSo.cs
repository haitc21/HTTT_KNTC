using KNTC.Complain;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace KNTC.Complain;

public class HoSo : FullAuditedAggregateRoot<Guid>
{
    public HoSo()
    {
        KQGQHoSos = new List<KQGQHoSo>();
        TepDinhKemHoSos = new List<TepDinhKemHoSo>();
    }
    public HoSo(Guid id) : base(id)
    {
        KQGQHoSos = new List<KQGQHoSo>();
        TepDinhKemHoSos = new List<TepDinhKemHoSo>();
    }
    public HoSo(Guid id, string maHoSo) : base(id)
    {
        SetMaHoSo(maHoSo);
        KQGQHoSos = new List<KQGQHoSo>();
        TepDinhKemHoSos = new List<TepDinhKemHoSo>();
    }
    public string MaHoSo { get; private set; }
    public string TieuDe { get; set; }
    public string NguoiDeNghi { get; set; }
    public string CccdCmnd { get; set; }
    public DateTime NgayCapCccdCmnd { get; set; }
    public string NoiCapCccdCmnd { get; set; }
    public DateTime NgaySinh { get; set; }
    public string DienThaoi { get; set; }
    public string Email { get; set; }
    public string DiaChiThuongTru { get; set; }
    public string DiaChiLienHe { get; set; }
    public string MaTinhTP { get; set; }
    public string MaQuanHuyen { get; set; }
    public string MaXaPhuongTT { get; set; }
    public LoaiVuViec LoaiVuViec { get; set; }
    public LinhVuc LinhVuc { get; set; }
    public DateTime NgayTiepNhan { get; set; }
    public DateTime NgayHenTraKQ { get; set; }
    public string NoiDungVuViec { get; set; }
    public string SoThua { get; set; }
    public string ToBanDo { get; set; }
    public string DienTich { get; set; }
    public string LoaiDat { get; set; }
    public string DiaChiThuaDat { get; set; }
    public string TinhThuaDat { get; set; }
    public string HuyenThuaDat { get; set; }
    public string XaThuaDat { get; set; }
    public string DuLieuToaDo { get; set; }
    public string DuLieuHinhHoc { get; set; }
    public short SoLanTraKQ { get; set; }
    public LoaiKetQua KetQua { get; set; }
    public virtual ICollection<KQGQHoSo> KQGQHoSos { get; set; }
    public virtual ICollection<TepDinhKemHoSo> TepDinhKemHoSos { get; set; }
    private void SetMaHoSo([NotNull] string maHoSo)
    {
        MaHoSo = Check.NotNullOrWhiteSpace(
            maHoSo,
            nameof(maHoSo),
            maxLength: HoSoConsts.MaxCodeLength
        );
    }

    internal HoSo ChangeMaHoSo([NotNull] string maHoSo)
    {
        SetMaHoSo(maHoSo);
        return this;
    }
}
