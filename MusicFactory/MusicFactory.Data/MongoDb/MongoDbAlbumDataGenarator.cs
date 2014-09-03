namespace MusicFactory.Data.MongoDb
{
    using MusicFactory.Models.MongoDbProjections;
    using System;
    using System.Collections.Generic;

    public class MongoDbAlbumDataGenarator
    {
        public ICollection<AlbumMongoDbProjection> GenerateData()
        {
            var albums = new List<AlbumMongoDbProjection>();
            
            var album =
                new AlbumMongoDbProjection() { AlbumTitle = "Music of the sun", ReleaseDate = DateTime.Now,  ArtistName = "Rihanna", ArtistLabel = "Virginia" };
            var album2 =
                new AlbumMongoDbProjection() { AlbumTitle = "Rap god!", ReleaseDate = DateTime.Now,  ArtistName = "Eminem", ArtistLabel = "Universal" };

            var album3 =
                new AlbumMongoDbProjection() { AlbumTitle = "Rap god!", ReleaseDate = DateTime.Now,  ArtistName = "Rihanna", ArtistLabel = "Virginia" };

            return albums;
        }
    }
}