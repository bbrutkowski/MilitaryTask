using Microsoft.EntityFrameworkCore;
using MilitaryTask.DataContext.Interface;
using MilitaryTask.Model;

namespace MilitaryTask.DataContext
{
    public class DataContext : DbContext, IDataContext
    {
        private const string _connectionString = "Data Source=.;Initial Catalog=MilitaryDB;Integrated Security=True;TrustServerCertificate=True;";
        public DbSet<Order> OrderTable { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);  
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
