using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using VitrineExpress.Data;
using VitrineExpress.Models;

namespace VitrineExpress.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly VitrineExpress.Data.VitrineContext _context;

        public LoginModel(VitrineExpress.Data.VitrineContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [BindProperty]
        [Required]
        public string Senha { get; set; }

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var usuario = _context.Usuarios.FirstOrDefault(u => u.Email == Email && u.Senha == Senha);

            if (usuario == null)
            {
                ModelState.AddModelError(string.Empty, "Usuário ou senha inválidos.");
                return Page();
            }

            // Aqui você pode adicionar a lógica de autenticação, como criar um cookie de autenticação
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.Nome),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim("UsuarioId", usuario.Id.ToString())
            };

            var identity = new ClaimsIdentity(claims, "VitrineCookie");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("VitrineCookie", principal);

            return RedirectToPage("/Index");
        }
    }
}
