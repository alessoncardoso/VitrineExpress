using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VitrineExpress.Enums;
using VitrineExpress.Models;

namespace VitrineExpress.Pages
{
    public class IndexModel : PageModel
    {
        private readonly VitrineExpress.Data.VitrineContext _context;

        public IndexModel(VitrineExpress.Data.VitrineContext context)
        {
            _context = context;
        }

        public List<Produto> Produtos { get; set; } = new();
        public List<Loja> Lojas { get; set; } = new();

        [TempData]
        public string? StatusMessage { get; set; }

        [BindProperty]
        public string? CnpjLojista { get; set; }

        public async Task OnGetAsync()
        {
            Produtos = await _context.Produtos
                .Where(p => p.Disponivel)
                .ToListAsync();

            Lojas = await _context.Lojas
               .Include(l => l.Enderecos)
               .OrderByDescending(l => l.Id)
               .Take(6)
               .ToListAsync();
        }

        public IActionResult OnPostAdicionarAoCarrinho(string id)
        {
            StatusMessage = "Produto adicionado ao carrinho!";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostCadastrarLojistaAsync()
        {
            if (string.IsNullOrEmpty(CnpjLojista))
            {
                StatusMessage = "Informe um CNPJ válido.";
                return RedirectToPage();
            }

            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString))
            {
                return RedirectToPage("/Account/Login");
            }

            if (!int.TryParse(userIdString, out int userId))
            {
                StatusMessage = "Erro ao identificar usuário.";
                return RedirectToPage();
            }

            var usuario = await _context.Usuarios.FindAsync(userId);
            if (usuario != null)
            {
                usuario.Cnpj = CnpjLojista;

                if (usuario.TipoUsuario == TipoUsuario.CLIENTE)
                {
                    usuario.TipoUsuario = TipoUsuario.LOJISTA;
                    _context.Usuarios.Update(usuario);
                }

                await _context.SaveChangesAsync();

                // Atualiza os claims para refletir a nova role Lojista
                await HttpContext.SignOutAsync();

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                    new Claim(ClaimTypes.Name, usuario.Nome),
                    new Claim(ClaimTypes.Role, usuario.TipoUsuario.ToString())
                };

                var claimsIdentity = new ClaimsIdentity(claims, "login");
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                await HttpContext.SignInAsync(claimsPrincipal);

                StatusMessage = "CNPJ cadastrado com sucesso! Agora você é um lojista.";
            }

            return RedirectToPage("/Lojas/Create");
        }
    }
}
