using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using VitrineExpress.Models;

namespace VitrineExpress.Pages.Carrinhos
{
    public class CreateModel : PageModel
    {
        private readonly VitrineExpress.Data.VitrineContext _context;

        public CreateModel(VitrineExpress.Data.VitrineContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Carrinho Carrinho { get; set; }

        [BindProperty]
        public List<ItemCarrinho> Itens { get; set; } = new List<ItemCarrinho>();

        public IActionResult OnGet()
        {
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Nome");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // 1. Criar o carrinho principal
            _context.Carrinhos.Add(Carrinho);
            await _context.SaveChangesAsync();

            // 2. Vincular os itens ao carrinho criado
            foreach (var item in Itens)
            {
                item.CarrinhoId = Carrinho.Id;
                _context.ItensCarrinho.Add(item);
            }

            await _context.SaveChangesAsync();

            // 3. REDIRECIONAR PARA PEDIDOS COM O CARRINHO ID
            return RedirectToPage("/Pedidos/Create", new { carrinhoId = Carrinho.Id });
        }
    }
}
