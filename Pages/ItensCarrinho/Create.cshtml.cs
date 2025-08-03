using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using VitrineExpress.Data;
using VitrineExpress.Models;

namespace VitrineExpress.Pages.ItensCarrinho
{
    public class CreateModel : PageModel
    {
        private readonly VitrineExpress.Data.VitrineContext _context;

        public CreateModel(VitrineExpress.Data.VitrineContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["CarrinhoId"] = new SelectList(_context.Set<Carrinho>(), "Id", "Id");
        ViewData["ProdutoId"] = new SelectList(_context.Produtos, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public ItemCarrinho ItemCarrinho { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ItensCarrinho.Add(ItemCarrinho);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
