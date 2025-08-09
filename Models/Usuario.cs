using System.ComponentModel.DataAnnotations;
using VitrineExpress.Enums;

namespace VitrineExpress.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100)]
        public required string Nome { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "Formato de e-mail inválido.")]
        [StringLength(100)]
        public required string Email { get; set; }

        [Phone(ErrorMessage = "Formato de telefone inválido.")]
        [StringLength(20)]
        public string? Telefone { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [MinLength(8, ErrorMessage = "A senha deve ter no mínimo 8 caracteres.")]
        [StringLength(255)]
        public required string Senha { get; set; }

        [StringLength(14, ErrorMessage = "O CPF deve conter no máximo 14 caracteres.")]
        public string? Cpf { get; set; }      // Para Cliente

        [StringLength(18, ErrorMessage = "O CNPJ deve conter no máximo 18 caracteres.")]
        public string? Cnpj { get; set; }     // Para Lojista

        [Required]
        public TipoUsuario TipoUsuario { get; set; }

        public ICollection<Loja>? Lojas { get; set; } = new List<Loja>();
        public ICollection<Endereco>? Enderecos { get; set; } = new List<Endereco>();
        public ICollection<Carrinho>? Carrinhos { get; set; } = new List<Carrinho>();
        public ICollection<Pedido>? Pedidos { get; set; } = new List<Pedido>();
    }
}