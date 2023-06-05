using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Prueba_v1.Models.dbModels
{
    public partial class Pia_ProgWebContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        public Pia_ProgWebContext()
        {
        }

        public Pia_ProgWebContext(DbContextOptions<Pia_ProgWebContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Carrito> Carritos { get; set; } = null!;
        public virtual DbSet<Categorium> Categoria { get; set; } = null!;
        public virtual DbSet<Comidum> Comida { get; set; } = null!;
        public virtual DbSet<DetalleDePedido> DetalleDePedidos { get; set; } = null!;
        public virtual DbSet<Estado> Estados { get; set; } = null!;
        public virtual DbSet<Pedido> Pedidos { get; set; } = null!;
        public virtual DbSet<Reseña> Reseñas { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { if (!optionsBuilder.IsConfigured) { optionsBuilder.UseSqlServer("Data Source=DESKTOP-B0MR3KQ;Initial Catalog=SistemaVentas;Integrated security = true"); } }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Carrito>(entity =>
            {
                entity.HasKey(e => e.IdCarrito);

                entity.HasKey(e => new { e.IdUsusario, e.IdComida });

                entity.Property(e => e.IdUsusario).ValueGeneratedOnAdd();

                entity.HasOne(d => d.IdComidaNavigation)
                    .WithMany(p => p.Carritos)
                    .HasForeignKey(d => d.IdComida)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Carrito_Comida");

                entity.HasOne(d => d.IdUsusarioNavigation)
                    .WithMany(p => p.Carritos)
                    .HasForeignKey(d => d.IdUsusario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Carrito_Pedido");

                entity.HasOne(d => d.IdUsusario1)
                    .WithMany(p => p.Carritos)
                    .HasForeignKey(d => d.IdUsusario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Carrito_Usuarios");
            });

            modelBuilder.Entity<Comidum>(entity =>
            {
                entity.HasOne(d => d.IdCategoriaNavigation)
                    .WithMany(p => p.Comida)
                    .HasForeignKey(d => d.IdCategoria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comida_Categoria");
            });

            modelBuilder.Entity<DetalleDePedido>(entity =>
            {
                entity.HasKey(e => new { e.IdPedido, e.IdComida });

                entity.Property(e => e.IdPedido).ValueGeneratedOnAdd();

                entity.HasOne(d => d.IdComidaNavigation)
                    .WithMany(p => p.DetalleDePedidos)
                    .HasForeignKey(d => d.IdComida)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Detalle_de_Pedido_Comida");

                entity.HasOne(d => d.IdPedidoNavigation)
                    .WithMany(p => p.DetalleDePedidos)
                    .HasForeignKey(d => d.IdPedido)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Detalle_de_Pedido_Pedido");
            });

            modelBuilder.Entity<Pedido>(entity =>
            {
                entity.Property(e => e.IdPedido).ValueGeneratedOnAdd();

                entity.Property(e => e.LugarRecoger).IsFixedLength();

                entity.HasOne(d => d.IdEstadoNavigation)
                    .WithMany(p => p.Pedidos)
                    .HasForeignKey(d => d.IdEstado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pedido_Estado");

                entity.HasOne(d => d.IdPedidoNavigation)
                    .WithOne(p => p.Pedido)
                    .HasForeignKey<Pedido>(d => d.IdPedido)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pedido_Usuarios");
            });

            modelBuilder.Entity<Reseña>(entity =>
            {
                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Reseñas)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reseñas_Comida");

                entity.HasOne(d => d.IdUsuario1)
                    .WithMany(p => p.Reseñas)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reseñas_Usuarios");
            });

 

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
