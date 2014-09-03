﻿namespace MusicFactory.Engine
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

            // Read the data from Excel and transfer it to the SQL Server
            flowHandler.TransferDataFromExcelToSqlServer();

            // LYUBO
            // Generate PDF and XML reports from SQL Server
            flowHandler.GeneratePdfReportForYear(2010);
            flowHandler.GenerateXmlReportForYear(2010);

            // IVCHO
            // Create the MySQL database
            flowHandler.CreateMySqlDatabase();

            // Generate reports from SQL Server AND Put the reports in JSON and MySQL
            flowHandler.TransferReportsToMySqlAndJson();

            //IVETO
            // Read XML data AND Transfer the data to SQL Server and MongoDB
            flowHandler.TransferXmlDataToMongoAndSqlServer();

            // SISI
            // Get the reports from MySQL AND Get the additional data from SQLite AND Save the report to Excel 2007

            flowHandler.SaveReportsFromSqliteAndMySqlToExcel();

            // LYUBO
            // Console interface for reports
<<<<<<< HEAD
            flowHandler.HandleUserInput();
=======

            //var musicFactoryContext = new MusicFactoryDbContext();
            //var store = new Store();
            //var order = new Order() { OrderDate = DateTime.Now.AddDays(-2), Price = 5, Store = store};
            //var order2 = new Order() { OrderDate = DateTime.Now.AddDays(2), Price = 5 };
            //var order3 = new Order() { OrderDate = DateTime.Now.AddDays(-2), Price = 2 };
            //var order4 = new Order() { OrderDate = DateTime.Now.AddDays(-2), Price = 1 };
            //var orders = new List<Order> { order, order2, order3, order4 };

            //musicFactoryContext.Orders.Add(order);
            //musicFactoryContext.SaveChanges();

            //var pers = new ExcelPersister();
            //pers.ExploreDirectory();

            //var pdfReporter = new PdfReporter();
            //pdfReporter.GenerateReport();
            //var xmlReporter = new XmlReporter();
            //xmlReporter.GenerateReport();
>>>>>>> origin/master
        }
    }
}