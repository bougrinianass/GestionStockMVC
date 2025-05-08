namespace AppGestionStockMVC.Models
{
    public interface IExportStrategy
    {
        byte[] Export(List<Produit> products);
        string ContentType { get; }
        string FileExtension { get; }
    }
}
