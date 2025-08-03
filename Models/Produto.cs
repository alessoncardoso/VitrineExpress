using VitrineExpress.Enums;

namespace VitrineExpress.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string ImagemUrl { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public int QuantidadeEstoque { get; set; }
        public bool ControlaEstoque { get; set; }
        public bool Disponivel { get; set; }
        public bool EmDestaque { get; set; }
        public int LojaId { get; set; }
        public Loja Loja { get; set; }

        public Categoria Categoria { get; set; }
        public Subcategoria Subcategoria { get; set; }

        public ICollection<ItemCarrinho> ItensCarrinho { get; set; } = new List<ItemCarrinho>();
        public ICollection<ItemPedido> ItensPedido { get; set; } = new List<ItemPedido>();
    }
}
