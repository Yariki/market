﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProductCatalog.Infrastructure.Persistence;

#nullable disable

namespace ProductCatalog.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(ProductCatalogDbContext))]
    partial class ProductCatalogDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ProductCatalog.Domain.Catalogs.Catalog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("ProductCatalog.Domain.Product.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CatalogId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("MaxStockThreshold")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("PictureFilename")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PictureUri")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UnitId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("_availableStock")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("AvailableStock");

                    b.Property<decimal>("_pricePerUnit")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("PricePerUnit");

                    b.HasKey("Id");

                    b.HasIndex("CatalogId");

                    b.HasIndex("UnitId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("ProductCatalog.Domain.Product.SellUnit", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UnitId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("_scalar")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("Scalar");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("UnitId");

                    b.ToTable("SellUnits");
                });

            modelBuilder.Entity("ProductCatalog.Domain.Product.Unit", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Abbriviation")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Units");
                });

            modelBuilder.Entity("ProductCatalog.Domain.Product.Product", b =>
                {
                    b.HasOne("ProductCatalog.Domain.Catalogs.Catalog", "Catalog")
                        .WithMany()
                        .HasForeignKey("CatalogId");

                    b.HasOne("ProductCatalog.Domain.Product.Unit", "Unit")
                        .WithMany()
                        .HasForeignKey("UnitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Catalog");

                    b.Navigation("Unit");
                });

            modelBuilder.Entity("ProductCatalog.Domain.Product.SellUnit", b =>
                {
                    b.HasOne("ProductCatalog.Domain.Product.Product", null)
                        .WithMany("SellUnits")
                        .HasForeignKey("ProductId");

                    b.HasOne("ProductCatalog.Domain.Product.Unit", "Unit")
                        .WithMany()
                        .HasForeignKey("UnitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Unit");
                });

            modelBuilder.Entity("ProductCatalog.Domain.Product.Product", b =>
                {
                    b.Navigation("SellUnits");
                });
#pragma warning restore 612, 618
        }
    }
}
