using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VitrineExpress.Data;
using VitrineExpress.Models;

namespace VitrineExpress.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly VitrineContext _context;

        public IndexModel(VitrineContext context)
        {
            _context = context;
        }

        public int UsuarioId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public Endereco EnderecoPrincipal { get; set; } = new Endereco();

        [TempData]
        public string StatusMessage { get; set; } = string.Empty;

        public async Task<IActionResult> OnGetAsync()
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdStr))
            {
                return RedirectToPage("./UserNotFound");
            }

            if (!int.TryParse(userIdStr, out int userId))
            {
                return RedirectToPage("./UserNotFound");
            }

            var user = await _context.Usuarios
                .Include(u => u.Enderecos)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return RedirectToPage("./UserNotFound");
            }

            UsuarioId = user.Id;
            Nome = user.Nome;
            Email = user.Email;

            // Fix for CS8601: Use null-coalescing operator to ensure a non-null value is assigned
            EnderecoPrincipal = user.Enderecos.FirstOrDefault() ?? new Endereco();

            return Page();
        }
    }
}
