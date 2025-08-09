using System.ComponentModel.DataAnnotations;

namespace VitrineExpress.Models
{
    public class Endereco
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo 'Rua' é obrigatório.")]
        [StringLength(100)]
        public required string Rua { get; set; }

        [Required(ErrorMessage = "O campo 'Número' é obrigatório.")]
        [StringLength(10)]
        public required string Numero { get; set; }

        [Required(ErrorMessage = "O campo 'Bairro' é obrigatório.")]
        [StringLength(100)]
        public required string Bairro { get; set; }

        [Required(ErrorMessage = "O campo 'Cidade' é obrigatório.")]
        [StringLength(100)]
        public required string Cidade { get; set; }

        [Required(ErrorMessage = "O campo 'Estado' é obrigatório.")]
        [StringLength(2, MinimumLength = 2)]
        public required string Estado { get; set; }

        [Required(ErrorMessage = "O campo 'CEP' é obrigatório.")]
        [StringLength(9)]
        public required string Cep { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        public Usuario? Usuario { get; set; }

        [Required]
        public int LojaId { get; set; }

        public Loja? Loja { get; set; }
    }
}
