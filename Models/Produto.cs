using System.ComponentModel.DataAnnotations;
using VitrineExpress.Enums;

namespace VitrineExpress.Models
{
    public class Produto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do produto é obrigatório.")]
        [StringLength(100)]
        public required string Nome { get; set; }

        [Required(ErrorMessage = "A imagem do produto é obrigatório.")]
        [StringLength(255)]
        public required string ImagemUrl { get; set; }

        [Required(ErrorMessage = "A descrição do produto é obrigatório.")]
        [StringLength(500)]
        public required string Descricao { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal Preco { get; set; }

        [Range(0, int.MaxValue)]
        public int QuantidadeEstoque { get; set; }

        public bool ControlaEstoque { get; set; }
        public bool Disponivel { get; set; }
        public bool EmDestaque { get; set; }

        [Required]
        public int LojaId { get; set; }

        public Loja? Loja { get; set; }

        [Required]
        public Categoria Categoria { get; set; }

        [Required]
        public Subcategoria Subcategoria { get; set; }

        public ICollection<ItemCarrinho> ItensCarrinho { get; set; } = new List<ItemCarrinho>();
        public ICollection<ItemPedido> ItensPedido { get; set; } = new List<ItemPedido>();
    }
}
