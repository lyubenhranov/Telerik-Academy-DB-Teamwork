namespace MusicFactory.Engine
{
    using MusicFactory.Data;
    using MusicFactory.Data.MongoDb;
    using MusicFactory.DataAccessModel_Sales;
    using MusicFactory.Models;
    using MusicFactory.Models.Repositories;
    using MusicFactory.Models.SQLite;
    using MusicFactory.Reporters;
    using MySql.Data.MySqlClient;
    using Newtonsoft.Json;
    using OfficeOpenXml;
    using System;
    using System.IO;
    using System.Linq;
    using System.Data.OleDb;
    using System.Data.SqlClient;

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
            excelPersister.ExtractFilesFromZip();
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

        public void TransferReportToMySql()
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
                    }
                }
            }
        }

        public void TransferReportsJson()
        {
            string connectionString = "Server=.; " +
            "Database=MusicFactory; Integrated Security=true";

            SqlConnection musicFactoryConnection = new SqlConnection(connectionString);

            musicFactoryConnection.Open();

            using (musicFactoryConnection)
            {
                string getAllSalesCommand = "SELECT countries.Name AS CountryName, SUM(orders.TotalSum) AS Sales, YEAR(orders.OrderDate) AS Year FROM Orders AS orders 	JOIN Stores AS stores ON stores.StoreId = orders.StoreID 	JOIN Addresses AS addresses ON addresses.AddressId = stores.AddressId 	JOIN Countries AS countries ON countries.CountryId = addresses.CountryId GROUP BY countries.Name, YEAR(orders.OrderDate)";

                SqlCommand getSalesCommand = new SqlCommand(getAllSalesCommand, musicFactoryConnection);

                string currentCountryName = "";
                decimal currentSales;
                int currentYear;

                SqlDataReader salesReader = getSalesCommand.ExecuteReader();

                while (salesReader.Read())
                {
                    currentCountryName = (string)salesReader["CountryName"];
                    currentSales = (decimal)salesReader["Sales"];
                    currentYear = (int)salesReader["Year"];

                    var salesObject = new
                    {
                        countryName = currentCountryName,
                        sales = currentSales,
                        year = currentYear
                    };

                    var serializedSalesObject = JsonConvert.SerializeObject(salesObject);
                    File.WriteAllText("..\\..\\..\\..\\Reports\\JSON\\" + currentCountryName + ".json", serializedSalesObject);
                }
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

            Console.WriteLine("SQLite database has been created and filled with data");

            var sqlLiteExpenses = sqliteRepository.GetExpensesData();

            var mySqlRepository = new MySqlRepository();
            var mySqlSales = mySqlRepository.GetExpensesData();

            string fileName = "Profit and Loss Report";
            
            // Create the Excel file after deleting any old report with the same name
            File.Delete("../../../../Reports/" + fileName + ".xlsx");

            FileInfo expensesFile = new FileInfo("../../../../Reports/" + fileName + ".xlsx");

            using (ExcelPackage excelPackage = new ExcelPackage(expensesFile))
            {
                ExcelWorksheet profitAndLossSheet = excelPackage.Workbook.Worksheets.Add("ProfitAndLoss");

                profitAndLossSheet.Cell(1, 1).Value = "CountryName";
                profitAndLossSheet.Cell(1, 2).Value = "Year";
                profitAndLossSheet.Cell(1, 3).Value = "Sales";
                profitAndLossSheet.Cell(1, 4).Value = "Expenses";
                profitAndLossSheet.Cell(1, 5).Value = "Profit";

                string countryName;
                int year;
                decimal sales;
                decimal expenses;
                int currentRow = 2;

                foreach(var salesRecord in mySqlSales)
                {
                    countryName = salesRecord.CountryName;
                    year = salesRecord.Year;
                    sales = salesRecord.Sales;
                    expenses = sqlLiteExpenses.Where(record => record.CountryName == countryName && record.Year == year).Select(record => record.Expenses).First();

                    profitAndLossSheet.Cell(currentRow, 1).Value = countryName.ToString();
                    profitAndLossSheet.Cell(currentRow, 2).Value = year.ToString();
                    profitAndLossSheet.Cell(currentRow, 3).Value = sales.ToString();
                    profitAndLossSheet.Cell(currentRow, 4).Value = expenses.ToString();
                    profitAndLossSheet.Cell(currentRow, 5).Value = (sales - expenses).ToString();

                    currentRow++;
                }           

                excelPackage.Save();
            }
        }

        private void CreateNewExcelFile(string fileName)
        {
            File.Delete("../../../../Reports/" + fileName + ".xlsx");

            FileInfo expensesFile = new FileInfo("../../../../Reports/" + fileName + ".xlsx");

            using (ExcelPackage excelPackage = new ExcelPackage(expensesFile))
            {
                ExcelWorksheet profitAndLossSheet = excelPackage.Workbook.Worksheets.Add("ProfitAndLoss");

                profitAndLossSheet.Cell(1, 1).Value = "CountryName";
                profitAndLossSheet.Cell(1, 2).Value = "Year";
                profitAndLossSheet.Cell(1, 3).Value = "Sales";
                profitAndLossSheet.Cell(1, 4).Value = "Expenses";
                profitAndLossSheet.Cell(1, 5).Value = "Profit";

                excelPackage.Save();
            }
        }
    }
}
