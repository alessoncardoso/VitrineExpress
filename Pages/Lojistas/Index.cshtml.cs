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
    public class IndexModel : PageModel
    {
        private readonly VitrineExpress.Data.VitrineContext _context;

        public IndexModel(VitrineExpress.Data.VitrineContext context)
        {
            _context = context;
        }

        public IList<Lojista> Lojista { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Lojista = await _context.Lojistas
                .Include(l => l.Usuario).ToListAsync();
        }
    }
}
