using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VitrineExpress.Data;
using VitrineExpress.Models;

namespace VitrineExpress.Pages.Carrinhos
{
    public class DetailsModel : PageModel
    {
        private readonly VitrineExpress.Data.VitrineContext _context;

        public DetailsModel(VitrineExpress.Data.VitrineContext context)
        {
            _context = context;
        }

        public Carrinho Carrinho { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carrinho = await _context.Carrinhos.FirstOrDefaultAsync(m => m.Id == id);
            if (carrinho == null)
            {
                return NotFound();
            }
            else
            {
                Carrinho = carrinho;
            }
            return Page();
        }
    }
}
