using OfficeOpenXml;


namespace AppGestionStockMVC.Models
{
    public class ExcelExportStrategy : IExportStrategy
    {
        public string ContentType => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        public string FileExtension => ".xlsx";

        [Obsolete]
        public byte[] Export(List<Produit> products)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;


            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Produits");
                worksheet.Cells[1, 1].Value = "Nom";
                worksheet.Cells[1, 2].Value = "Quantité";

                for (int i = 0; i < products.Count; i++)
                {
                    worksheet.Cells[i + 2, 1].Value = products[i].Name;
                    worksheet.Cells[i + 2, 2].Value = products[i].Quantity;
                }

                return package.GetAsByteArray();
            }
        }
    }
}
