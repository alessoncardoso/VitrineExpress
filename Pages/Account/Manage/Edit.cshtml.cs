using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VitrineExpress.Data;
using VitrineExpress.Models;

namespace VitrineExpress.Pages.Account.Manage
{
    public class EditModel(VitrineContext context) : PageModel
    {
        private readonly VitrineContext _context = context;

        [BindProperty]
        public required InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "O nome é obrigatório.")]
            public required string Nome { get; set; }

            [Required(ErrorMessage = "O email é obrigatório.")]
            public required string Email { get; set; }

            [Phone(ErrorMessage = "Formato de telefone inválido.")]
            public string? Telefone { get; set; }
        }

        private void LoadUser(Usuario user)
        {
            Input = new InputModel
            {
                Nome = user.Nome,
                Email = user.Email,
                Telefone = user.Telefone,
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _context.Usuarios.FindAsync(int.Parse(userId!));
            if (user == null) return NotFound();

            LoadUser(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _context.Usuarios.FindAsync(int.Parse(userId!));
            if (user == null) return NotFound();

            if (!ModelState.IsValid)
            {
                LoadUser(user);
                return Page();
            }

            user.Nome = Input.Nome;
            user.Email = Input.Email;
            user.Telefone = Input.Telefone;
            await _context.SaveChangesAsync();

            TempData["StatusMessage"] = "Seu perfil foi atualizado com sucesso.";
            return RedirectToPage("./Index");
        }
    }
}