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
        public override void GenerateReport(int year)
        {
            using (Document doc = new Document())
            {
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(@"..\..\..\Reports\report.pdf", FileMode.Create));

                doc.Open();

                Rectangle page = doc.PageSize;

                PdfPTable head = new PdfPTable(1);

                head.TotalWidth = page.Width;

                Phrase phrase = new Phrase(DateTime.UtcNow.ToShortTimeString(), new Font(Font.FontFamily.COURIER, 12));

                PdfPCell c = new PdfPCell(phrase);
                c.Border = Rectangle.NO_BORDER;
                c.VerticalAlignment = Element.ALIGN_TOP;
                c.HorizontalAlignment = Element.ALIGN_CENTER;

                head.AddCell(c);

                head.WriteSelectedRows(
                    // first/last row; -1 writes all rows 
                    0, -1,
                    // left offset
                    0,
                    // ** bottom** yPos of the table
                    page.Height - doc.TopMargin + head.TotalHeight + 20,
                    writer.DirectContent);

                // Table heading
                Paragraph p = new Paragraph(String.Format("Sales by Artists for Year {0}", year));
                p.Alignment = 1;
                doc.Add(p);

                doc.Add(GeneratePdfTableFromData(year));

                Console.WriteLine("PDF report has been successfully generated");
            }
        }

        private IElement GeneratePdfTableFromData(int year)
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

            string connectionString = "Server=LYUBENPC; " +
            "Database=MusicFactoryTest; Integrated Security=true";

            SqlConnection dbConnection = new SqlConnection(connectionString);

            dbConnection.Open();

            using (dbConnection)
            {
                SqlCommand salesByArtistCommand = new SqlCommand("SELECT artists.Name, SUM(orders.TotalSum) AS [Sales] FROM Orders AS orders JOIN Albums AS albums ON orders.AlbumID = albums.AlbumID	JOIN Artists AS artists ON albums.ArtistID = artists.ArtistID WHERE YEAR(orders.OrderDate) = @year GROUP BY artists.Name ORDER BY artists.Name", dbConnection);

                salesByArtistCommand.Parameters.AddWithValue("@year", year);

                SqlDataReader salesByArtistReader = salesByArtistCommand.ExecuteReader();

                while (salesByArtistReader.Read())
                {
                    table.AddCell((string)salesByArtistReader["Name"]);
                    table.AddCell(((decimal)salesByArtistReader["Sales"]).ToString());
                }
            }

            return table;
        }
    }
}
