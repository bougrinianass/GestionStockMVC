using AppGestionStockMVC.Models;

namespace AppGestionStockMVC.ViewModel
{
    public class CommandeDetails
    {
        public IEnumerable<Commande> commandes;
        public IEnumerable<LigneCommande> detailsCommande;

    }
}
