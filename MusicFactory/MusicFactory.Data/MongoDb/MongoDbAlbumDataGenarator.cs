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

            var song1 = new SongMongoDbProjection() { Title = "SOong1", Duration = 3, GenreName = "RNB" };
            var song2 = new SongMongoDbProjection() { Title = "SOon2", Duration = 31, GenreName = "Rock" };
            var song3 = new SongMongoDbProjection() { Title = "SOong33333333", Duration = 72, GenreName = "Rap" };
            List<SongMongoDbProjection> songs = new List<SongMongoDbProjection> { song1, song2, song3 };
            
            var album =
                new AlbumMongoDbProjection() { AlbumTitle = "Rap god!", ReleaseDate = DateTime.Now, AlbumPrice = 5.2m, ArtistName = "Rihanna", Songs = songs, ArtistLabel = "Virginia" };

            return albums;
        }
    }
}