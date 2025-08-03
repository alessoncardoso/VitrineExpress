using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VitrineExpress.Models;

namespace VitrineExpress.Data
{
    public class VitrineContext : DbContext
    {
        public VitrineContext (DbContextOptions<VitrineContext> options)
            : base(options)
        {
        }

        public DbSet<VitrineExpress.Models.Usuario> Usuarios { get; set; }
        public DbSet<VitrineExpress.Models.Produto> Produtos { get; set; }
        public DbSet<VitrineExpress.Models.Pedido> Pedidos { get; set; }
        public DbSet<VitrineExpress.Models.Lojista> Lojistas { get; set; }
        public DbSet<VitrineExpress.Models.Loja> Lojas { get; set; }
        public DbSet<VitrineExpress.Models.ItemPedido> ItensPedido { get; set; }
        public DbSet<VitrineExpress.Models.ItemCarrinho> ItensCarrinho { get; set; }
        public DbSet<VitrineExpress.Models.Endereco> Enderecos { get; set; }
        public DbSet<VitrineExpress.Models.Cliente> Clientes { get; set; }
        public DbSet<VitrineExpress.Models.Carrinho> Carrinhos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurações de mapeamento para as entidades/tabelas
            modelBuilder.Entity<Usuario>().ToTable("Usuarios");
            modelBuilder.Entity<Produto>().ToTable("Produtos");
            modelBuilder.Entity<Pedido>().ToTable("Pedidos");
            modelBuilder.Entity<Lojista>().ToTable("Lojistas");
            modelBuilder.Entity<Loja>().ToTable("Lojas");
            modelBuilder.Entity<ItemPedido>().ToTable("ItensPedido");
            modelBuilder.Entity<ItemCarrinho>().ToTable("ItensCarrinho");
            modelBuilder.Entity<Endereco>().ToTable("Enderecos");
            modelBuilder.Entity<Cliente>().ToTable("Clientes");
            modelBuilder.Entity<Carrinho>().ToTable("Carrinhos");

            // Configurações adicionais, como chaves primárias, relacionamentos, etc.

            // Usuario → Cliente (um-para-um)
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Cliente)
                .WithOne(c => c.Usuario)
                .HasForeignKey<Cliente>(c => c.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            // Usuario → Lojista (um-para-um)
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Lojista)
                .WithOne(l => l.Usuario)
                .HasForeignKey<Lojista>(l => l.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            // Cliente → Endereços (um-para-muitos)
            modelBuilder.Entity<Cliente>()
                .HasMany(c => c.Enderecos)
                .WithOne(e => e.Cliente)
                .HasForeignKey(e => e.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);

            // Cliente → Carrinho (um-para-muitos)
            modelBuilder.Entity<Cliente>()
                .HasMany(c => c.Carrinhos)
                .WithOne(ca => ca.Cliente)
                .HasForeignKey(ca => ca.ClienteId)
                .OnDelete(DeleteBehavior.Cascade);

            // Cliente → Pedido (um-para-muitos)
            modelBuilder.Entity<Cliente>()
                .HasMany(c => c.Pedidos)
                .WithOne(p => p.Cliente)
                .HasForeignKey(p => p.ClienteId)
                .OnDelete(DeleteBehavior.Cascade);

            // Lojista → Loja (um-para-muitos)
            modelBuilder.Entity<Lojista>()
                .HasMany(l => l.Lojas)
                .WithOne(lo => lo.Lojista)
                .HasForeignKey(lo => lo.LojistaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Loja → Endereços (um-para-muitos)
            modelBuilder.Entity<Loja>()
                .HasMany(l => l.Enderecos)
                .WithOne(e => e.Loja)
                .HasForeignKey(e => e.LojaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Loja → Produto (um-para-muitos)
            modelBuilder.Entity<Loja>()
                .HasMany(l => l.Produtos)
                .WithOne(p => p.Loja)
                .HasForeignKey(p => p.LojaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Loja → Pedido (um-para-muitos)
            modelBuilder.Entity<Loja>()
                .HasMany(l => l.Pedidos)
                .WithOne(p => p.Loja)
                .HasForeignKey(p => p.LojaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Carrinho → ItensCarrinho (um-para-muitos)
            modelBuilder.Entity<Carrinho>()
                .HasMany(c => c.ItensCarrinho)
                .WithOne(ic => ic.Carrinho)
                .HasForeignKey(ic => ic.CarrinhoId)
                .OnDelete(DeleteBehavior.Cascade);

            // Produto → ItensCarrinho (um-para-muitos)
            modelBuilder.Entity<Produto>()
                .HasMany(p => p.ItensCarrinho)
                .WithOne(ic => ic.Produto)
                .HasForeignKey(ic => ic.ProdutoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Pedido → ItensPedido (um-para-muitos)
            modelBuilder.Entity<Pedido>()
                .HasMany(p => p.ItensPedido)
                .WithOne(ip => ip.Pedido)
                .HasForeignKey(ip => ip.PedidoId)
                .OnDelete(DeleteBehavior.Cascade);

            // Produto → ItensPedido (um-para-muitos)
            modelBuilder.Entity<Produto>()
                .HasMany(p => p.ItensPedido)
                .WithOne(ip => ip.Produto)
                .HasForeignKey(ip => ip.ProdutoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
