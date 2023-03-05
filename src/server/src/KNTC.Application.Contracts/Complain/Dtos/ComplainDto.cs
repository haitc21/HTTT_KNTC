using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using KNTC.Complain;
using Volo.Abp.Application.Dtos;

namespace KNTC.Complain.Dtos;

public class ComplainDto : AuditedEntityDto<Guid>
{
    public Guid IdHoSo { get; set; }
    //public LoaiVuViec LoaiVuViec { get; set; }
    public LinhVuc LinhVuc { get; set; }
    public int Status { get; set; }
    //public string TieuDe { get; set; }

    //1.THÔNG TIN KHIẾU NẠI/KHIẾU KIỆN
    public string MaHoSo { get; set; }
    public string NguoiDeNghi { get; set; }
    public string CccdCmnd { get; set; }
    public DateTime NgaySinh { get; set; }
    public string DienThoai { get; set; }
    public string Email { get; set; }
    public string DiaChiLienHe { get; set; }
    public string MaTinhTP { get; set; }
    public string MaQuanHuyen { get; set; }
    public string MaXaPhuongTT { get; set; }
    public DateTime NgayTiepNhan { get; set; }
    public DateTime NgayHenTraKQ { get; set; }
    public string NoiDungVuViec { get; set; }
    public string BoPhanDangXL { get; set; }
    //Chưa dùng đến
    public DateTime NgayCapCccdCmnd { get; set; }
    public string NoiCapCccdCmnd { get; set; }
    public string DiaChiThuongTru { get; set; }

    //2.THÔNG TIN THỬA ĐẤT
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

    //3.THÔNG TIN GIẢI QUYẾT
    public LoaiKhieuNai LoaiKNLan1 { get; set; }
    public DateTime NgayTraLan1 { get; set; }
    public string ThamQuyenLan1 { get; set; }
    public string SoQDLan1 { get; set; }
    public DateTime NgayQDLan1 { get; set; }    
    public LoaiKetQua KetQuaKNLan1{ get; set; }

    public LoaiKhieuNai LoaiKNLan2 { get; set; }
    public DateTime NgayTraLan2 { get; set; }
    public string ThamQuyenLan2 { get; set; }
    public string SoQDLan2 { get; set; }
    public DateTime NgayQDLan2 { get; set; }
    public LoaiKetQua KetQuaKNLan2 { get; set; }

    public string GhiChu { get; set; }

    //4.File gắn kèm
    //public virtual List<KQGQHoSoDto> KQGQHoSos { get; set; }
    public virtual List<TepDinhKemHoSoDto> TepDinhKemHoSos { get; set; }
}
