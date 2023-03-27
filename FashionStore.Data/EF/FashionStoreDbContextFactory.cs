using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FashionStore.Data.EF
{
    public class FashionStoreDbContextFactory : IDesignTimeDbContextFactory<FashionStoreDbContext>
    {
        public FashionStoreDbContext CreateDbContext(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<FashionStoreDbContext>();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("FashionStoreDb"));

            return new FashionStoreDbContext(optionsBuilder.Options);
        }
    }
}
