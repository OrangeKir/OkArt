using System.Reflection;
using FluentMigrator.Runner;

namespace OkArt.Infrastructure.Helpers;

public static class MigrationsHelper
{
    public static void Migrate(string dbConnectionString)
    {
        using var provider = new ServiceCollection()
            .AddFluentMigratorCore()
            .ConfigureRunner(rb => rb.AddPostgres11_0()
                .WithGlobalConnectionString(dbConnectionString)
                .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations())
            .AddLogging(lb => lb.AddFluentMigratorConsole())
            .BuildServiceProvider();
        
        using var scope = provider.CreateScope();
        var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
        runner.MigrateUp();
    }
}