using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using DataBaseFirst.Models;

#nullable disable

namespace DataBaseFirst.Data
{
    public partial class DatabaseFirstDBContext : DbContext
    {
        public DatabaseFirstDBContext()
        {
        }

        public DatabaseFirstDBContext(DbContextOptions<DatabaseFirstDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<ItemPedido> ItemPedidos { get; set; }
        public virtual DbSet<Pedido> Pedidos { get; set; }
        public virtual DbSet<Produto> Produtos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source = .\\SQLEXPRESS; Initial Catalog = DatabaseFirstDB; User Id=sa; Password=Admin1234;")
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("Cliente");

                entity.Property(e => e.Cpf).HasColumnName("CPF");

                entity.Property(e => e.Nome).IsRequired();
            });

            modelBuilder.Entity<ItemPedido>(entity =>
            {
                entity.ToTable("ItemPedido");

                entity.HasOne(d => d.IdPedidoNavigation)
                    .WithMany(p => p.ItemPedidos)
                    .HasForeignKey(d => d.IdPedido);

                entity.HasOne(d => d.IdProdutoNavigation)
                    .WithMany(p => p.ItemPedidos)
                    .HasForeignKey(d => d.IdProduto);
            });

            modelBuilder.Entity<Pedido>(entity =>
            {
                entity.ToTable("Pedido");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.Pedidos)
                    .HasForeignKey(d => d.IdCliente);
            });

            modelBuilder.Entity<Produto>(entity =>
            {
                entity.ToTable("Produto");

                entity.Property(e => e.Titulo).IsRequired();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
