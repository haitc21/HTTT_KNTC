using Microsoft.SqlServer.Types;
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
    public async Task<SpatialData> CreateAsync([NotNull] SqlGeometry _geometry)
    {

        Check.NotNull(_geometry, nameof(_geometry));
       
        return new SpatialData(_geometry)
        {
            geometry = _geometry
        };
    }
    public async Task UpdateAsync([NotNull] SpatialData spatialData,
                                                 [NotNull] SqlGeometry geometry)
    {
        Check.NotNull(spatialData, nameof(spatialData));
        Check.NotNull(geometry, nameof(geometry));

        if (spatialData.geometry != geometry)
        {
            await ChangeGeometry(spatialData, geometry);
        }

        spatialData.geometry = geometry;
    }
    private async Task ChangeGeometry(SpatialData spatialData, SqlGeometry geometry)
    {
        spatialData.ChangeGeometry(geometry);
    }
}
