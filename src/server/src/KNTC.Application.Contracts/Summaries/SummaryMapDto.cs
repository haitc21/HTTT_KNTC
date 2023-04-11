﻿using KNTC.Units;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Caching;

namespace KNTC.Summaries;
[CacheName("SummaryMap")]
public class SummaryMapCache
{
    public List<SummaryMapDto> Items { get; set; }
}

public class SummaryMapDto
{
    public Guid Id { get; set; }
    public LoaiVuViec LoaiVuViec { get; set; }
    public string DuLieuToaDo { get; set; }
    public string DuLieuHinhHoc { get; set; }

}