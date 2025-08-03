using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VitrineExpress.Data;
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

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loja = await _context.Lojas.FirstOrDefaultAsync(m => m.Id == id);
            if (loja == null)
            {
                return NotFound();
            }
            else
            {
                Loja = loja;
            }
            return Page();
        }
    }
}
