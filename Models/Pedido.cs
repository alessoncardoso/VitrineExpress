using System.ComponentModel.DataAnnotations;
using VitrineExpress.Enums;

namespace VitrineExpress.Models
{
    public class Pedido
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "A data do pedido é obrigatória.")]
        public DateTime DataPedido { get; set; }

        [Required(ErrorMessage = "O valor total é obrigatório.")]
        [Range(typeof(decimal), "0,01", "9999999999", ErrorMessage = "O valor total deve ser maior que zero.")]
        public decimal ValorTotal { get; set; }

        [Required(ErrorMessage = "O cliente é obrigatório.")]
        public int UsuarioId { get; set; }

        public Usuario? Usuario { get; set; }

        [Required(ErrorMessage = "A loja é obrigatória.")]
        public int LojaId { get; set; }

        public Loja? Loja { get; set; }

        // NOVO: Referência ao Carrinho que originou este Pedido
        public int? CarrinhoId { get; set; }
        public Carrinho? Carrinho { get; set; }

        [Required(ErrorMessage = "O status do pedido é obrigatório.")]
        public StatusPedido StatusPedido { get; set; }

        [Required(ErrorMessage = "O tipo de entrega é obrigatório.")]
        public TipoEntrega TipoEntrega { get; set; }

        public ICollection<ItemPedido> ItensPedido { get; set; } = new List<ItemPedido>();
    }
}
