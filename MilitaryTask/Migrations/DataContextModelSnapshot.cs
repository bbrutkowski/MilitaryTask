﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using DataContextAlias = MilitaryTask.DataContext.DataContext;

#nullable disable

namespace MilitaryTask.Migrations
{
    [DbContext(typeof(DataContextAlias))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MilitaryTask.Model.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("ErpOrderId")
                        .HasColumnType("int");

                    b.Property<int?>("InvoiceId")
                        .HasColumnType("int");

                    b.Property<string>("OrderId")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("nvarchar(45)");

                    b.Property<int?>("StoreId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("OrderTable", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
