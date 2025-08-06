using System.ComponentModel.DataAnnotations;
using VitrineExpress.Enums;

namespace VitrineExpress.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100)]
        public required string Nome { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "Formato de e-mail inválido.")]
        [StringLength(100)]
        public required string Email { get; set; }

        [Phone(ErrorMessage = "Formato de telefone inválido.")]
        [StringLength(20)]
        public string? Telefone { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [MinLength(8, ErrorMessage = "A senha deve ter no mínimo 8 caracteres.")]
        [StringLength(255)]
        public required string Senha { get; set; }

        public Cliente? Cliente { get; set; }
        public Lojista? Lojista { get; set; }
    }
}