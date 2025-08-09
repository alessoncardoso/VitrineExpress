using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VitrineExpress.Enums;

namespace VitrineExpress.Models
{
    public class Loja
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O CNPJ da loja é obrigatório.")]
        [StringLength(18)]
        public string? Cnpj { get; set; }

        [Required(ErrorMessage = "O nome da loja é obrigatório.")]
        [StringLength(100)]
        public required string Nome { get; set; }

        [StringLength(255)]
        public string? ImagemUrl { get; set; }

        [StringLength(500)]
        public string? Descricao { get; set; }

        [Phone(ErrorMessage = "Formato de telefone inválido.")]
        [StringLength(20)]
        public string? Telefone { get; set; }

        [Range(0, double.MaxValue)]
        public double? TaxaEntrega { get; set; }

        [Range(0, double.MaxValue)]
        public double? ValorMinimoPedido { get; set; }

        [NotMapped]
        public Dictionary<string, string> HorarioFuncionamento { get; set; } = new();

        public bool RetiradaDisponivel { get; set; }
        public bool EntregaDisponivel { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        public Usuario? Usuario { get; set; }

        [Required]
        public StatusLoja StatusAtual { get; set; }

        public ICollection<Endereco> Enderecos { get; set; } = new List<Endereco>();
        public ICollection<Produto> Produtos { get; set; } = new List<Produto>();
        public ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
    }
}
