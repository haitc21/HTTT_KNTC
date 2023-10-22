using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace KNTC.Data;

/* This is used if database provider does't define
 * IKNTCDbSchemaMigrator implementation.
 */

public class NullKNTCDbSchemaMigrator : IKNTCDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}