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
            var order = new Order() { OrderDate = DateTime.Now.AddDays(-2), Price = 5 };
            var order2 = new Order() { OrderDate = DateTime.Now.AddDays(2), Price = 5 };
            var order3 = new Order() { OrderDate = DateTime.Now.AddDays(-2), Price = 2 };
            var order4 = new Order() { OrderDate = DateTime.Now.AddDays(-2), Price = 1 };
            var orders = new List<Order> { order, order2, order3, order4 };
           // var pers = new ExcelPersister();


            //var pdfReporter = new PdfReporter();
            //pdfReporter.GenerateReport();
            //var xmlReporter = new XmlReporter();
            //xmlReporter.GenerateReport();
        }
    }
}