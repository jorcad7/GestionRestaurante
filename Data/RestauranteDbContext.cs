using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Restaurante.Models;

namespace Restaurante.Data;

public partial class RestauranteDbContext : DbContext
{
    public RestauranteDbContext()
    {
    }

    public RestauranteDbContext(DbContextOptions<RestauranteDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Detalleordene> Detalleordenes { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<Ordene> Ordenes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("server=localhost;port=3306;database=sistema_restaurante;user=root;password=root;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.ClienteId).HasName("PRIMARY");
        });

        modelBuilder.Entity<Detalleordene>(entity =>
        {
            entity.HasKey(e => e.DetalleId).HasName("PRIMARY");

            entity.HasOne(d => d.Menu).WithMany(p => p.Detalleordenes)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_detalle_menus");

            entity.HasOne(d => d.Orden).WithMany(p => p.Detalleordenes).HasConstraintName("fk_detalle_ordenes");
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.MenuId).HasName("PRIMARY");

            entity.Property(e => e.Disponible).HasDefaultValueSql("'1'");
        });

        modelBuilder.Entity<Ordene>(entity =>
        {
            entity.HasKey(e => e.OrdenId).HasName("PRIMARY");

            entity.Property(e => e.Estado).HasDefaultValueSql("'Pendiente'");
            entity.Property(e => e.FechaOrden).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Ordenes)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_ordenes_clientes");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
