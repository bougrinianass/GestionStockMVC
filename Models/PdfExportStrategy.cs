using AppGestionStockMVC.Models;
using iTextSharp.text.pdf;
using iTextSharp.text;
using OfficeOpenXml;


namespace AppGestionStockMVC.Models
{
    public class PdfExportStrategy : IExportStrategy
    {
        public string ContentType => "application/pdf";
        public string FileExtension => ".pdf";

        public byte[] Export(List<Produit> products)
        {
            using (var stream = new MemoryStream())
            {
                var document = new Document(PageSize.A4);
                PdfWriter.GetInstance(document, stream);
                document.Open();

                var titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16);
                var tableFont = FontFactory.GetFont(FontFactory.HELVETICA, 12);

                document.Add(new Paragraph("Liste des Produits", titleFont));
                document.Add(new Paragraph(" ")); // espace

                PdfPTable table = new PdfPTable(2); // 2 colonnes : Nom et Quantité
                table.AddCell(new Phrase("Nom", tableFont));
                table.AddCell(new Phrase("Quantité", tableFont));

                foreach (var product in products)
                {
                    table.AddCell(new Phrase(product.Name, tableFont));
                    table.AddCell(new Phrase(product.Quantity.ToString(), tableFont));
                }

                document.Add(table);
                document.Close();

                return stream.ToArray();
            }
        }
    }
}








