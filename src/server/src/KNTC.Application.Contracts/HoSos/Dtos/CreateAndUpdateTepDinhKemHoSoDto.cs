﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;
using static KNTC.Permissions.KNTCPermissions;

namespace KNTC.HoSos;

public class CreateAndUpdateTepDinhKemHoSoDto : EntityDto<Guid>
{
    [Required]
    public Guid IdHoSo { get; set; }
    [Required]
    [MaxLength(HoSoConsts.MaxTenTaiLieuLength)]
    public string TenTaiLieu { get; set; }
    [Required]
    [MaxLength(HoSoConsts.MaxHinhThucLength)]
    public string HinhThuc { get; set; }
    public DateTime ThoiGianBanHanh { get; set; }
    public DateTime NgayNhan { get; set; }
    [Required]
    [MaxLength(HoSoConsts.MaxThuTuButLucLength)]
    public short ThuTuButLuc { get; set; }
    public string NoiDungChinh { get; set; }
}
