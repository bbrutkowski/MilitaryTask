using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DbConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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

            modelBuilder.Entity<AccountBalance>(x =>
            {
                x.HasKey(k => k.Id);
            });

            modelBuilder.Entity<TaxRate>(x =>
            {
                x.HasKey(k => k.Id);
            });

            modelBuilder.Entity<Amount>(x =>
            {
                x.HasKey(k => k.Id);
            });

            modelBuilder.Entity<Bill>(x =>
            {
                x.HasOne(t => t.Tender)
                 .WithMany(b => b.Bills)
                 .HasForeignKey(ti => ti.TenderId)
                 .OnDelete(DeleteBehavior.Restrict);

                x.HasOne(b => b.BillType)
                 .WithOne()
                 .HasForeignKey<Bill>(bt => bt.BillTypeId)
                 .OnDelete(DeleteBehavior.Cascade);

                x.HasOne(a => a.Amount)
                 .WithOne()
                 .HasForeignKey<Bill>(ai => ai.AmountId)
                 .OnDelete(DeleteBehavior.Cascade);

                x.HasOne(tr => tr.TaxRate)
                 .WithOne()
                 .HasForeignKey<Bill>(tr => tr.TaxRateId)
                 .OnDelete(DeleteBehavior.Cascade);

                x.HasOne(ab => ab.AccountBalance)
                 .WithOne()
                 .HasForeignKey<Bill>(ab => ab.AccountBalanceId)
                 .OnDelete(DeleteBehavior.Cascade);
            });

            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
