namespace VitrineExpress.Models
{
    public class Endereco
    {
        public int Id { get; set; }
        public string Rua { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Cep { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public int LojaId { get; set; }
        public Loja Loja { get; set; }
    }
}
