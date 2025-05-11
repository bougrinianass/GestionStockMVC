using System.Security.Claims;
using AppGestionStockMVC.Models;
using AppGestionStockMVC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppGestionStockMVC.Controllers
{
    public class BoutiqueController : Controller
    {
        private readonly BoutiqueService _boutiqueService;
        private readonly StockService _stockService;
        private readonly PanierService _panier;

        public BoutiqueController(BoutiqueService boutiqueService, StockService stockService,PanierService panier)
        {
            _boutiqueService = boutiqueService;
            _stockService = stockService;
            _panier = panier;
        }

        [HttpPost]
        public async Task<IActionResult> AjouterAuPanier(int produitId, int quantite)
        {
            var produits = await _stockService.GetProduitsAsync();
            var produit = produits.FirstOrDefault(p => p.Id == produitId);
            if (produit == null) return NotFound();

            _panier.AjouterAuPanier(new PanierItem
            {
                ProduitId = produit.Id,
                NomProduit = produit.Name,
                PrixUnitaire = (decimal)produit.Price,
                Quantite = quantite
            });

            TempData["Message"] = "Produit ajouté au panier.";
            return RedirectToAction("Index");
        }

        public IActionResult MonPanier()
        {
            var panier = _panier.GetPanier();
            return View(panier);
        }

        public async Task<IActionResult> Index()
        {
            var produits = await _stockService.GetProduitsAsync();
            return View(produits);
        }

        public async Task<IActionResult> Details(int id)
        {
            var produits = await _stockService.GetProduitsAsync();
            var produit = produits.FirstOrDefault(p=>p.Id==id);
            return View(produit);
        }

        [Authorize]
        [HttpPost]
        public IActionResult ValiderCommande()
        {
            var panier = _panier.GetPanier();
            if (!panier.Any()) return BadRequest("Panier vide");

            var commande = new Commande
            {
                ClientId = User.FindFirst(ClaimTypes.NameIdentifier).Value,
                DateCommande = DateTime.UtcNow,
                Statut = "EnCours",
                LignesCommande = panier.Select(p => new LigneCommande
                {
                    ProduitId = p.ProduitId,
                    Quantite = p.Quantite,
                    PrixUnitaire = p.PrixUnitaire
                }).ToList()
            };

            _boutiqueService.AddCommandeAsync(commande);

            _panier.ViderPanier();
            TempData["Message"] = "Commande enregistrée avec succès.";

            return RedirectToAction("Index");
        }

    }
}
