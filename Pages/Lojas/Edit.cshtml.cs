using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VitrineExpress.Data;
using VitrineExpress.Models;

namespace VitrineExpress.Pages.Lojas
{
    public class EditModel : PageModel
    {
        private readonly VitrineExpress.Data.VitrineContext _context;

        public EditModel(VitrineExpress.Data.VitrineContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Loja Loja { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loja =  await _context.Loja.FirstOrDefaultAsync(m => m.Id == id);
            if (loja == null)
            {
                return NotFound();
            }
            Loja = loja;
           ViewData["LojistaId"] = new SelectList(_context.Lojista, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Loja).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LojaExists(Loja.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool LojaExists(int id)
        {
            return _context.Loja.Any(e => e.Id == id);
        }
    }
}
