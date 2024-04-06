using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public static class DbContextOptionsBuilderExtensions
    {
        const string Mssql = "mssql";
        const string Sqlite = "sqlite";
        const string Postgres = "postgres";

        public static DbContextOptionsBuilder UseDatabase(this DbContextOptionsBuilder builder, string? provider, IConfiguration configuration) 
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(provider), provider);
            provider = provider?.ToLower();

            return provider switch
            {
                Mssql => builder.UseSqlServer(configuration.GetConnectionString("Mssql"), opt =>
                {
                    opt.MigrationsAssembly("MssqlMigrations");
                }),
                Sqlite => builder.UseSqlite(configuration.GetConnectionString("Sqlite"), opt =>
                {
                    opt.MigrationsAssembly("SqliteMigrations");
                }),
                Postgres => builder.UseNpgsql(configuration.GetConnectionString("Postgres"), opt =>
                {
                    opt.MigrationsAssembly("PostgresMigrations");
                }),

                _ => throw new InvalidOperationException($"Unsupported provider: {provider}")
            };
        }
    }
}
