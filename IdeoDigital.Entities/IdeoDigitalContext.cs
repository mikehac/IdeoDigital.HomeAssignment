using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace IdeoDigital.Entities;

public partial class IdeoDigitalContext : DbContext
{
    public IdeoDigitalContext()
    {
    }

    public IdeoDigitalContext(DbContextOptions<IdeoDigitalContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=nas,1433;Database=IdeoDigital;Trusted_Connection=True;Persist Security Info=True;User ID=sa;Password=jacui4Nhftk;Encrypt=False;Integrated Security=False;Trusted_Connection=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("Customer");

            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.ShippingAddress).HasMaxLength(100);
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.ToTable("Invoice");

            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.DueDate).HasColumnType("datetime");
            entity.Property(e => e.SubTotal).HasColumnType("money");
            entity.Property(e => e.Tax).HasColumnType("money");

            entity.HasOne(d => d.Customer).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Invoice_Customer");

            entity.HasOne(d => d.Status).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Invoice_Status");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Invoice_Supplier");
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.ToTable("Item");

            entity.Property(e => e.Description).HasMaxLength(100);
            entity.Property(e => e.Rate).HasColumnType("money");

            entity.HasOne(d => d.Invoice).WithMany(p => p.Items)
                .HasForeignKey(d => d.InvoiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Item_Invoice");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.ToTable("Status");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.ToTable("Supplier");

            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
