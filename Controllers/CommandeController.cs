using System.Security.Claims;
using AppGestionStockMVC.Models;
using AppGestionStockMVC.Services;
using AppGestionStockMVC.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace AppGestionStockMVC.Controllers
{
    public class CommandeController : Controller
    {
        private readonly BoutiqueService _boutiqueService;

        public CommandeController(BoutiqueService boutiqueService)
        {
            _boutiqueService = boutiqueService;
        }
        public async Task<IActionResult> Index()
        {
            string clientId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var commandes = await _boutiqueService.GetCommandesAsync();
            var commandesClient = commandes.Where(c => c.ClientId == clientId).ToList();
            return View(commandesClient);
        }
    }
}
