using System.ComponentModel.DataAnnotations;

namespace VitrineExpress.Models
{
    public class Endereco
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo 'Rua' é obrigatório.")]
        public string Rua { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo 'Número' é obrigatório.")]
        public string Numero { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo 'Bairro' é obrigatório.")]
        public string Bairro { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo 'Cidade' é obrigatório.")]
        public string Cidade { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo 'Estado' é obrigatório.")]
        public string Estado { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo 'CEP' é obrigatório.")]

        public string Cep { get; set; } = string.Empty;

        public int? UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }

        public int? LojaId { get; set; }
        public Loja? Loja { get; set; }
    }
}
