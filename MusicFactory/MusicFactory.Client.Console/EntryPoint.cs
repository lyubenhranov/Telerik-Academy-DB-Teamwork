namespace MusicFactory.Engine
{
    class EntryPoint
    {
        static void Main()
        {
            var flowHandler = new WorkflowMediator();

            // SASHO   
            // Fill in MongoDB database with data
            flowHandler.FillMongoDbWithData();

            // Insert the data to SQL Server
            flowHandler.TransferDataFromMongoToSqlServer();

            //IVETO
            // Read XML data AND Transfer the data to SQL Server and MongoDB
            flowHandler.TransferXmlDataToMongoAndSqlServer();

            // Read the data from Excel and transfer it to the SQL Server
            flowHandler.TransferDataFromExcelToSqlServer();

            // LYUBO
            // Generate PDF and XML reports from SQL Server
            flowHandler.GeneratePdfReportForYear(2014, "2014-Artists-Sales-Report");
            flowHandler.GenerateXmlReportForYear(2014, "2014-Artists-Sales-Report");

            // IVCHO
            // Create the MySQL database
            flowHandler.CreateMySqlDatabase();

            // Generate reports from SQL Server AND Put the reports in JSON and MySQL
            flowHandler.TransferReportsToMySqlAndJson();

            

            // SISI
            // Get the reports from MySQL AND Get the additional data from SQLite AND Save the report to Excel 2007

            // DIVIDED THE METHOD INTO 2 METHODS BECAUSE WE HAVE TO SEEK FOR SIMPLICITY
            flowHandler.SaveReportsFromSqliteToExcel();
            flowHandler.SaveReportsFromMySqlToExcel();

            // LYUBO
            // Console interface for reports
            flowHandler.HandleUserInput();
        }
    }
}