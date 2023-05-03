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
        public virtual DbSet<DailyOrder> DailyOrders { get; set; } = null!;
        public virtual DbSet<DailyOrderDish> DailyOrderDishes { get; set; } = null!;
        public virtual DbSet<Dish> Dishes { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderDish> OrderDishes { get; set; } = null!;
        public virtual DbSet<Position> Positions { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductDish> ProductDishes { get; set; } = null!;
        public virtual DbSet<Table> Tables { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySQL("Server = localhost; Database = CRMdb; Uid = root; Pwd = 2220rorooad;");
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

            modelBuilder.Entity<DailyOrder>(entity =>
            {
                entity.ToTable("DailyOrder", "CRMdb");

                entity.HasIndex(e => e.ClientId, "fk_DailyOrder_Client1_idx");

                entity.HasIndex(e => e.EmployeeId, "fk_DailyOrder_Employee1_idx");

                entity.HasIndex(e => e.TableId, "fk_DailyOrder_Table1_idx");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ClientId).HasColumnName("Client_ID");

                entity.Property(e => e.EmployeeId).HasColumnName("Employee_ID");

                entity.Property(e => e.Price).HasPrecision(9);

                entity.Property(e => e.TableId).HasColumnName("Table_ID");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.DailyOrders)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("fk_DailyOrder_Client1");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.DailyOrders)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("fk_DailyOrder_Employee1");

                entity.HasOne(d => d.Table)
                    .WithMany(p => p.DailyOrders)
                    .HasForeignKey(d => d.TableId)
                    .HasConstraintName("fk_DailyOrder_Table1");
            });

            modelBuilder.Entity<DailyOrderDish>(entity =>
            {
                entity.ToTable("DailyOrder_Dish", "CRMdb");

                entity.HasIndex(e => e.DailyOrderId, "fk_DailyOrder_Dish_DailyOrder1_idx");

                entity.HasIndex(e => e.DishId, "fk_DailyOrder_Dish_Dish2_idx");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DailyOrderId).HasColumnName("DailyOrder_ID");

                entity.Property(e => e.DishId).HasColumnName("Dish_ID");

                entity.HasOne(d => d.DailyOrder)
                    .WithMany(p => p.DailyOrderDishes)
                    .HasForeignKey(d => d.DailyOrderId)
                    .HasConstraintName("fk_DailyOrder_Dish_DailyOrder1");

                entity.HasOne(d => d.Dish)
                    .WithMany(p => p.DailyOrderDishes)
                    .HasForeignKey(d => d.DishId)
                    .HasConstraintName("fk_DailyOrder_Dish_Dish2");
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

                entity.HasIndex(e => e.PositionId, "fk_Employee_Position1_idx");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Login).HasMaxLength(45);

                entity.Property(e => e.Name).HasMaxLength(45);

                entity.Property(e => e.Password).HasMaxLength(45);

                entity.Property(e => e.PositionId).HasColumnName("Position_ID");

                entity.HasOne(d => d.Position)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.PositionId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_Employee_Position1");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order", "CRMdb");

                entity.HasIndex(e => e.ClientId, "fk_Order_Client1_idx");

                entity.HasIndex(e => e.EmployeeId, "fk_Order_Employee1_idx");

                entity.HasIndex(e => e.TableId, "fk_Order_Table1_idx");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ClientId).HasColumnName("Client_ID");

                entity.Property(e => e.EmployeeId).HasColumnName("Employee_ID");

                entity.Property(e => e.Price).HasPrecision(9);

                entity.Property(e => e.TableId).HasColumnName("Table_ID");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("fk_Order_Client1");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("fk_Order_Employee1");

                entity.HasOne(d => d.Table)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.TableId)
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

            modelBuilder.Entity<Position>(entity =>
            {
                entity.ToTable("Position", "CRMdb");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(45);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product", "CRMdb");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Category).HasMaxLength(45);

                entity.Property(e => e.Amount);
                
                entity.Property(e => e.LifeTime);
                
                entity.Property(e => e.DeliveryDateTime);
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
                    .HasConstraintName("fk_Product_Dish_Dish1");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductDishes)
                    .HasForeignKey(d => d.ProductId)
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
