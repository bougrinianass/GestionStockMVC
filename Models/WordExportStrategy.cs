using System.IO;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace AppGestionStockMVC.Models
{
    public class WordExportStrategy : IExportStrategy
    {
        public string ContentType => "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
        public string FileExtension => ".docx";

        public byte[] Export(List<Produit> products)
        {
            using (var memoryStream = new MemoryStream())
            {
                Xceed.Words.NET.Licenser.LicenseKey = "FREE-LICENSE-KEY";

                // Créer un nouveau document Word
                using (var document = DocX.Create(memoryStream))
                {
                    // Ajouter un titre au document
                    document.InsertParagraph("Liste des Produits")
                        .FontSize(16)
                        .Bold().Alignment = Alignment.center;

                    document.InsertParagraph("\n");

                    // Créer un tableau
                    var table = document.AddTable(products.Count + 1, 2); // +1 pour l'en-tête
                    table.SetWidths(new float[] { 200f, 100f }); // Définir la largeur des colonnes

                    // Ajouter l'en-tête
                    table.Rows[0].Cells[0].Paragraphs[0].Append("Nom").Bold();
                    table.Rows[0].Cells[1].Paragraphs[0].Append("Quantité").Bold();

                    // Remplir le tableau avec les produits
                    for (int i = 0; i < products.Count; i++)
                    {
                        table.Rows[i + 1].Cells[0].Paragraphs[0].Append(products[i].Name);
                        table.Rows[i + 1].Cells[1].Paragraphs[0].Append(products[i].Quantity.ToString());
                    }

                    document.InsertTable(table);

                    // Sauvegarder le fichier Word dans le MemoryStream
                    document.Save();
                }

                return memoryStream.ToArray();
            }
        }
    }

}
