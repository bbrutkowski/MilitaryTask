using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace MilitaryTask.DataContext
{
    public class MyDataContextFactory : IDesignTimeDbContextFactory<MyDataContext>
    {
        public MyDataContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<MyDataContext>();
            var connectionString = configuration.GetConnectionString("DbConnection");

            optionsBuilder.UseSqlServer(connectionString);

            return new MyDataContext(configuration);
        }
    }
}
