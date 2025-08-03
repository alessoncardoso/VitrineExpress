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

        public DbSet<VitrineExpress.Models.Usuario> Usuario { get; set; }
        public DbSet<VitrineExpress.Models.Produto> Produto { get; set; }
        public DbSet<VitrineExpress.Models.Pedido> Pedido { get; set; }
        public DbSet<VitrineExpress.Models.Lojista> Lojista { get; set; }
        public DbSet<VitrineExpress.Models.Loja> Loja { get; set; }
        public DbSet<VitrineExpress.Models.ItemPedido> ItemPedido { get; set; }
        public DbSet<VitrineExpress.Models.ItemCarrinho> ItemCarrinho { get; set; }
        public DbSet<VitrineExpress.Models.Endereco> Endereco { get; set; }
        public DbSet<VitrineExpress.Models.Cliente> Cliente { get; set; }
        public DbSet<VitrineExpress.Models.Carrinho> Carrinho { get; set; }
    }
}
