namespace MusicFactory.Reporters
{
    using Contracts;
    using iTextSharp.text;
    using iTextSharp.text.pdf;
    using System;
    using System.IO;

    public class PdfReporter : IReporter
    {
        public void GenerateReport()
        {
            using (Document doc = new Document())
            {
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream("report.pdf", FileMode.Create));

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
                Paragraph p = new Paragraph("Product reports");
                p.Alignment = 1;
                doc.Add(p);

                doc.Add(GenerateTableFromData());
            }
        }

        private IElement GenerateTableFromData()
        {
            string[] col = {"No", "Name", "City"};
            PdfPTable table = new PdfPTable(3);

            table.WidthPercentage = 100;

            table.SetWidths(new Single[] { 1, 5, 4 });

            table.SpacingBefore = 10;

            for (int i = 0; i < col.Length; i++)
            {
                PdfPCell cell = new PdfPCell(new Phrase(col[i]));
                cell.BackgroundColor = new BaseColor(42, 212, 255);
                table.AddCell(cell);
            }

            for (int i = 0; i < 2; i++)
            {
                table.AddCell("Number" + i);
                table.AddCell("Name" + i);
                table.AddCell("City" + i);
            }

            return table;
        }
    }
}
