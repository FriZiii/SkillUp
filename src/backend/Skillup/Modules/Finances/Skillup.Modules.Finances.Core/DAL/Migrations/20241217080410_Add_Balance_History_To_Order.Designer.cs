﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Skillup.Modules.Finances.Core.DAL;

#nullable disable

namespace Skillup.Modules.Finances.Core.DAL.Migrations
{
    [DbContext(typeof(FinancesDbContext))]
    [Migration("20241217080410_Add_Balance_History_To_Order")]
    partial class Add_Balance_History_To_Order
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("finances")
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Skillup.Modules.Finances.Core.Entities.BalanceHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("WalletId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("WalletId");

                    b.ToTable("BalanceHistories", "finances");
                });

            modelBuilder.Entity("Skillup.Modules.Finances.Core.Entities.Cart", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("DiscountCodeId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("DiscountCodeId");

                    b.ToTable("Carts", "finances");
                });

            modelBuilder.Entity("Skillup.Modules.Finances.Core.Entities.CartItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CartId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ItemId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("CartId");

                    b.HasIndex("ItemId");

                    b.ToTable("CartItems", "finances");
                });

            modelBuilder.Entity("Skillup.Modules.Finances.Core.Entities.DiscountCode", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("AppliesToEntireCart")
                        .HasColumnType("boolean");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("DiscountValue")
                        .HasPrecision(18, 2)
                        .HasColumnType("numeric(18,2)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsPublic")
                        .HasColumnType("boolean");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uuid");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.HasIndex("IsActive");

                    b.HasIndex("IsPublic");

                    b.HasIndex("OwnerId");

                    b.ToTable("DiscountCodes", "finances");

                    b.HasDiscriminator<int>("Type");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Skillup.Modules.Finances.Core.Entities.DiscountedItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("DiscountCodeId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ItemId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("DiscountCodeId");

                    b.HasIndex("ItemId");

                    b.ToTable("DiscountedItems", "finances");
                });

            modelBuilder.Entity("Skillup.Modules.Finances.Core.Entities.Item", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Items", "finances");
                });

            modelBuilder.Entity("Skillup.Modules.Finances.Core.Entities.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("BalanceHistoryId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("OrdererId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("BalanceHistoryId");

                    b.HasIndex("OrdererId");

                    b.ToTable("Orders", "finances");
                });

            modelBuilder.Entity("Skillup.Modules.Finances.Core.Entities.OrderItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ItemId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("ItemPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItems", "finances");
                });

            modelBuilder.Entity("Skillup.Modules.Finances.Core.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("Users", "finances");
                });

            modelBuilder.Entity("Skillup.Modules.Finances.Core.Entities.Wallet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId")
                        .IsUnique();

                    b.ToTable("Wallets", "finances");
                });

            modelBuilder.Entity("Skillup.Modules.Finances.Core.ValueObjects.FixedAmountDiscountCode", b =>
                {
                    b.HasBaseType("Skillup.Modules.Finances.Core.Entities.DiscountCode");

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("Skillup.Modules.Finances.Core.ValueObjects.PercentageDiscountCode", b =>
                {
                    b.HasBaseType("Skillup.Modules.Finances.Core.Entities.DiscountCode");

                    b.HasDiscriminator().HasValue(0);
                });

            modelBuilder.Entity("Skillup.Modules.Finances.Core.Entities.BalanceHistory", b =>
                {
                    b.HasOne("Skillup.Modules.Finances.Core.Entities.Wallet", "Wallet")
                        .WithMany()
                        .HasForeignKey("WalletId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Wallet");
                });

            modelBuilder.Entity("Skillup.Modules.Finances.Core.Entities.Cart", b =>
                {
                    b.HasOne("Skillup.Modules.Finances.Core.Entities.DiscountCode", "DiscountCode")
                        .WithMany()
                        .HasForeignKey("DiscountCodeId");

                    b.Navigation("DiscountCode");
                });

            modelBuilder.Entity("Skillup.Modules.Finances.Core.Entities.CartItem", b =>
                {
                    b.HasOne("Skillup.Modules.Finances.Core.Entities.Cart", "Cart")
                        .WithMany("Items")
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Skillup.Modules.Finances.Core.Entities.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cart");

                    b.Navigation("Item");
                });

            modelBuilder.Entity("Skillup.Modules.Finances.Core.Entities.DiscountCode", b =>
                {
                    b.HasOne("Skillup.Modules.Finances.Core.Entities.User", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Skillup.Modules.Finances.Core.Entities.DiscountedItem", b =>
                {
                    b.HasOne("Skillup.Modules.Finances.Core.Entities.DiscountCode", "DiscountCode")
                        .WithMany("DiscountedItems")
                        .HasForeignKey("DiscountCodeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Skillup.Modules.Finances.Core.Entities.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DiscountCode");

                    b.Navigation("Item");
                });

            modelBuilder.Entity("Skillup.Modules.Finances.Core.Entities.Order", b =>
                {
                    b.HasOne("Skillup.Modules.Finances.Core.Entities.BalanceHistory", "BalanceHistory")
                        .WithMany()
                        .HasForeignKey("BalanceHistoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Skillup.Modules.Finances.Core.Entities.User", "Orderer")
                        .WithMany()
                        .HasForeignKey("OrdererId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BalanceHistory");

                    b.Navigation("Orderer");
                });

            modelBuilder.Entity("Skillup.Modules.Finances.Core.Entities.OrderItem", b =>
                {
                    b.HasOne("Skillup.Modules.Finances.Core.Entities.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Skillup.Modules.Finances.Core.Entities.Order", "Order")
                        .WithMany("Items")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Skillup.Modules.Finances.Core.Entities.Wallet", b =>
                {
                    b.HasOne("Skillup.Modules.Finances.Core.Entities.User", "Owner")
                        .WithOne()
                        .HasForeignKey("Skillup.Modules.Finances.Core.Entities.Wallet", "OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Skillup.Modules.Finances.Core.Entities.Cart", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("Skillup.Modules.Finances.Core.Entities.DiscountCode", b =>
                {
                    b.Navigation("DiscountedItems");
                });

            modelBuilder.Entity("Skillup.Modules.Finances.Core.Entities.Order", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
