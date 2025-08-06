using System.ComponentModel.DataAnnotations;

namespace VitrineExpress.Models
{
    public class Lojista
    {
        public int Id { get; set; }

        [StringLength(18, ErrorMessage = "O CNPJ deve conter no máximo 18 caracteres.")]
        public string? Cnpj { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        public Usuario? Usuario { get; set; }

        public ICollection<Loja> Lojas { get; set; } = new List<Loja>();
    }
}
