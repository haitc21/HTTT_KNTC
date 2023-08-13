using System;
using Volo.Abp.EventBus;

namespace KNTC.Complains;

[EventName("CreateComplain")]
public class CreateComplainEto
{
    public CreateComplainEto()
    {
    }

    public CreateComplainEto(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
    public string MaHoSo { get; private set; }
    public LinhVuc LinhVuc { get; set; }
    public string TieuDe { get; set; }
    public string NguoiNopDon { get; set; }
    public string CccdCmnd { get; set; }
    public string DienThoai { get; set; }
    public DateTime ThoiGianTiepNhan { get; set; }
    public int MaTinhTP { get; set; }
    public int MaQuanHuyen { get; set; }
    public int MaXaPhuongTT { get; set; }
    public LoaiKetQua? KetQua { get; set; }
    public bool CongKhai { get; set; }
    public string? DuLieuToaDo { get; set; }
    public string? DuLieuHinhHoc { get; set; }
    public string GhiChu { get; set; }
    public ThaoTac ThaoTac { get; set; }
}