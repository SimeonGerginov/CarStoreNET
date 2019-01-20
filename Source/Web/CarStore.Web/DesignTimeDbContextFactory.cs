using System.IO;

using CarStore.Common;
using CarStore.Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CarStore.Web
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<CarStoreDbContext>
    {
        public CarStoreDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(GlobalConstants.JsonFile)
                .Build();

            var builder = new DbContextOptionsBuilder<CarStoreDbContext>();
            var connectionString = configuration.GetConnectionString(GlobalConstants.ConnectionStringKey);

            builder.UseSqlServer(connectionString);

            return new CarStoreDbContext(builder.Options);
        }
    }
}
