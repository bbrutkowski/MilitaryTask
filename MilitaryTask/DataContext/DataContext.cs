using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MilitaryTask.Model;

namespace MilitaryTask.DataContext
{
    public class DataContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DataContext(IConfiguration configuration) => _configuration = configuration;

        public DbSet<Order> Orders { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<BillType> BillTypes { get; set; }
        public DbSet<Tender> Tenders { get; set; }
        public DbSet<Amount> Amounts { get; set; }
        public DbSet<TaxRate> TaxRates { get; set; }
        public DbSet<AccountBalance> AccountBalances { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DbConnection"))
                          .EnableSensitiveDataLogging()
                          .LogTo(Console.WriteLine, LogLevel.Information);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>(x =>
            {
                x.ToTable("OrderTable", "dbo");

                x.HasKey(e => e.Id)
                 .HasName("PK_panel_lista");

                x.Property(e => e.Id)
                 .ValueGeneratedOnAdd();

                x.Property(e => e.OrderId)
                 .IsRequired()
                 .HasMaxLength(45);

                x.Property(e => e.ErpOrderId)
                 .IsRequired(false);

                x.Property(e => e.InvoiceId)
                 .IsRequired(false);

                x.Property(e => e.StoreId)
                 .IsRequired(false);

                x.HasIndex(e => new { e.OrderId, e.StoreId })
                 .IsUnique()
                 .HasDatabaseName("si");
            });     

            modelBuilder.Entity<Bill>(x =>
            {
                x.HasOne(b => b.Tender)
                 .WithMany(t => t.Bills)
                 .HasForeignKey(b => b.TenderId)
                 .OnDelete(DeleteBehavior.Cascade);

                x.HasOne(b => b.BillType)
                 .WithOne(bt => bt.Bill)
                 .HasForeignKey<Bill>(b => b.BillTypeId)
                 .OnDelete(DeleteBehavior.Cascade);

                x.HasOne(b => b.Amount)
                 .WithOne(a => a.Bill)
                 .HasForeignKey<Bill>(b => b.AmountId)
                 .OnDelete(DeleteBehavior.Cascade);

                x.HasOne(b => b.TaxRate)
                 .WithOne(tr => tr.Bill)
                 .HasForeignKey<Bill>(b => b.TaxRateId)
                 .OnDelete(DeleteBehavior.Cascade);

                x.HasOne(b => b.AccountBalance)
                 .WithOne(ab => ab.Bill)
                 .HasForeignKey<Bill>(b => b.AccountBalanceId)
                 .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
