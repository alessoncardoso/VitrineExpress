using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VitrineExpress.Models;

namespace VitrineExpress.Pages.Lojas
{
    public class CreateModel : PageModel
    {

        private readonly VitrineExpress.Data.VitrineContext _context;
        private readonly IWebHostEnvironment _environment;

        public CreateModel(VitrineExpress.Data.VitrineContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [BindProperty]
        public Loja Loja { get; set; } = default!;

        [BindProperty]
        public IFormFile? ImagemUpload { get; set; }

        public IActionResult OnGet()
        {
            // Exibe nome do usuário logado
            var userName = User.FindFirst(ClaimTypes.Name)?.Value;
            if (!string.IsNullOrEmpty(userName))
            {
                ViewData["UsuarioNome"] = userName;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Upload de imagem, caso tenha sido enviada
            if (ImagemUpload != null && ImagemUpload.Length > 0)
            {
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "images/lojas");

                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(ImagemUpload.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using var fileStream = new FileStream(filePath, FileMode.Create);
                await ImagemUpload.CopyToAsync(fileStream);

                Loja.ImagemUrl = "/images/lojas/" + uniqueFileName;
            }
            else
            {
                // Se não enviou imagem, define padrão
                Loja.ImagemUrl = "/images/lojas/imagem-padrao.png";
            }

            // Obtém o ID do usuário logado
            var userIdClaim = User.FindFirst("UsuarioId")?.Value
                              ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int usuarioId))
            {
                ModelState.AddModelError(string.Empty, "Usuário não autenticado corretamente.");
                return Page();
            }

            // Verifica se o usuário existe
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == usuarioId);
            if (usuario == null)
            {
                ModelState.AddModelError(string.Empty, "Usuário não encontrado.");
                return Page();
            }

            // Vincula a loja ao usuário logado
            Loja.UsuarioId = usuarioId;

            _context.Lojas.Add(Loja);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
