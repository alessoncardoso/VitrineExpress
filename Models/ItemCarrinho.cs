using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VitrineExpress.Models
{
    public class ItemCarrinho
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "A quantidade é obrigatória.")]
        [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser no mínimo 1.")]
        public int Quantidade { get; set; }

        [Required(ErrorMessage = "O preço unitário é obrigatório.")]
        [Range(typeof(decimal), "0,01", "9999999999", ErrorMessage = "O preço unitário deve ser maior que zero.")]
        public decimal PrecoUnitario { get; set; }

        [NotMapped] // Campo calculado, não armazenado no banco
        public decimal Subtotal => Quantidade * PrecoUnitario;

        [Required(ErrorMessage = "O carrinho é obrigatório.")]
        public int CarrinhoId { get; set; }
        public Carrinho? Carrinho { get; set; }

        [Required(ErrorMessage = "O produto é obrigatório.")]
        public int ProdutoId { get; set; }
        public Produto? Produto { get; set; }
    }
}
