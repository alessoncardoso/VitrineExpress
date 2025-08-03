using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VitrineExpress.Data;
using VitrineExpress.Models;

namespace VitrineExpress.Pages.ItensPedido
{
    public class DeleteModel : PageModel
    {
        private readonly VitrineExpress.Data.VitrineContext _context;

        public DeleteModel(VitrineExpress.Data.VitrineContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ItemPedido ItemPedido { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itempedido = await _context.ItemPedido.FirstOrDefaultAsync(m => m.Id == id);

            if (itempedido == null)
            {
                return NotFound();
            }
            else
            {
                ItemPedido = itempedido;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itempedido = await _context.ItemPedido.FindAsync(id);
            if (itempedido != null)
            {
                ItemPedido = itempedido;
                _context.ItemPedido.Remove(ItemPedido);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
