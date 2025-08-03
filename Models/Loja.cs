using System.ComponentModel.DataAnnotations.Schema;
using VitrineExpress.Enums;

namespace VitrineExpress.Models
{
    public class Loja
    {
        public int Id { get; set; }
        public string Cnpj { get; set; }
        public string Nome { get; set; }
        public string ImagemUrl { get; set; }
        public string Descricao { get; set; }
        public string Telefone { get; set; }
        public double TaxaEntrega { get; set; }
        public double ValorMinimoPedido { get; set; }
        [NotMapped]
        public Dictionary<string, string> HorarioFuncionamento { get; set; } = new();
        public bool RetiradaDisponivel { get; set; }
        public bool EntregaDisponivel { get; set; }
        public int LojistaId { get; set; }
        public Lojista Lojista { get; set; }

        public StatusLoja StatusAtual { get; set; }

        public ICollection<Endereco> Enderecos { get; set; } = new List<Endereco>();
        public ICollection<Produto> Produtos { get; set; } = new List<Produto>();
        public ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
    }
}
