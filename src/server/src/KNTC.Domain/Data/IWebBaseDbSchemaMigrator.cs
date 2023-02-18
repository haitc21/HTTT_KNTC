using System.Threading.Tasks;

namespace KNTC.Data;

public interface IKNTCDbSchemaMigrator
{
    Task MigrateAsync();
}