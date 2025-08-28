using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VitrineExpress.Data;
using VitrineExpress.Models;

namespace VitrineExpress.Pages.Account.Manage
{
    public class ChangePasswordModel : PageModel
    {
        private readonly VitrineContext _context;
        private readonly IPasswordHasher<Usuario> _passwordHasher;

        public ChangePasswordModel(VitrineContext context, IPasswordHasher<Usuario> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "O campo Senha atual é obrigatório.")]
            [DataType(DataType.Password)]
            [Display(Name = "Senha atual")]
            public string OldPassword { get; set; }

            [Required(ErrorMessage = "O campo Nova senha é obrigatório.")]
            [StringLength(100, ErrorMessage = "A {0} deve ter no mínimo {2} e no máximo {1} caracteres.", MinimumLength = 8)]
            [DataType(DataType.Password)]
            [Display(Name = "Nova senha")]
            public string NewPassword { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirmar nova senha")]
            [Compare("NewPassword", ErrorMessage = "A nova senha e a senha de confirmação não correspondem.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _context.Usuarios.FindAsync(int.Parse(userId!));
            if (user == null)
            {
                return NotFound($"Não foi possível encontrar o usuário.");
            }

            var verificationResult = _passwordHasher.VerifyHashedPassword(user, user.Senha, Input.OldPassword);
            if (verificationResult == PasswordVerificationResult.Failed)
            {
                ModelState.AddModelError(string.Empty, "Senha atual incorreta.");
                return Page();
            }

            user.Senha = _passwordHasher.HashPassword(user, Input.NewPassword);
            await _context.SaveChangesAsync();

            TempData["StatusMessage"] = "Sua senha foi alterada com sucesso.";
            return RedirectToPage("./Index");
        }
    }
}