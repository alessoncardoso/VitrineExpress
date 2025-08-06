using System.ComponentModel.DataAnnotations;

namespace VitrineExpress.Models
{
    public class ItemPedido
    {
        public int Id { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantidade { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal PrecoUnitario { get; set; }

        public decimal Subtotal => Quantidade * PrecoUnitario;

        [Required]
        public int PedidoId { get; set; }

        public Pedido? Pedido { get; set; }

        [Required]
        public int ProdutoId { get; set; }

        public Produto? Produto { get; set; }
    }
}
