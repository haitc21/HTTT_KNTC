﻿using NetTopologySuite.Geometries;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace KNTC.SpatialDatas;

public class SpatialData : FullAuditedAggregateRoot<int>
{
    public SpatialData()
    {

    }
    public SpatialData(int id) : base(id)
    {

    }
    // NotMapped khi chạy DbMigrator
    //[NotMapped]
    public Geometry Geometry { get; set; }
    public string GeoJson { get; set; }
}
