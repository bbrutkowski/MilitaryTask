using Microsoft.EntityFrameworkCore;
using MilitaryTask.Model;

namespace MilitaryTask.DataContext
{
    public class DataContext : DbContext
    {
        public DbSet<Order> OrderTable { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(x =>
            {
                x.ToTable("OrderTable");

                x.HasKey(y => y.Id);

                x.Property(y => y.OrderId)
                 .IsRequired()
                 .HasMaxLength(45);

                x.Property(y => y.ErpOrderId).IsRequired(false);
                x.Property(y => y.InvoiceId).IsRequired(false);
                x.Property(y => y.StoreId).IsRequired(false);                       
            }); 
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
