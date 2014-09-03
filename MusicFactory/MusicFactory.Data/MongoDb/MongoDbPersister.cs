namespace MusicFactory.Data.MongoDb
{
    using MusicFactory.Models;
    using MongoDB.Driver;
    using System.Collections.Generic;
    using System;
    using System.Linq;
    using MusicFactory.Models.MongoDbProjections;

    public class MongoDbPersister
    {
        private const string MongoDbConnectionString = "mongodb://localhost";
        private const string DefaultDatabaseName = "Albums";

        private MongoClient Client { get; set; }

        private MongoServer Server { get; set; }

        private MongoDatabase Database { get; set; }

        public MongoDbPersister() : this(new MongoClient(MongoDbPersister.MongoDbConnectionString), MongoDbPersister.DefaultDatabaseName)
        {
        }

        public MongoDbPersister(MongoClient client, string databaseName)
        {
            this.Client = client;
            this.Server = this.Client.GetServer();
            this.Database = this.Server.GetDatabase(databaseName);
        }

        /// <summary>
        /// Method for generating dummy data
        /// </summary>
        public void SaveDummyData()
        {
            this.Database.DropCollection("albums");
            this.Database.CreateCollection("albums");

            var collection = this.Database.GetCollection<AlbumMongoDbProjection>("albums");
           

            var albums = MongoDbAlbumDataGenarator.GenerateAlbums();

            collection.InsertBatch(albums);
        }

        public AlbumMongoDbProjection GetSingleAlbum()
        {
            var collection = this.Database.GetCollection<AlbumMongoDbProjection>("albums");

            return collection.FindOne();
        }

        public ICollection<AlbumMongoDbProjection> GetAllAlbums()
        {
            var collection = this.Database.GetCollection<AlbumMongoDbProjection>("albums");

            return collection.FindAllAs<AlbumMongoDbProjection>().ToList();
        }
    }
}