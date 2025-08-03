using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VitrineExpress.Data;
using VitrineExpress.Models;

namespace VitrineExpress.Pages.ItensCarrinho
{
    public class DeleteModel : PageModel
    {
        private readonly VitrineExpress.Data.VitrineContext _context;

        public DeleteModel(VitrineExpress.Data.VitrineContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ItemCarrinho ItemCarrinho { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemcarrinho = await _context.ItensCarrinho.FirstOrDefaultAsync(m => m.Id == id);

            if (itemcarrinho == null)
            {
                return NotFound();
            }
            else
            {
                ItemCarrinho = itemcarrinho;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemcarrinho = await _context.ItensCarrinho.FindAsync(id);
            if (itemcarrinho != null)
            {
                ItemCarrinho = itemcarrinho;
                _context.ItensCarrinho.Remove(ItemCarrinho);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
