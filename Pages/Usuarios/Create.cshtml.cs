using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VitrineExpress.Models;

namespace VitrineExpress.Pages.Usuarios
{
    public class CreateModel : PageModel
    {
        private readonly VitrineExpress.Data.VitrineContext _context;

        public CreateModel(VitrineExpress.Data.VitrineContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Usuario Usuario { get; set; } = default!;

        public IActionResult OnGet()
        {
            return Page();
        }

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {

            // Verifica se o e-mail já existe
            if (_context.Usuarios.Any(u => u.Email == Usuario.Email))
            {
                ModelState.AddModelError("Usuario.Email", "Este e-mail já está em uso.");
                return Page();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Cria o hasher e gera o hash da senha
            var hasher = new PasswordHasher<Usuario>();
            Usuario.Senha = hasher.HashPassword(Usuario, Usuario.Senha);

            // Definindo o tipo de usuário como Cliente por padrão
            Usuario.TipoUsuario = Enums.TipoUsuario.CLIENTE;

            _context.Usuarios.Add(Usuario);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
