﻿// <auto-generated />
using System;
using Bl;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Bl.Migrations
{
    [DbContext(typeof(DepiProjectContext))]
    partial class DepiProjectContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domains.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("ImagePath")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<bool?>("OnDelete")
                        .HasColumnType("bit");

                    b.Property<int?>("ParentCategoryId")
                        .HasColumnType("int");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime");

                    b.HasKey("CategoryId")
                        .HasName("PK__Categori__19093A0B95CEDCD5");

                    b.HasIndex("ParentCategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Domains.Consultation", b =>
                {
                    b.Property<int>("ConsultationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ConsultationId"));

                    b.Property<decimal>("ConsultationCost")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime");

                    b.Property<int>("EngineerId")
                        .HasColumnType("int");

                    b.Property<string>("IssueDescription")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ConsultationId")
                        .HasName("PK__Consulta__5D014A98E71095FA");

                    b.HasIndex(new[] { "CustomerId" }, "IX_Consultations_CustomerId");

                    b.HasIndex(new[] { "EngineerId" }, "IX_Consultations_EngineerId");

                    b.ToTable("Consultations");
                });

            modelBuilder.Entity("Domains.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerId"));

                    b.Property<string>("Address")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("ConfirmPassword")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("FreeConsultationCount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("CustomerId")
                        .HasName("PK__Customer__A4AE64D8279EDDF0");

                    b.HasIndex(new[] { "Email" }, "IX_Customers_Email");

                    b.HasIndex(new[] { "Email" }, "UQ__Customer__A9D10534AD9FBD18")
                        .IsUnique();

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Domains.Delivery", b =>
                {
                    b.Property<int>("DeliveryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DeliveryId"));

                    b.Property<string>("DeliveryAddress")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("DeliveryName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Status")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("DeliveryId")
                        .HasName("PK__Deliveri__626D8FCEE3CDBDD4");

                    b.HasIndex("OrderId");

                    b.ToTable("Deliveries");
                });

            modelBuilder.Entity("Domains.Engineer", b =>
                {
                    b.Property<int>("EngineerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EngineerId"));

                    b.Property<string>("Address")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("ConfirmPassword")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Phone")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("EngineerId")
                        .HasName("PK__Engineer__1FA0F1CE1336A49F");

                    b.HasIndex(new[] { "Email" }, "IX_Engineers_Email");

                    b.HasIndex(new[] { "Email" }, "UQ__Engineer__A9D105345530756A")
                        .IsUnique();

                    b.ToTable("Engineers");
                });

            modelBuilder.Entity("Domains.Item", b =>
                {
                    b.Property<int>("ItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ItemId"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int?>("ItemDetailsId")
                        .HasColumnType("int");

                    b.Property<string>("ItemName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ItemType")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool?>("OnDelete")
                        .HasColumnType("bit");

                    b.Property<decimal>("PurchasePrice")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<decimal>("SalesPrice")
                        .HasColumnType("decimal(18, 2)");

                    b.HasKey("ItemId")
                        .HasName("PK__Items__727E838B91CA8EA6");

                    b.HasIndex("ItemDetailsId");

                    b.HasIndex(new[] { "CategoryId" }, "IX_Items_CategoryId");

                    b.HasIndex(new[] { "ItemName" }, "IX_Items_ItemName");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("Domains.ItemDetail", b =>
                {
                    b.Property<int>("ItemDetailsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ItemDetailsId"));

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Gpu")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("HardDisk")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("OsName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Processor")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("RamSize")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ScreenResolution")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ScreenSize")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Weight")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ItemDetailsId")
                        .HasName("PK__ItemDeta__95C3001A4509211B");

                    b.ToTable("ItemDetails");
                });

            modelBuilder.Entity("Domains.ItemDiscount", b =>
                {
                    b.Property<int>("ItemDiscountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ItemDiscountId"));

                    b.Property<decimal?>("DiscountPercent")
                        .HasColumnType("decimal(5, 2)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime");

                    b.HasKey("ItemDiscountId")
                        .HasName("PK__ItemDisc__495136BAC5FE1C5A");

                    b.HasIndex(new[] { "ItemId" }, "IX_ItemDiscounts_ItemId");

                    b.ToTable("ItemDiscounts");
                });

            modelBuilder.Entity("Domains.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"));

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<DateTime>("OrderDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("OrderId")
                        .HasName("PK__Orders__C3905BCF70D179EC");

                    b.HasIndex(new[] { "CustomerId" }, "IX_Orders_CustomerId");

                    b.HasIndex(new[] { "ItemId" }, "IX_Orders_ItemId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Domains.Payment", b =>
                {
                    b.Property<int>("PaymentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PaymentId"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<DateTime>("PaymentDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("PaymentMethod")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("PaymentId")
                        .HasName("PK__Payments__9B556A3863E9243E");

                    b.HasIndex("CustomerId");

                    b.HasIndex("OrderId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("ElectronicsFix.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Address")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("ConfirmPassword")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Phone")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Domains.Category", b =>
                {
                    b.HasOne("Domains.Category", "ParentCategory")
                        .WithMany("InverseParentCategory")
                        .HasForeignKey("ParentCategoryId")
                        .HasConstraintName("FK_Categories_ParentCategoryId");

                    b.Navigation("ParentCategory");
                });

            modelBuilder.Entity("Domains.Consultation", b =>
                {
                    b.HasOne("Domains.Customer", "Customer")
                        .WithMany("Consultations")
                        .HasForeignKey("CustomerId")
                        .IsRequired()
                        .HasConstraintName("FK_Consultations_Customer");

                    b.HasOne("Domains.Engineer", "Engineer")
                        .WithMany("Consultations")
                        .HasForeignKey("EngineerId")
                        .IsRequired()
                        .HasConstraintName("FK_Consultations_Engineer");

                    b.Navigation("Customer");

                    b.Navigation("Engineer");
                });

            modelBuilder.Entity("Domains.Delivery", b =>
                {
                    b.HasOne("Domains.Order", "Order")
                        .WithMany("Deliveries")
                        .HasForeignKey("OrderId")
                        .IsRequired()
                        .HasConstraintName("FK_Deliveries_Order");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Domains.Item", b =>
                {
                    b.HasOne("Domains.Category", "Category")
                        .WithMany("Items")
                        .HasForeignKey("CategoryId")
                        .IsRequired()
                        .HasConstraintName("FK_Items_Category");

                    b.HasOne("Domains.ItemDetail", "ItemDetails")
                        .WithMany("Items")
                        .HasForeignKey("ItemDetailsId")
                        .HasConstraintName("FK_Items_ItemDetails");

                    b.Navigation("Category");

                    b.Navigation("ItemDetails");
                });

            modelBuilder.Entity("Domains.ItemDiscount", b =>
                {
                    b.HasOne("Domains.Item", "Item")
                        .WithMany("ItemDiscounts")
                        .HasForeignKey("ItemId")
                        .IsRequired()
                        .HasConstraintName("FK_ItemDiscounts_Item");

                    b.Navigation("Item");
                });

            modelBuilder.Entity("Domains.Order", b =>
                {
                    b.HasOne("Domains.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .IsRequired()
                        .HasConstraintName("FK_Orders_Customer");

                    b.HasOne("Domains.Item", "Item")
                        .WithMany("Orders")
                        .HasForeignKey("ItemId")
                        .IsRequired()
                        .HasConstraintName("FK_Orders_Item");

                    b.Navigation("Customer");

                    b.Navigation("Item");
                });

            modelBuilder.Entity("Domains.Payment", b =>
                {
                    b.HasOne("Domains.Customer", "Customer")
                        .WithMany("Payments")
                        .HasForeignKey("CustomerId")
                        .IsRequired()
                        .HasConstraintName("FK_Payments_Customer");

                    b.HasOne("Domains.Order", "Order")
                        .WithMany("Payments")
                        .HasForeignKey("OrderId")
                        .IsRequired()
                        .HasConstraintName("FK_Payments_Order");

                    b.Navigation("Customer");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Domains.Category", b =>
                {
                    b.Navigation("InverseParentCategory");

                    b.Navigation("Items");
                });

            modelBuilder.Entity("Domains.Customer", b =>
                {
                    b.Navigation("Consultations");

                    b.Navigation("Orders");

                    b.Navigation("Payments");
                });

            modelBuilder.Entity("Domains.Engineer", b =>
                {
                    b.Navigation("Consultations");
                });

            modelBuilder.Entity("Domains.Item", b =>
                {
                    b.Navigation("ItemDiscounts");

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Domains.ItemDetail", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("Domains.Order", b =>
                {
                    b.Navigation("Deliveries");

                    b.Navigation("Payments");
                });
#pragma warning restore 612, 618
        }
    }
}
