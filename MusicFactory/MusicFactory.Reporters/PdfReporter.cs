namespace MusicFactory.Reporters
{
    using Contracts;
    using iTextSharp.text;
    using iTextSharp.text.pdf;
    using System;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.IO;

    public class PdfReporter : IReporter
    {
        public void GenerateReport()
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
                Paragraph p = new Paragraph("Product reports");
                p.Alignment = 1;
                doc.Add(p);

                doc.Add(GenerateTableFromData());

                Console.WriteLine("PDF report has been successfully generated");
            }
        }

        private IElement GenerateTableFromData()
        {
            string[] col = {"ID", "Name", "Title"};
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

            string connectionString = "Server=LYUBENPC; " +
            "Database=Northwind; Integrated Security=true";

            SqlConnection dbConnection = new SqlConnection(connectionString);

            dbConnection.Open();

            using (dbConnection)
            {
                SqlCommand employeesCommand = new SqlCommand("SELECT TOP 10 EmployeeId AS ID, FirstName + ' ' + LastName AS [Full Name], Title FROM Employees", dbConnection);

                SqlDataReader employeesReader = employeesCommand.ExecuteReader();

                while (employeesReader.Read())
                {
                    table.AddCell(((int)employeesReader["ID"]).ToString());
                    table.AddCell((string)employeesReader["Full Name"]);
                    table.AddCell((string)employeesReader["Title"]);
                }
            }

            return table;
        }
    }
}
