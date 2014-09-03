namespace MusicFactory.Data.MongoDb
{
    using MusicFactory.Models.MongoDbProjections;
    using System;
    using System.Collections.Generic;

    public static class MongoDbAlbumDataGenarator
    {
        public static ICollection<AlbumMongoDbProjection> GenerateAlbums()
        {
            var albums = new List<AlbumMongoDbProjection>();

            var musicOfTheSunSongs = new List<SongMongoDbProjection>()
            {
                new SongMongoDbProjection() { Title = "Pon de Replay", Duration = 226, GenreName = "Dance" },
                new SongMongoDbProjection() { Title = "Here I go Again", Duration = 251, GenreName = "Pop" },
                new SongMongoDbProjection() { Title = "You dont love me", Duration = 246, GenreName = "Pop" },
            };

            var infiniteSongs = new List<SongMongoDbProjection>()
            {
                new SongMongoDbProjection() { Title = "Infinite", Duration = 224, GenreName = "Rap" },
                new SongMongoDbProjection() { Title = "Tonite", Duration = 251, GenreName = "Rap" },
                new SongMongoDbProjection() { Title = "Never 2 far", Duration = 246, GenreName = "Hip hop" },
            };

            var recoverySongs = new List<SongMongoDbProjection>()
            {
                new SongMongoDbProjection() { Title = "Not Afraid", Duration = 219, GenreName = "Hip hop" },
                new SongMongoDbProjection() { Title = "On fire", Duration = 234, GenreName = "Rap" },
                new SongMongoDbProjection() { Title = "Cinderella man", Duration = 187, GenreName = "Hip hop" },
            };

            var abbeyRoadSongs = new List<SongMongoDbProjection>()
            {
                new SongMongoDbProjection() { Title = "Oh! Darling", Duration = 219, GenreName = "Rock" },
                new SongMongoDbProjection() { Title = "Come Together", Duration = 234, GenreName = "Rock" },
                new SongMongoDbProjection() { Title = "Because", Duration = 187, GenreName = "Rock" },
            };

            var musicOfTheSun =
                new AlbumMongoDbProjection() { AlbumTitle = "Music of the sun", ReleaseDate = new DateTime(2005,5,17), ArtistName = "Rihanna", ArtistLabel = "Virginia", Songs = musicOfTheSunSongs };
            var infinite =
                new AlbumMongoDbProjection() { AlbumTitle = "Infinite", ReleaseDate = new DateTime(2011,5,17), ArtistName = "Eminem", ArtistLabel = "Shady", Songs = infiniteSongs };

            var recovery =
                new AlbumMongoDbProjection() { AlbumTitle = "Recovery", ReleaseDate = new DateTime(2013,4,25), ArtistName = "Eminem", ArtistLabel = "Shady", Songs = recoverySongs };
            var abbeyRoad =
                new AlbumMongoDbProjection() { AlbumTitle = "Abbey Road", ReleaseDate = new DateTime(1969,9,26), ArtistName = "The Beatles", ArtistLabel = "Apple", Songs = abbeyRoadSongs };
          
            var letItBe =
                new AlbumMongoDbProjection() { AlbumTitle = "Let it be", ReleaseDate = new DateTime(1970,5,8), ArtistName = "The Beatles", ArtistLabel = "Apple" };
            var help =
                new AlbumMongoDbProjection() { AlbumTitle = "Help!", ReleaseDate = new DateTime(1965,8,6), ArtistName = "The Beatles", ArtistLabel = "Parlophone" };

            albums.Add(musicOfTheSun);
            albums.Add(infinite);
            albums.Add(recovery);
            albums.Add(abbeyRoad);
            albums.Add(letItBe);
            albums.Add(help);
            return albums;
        }
    }
}