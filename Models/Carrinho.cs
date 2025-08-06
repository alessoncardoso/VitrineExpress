using System.ComponentModel.DataAnnotations;

namespace VitrineExpress.Models
{
    public class Carrinho
    {
        public int Id { get; set; }

        [Range(0, double.MaxValue)]
        public decimal ValorTotal { get; set; }

        [Required]
        public int ClienteId { get; set; }

        public Cliente? Cliente { get; set; }

        public ICollection<ItemCarrinho> ItensCarrinho { get; set; } = new List<ItemCarrinho>();
    }
}
