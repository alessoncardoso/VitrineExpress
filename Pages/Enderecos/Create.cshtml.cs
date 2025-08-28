using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
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

        [BindProperty]
        public Endereco Endereco { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public int? LojaId { get; set; } // agora opcional

        public Loja? Loja { get; set; }
        public Usuario? Usuario { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            // Pega o usuário logado, se existir
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(userId))
            {
                Usuario = await _context.Usuarios
                    .FirstOrDefaultAsync(u => u.Id == int.Parse(userId));
            }

            // Busca loja se LojaId tiver valor
            if (LojaId.HasValue)
            {
                Loja = await _context.Lojas
                    .Include(l => l.Enderecos)
                    .FirstOrDefaultAsync(l => l.Id == LojaId.Value);
            }

            // Se já existe endereço para essa loja, carrega
            if (Loja is not null)
            {
                Endereco = Loja.Enderecos.FirstOrDefault()
                    ?? CriarNovoEndereco();
            }
            else
            {
                Endereco = CriarNovoEndereco();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Se usuário logado, atribui ao endereço
            if (!string.IsNullOrEmpty(userId))
            {
                Endereco.UsuarioId = int.Parse(userId);
            }

            // Se loja foi informada, atribui ao endereço
            Loja? loja = null;
            if (LojaId.HasValue)
            {
                loja = await _context.Lojas
                    .Include(l => l.Enderecos)
                    .FirstOrDefaultAsync(l => l.Id == LojaId.Value);

                if (loja != null)
                    Endereco.LojaId = loja.Id;
            }

            // Salva ou atualiza
            if (loja != null)
            {
                var enderecoExistente = loja.Enderecos.FirstOrDefault();
                if (enderecoExistente is null)
                    _context.Enderecos.Add(Endereco);
                else
                    _context.Entry(enderecoExistente).CurrentValues.SetValues(Endereco);
            }
            else
            {
                _context.Enderecos.Add(Endereco);
            }

            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }

        private Endereco CriarNovoEndereco()
        {
            return new Endereco
            {
                LojaId = Loja?.Id,
                UsuarioId = Usuario?.Id ?? 0,
                Rua = string.Empty,
                Numero = string.Empty,
                Bairro = string.Empty,
                Cidade = string.Empty,
                Estado = string.Empty,
                Cep = string.Empty
            };
        }
    }
}
