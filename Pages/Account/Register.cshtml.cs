using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using VitrineExpress.Data;
using VitrineExpress.Models;

namespace VitrineExpress.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly VitrineExpress.Data.VitrineContext _context;

        public RegisterModel(VitrineExpress.Data.VitrineContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Usuario Usuario { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            // Verifica se o e-mail já existe
            if (_context.Usuarios.Any(u => u.Email == Usuario.Email))
            {
                ModelState.AddModelError("Usuario.Email", "Este e-mail já está em uso.");
                return Page();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Usuarios.Add(Usuario);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Login");
        }
    }
}
