using Microsoft.EntityFrameworkCore;
using MilitaryTask.Model;

namespace MilitaryTask.DataContext
{
    internal class DataContext : DbContext
    {
        private const string _connectionString = "Data Source=.;Initial Catalog=MilitaryDB;Integrated Security=True;TrustServerCertificate=True;";
        public DbSet<Order> OrderTable { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
