using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VitrineExpress.Data;
using VitrineExpress.Models;

namespace VitrineExpress.Pages.Lojistas
{
    public class DetailsModel : PageModel
    {
        private readonly VitrineExpress.Data.VitrineContext _context;

        public DetailsModel(VitrineExpress.Data.VitrineContext context)
        {
            _context = context;
        }

        public Lojista Lojista { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lojista = await _context.Lojistas.FirstOrDefaultAsync(m => m.Id == id);
            if (lojista == null)
            {
                return NotFound();
            }
            else
            {
                Lojista = lojista;
            }
            return Page();
        }
    }
}
