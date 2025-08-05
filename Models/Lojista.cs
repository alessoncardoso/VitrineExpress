namespace VitrineExpress.Models
{
    public class Lojista
    {
        public int Id { get; set; }
        public string Cnpj { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public ICollection<Loja> Lojas { get; set; } = new List<Loja>();
    }
}