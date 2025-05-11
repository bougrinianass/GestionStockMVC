using AppGestionStockMVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace AppGestionStockMVC.Controllers
{
    public class ClientController : Controller
    {
        private readonly ClientService _clientService;

        public ClientController(ClientService clientService)
        {
            _clientService = clientService;
        }

        public async Task<IActionResult> Index()
        {
            var clients = await _clientService.GetClientsAsync();
            return View(clients);
        }
    }
}
