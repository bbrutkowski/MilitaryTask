using Microsoft.EntityFrameworkCore;
using MilitaryTask.Model;

namespace MilitaryTask.DataContext
{
    public class DataContext : DbContext
    {
        private const string _connectionString = "Data Source=.;Initial Catalog=MilitaryDB;Integrated Security=True;TrustServerCertificate=True;";

        public DbSet<Order> OrderTable { get; set; }
        public DbSet<BillingEntry> Billings { get; set; }
        public DbSet<Model.Type> BillingTypes { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Tax> Taxes { get; set; }
        public DbSet<Amount> Values { get; set; }
        public DbSet<Balance> Balances { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
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
