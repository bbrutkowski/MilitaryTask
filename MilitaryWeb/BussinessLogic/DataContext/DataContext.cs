using Microsoft.EntityFrameworkCore;
using MilitaryWeb.BussinessLogic.Model;

namespace MilitaryWeb.BussinessLogic.DataContext
{
    public class DataContext : DbContext
    {
        public DbSet<FavoriteProduct> FavoriteProducts { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
    }
}
