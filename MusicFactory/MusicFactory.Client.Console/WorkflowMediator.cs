namespace MusicFactory.Engine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using MusicFactory.Reporters;
    using MusicFactory.Models.SQLite;
    using MySql.Data.MySqlClient;
    using System.Text;
    using System.Threading.Tasks;

    public class WorkflowMediator
    {
        public void FillMongoDbWithData()
        {

        }

        public void TransferDataFromMongoToSqlServer()
        {

        }

        public void TransferDataFromExcelToSqlServer()
        {

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
            }
        }

        public void TransferReportsToMySqlAndJson()
        {

        }

        public void TransferXmlDataToMongoAndSqlServer()
        {

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
