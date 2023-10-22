using Dapper;
using KNTC.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories.Dapper;
using Volo.Abp.EntityFrameworkCore;

namespace KNTC.Summaries;

public class SummaryDapperRepository :
    DapperRepository<KNTCDbContext>, ITransientDependency, ISummaryDapperRepository
{
    public SummaryDapperRepository(IDbContextProvider<KNTCDbContext> dbContextProvider)
    : base(dbContextProvider)
    {
    }
    public virtual async Task RefreshView()
    {
        var dbConnection = await GetDbConnectionAsync();
        await dbConnection.QueryAsync(
            "REFRESH MATERIALIZED VIEW \"KNTC\".\"Summaries\"",
            transaction: await GetDbTransactionAsync());
    }
}
