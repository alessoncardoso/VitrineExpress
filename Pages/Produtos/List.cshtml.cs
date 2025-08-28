using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VitrineExpress.Data;
using VitrineExpress.Models;

namespace VitrineExpress.Pages.Produtos
{
    public class CatalogoModel : PageModel
    {
        private readonly VitrineExpress.Data.VitrineContext _context;

        public CatalogoModel(VitrineExpress.Data.VitrineContext context)
        {
            _context = context;
        }

        public IList<Produto> Produto { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Produto = await _context.Produtos
                .Include(p => p.Loja).ToListAsync();
        }
    }
}
