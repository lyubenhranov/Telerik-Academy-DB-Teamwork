namespace MusicFactory.Engine
{
    using MusicFactory.Data;
    using MusicFactory.Models;
    using MusicFactory.Reporters;

    class EntryPoint
    {
        static void Main()


        {

            //var album = new Album();
            //MusicFactoryDbContext db = new MusicFactoryDbContext();

            //db.Albums.Add(album);


            var persister = new MongoDbPersister();

            persister.SaveData();
            var album = persister.GetSingleAlbum();

            MongoDbToSqlServerTransferer transferer = new MongoDbToSqlServerTransferer();

            transferer.TransferSingleRecord();

            //var pdfReporter = new PdfReporter();

            //pdfReporter.GenerateReport();

            //var xmlReporter = new XmlReporter();

            //xmlReporter.GenerateReport();
        }
    }
}