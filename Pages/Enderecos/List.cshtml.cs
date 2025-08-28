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
    public class ListModel : PageModel
    {
        private readonly VitrineExpress.Data.VitrineContext _context;

        public ListModel(VitrineExpress.Data.VitrineContext context)
        {
            _context = context;
        }

        public IList<Endereco> Endereco { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Endereco = await _context.Enderecos
                .Include(e => e.Loja)
                .Include(e => e.Usuario).ToListAsync();
        }
    }
}
