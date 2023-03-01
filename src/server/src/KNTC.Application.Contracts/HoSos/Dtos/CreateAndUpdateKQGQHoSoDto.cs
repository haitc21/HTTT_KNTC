using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;
using static KNTC.Permissions.KNTCPermissions;

namespace KNTC.HoSos;

public class CreateAndUpdateKQGQHoSoDto : EntityDto<Guid>
{
    [Required]
    public Guid IdHoSo { get; set; }
    [Required]
    public short LanGQ { get; set; }
    public DateTime NgayTraKQ { get; set; }
    [Required]
    [MaxLength(HoSoConsts.MaxThamQuyenLength)]
    public string ThamQuyen { get; set; }
    [Required]
    [MaxLength(HoSoConsts.MaxSoQDLength)]
    public string SoQD { get; set; }
    [MaxLength(HoSoConsts.MaxGhiChuLength)]
    public string GhiChu { get; set; }
    [Required]
    public LoaiKetQua KetQua { get; set; }
}
