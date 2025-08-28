using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VitrineExpress.Enums;

namespace VitrineExpress.Models
{
    public class Loja
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O CNPJ da loja é obrigatório.")]
        [StringLength(18, ErrorMessage = "O CNPJ deve ter no máximo 18 caracteres.")]
        public string Cnpj { get; set; } = string.Empty;

        [Required(ErrorMessage = "O nome da loja é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome da loja deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [StringLength(255, ErrorMessage = "A URL da imagem deve ter no máximo 255 caracteres.")]
        public string? ImagemUrl { get; set; }

        [StringLength(500, ErrorMessage = "A descrição deve ter no máximo 500 caracteres.")]
        public string? Descricao { get; set; }

        [Phone(ErrorMessage = "Formato de telefone inválido.")]
        [StringLength(20, ErrorMessage = "O telefone deve ter no máximo 20 caracteres.")]
        public string? Telefone { get; set; }

        [Range(typeof(decimal), "0,01", "9999999999", ErrorMessage = "A taxa de entrega não pode ser negativa.")]
        public decimal? TaxaEntrega { get; set; }

        [Range(typeof(decimal), "0,01", "9999999999", ErrorMessage = "O valor mínimo do pedido não pode ser negativo.")]
        public decimal? ValorMinimoPedido { get; set; }

        [NotMapped]
        public Dictionary<string, string> HorarioFuncionamento { get; set; } = new();

        public bool RetiradaDisponivel { get; set; }
        public bool EntregaDisponivel { get; set; }

        [Required(ErrorMessage = "O usuário dono da loja é obrigatório.")]
        public int UsuarioId { get; set; }

        public Usuario? Usuario { get; set; }

        [Required(ErrorMessage = "O status da loja é obrigatório.")]
        public StatusLoja StatusAtual { get; set; }

        public ICollection<Endereco> Enderecos { get; set; } = new List<Endereco>();
        public ICollection<Produto> Produtos { get; set; } = new List<Produto>();
        public ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
    }
}
