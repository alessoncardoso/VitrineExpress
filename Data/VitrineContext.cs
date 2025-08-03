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
    }
}
