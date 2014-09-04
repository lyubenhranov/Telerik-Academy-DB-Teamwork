namespace MusicFactory.Engine
{
    using System;

    class EntryPoint
    {
        static void Main()
        {
            var flowHandler = new WorkflowMediator();

            flowHandler.FillMongoDbWithData();
            Console.WriteLine("MongoDB database has been created and filled with data");

            flowHandler.TransferDataFromMongoToSqlServer();
            Console.WriteLine("MongoDB data has been transferred to the SQL Server");

            flowHandler.TransferXmlDataToMongoAndSqlServer();
            Console.WriteLine("XML data has been transferred to the SQL Server");

            flowHandler.TransferDataFromExcelToSqlServer();
            Console.WriteLine("Excel orders have been transferred to the SQL Server");

            //flowHandler.GeneratePdfReportForYear(2013, "2013-Artists-Sales-Report");
            Console.WriteLine("PDF report has been successfully generated");

            //flowHandler.GenerateXmlReportForYear(2013, "2013-Artists-Sales-Report");
            Console.WriteLine("XML report has been successfully generated");

            flowHandler.TransferReportsJson();
            Console.WriteLine("Sales reports from SQL Server have been transferred to JSON files");

            flowHandler.TransferReportToMySql();
            Console.WriteLine("Sales reports from SQL Server have been transferred to MySQL and JSON files");
            
            flowHandler.SaveReportsFromSqliteAndMySqlToExcel();
            Console.WriteLine("Combined reports from MySQL and SQLite have been transferred to an Excel 2007 file");
        }
    }
}