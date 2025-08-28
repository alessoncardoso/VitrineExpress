using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VitrineExpress.Enums;
using VitrineExpress.Models;

namespace VitrineExpress.Pages.Lojas
{
    public class DetailsModel : PageModel
    {
        private readonly VitrineExpress.Data.VitrineContext _context;

        public DetailsModel(VitrineExpress.Data.VitrineContext context)
        {
            _context = context;
        }

        public Loja Loja { get; set; } = default!;

        // Propriedade que define se o usuário atual pode gerenciar a loja e seus produtos
        public bool CanManage { get; private set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Busca a loja e já inclui (Include) todos os dados relacionados que vamos precisar na página
            var loja = await _context.Lojas
                .Include(l => l.Usuario)
                .Include(l => l.Enderecos)
                .Include(l => l.Produtos)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (loja == null)
            {
                return NotFound();
            }

            Loja = loja;

            // Lógica de permissão: ADMIN pode tudo, LOJISTA só pode gerenciar a própria loja.
            CanManage = false;
            if (User.Identity?.IsAuthenticated == true)
            {
                var isAdmin = User.IsInRole(TipoUsuario.ADMIN.ToString());
                var currentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

                if (isAdmin || Loja.UsuarioId == currentUserId)
                {
                    CanManage = true;
                }
            }

            return Page();
        }
    }
}