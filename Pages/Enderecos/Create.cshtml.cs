using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using VitrineExpress.Data;
using VitrineExpress.Models;

namespace VitrineExpress.Pages.Enderecos
{
    public class CreateModel : PageModel
    {
        private readonly VitrineExpress.Data.VitrineContext _context;

        public CreateModel(VitrineExpress.Data.VitrineContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["ClienteId"] = new SelectList(_context.Set<Cliente>(), "Id", "Id");
        ViewData["LojaId"] = new SelectList(_context.Lojas, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Endereco Endereco { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Enderecos.Add(Endereco);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
