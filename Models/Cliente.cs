using System.ComponentModel.DataAnnotations;

namespace VitrineExpress.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [StringLength(14, ErrorMessage = "O CPF deve conter no máximo 14 caracteres.")]
        public string? Cpf { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        public Usuario? Usuario { get; set; }

        public ICollection<Endereco> Enderecos { get; set; } = new List<Endereco>();
        public ICollection<Carrinho> Carrinhos { get; set; } = new List<Carrinho>();
        public ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
    }
}
