using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using VitrineExpress.Data;
using VitrineExpress.Models;

namespace VitrineExpress.Pages.Lojas
{
    public class EditModel : PageModel
    {
        private readonly VitrineExpress.Data.VitrineContext _context;
        private readonly IWebHostEnvironment _environment;

        public EditModel(VitrineExpress.Data.VitrineContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [BindProperty]
        public Loja Loja { get; set; } = default!;

        [BindProperty]
        public IFormFile? ImagemUpload { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loja =  await _context.Lojas.FirstOrDefaultAsync(m => m.Id == id);
            if (loja == null)
            {
                return NotFound();
            }
            Loja = loja;
           ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id");
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

            // Upload de imagem, caso tenha sido enviada
            if (ImagemUpload != null && ImagemUpload.Length > 0)
            {
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "images/lojas");

                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(ImagemUpload.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using var fileStream = new FileStream(filePath, FileMode.Create);
                await ImagemUpload.CopyToAsync(fileStream);

                Loja.ImagemUrl = "/images/lojas/" + uniqueFileName;
            }
            else
            {
                // Se não enviou imagem, define padrão
                Loja.ImagemUrl = "/images/lojas/imagem-padrao.png";
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
            return _context.Lojas.Any(e => e.Id == id);
        }
    }
}
