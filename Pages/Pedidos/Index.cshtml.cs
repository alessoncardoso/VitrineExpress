using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VitrineExpress.Data;
using VitrineExpress.Models;

namespace VitrineExpress.Pages.Pedidos
{
    public class IndexModel : PageModel
    {
        private readonly VitrineExpress.Data.VitrineContext _context;

        public IndexModel(VitrineExpress.Data.VitrineContext context)
        {
            _context = context;
        }

        public IList<Pedido> Pedido { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Pedido = await _context.Pedidos
                .Include(p => p.Cliente)
                .Include(p => p.Loja).ToListAsync();
        }
    }
}
