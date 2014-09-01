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
                songsToDb.Add(new Song { Title = song.Title, Duration = song.Duration });
            }
            artist.Songs = songsToDb;
            
            var normalizedAlbum = new Album() { Title = album.AlbumTitle, Price = album.AlbumPrice, ReleaseDate = album.ReleaseDate, Songs = songsToDb, Artist = artist };

            foreach (var song in songsToDb)
            {
                song.Album = normalizedAlbum;
            }
            artist.Albums.Add(normalizedAlbum);

            if (album.LabelId != default(Guid))
            {
                label.LabelID = album.LabelId;
            }
            if (album.ArtistId != default(Guid))
            {
                artist.ArtistID = album.ArtistId;
            }

            return normalizedAlbum;
        }

        public Album ValidateAlbumInfo(Album album)
        {
            var artistInDb = this.SqlServerContext.Artists.FirstOrDefault(a => a.Name == album.Artist.Name);

            if (artistInDb != null)
            {
                album.Artist = artistInDb;
                foreach (var song in album.Songs)
                {
                    song.Artist = artistInDb;
                }
            }
           
            //var labelInDb = this.SqlServerContext.Lables.FirstOrDefault(l => l.LegalName == album);
            //if (labelInDb != null)
            //{
            //    album.LabelID = labelInDb.LabelID;
            //}

            return album;
        }

        public void TransferSingleRecord()
        {
            var album = this.MongoDbPersister.GetSingleAlbum();
            var normalizedAlbum = this.NormalizeMongoDbRecord(album);

            SqlServerContext.Albums.Add(ValidateAlbumInfo(normalizedAlbum));
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