using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using VitrineExpress.Data;
using VitrineExpress.Models;

namespace VitrineExpress.Pages.Account.Manage
{
    public class DeleteModel : PageModel
    {
        private readonly VitrineContext _context;

        public DeleteModel(VitrineContext context)
        {
            _context = context;
        }

        public string NomeUsuario { get; set; }

        [BindProperty]
        [Display(Name = "Confirmação")]
        public string ConfirmationText { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _context.Usuarios.FindAsync(int.Parse(userId!));
            if (user == null)
            {
                return NotFound();
            }

            NomeUsuario = user.Nome;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _context.Usuarios.FindAsync(int.Parse(userId!));
            if (user == null)
            {
                return NotFound();
            }

            // Validação da frase de confirmação
            const string requiredPhrase = "eu quero excluir a conta";
            if (ConfirmationText != requiredPhrase)
            {
                ModelState.AddModelError("ConfirmationText", "O texto de confirmação está incorreto. A exclusão foi cancelada.");
                NomeUsuario = user.Nome; // Garante que o nome seja exibido novamente na página de erro
                return Page();
            }

            _context.Usuarios.Remove(user);
            await _context.SaveChangesAsync();

            // Desloga o usuário antes de redirecionar
            await HttpContext.SignOutAsync("VitrineCookie");

            TempData["GlobalMessage"] = "Sua conta foi excluída com sucesso.";
            return Redirect("~/"); // Redireciona para a página inicial do site
        }
    }
}