using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SIPI_CRM_System.Models
{
    public partial class CRMdbContext : DbContext
    {
        public CRMdbContext()
        {
        }

        public CRMdbContext(DbContextOptions<CRMdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Client> Clients { get; set; } = null!;
        public virtual DbSet<Dish> Dishes { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderDish> OrderDishes { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductDish> ProductDishes { get; set; } = null!;
        public virtual DbSet<Table> Tables { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("Server = localhost; Database = CRMdb; Uid = root; Pwd = password;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("Client", "CRMdb");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Email).HasMaxLength(45);

                entity.Property(e => e.Name).HasMaxLength(45);

                entity.Property(e => e.PhoneNumber).HasMaxLength(10);
            });

            modelBuilder.Entity<Dish>(entity =>
            {
                entity.ToTable("Dish", "CRMdb");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Category).HasMaxLength(45);

                entity.Property(e => e.Mass).HasPrecision(6, 3);

                entity.Property(e => e.Name).HasMaxLength(45);

                entity.Property(e => e.Price).HasPrecision(9);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee", "CRMdb");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Login).HasMaxLength(45);

                entity.Property(e => e.Name).HasMaxLength(45);

                entity.Property(e => e.Password).HasMaxLength(45);

                entity.Property(e => e.Position).HasMaxLength(45);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order", "CRMdb");

                entity.HasIndex(e => e.ClientId, "fk_Order_Client1_idx");

                entity.HasIndex(e => e.TableId, "fk_Order_Table1_idx");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ClientId).HasColumnName("Client_ID");

                entity.Property(e => e.Price).HasPrecision(9);

                entity.Property(e => e.TableId).HasColumnName("Table_ID");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Order_Client1");

                entity.HasOne(d => d.Table)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.TableId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Order_Table1");
            });

            modelBuilder.Entity<OrderDish>(entity =>
            {
                entity.ToTable("Order_Dish", "CRMdb");

                entity.HasIndex(e => e.DishId, "fk_Order_Dish_Dish1_idx");

                entity.HasIndex(e => e.OrderId, "fk_Order_Dish_Order1_idx");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DishId).HasColumnName("Dish_ID");

                entity.Property(e => e.OrderId).HasColumnName("Order_ID");

                entity.HasOne(d => d.Dish)
                    .WithMany(p => p.OrderDishes)
                    .HasForeignKey(d => d.DishId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Order_Dish_Dish1");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDishes)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Order_Dish_Order1");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product", "CRMdb");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Category).HasMaxLength(45);

                entity.Property(e => e.Name).HasMaxLength(45);
            });

            modelBuilder.Entity<ProductDish>(entity =>
            {
                entity.ToTable("Product_Dish", "CRMdb");

                entity.HasIndex(e => e.DishId, "fk_Product_Dish_Dish1_idx");

                entity.HasIndex(e => e.ProductId, "fk_Product_Dish_Product1_idx");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Amount).HasPrecision(6, 3);

                entity.Property(e => e.DishId).HasColumnName("Dish_ID");

                entity.Property(e => e.ProductId).HasColumnName("Product_ID");

                entity.HasOne(d => d.Dish)
                    .WithMany(p => p.ProductDishes)
                    .HasForeignKey(d => d.DishId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Product_Dish_Dish1");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductDishes)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Product_Dish_Product1");
            });

            modelBuilder.Entity<Table>(entity =>
            {
                entity.ToTable("Table", "CRMdb");

                entity.Property(e => e.Id).HasColumnName("ID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
