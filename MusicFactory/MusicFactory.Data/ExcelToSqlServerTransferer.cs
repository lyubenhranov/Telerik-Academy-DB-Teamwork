namespace MusicFactory.Data
{
    using System;
    using System.Data.OleDb;
    using System.IO;
    using System.Linq;
    using Ionic.Zip;
    using System.Globalization;
    using MusicFactory.Models;
    using System.Collections.Generic;
    using System.Data;
    using MusicFactory.Data;
    using System.Data.Entity;

    public class ExcelToSqlServerTransferer
    {
        public const string ReportFileLocation = "../../";
        public const string ReportFileName = "Sales reports.zip";
        public const string TempFileLocation = "temp/";

        private MusicFactoryDbContext DbContext { get; set; }

        public ExcelToSqlServerTransferer()
            : this(new MusicFactoryDbContext())
        {
        }

        public ExcelToSqlServerTransferer(MusicFactoryDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        public void ExtractFilesFromZip()
        {
            using (ZipFile zip = ZipFile.Read(ReportFileLocation + ReportFileName))
            {
                foreach (ZipEntry e in zip)
                {
                    e.Extract(TempFileLocation);
                }
            }
        }

        public void ExploreDirectory()
        {
            foreach (var folder in Directory.GetDirectories("temp"))
            {
                var folderDate = DateTime.ParseExact(folder.Substring(5), "dd-MMM-yyyy", CultureInfo.InvariantCulture);

                foreach (var subFolder in Directory.GetFiles(folder))
                {
                    var orders = ParseFile(subFolder, folderDate);

                    foreach (var ord in orders)
                    {
                        this.DbContext.Orders.Add(ord);
                    }
                    this.DbContext.SaveChanges();
                }
            }
        }

        public ICollection<Order> ParseFile(string path, DateTime date)
        {
            var orders = new List<Order>();
            var fileName = path.Substring(path.LastIndexOf('\\') + 1);
            var storeName = fileName.Substring(0, fileName.IndexOf("Sales") - 1);
            storeName = storeName.Replace('-', ' ');

            Store store = this.DbContext.Stores.FirstOrDefault();

            var connection = new OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0;Data Source='" + path + "';Extended Properties=Excel 8.0;");
            connection.Open();
            using (connection)
            {
                var command = new OleDbDataAdapter("SELECT * FROM [Sales$]", connection);
                var table = new DataTable();
                command.Fill(table);

                var rows = table.Rows;
                for (int i = 1; i < rows.Count; i++)
                {
                    var albumId = int.Parse(rows[i][0].ToString());
                    var quantity = int.Parse(rows[i][1].ToString());
                    var price = decimal.Parse(rows[i][2].ToString());
                    var total = decimal.Parse(rows[i][3].ToString());

                    var order = new Order() { AlbumId = albumId, Price = price, Quantity = quantity, TotalSum = total, OrderDate = date, Store = store };
                    orders.Add(order);
                }
            }

            return orders;
        }
    }
}