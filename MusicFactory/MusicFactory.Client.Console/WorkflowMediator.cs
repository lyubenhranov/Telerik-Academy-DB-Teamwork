namespace MusicFactory.Engine
{
    using MusicFactory.Data;
    using MusicFactory.Data.MongoDb;
    using MusicFactory.DataAccessModel_Sales;
    using MusicFactory.Models;
    using MusicFactory.Models.SQLite;
    using MusicFactory.Reporters;
    using MySql.Data.MySqlClient;
    using Newtonsoft.Json;
    using System;
    using System.IO;
    using System.Linq;

    public class WorkflowMediator
    {
        public void FillMongoDbWithData()
        {
            var mongodbPersister = new MongoDbPersister();
            mongodbPersister.SaveDummyData();
        }

        public void TransferDataFromMongoToSqlServer()
        {
            var transferer = new MongoDbToSqlServerTransferer();
            transferer.TransferAllRecords();
        }

        public void TransferDataFromExcelToSqlServer()
        {
            var excelPersister = new ExcelToSqlServerTransferer();
            excelPersister.ExploreDirectory();
        }

        public void GeneratePdfReportForYear(int year, string fileName)
        {
            var pdfReporter = new PdfReporter();
            pdfReporter.GenerateReport(year, fileName);
        }

        public void GenerateXmlReportForYear(int year, string fileName)
        {
            var xmlReporter = new XmlReporter();
            xmlReporter.GenerateReport(year, fileName);
        }

        public void CreateMySqlDatabase()
        {
            string connectionString = "Server=localhost;Port=3306;Database=;Uid=root;Pwd=;";

            MySqlConnection musicFactoryDatabaseContext = new MySqlConnection(connectionString);

            musicFactoryDatabaseContext.Open();

            using (musicFactoryDatabaseContext)
            {
                MySqlCommand insertBookCommand = new MySqlCommand("CREATE DATABASE  IF NOT EXISTS `musicfactory`; USE `musicfactory`;  DROP TABLE IF EXISTS `salesbycountry`; /*!40101 SET @saved_cs_client     = @@character_set_client */; /*!40101 SET character_set_client = utf8 */; CREATE TABLE `salesbycountry` (   `CountryName` varchar(100) NOT NULL,   `Sales` decimal(10,0) NOT NULL,   `Year` int(11) NOT NULL,   PRIMARY KEY (`CountryName`) ) ENGINE=InnoDB DEFAULT CHARSET=latin1;", musicFactoryDatabaseContext);

                insertBookCommand.ExecuteNonQuery();

                EntitiesModel dbContext = new EntitiesModel();

                using (dbContext)
                {
                    string[] allPaths = Directory.GetFiles("..\\..\\..\\..\\Reports\\JSON\\");

                    foreach (var path in allPaths)
                    {
                        string[] report = File.ReadAllLines(path);

                        var parsedReport = JsonConvert.DeserializeObject<Salesbycountry>(report[0]);

                        Salesbycountry salesTable = new Salesbycountry();
                        salesTable.CountryName = parsedReport.CountryName;
                        salesTable.Sales = parsedReport.Sales;
                        salesTable.Year = parsedReport.Year;

                        dbContext.Add(salesTable);

                        dbContext.SaveChanges();

                        Console.WriteLine("Data has been loaded to MySQL successfully");
                    }
                }
            }
        }

        public void TransferReportsToMySqlAndJson()
        {
            MusicFactoryDbContext context = new MusicFactoryDbContext();

            using (context)
            {
                var allData = from order in context.Orders
                              join store in context.Stores on order.StoreId equals store.StoreId
                              join address in context.Addresses on store.AddressId equals address.AddressId
                              join country in context.Countries on address.CountryId equals country.CountryId
                              select new
                              {
                                  countryName = country.Name,
                                  countryId = country.CountryId,
                                  orderTotalSales = order.TotalSum,
                                  orderDate = order.OrderDate
                              };

                var groupedData = allData.GroupBy(a => a.countryName);

                foreach (var eachGroup in groupedData)
                {
                    foreach (var data in eachGroup)
                    {
                        var salesObject = new
                        {
                            countryName = data.countryName,
                            sales = data.orderTotalSales,
                            year = data.orderDate.Year
                        };

                        var serializedSalesObject = JsonConvert.SerializeObject(salesObject);
                        File.WriteAllText("..\\..\\..\\..\\Reports\\JSON\\" + data.countryId + ".json", serializedSalesObject);
                    }
                }

                Console.WriteLine("JSON files have been generated successfully");
            }
        }

        public void TransferXmlDataToMongoAndSqlServer()
        {
            var musicFactoryContext = new MusicFactoryDbContext();
            using (musicFactoryContext)
            {
                var xmlDataImporter = new XmlDataImporter(musicFactoryContext);
                xmlDataImporter.ImportDataFromXML();
            }
        }

        public void SaveReportsFromSqliteAndMySqlToExcel()
        {
            var sqliteRepository = new SQLiteRepository();

            sqliteRepository.CreateDatabase("additionalProductInfo");
            sqliteRepository.FillDatabaseWithData();
        }

        public void HandleUserInput()
        {
            //string currentCommand = Console.ReadLine();

            //while (currentCommand != "end")
            //{
            //    currentCommand = Console.ReadLine();
            //}
        }
    }
}
