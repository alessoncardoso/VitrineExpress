using VitrineExpress.Enums;

namespace VitrineExpress.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public DateTime DataPedido { get; set; }
        public decimal ValorTotal { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public int LojaId { get; set; }
        public Loja Loja { get; set; }

        public StatusPedido StatusPedido { get; set; }
        public TipoEntrega TipoEntrega { get; set; }

        public ICollection<ItemPedido> ItensPedido { get; set; } = new List<ItemPedido>();
    }
}
