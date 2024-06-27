using CSharpFunctionalExtensions;
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

            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
