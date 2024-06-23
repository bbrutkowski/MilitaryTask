using Microsoft.EntityFrameworkCore;
using MilitaryASPWeb.BussinessLogic.Model;

namespace MilitaryASPWeb.BussinessLogic.DataContext
{
    public class DataContext : DbContext
    {
        public DbSet<FavoriteProduct> FavoriteProducts { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
    }
}
