using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VitrineExpress.Data;
using VitrineExpress.Models;

namespace VitrineExpress.Pages.Usuarios
{
    public class DeleteModel : PageModel
    {
        private readonly VitrineExpress.Data.VitrineContext _context;

        public DeleteModel(VitrineExpress.Data.VitrineContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Usuario Usuario { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(m => m.Id == id);

            if (usuario == null)
            {
                return NotFound();
            }
            else
            {
                Usuario = usuario;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .Include(u => u.Lojas)
                .Include(u => u.Enderecos)
                .Include(u => u.Carrinhos)
                .Include(u => u.Pedidos)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (usuario != null)
            {
                if (usuario.Lojas != null && usuario.Lojas.Any())
                {
                    _context.Lojas.RemoveRange(usuario.Lojas);
                }

                if (usuario.Enderecos != null && usuario.Enderecos.Any())
                {
                    _context.Enderecos.RemoveRange(usuario.Enderecos);
                }

                if (usuario.Carrinhos != null && usuario.Carrinhos.Any())
                {
                    foreach (var carrinho in usuario.Carrinhos)
                    {
                        var itensCarrinho = await _context.ItensCarrinho
                            .Where(ic => ic.CarrinhoId == carrinho.Id)
                            .ToListAsync();

                        _context.ItensCarrinho.RemoveRange(itensCarrinho);
                    }
                    _context.Carrinhos.RemoveRange(usuario.Carrinhos);
                }

                if (usuario.Pedidos != null && usuario.Pedidos.Any())
                {
                    foreach (var pedido in usuario.Pedidos)
                    {
                        var itensPedido = await _context.ItensPedido
                            .Where(ip => ip.PedidoId == pedido.Id)
                            .ToListAsync();

                        _context.ItensPedido.RemoveRange(itensPedido);
                    }
                    _context.Pedidos.RemoveRange(usuario.Pedidos);
                }

                _context.Usuarios.Remove(usuario);

                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
