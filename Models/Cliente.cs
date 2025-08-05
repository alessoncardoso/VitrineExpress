namespace VitrineExpress.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Cpf { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public ICollection<Endereco> Enderecos { get; set; } = new List<Endereco>();
        public ICollection<Carrinho> Carrinhos { get; set; } = new List<Carrinho>();
        public ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
    }
}
