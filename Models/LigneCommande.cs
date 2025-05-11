namespace AppGestionStockMVC.Models
{
    public class LigneCommande
    {
        public int Id { get; set; }

        public int CommandeId { get; set; }
        public virtual Commande ?Commande { get; set; }

        public int ProduitId { get; set; }
        public virtual Produit ?Produit { get; set; }

        public int Quantite { get; set; }
        public decimal PrixUnitaire { get; set; }
    }
}
