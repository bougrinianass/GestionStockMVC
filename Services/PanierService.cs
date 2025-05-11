using AppGestionStockMVC.Models;
using System.Text.Json;

namespace AppGestionStockMVC.Services
{
        public class PanierService
        {
            private readonly IHttpContextAccessor _httpContextAccessor;
            private const string PanierKey = "Panier";

            public PanierService(IHttpContextAccessor httpContextAccessor)
            {
                _httpContextAccessor = httpContextAccessor;
            }

            private ISession Session => _httpContextAccessor.HttpContext.Session;

            public List<PanierItem> GetPanier()
            {
                var data = Session.GetString(PanierKey);
            return string.IsNullOrEmpty(data) ? new List<PanierItem>() : JsonSerializer.Deserialize<List<PanierItem>>(data);
            }

            public void AjouterAuPanier(PanierItem item)
            {
                var panier = GetPanier();
                var existant = panier.FirstOrDefault(p => p.ProduitId == item.ProduitId);
                if (existant != null)
                {
                    existant.Quantite += item.Quantite;
                }
                else
                {
                    panier.Add(item);
                }
                SavePanier(panier);
            }

            public void ViderPanier()
            {
                Session.Remove(PanierKey);
            }

            private void SavePanier(List<PanierItem> panier)
            {
                var data = JsonSerializer.Serialize(panier);
                Session.SetString(PanierKey, data);
            }
        }

    }



