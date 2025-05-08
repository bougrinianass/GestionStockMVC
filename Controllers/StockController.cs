using AppGestionStockMVC.Data;
using AppGestionStockMVC.Models;
using AppGestionStockMVC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppGestionStockMVC.Controllers
{
    [Authorize]
    public class StockController : Controller
    {

        private readonly StockService _stockService;

        public StockController(StockService stockService)
        {
            _stockService = stockService;
        }

        public async Task<IActionResult> Index()
        {
            var produits = await _stockService.GetProduitsAsync();
            return View(produits);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var produits = await _stockService.GetProduitsAsync();
            return View(produits.FirstOrDefault(s => s.Id == id));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> CreateProduit(Produit produit)
        {
            var success=await _stockService.AddProduitAsync(produit);
            if (success)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Échec de l'ajout du produit");
            return View(produit);
        }

        public async Task<IActionResult> EditProduit(Produit produit)
        {
            var success = await _stockService.EditProduit(produit);
            if (success)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Échec de l'ajout du produit");
            return View("Edit", produit);
        }

        public async Task<IActionResult> Details(int id)
        {
            var produits = await _stockService.GetProduitsAsync();
            var produit = produits.FirstOrDefault(produits => produits.Id == id);
            return View(produit);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _stockService.DeleteProduitAsync(id);
            if (success)
            {
                return RedirectToAction("Index");
            }

            return BadRequest("Erreur lors de la suppression du produit.");
        }

        public async Task<ActionResult> Export(string type)
        {
            var products = await _stockService.GetProduitsAsync();
            IExportStrategy strategy=null;

            switch (type.ToLower())
            {
                case "excel":
                    strategy = new ExcelExportStrategy();
                    break;
                case "pdf":
                    strategy = new PdfExportStrategy(); // À implémenter
                    break;
                case "word":
                    strategy = new WordExportStrategy(); // À implémenter
                    break;
            }

            var context = new ExportContext(strategy);
            var fileContent = context.Export(products);

            return File(fileContent, context.ContentType, $"Produits{context.FileExtension}");
        }

    }
}
