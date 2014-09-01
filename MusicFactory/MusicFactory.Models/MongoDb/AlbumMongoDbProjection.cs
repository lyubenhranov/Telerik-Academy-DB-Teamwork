namespace MusicFactory.Models.MongoDb
{
    using System;
    using System.Collections.Generic;

    public class AlbumMongoDbProjection
    {
        public Guid Id { get; set; }

        public string AlbumTitle { get; set; }

        public decimal AlbumPrice { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string ArtistName { get; set; }

        public string ArtistLabel { get; set; }

        public ICollection<SongMongoDbProjection> Songs { get; set; }

        public AlbumMongoDbProjection()
        {
            this.Songs = new HashSet<SongMongoDbProjection>();
        }
    }
}