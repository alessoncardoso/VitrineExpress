using System.ComponentModel.DataAnnotations;
using VitrineExpress.Enums;

namespace VitrineExpress.Models
{
    public class Pedido
    {
        public int Id { get; set; }

        [Required]
        public DateTime DataPedido { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal ValorTotal { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        public Usuario? Usuario { get; set; }

        [Required]
        public int LojaId { get; set; }

        public Loja? Loja { get; set; }

        [Required]
        public StatusPedido StatusPedido { get; set; }

        [Required]
        public TipoEntrega TipoEntrega { get; set; }

        public ICollection<ItemPedido> ItensPedido { get; set; } = new List<ItemPedido>();
    }
}
