namespace MusicFactory.Data.MongoDb
{
    using MusicFactory.Models;
    using MusicFactory.Models.MongoDb;
    using MongoDB.Driver;
    using System.Collections.Generic;
    using System;
    using System.Linq;

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
        public void SaveData()
        {
           // this.Database.DropCollection("albums");
           // this.Database.CreateCollection("albums");

            var collection = this.Database.GetCollection<AlbumMongoDbProjection>("albums");
            
            var song1 = new SongMongoDbProjection() { Title = "SOong1", Duration = 3, GenreName = "RNB" };
            var song2 = new SongMongoDbProjection() { Title = "SOon2", Duration = 31, GenreName = "Rock" };
            var song3 = new SongMongoDbProjection() { Title = "SOong33333333", Duration = 72, GenreName = "Rap" };
            List<SongMongoDbProjection> songs = new List<SongMongoDbProjection> { song1, song2, song3 };

            var album =
                new AlbumMongoDbProjection() { AlbumTitle = "Rap god!", ReleaseDate = DateTime.Now, AlbumPrice = 5.2m, ArtistName = "Rihanna", Songs = songs, ArtistLabel = "Virginia" };

            collection.Insert<AlbumMongoDbProjection>(album);
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