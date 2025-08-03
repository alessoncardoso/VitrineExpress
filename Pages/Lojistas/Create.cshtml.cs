using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using VitrineExpress.Data;
using VitrineExpress.Models;

namespace VitrineExpress.Pages.Lojistas
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
        ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Lojista Lojista { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Lojistas.Add(Lojista);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
