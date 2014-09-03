namespace MusicFactory.Reporters
{
    using Templates;
    using iTextSharp.text;
    using iTextSharp.text.pdf;
    using System;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.IO;

    public class PdfReporter : SalesReporter
    {
        private Document pdfDocument = new Document();

        public override void GenerateReport(int year, string fileName)
        {
            this.pdfDocument = CreatePdfDocument(year, fileName);

            SqlConnection musicFactoryDbConnection = this.GetDatabaseConnection();

            this.TransferDataToFile(year, fileName, musicFactoryDbConnection);

            Console.WriteLine("PDF report has been successfully generated");
        }

        private Document CreatePdfDocument(int year, string fileName)
        {
            Document pdfDocument = new Document();

            PdfWriter writer = PdfWriter.GetInstance(pdfDocument, new FileStream(System.Configuration.ConfigurationManager.AppSettings["ReportsFolderPath"] + fileName + ".pdf", FileMode.Create));

            pdfDocument.Open();

            Rectangle page = pdfDocument.PageSize;

            PdfPTable head = new PdfPTable(1);

            head.TotalWidth = page.Width;

            Phrase phrase = new Phrase(DateTime.UtcNow.ToShortTimeString(), new Font(Font.FontFamily.COURIER, 12));

            PdfPCell cell = new PdfPCell(phrase);
            cell.Border = Rectangle.NO_BORDER;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;

            head.AddCell(cell);

            head.WriteSelectedRows(
                // first/last row; -1 writes all rows 
                0, -1,
                // left offset
                0,
                // ** bottom** yPos of the table
                page.Height - pdfDocument.TopMargin + head.TotalHeight + 20,
                writer.DirectContent);

            // Table heading
            Paragraph pageTitle = new Paragraph(String.Format("Sales by Artists for Year {0}", year));
            pageTitle.Alignment = 1;
            pdfDocument.Add(pageTitle);

            return pdfDocument;
        }

        private PdfPTable GeneratePdfSalesTable()
        {
            string[] columnTitles = {"Artist Name", "Sales"};
            PdfPTable table = new PdfPTable(2);

            table.WidthPercentage = 100;

            table.SetWidths(new Single[] { 5, 1 });

            table.SpacingBefore = 10;

            for (int i = 0; i < columnTitles.Length; i++)
            {
                PdfPCell cell = new PdfPCell(new Phrase(columnTitles[i]));
                cell.BackgroundColor = new BaseColor(42, 212, 255);
                table.AddCell(cell);
            }

            return table;
        }

        protected override void TransferDataToFile(int year, string fileName, SqlConnection musicFactoryDbConnection)
        {
            var table = this.GeneratePdfSalesTable();

            musicFactoryDbConnection.Open();

            using (musicFactoryDbConnection)
            {
                SqlCommand salesByArtistCommand = this.GetSqlCommand(year, musicFactoryDbConnection);

                SqlDataReader salesByArtistReader = salesByArtistCommand.ExecuteReader();

                while (salesByArtistReader.Read())
                {
                    table.AddCell((string)salesByArtistReader["Name"]);
                    table.AddCell(((decimal)salesByArtistReader["Sales"]).ToString());
                }
            }

            using (pdfDocument)
            {
                this.pdfDocument.Add(table);
            }
        }
    }
}
