using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Npgsql;

namespace portfolio_backend_Csharp.Data
{
    public class ProjectBackendContextFactory : IDesignTimeDbContextFactory<ProjectBackendContext>
    {
        public ProjectBackendContext CreateDbContext(string[] args)
        {
            var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL")
                ?? throw new InvalidOperationException("DATABASE_URL non trouvé");

            DbContextOptionsBuilder<ProjectBackendContext> optionsBuilder = new();

            if (databaseUrl.StartsWith("Host="))
            {
                // ✅ Connexion locale déjà formatée
                optionsBuilder.UseNpgsql(databaseUrl);
            }
            else if (databaseUrl.StartsWith("postgresql://") || databaseUrl.StartsWith("postgres://"))
            {
                // ✅ Connexion Render à parser
                var databaseUrlFixed = databaseUrl.Replace("postgresql://", "postgres://");
                var uri = new Uri(databaseUrlFixed);

                var userInfo = uri.UserInfo.Split(':');
                if (userInfo.Length != 2)
                    throw new InvalidOperationException("Format USER:PASS invalide dans DATABASE_URL");

                var npgsqlBuilder = new NpgsqlConnectionStringBuilder
                {
                    Host = uri.Host,
                    Port = uri.Port > 0 ? uri.Port : 5432,
                    Username = userInfo[0],
                    Password = userInfo[1],
                    Database = uri.AbsolutePath.TrimStart('/'),
                    SslMode = SslMode.Require
                    //TrustServerCertificate = true
                };

                optionsBuilder.UseNpgsql(npgsqlBuilder.ConnectionString);
            }
            else
            {
                throw new InvalidOperationException("Format de DATABASE_URL non reconnu.");
            }

            return new ProjectBackendContext(optionsBuilder.Options);
        }
    }
}
