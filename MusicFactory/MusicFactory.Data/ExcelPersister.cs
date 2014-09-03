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

    public class ExcelPersister
    {
        public const string ReportFileLocation = "../../";
        public const string ReportFileName = "Sales reports.zip";
        public const string TempFileLocation = "temp/";

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
                    //Console.WriteLine(subFolder);
                    ParseFile(subFolder, folderDate);
                }
            }
        }

        public ICollection<Order> ParseFile(string path, DateTime date)
        {


            var orders = new List<Order>();
            var fileName = path.Substring(path.LastIndexOf('\\') + 1);
            var locationName = fileName.Substring(0, fileName.IndexOf("Sales") - 1);
            locationName = locationName.Replace('-', ' ');

            var connection = new OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0;Data Source='" + path + "';Extended Properties=Excel 8.0;");
            connection.Open();
            using (connection)
            {
                var command = new OleDbDataAdapter("SELECT * FROM [Sales$]", connection);
                var table = new DataTable();
                command.Fill(table);

                Console.WriteLine(table.Rows[1].ItemArray[0]);
                for (int i = 2; i < table.Rows.Count; i++)
                {

                }

                //foreach (DataRow row in table.Rows)
                //{

                //    var albumId = (Guid)row["AlbumId"];
                //    var quantity = (int)row["Quantity"];
                //    var price = (decimal)row["Price"];
                //    var total = (decimal)row["Total"];

                    
                //    var order = new Order() { AlbumId = albumId, Price = price, Quantity = quantity, TotalSum = total, OrderDate = date,  };
                //}
            }
            Console.WriteLine(locationName);

            return orders;
        }
        
    }
}