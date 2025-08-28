using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VitrineExpress.Models
{
    public class ItemPedido
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "A quantidade é obrigatória.")]
        [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser no mínimo 1.")]
        public int Quantidade { get; set; }

        [Required(ErrorMessage = "O preço unitário é obrigatório.")]
        [Range(typeof(decimal), "0,01", "9999999999", ErrorMessage = "O preço unitário deve ser maior que zero.")]
        public decimal PrecoUnitario { get; set; }

        [NotMapped] // Evita mapeamento direto no banco, pois é calculado
        public decimal Subtotal => Quantidade * PrecoUnitario;

        [Required(ErrorMessage = "O pedido é obrigatório.")]
        public int PedidoId { get; set; }
        public Pedido? Pedido { get; set; }

        [Required(ErrorMessage = "O produto é obrigatório.")]
        public int ProdutoId { get; set; }
        public Produto? Produto { get; set; }
    }
}
