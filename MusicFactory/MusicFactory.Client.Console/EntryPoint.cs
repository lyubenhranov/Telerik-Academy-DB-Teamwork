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
            flowHandler.HandleUserInput();

            //var musicFactoryContext = new MusicFactoryDbContext();
            //var country = new Country() { Name = "bulgaria" };
            //var address = new Address() { AddressText = "addr" ,Country = country};
            //var store = new Store() { Name = "Zmeyovo" ,Address = address};
            
            //musicFactoryContext.Stores.Add(store);
            //musicFactoryContext.SaveChanges();

            //var order = new Order() { OrderDate = DateTime.Now.AddDays(-2), Price = 5, Store = store};
            //var order2 = new Order() { OrderDate = DateTime.Now.AddDays(2), Price = 5 };
            //var order3 = new Order() { OrderDate = DateTime.Now.AddDays(-2), Price = 2 };
            //var order4 = new Order() { OrderDate = DateTime.Now.AddDays(-2), Price = 1 };
            //var orders = new List<Order> { order, order2, order3, order4 };

            //musicFactoryContext.Orders.Add(order);
            

            var pers = new ExcelToSqlServerTransferer();
            pers.ExploreDirectory();

            //var pdfReporter = new PdfReporter();
            //pdfReporter.GenerateReport();
            //var xmlReporter = new XmlReporter();
            //xmlReporter.GenerateReport();
        }
    }
}