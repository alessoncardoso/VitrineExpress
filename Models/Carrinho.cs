namespace VitrineExpress.Models
{
    public class Carrinho
    {
        public int Id { get; set; }
        public decimal ValorTotal { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        public ICollection<ItemCarrinho> ItensCarrinho { get; set; } = new List<ItemCarrinho>();
    }
}
