using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VitrineExpress.Models;

namespace VitrineExpress.Pages.Enderecos
{
    public class EditModel : PageModel
    {
        private readonly VitrineExpress.Data.VitrineContext _context;

        public EditModel(VitrineExpress.Data.VitrineContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Endereco Endereco { get; set; } = default!;

        // Para exibir o nome do lojista ou loja
        public string Nome { get; set; } = string.Empty;

        // Lista de estados para o dropdown
        public SelectList Estados { get; set; } = default!;

        // Popula o dropdown de estados
        private void PopulateEstadosDropDownList(object? selectedEstado = null)
        {
            var listaDeEstados = new List<string>
            {
                "AC", "AL", "AP", "AM", "BA", "CE", "DF", "ES", "GO", "MA", "MT", "MS",
                "MG", "PA", "PB", "PR", "PE", "PI", "RJ", "RN", "RS", "RO", "RR", "SC",
                "SP", "SE", "TO"
            };
            Estados = new SelectList(listaDeEstados, selectedEstado);
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            var endereco = await _context.Enderecos
                .Include(e => e.Usuario)
                .Include(e => e.Loja)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (endereco == null) return NotFound();

            Endereco = endereco;

            // Define o nome do proprietário
            if (Endereco.UsuarioId != null)
                Nome = Endereco.Usuario?.Nome ?? "Não encontrado";
            else if (Endereco.LojaId != null)
                Nome = Endereco.Loja?.Nome ?? "Não encontrada";

            PopulateEstadosDropDownList(Endereco.Estado);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var enderecoOriginal = await _context.Enderecos
                .AsNoTracking()
                .Include(e => e.Usuario)
                .Include(e => e.Loja)
                .FirstOrDefaultAsync(e => e.Id == Endereco.Id);

            if (enderecoOriginal == null) return NotFound();

            // Preserva os IDs do proprietário
            Endereco.UsuarioId = enderecoOriginal.UsuarioId;
            Endereco.LojaId = enderecoOriginal.LojaId;

            // Garante que Estado não fique vazio
            if (string.IsNullOrEmpty(Endereco.Estado))
                Endereco.Estado = enderecoOriginal.Estado;

            if (!ModelState.IsValid)
            {
                PopulateEstadosDropDownList(Endereco.Estado);

                if (Endereco.UsuarioId != null)
                    Nome = enderecoOriginal.Usuario?.Nome ?? "Não encontrado";
                else if (Endereco.LojaId != null)
                    Nome = enderecoOriginal.Loja?.Nome ?? "Não encontrada";

                return Page();
            }

            _context.Attach(Endereco).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            if (Endereco.UsuarioId != null)
                return RedirectToPage("/Account/Manage/Index");
            else
                return RedirectToPage("./Index");

        }
    }
}
