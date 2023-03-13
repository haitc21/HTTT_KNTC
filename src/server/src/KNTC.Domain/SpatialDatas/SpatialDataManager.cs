using Microsoft.SqlServer.Types;
using NetTopologySuite.Geometries;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace KNTC.SpatialDatas;

public class SpatialDataManager : DomainService
{
    private readonly IRepository<SpatialData, int> _spatialDataRepo;
    public SpatialDataManager(IRepository<SpatialData, int> spatialDataRepo)
    {
        _spatialDataRepo = spatialDataRepo;
    }
    public async Task<SpatialData> CreateAsync([NotNull] Geometry _geometry)
    {

        Check.NotNull(_geometry, nameof(_geometry));
       
        return new SpatialData(_geometry)
        {
            Geometry = _geometry
        };
    }
    public async Task UpdateAsync([NotNull] SpatialData spatialData,
                                                 [NotNull] Geometry geometry)
    {
        Check.NotNull(spatialData, nameof(spatialData));
        Check.NotNull(geometry, nameof(geometry));

        if (spatialData.Geometry != geometry)
        {
            await ChangeGeometry(spatialData, geometry);
        }

        spatialData.Geometry = geometry;
    }
    private async Task ChangeGeometry(SpatialData spatialData, Geometry geometry)
    {
        spatialData.ChangeGeometry(geometry);
    }
}
