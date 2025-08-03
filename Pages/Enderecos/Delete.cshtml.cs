using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VitrineExpress.Data;
using VitrineExpress.Models;

namespace VitrineExpress.Pages.Enderecos
{
    public class DeleteModel : PageModel
    {
        private readonly VitrineExpress.Data.VitrineContext _context;

        public DeleteModel(VitrineExpress.Data.VitrineContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Endereco Endereco { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var endereco = await _context.Enderecos.FirstOrDefaultAsync(m => m.Id == id);

            if (endereco == null)
            {
                return NotFound();
            }
            else
            {
                Endereco = endereco;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var endereco = await _context.Enderecos.FindAsync(id);
            if (endereco != null)
            {
                Endereco = endereco;
                _context.Enderecos.Remove(Endereco);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
