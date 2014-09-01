namespace MusicFactory.Data
{
    using System.Collections.Generic;
    using System;
    using System.Linq;
    using MusicFactory.Models;
    using MusicFactory.Models.MongoDb;
    using MongoDB.Driver;
    using System.Data.Entity;

    public class MongoDbToSqlServerTransferer
    {
        private MongoDbPersister MongoDbPersister { get; set; }

        private MusicFactoryDbContext SqlServerContext { get; set; }

        public MongoDbToSqlServerTransferer() : this(new MongoDbPersister(), new MusicFactoryDbContext())
        {
        }

        public MongoDbToSqlServerTransferer(MongoDbPersister mongoDbPersister, MusicFactoryDbContext sqlServerContext)
        {
            this.MongoDbPersister = mongoDbPersister;
            this.SqlServerContext = sqlServerContext;
        }
       
        private Album NormalizeMongoDbRecord(AlbumMongoDbProjection album)
        {
            var label = new Lable() { LegalName = album.ArtistLabel };
            var songs = album.Songs;
            var artist = new Artist() { Name = album.ArtistName, Lable = label };
            var songsToDb = new List<Song>();
            foreach (var song in songs)
            {
                songsToDb.Add(new Song { Title = song.Title});
            }
            artist.Songs = songsToDb;
            
            var normalizedAlbum = new Album() { Title = album.AlbumTitle, Price = album.AlbumPrice, ReleaseDate = album.ReleaseDate, Songs = songsToDb, Artist = artist };

            foreach (var song in songsToDb)
            {
                song.Album = normalizedAlbum;
            }
            artist.Albums.Add(normalizedAlbum);

            return normalizedAlbum;
        }

        public Album ValidateAlbumInfo(Album album)
        {
            // TODO implement validation

            return album;
        }

        public void TransferSingleRecord()
        {
            var album = this.MongoDbPersister.GetSingleAlbum();
            var normalizedAlbum = this.NormalizeMongoDbRecord(album);
            SqlServerContext.Albums.Add(normalizedAlbum);
            this.SqlServerContext.SaveChanges();
        }

        public void TransferAllRecords()
        {
            var albums = this.MongoDbPersister.GetAllAlbums();
            var normalizedAlbums = albums.Select(x => this.NormalizeMongoDbRecord(x));
            this.SqlServerContext.Albums.AddRange(normalizedAlbums);
            this.SqlServerContext.SaveChanges();
        }
    }
}