using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VitrineExpress.Data;
using VitrineExpress.Models;

namespace VitrineExpress.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly VitrineContext _db;

        public DetailsModel(VitrineContext db)
        {
            _db = db;
        }
        public List<Produto> Produtos { get; set; } = new();
        public Carrinho? CarrinhoAtual { get; set; }
        public List<ItemCarrinho> ItensCarrinho { get; set; } = new();

        public async Task OnGetAsync()
        {
            // Produtos para a seção de "Produtos"
            Produtos = await _db.Produtos
                .Where(p => p.Disponivel)
                .ToListAsync();

            // Carregar carrinho do usuário logado (exemplo usando Id fixo)
            var usuarioId = 1; // depois substituir pelo ID do usuário autenticado
            CarrinhoAtual = await _db.Carrinhos
                .Include(c => c.ItensCarrinho)
                .ThenInclude(ic => ic.Produto)
                .FirstOrDefaultAsync(c => c.UsuarioId == usuarioId);

            ItensCarrinho = CarrinhoAtual?.ItensCarrinho.ToList() ?? new List<ItemCarrinho>();
        }

    }
}
