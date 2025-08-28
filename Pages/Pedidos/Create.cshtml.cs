using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VitrineExpress.Models;

namespace VitrineExpress.Pages.Pedidos
{
    public class CreateModel : PageModel
    {
        private readonly VitrineExpress.Data.VitrineContext _context;

        public CreateModel(VitrineExpress.Data.VitrineContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Pedido Pedido { get; set; }

        [BindProperty]
        public List<ItemPedido> Itens { get; set; } = new List<ItemPedido>();

        [BindProperty(SupportsGet = true)]
        public int CarrinhoId { get; set; }

        public List<ItemCarrinho> ItensCarrinho { get; set; }
        public decimal ValorTotalCarrinho { get; set; }

        public async Task<IActionResult> OnGetAsync(int? carrinhoId)
        {
            if (carrinhoId == null)
            {
                return NotFound();
            }

            CarrinhoId = carrinhoId.Value;

            // Carregar itens do carrinho do banco
            ItensCarrinho = await _context.ItensCarrinho
                .Include(i => i.Produto)
                .Where(i => i.CarrinhoId == carrinhoId)
                .ToListAsync();

            ValorTotalCarrinho = ItensCarrinho.Sum(i => i.Quantidade * i.PrecoUnitario);

            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Nome");
            ViewData["LojaId"] = new SelectList(_context.Lojas, "Id", "Nome");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Recarregar dados se necessário
                return await OnGetAsync(CarrinhoId);
            }

            try
            {
                // 1. Verificar se o carrinho existe e não foi processado
                var carrinho = await _context.Carrinhos
                    .Include(c => c.ItensCarrinho)
                    .FirstOrDefaultAsync(c => c.Id == CarrinhoId && !c.Processado);

                if (carrinho == null)
                {
                    ModelState.AddModelError("", "Carrinho não encontrado ou já processado.");
                    return await OnGetAsync(CarrinhoId);
                }

                // 2. Criar o pedido principal vinculado ao carrinho
                Pedido.CarrinhoId = CarrinhoId; // VINCULA AO CARRINHO
                _context.Pedidos.Add(Pedido);
                await _context.SaveChangesAsync();

                // 3. Converter ItensCarrinho em ItensPedido
                foreach (var itemCarrinho in carrinho.ItensCarrinho)
                {
                    var itemPedido = new ItemPedido
                    {
                        ProdutoId = itemCarrinho.ProdutoId,
                        Quantidade = itemCarrinho.Quantidade,
                        PrecoUnitario = itemCarrinho.PrecoUnitario,
                        PedidoId = Pedido.Id
                    };
                    _context.ItensPedido.Add(itemPedido);
                }

                // 4. Marcar carrinho como processado
                carrinho.Processado = true;
                _context.Carrinhos.Update(carrinho);

                // 5. Salvar tudo no banco
                await _context.SaveChangesAsync();

                return RedirectToPage("./Details", new { id = Pedido.Id });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Erro ao criar pedido: " + ex.Message);
                return await OnGetAsync(CarrinhoId);
            }
        }
    }
}
