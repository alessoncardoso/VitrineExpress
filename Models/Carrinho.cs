using System.ComponentModel.DataAnnotations;

namespace VitrineExpress.Models
{
    public class Carrinho
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O valor total é obrigatório.")]
        [Range(typeof(decimal), "0,01", "9999999999", ErrorMessage = "O valor total não pode ser negativo.")]
        public decimal ValorTotal { get; set; }

        [Required(ErrorMessage = "O usuário é obrigatório.")]
        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }

        // NOVO: Indica se o carrinho foi convertido em pedido
        public bool Processado { get; set; } = false;

        // NOVO: Referência ao Pedido criado a partir deste Carrinho
        public Pedido? Pedido { get; set; }

        public ICollection<ItemCarrinho> ItensCarrinho { get; set; } = new List<ItemCarrinho>();
    }
}
