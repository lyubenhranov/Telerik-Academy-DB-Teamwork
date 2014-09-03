namespace MusicFactory.Engine
{
    using MusicFactory.Data;
    using MusicFactory.Models;
    using MusicFactory.Reporters;
    using System;
    using System.Collections.Generic;

    class EntryPoint
    {
        static void Main()
        {
            // SASHO   
            // Fill in MongoDB database with data

            // Insert the data to SQL Server

            // Read the data from Excel and transfer it to the SQL Server

            // LYUBO
            // Generate PDF and XML reports from SQL Server


            // IVCHO
            // Generate reports from SQL Server

            // Create the MySQL database

            // Put the reports in JSON and MySQL

            //IVETO
            // Read XML data

            // Transfer the data to SQL Server and MongoDB

            // SISI
            // Get the reports from MySQL

            // Get the additional data from SQLite

            // Save the report to Excel 2007
            //var musicFactoryContext = new MusicFactoryDbContext();

            // LYUBO
            // Console interface for reports

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