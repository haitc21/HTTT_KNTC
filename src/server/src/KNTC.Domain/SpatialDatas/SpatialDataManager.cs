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
    public async Task<SpatialData> CreateAsync([NotNull]string geoJson)
    {
        Check.NotNullOrWhiteSpace(geoJson, nameof(geoJson));
        return new SpatialData()
        {
            GeoJson = geoJson
            // suy geometry tu geo json
        };
    }

    public async Task UpdateAsync([NotNull] SpatialData spatialData, string geoJson)
    {
        Check.NotNull(spatialData, nameof(spatialData));

        spatialData.GeoJson = geoJson;
        // suy geometry tu geo json
    }
}
