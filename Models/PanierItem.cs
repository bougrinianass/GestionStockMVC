namespace AppGestionStockMVC.Models
{
    public class PanierItem
    {
        public int ProduitId { get; set; }
        public string NomProduit { get; set; }
        public decimal PrixUnitaire { get; set; }
        public int Quantite { get; set; }
    }
}
