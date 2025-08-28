using System.ComponentModel.DataAnnotations;
using VitrineExpress.Enums;

namespace VitrineExpress.Models
{
    public class Produto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do produto é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome do produto deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [StringLength(255, ErrorMessage = "A URL da imagem deve ter no máximo 255 caracteres.")]
        public string? ImagemUrl { get; set; }

        [StringLength(500, ErrorMessage = "A descrição deve ter no máximo 500 caracteres.")]
        public string? Descricao { get; set; }

        [Required(ErrorMessage = "O preço é obrigatório.")]
        [Range(typeof(decimal), "0,01", "9999999999", ErrorMessage = "O preço deve ser maior que zero.")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "A quantidade em estoque é obrigatória.")]
        [Range(0, int.MaxValue, ErrorMessage = "A quantidade em estoque não pode ser negativa.")]
        public int QuantidadeEstoque { get; set; }

        public bool ControlaEstoque { get; set; }
        public bool Disponivel { get; set; }
        public bool EmDestaque { get; set; }

        [Required(ErrorMessage = "A loja é obrigatória.")]
        public int LojaId { get; set; }

        public Loja? Loja { get; set; }

        [Required(ErrorMessage = "A categoria é obrigatória.")]
        public Categoria Categoria { get; set; }

        [Required(ErrorMessage = "A subcategoria é obrigatória.")]
        public Subcategoria Subcategoria { get; set; }

        public ICollection<ItemCarrinho> ItensCarrinho { get; set; } = new List<ItemCarrinho>();
        public ICollection<ItemPedido> ItensPedido { get; set; } = new List<ItemPedido>();
    }
}
