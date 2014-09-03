namespace MusicFactory.Engine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using MusicFactory.Reporters;
    using MusicFactory.Models.SQLite;
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

        public void GeneratePdfReportForYear(int year)
        {
            var pdfReporter = new PdfReporter();
            pdfReporter.GenerateReport(year);
        }

        public void GenerateXmlReportForYear(int year)
        {
            var xmlReporter = new XmlReporter();
            xmlReporter.GenerateReport(year);
        }

        public void CreateMySqlDatabase()
        {

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

            sqliteRepository.CreateDatabase();
            sqliteRepository.FillDatabaseWithData();
        }

        public void HandleUserInput()
        {

        }
    }
}
