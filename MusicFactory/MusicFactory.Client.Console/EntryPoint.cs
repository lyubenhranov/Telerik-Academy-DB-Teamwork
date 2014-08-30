namespace MusicFactory.Engine
{
    using MusicFactory.Models;
    using MusicFactory.Reporters;

    class EntryPoint
    {
        static void Main()
        {
            var pdfReporter = new PdfReporter();

            pdfReporter.GenerateReport();

            var xmlReporter = new XmlReporter();

            xmlReporter.GenerateReport();
        }
    }
}