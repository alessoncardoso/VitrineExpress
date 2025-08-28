using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using VitrineExpress.Data;
using VitrineExpress.Models;

namespace VitrineExpress.Pages.Produtos
{
    public class CreateModel : PageModel
    {
        private readonly VitrineExpress.Data.VitrineContext _context;
        private readonly IWebHostEnvironment _environment;

        public CreateModel(VitrineExpress.Data.VitrineContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public IActionResult OnGet()
        {
            ViewData["LojaId"] = new SelectList(_context.Set<Loja>(), "Id", "Nome");
            return Page();
        }

        [BindProperty]
        public Produto Produto { get; set; } = default!;

        [BindProperty]
        public IFormFile? ImagemUpload { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Upload de imagem, caso tenha sido enviada
            if (ImagemUpload != null && ImagemUpload.Length > 0)
            {
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "images/produtos");

                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(ImagemUpload.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using var fileStream = new FileStream(filePath, FileMode.Create);
                await ImagemUpload.CopyToAsync(fileStream);

                Produto.ImagemUrl = "/images/produtos/" + uniqueFileName;
            }
            else
            {
                // Se não enviou imagem, define padrão
                Produto.ImagemUrl = "/images/produtos/imagem-padrao.png";
            }

            if (!ModelState.IsValid)
            {
                ViewData["LojaId"] = new SelectList(_context.Set<Loja>(), "Id", "Nome", Produto.LojaId);
                return Page();
            }

            _context.Produtos.Add(Produto);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
