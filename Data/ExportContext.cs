using AppGestionStockMVC.Models;

namespace AppGestionStockMVC.Data
{
    public class ExportContext
    {
        private IExportStrategy _strategy;

        public ExportContext(IExportStrategy strategy)
        {
            _strategy = strategy;
        }

        public byte[] Export(List<Produit> products)
        {
            return _strategy.Export(products);
        }

        public string ContentType => _strategy.ContentType;
        public string FileExtension => _strategy.FileExtension;
    }

}
