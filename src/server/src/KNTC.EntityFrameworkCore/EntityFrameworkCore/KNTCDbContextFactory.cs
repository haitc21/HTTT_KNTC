using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace KNTC.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */

public class KNTCDbContextFactory : IDesignTimeDbContextFactory<KNTCDbContext>
{
    public KNTCDbContext CreateDbContext(string[] args)
    {
        KNTCEfCoreEntityExtensionMappings.Configure();

        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<KNTCDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"), x =>
            {
                x.UseNetTopologySuite();
            });

        return new KNTCDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../KNTC.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}