namespace MusicFactory.Models.MongoDbProjections
{
    using System;

    public class SongMongoDbProjection
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public int Duration { get; set; }

        public string GenreName { get; set; }

        
    }
}