namespace MusicFactory.Data.MongoDb
{
    using System.Collections.Generic;
    using System;
    using System.Linq;
    using MusicFactory.Data.MongoDb;
    using MusicFactory.Models;
    using MongoDB.Driver;
    using System.Data.Entity;
    using MusicFactory.Models.MongoDbProjections;
    
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
       
        /// <summary>
        /// Parses the album projection from the MongoDb database to valid Sql Server objects
        /// </summary>
        /// <param name="albumProjection">MongoDB album projection entry</param>
        /// <returns>The parsed album with all the needed references</returns>
        private Album ParseMongoDbRecord(AlbumMongoDbProjection albumProjection)
        {
            var label = new Lable() { LegalName = albumProjection.ArtistLabel };
            var songs = albumProjection.Songs;
            var artist = new Artist() { Name = albumProjection.ArtistName, Lable = label };
            var songsToDb = new List<Song>();
            var genres = new List<Genre>();
            foreach (var song in songs)
            {
                var genre = genres.FirstOrDefault(g => g.Name == song.GenreName);
                if (genre == null)
                {
                    genre = new Genre() { Name = song.GenreName };
                }
                genres.Add(genre);
                songsToDb.Add(new Song { Title = song.Title, Duration = song.Duration, Genre = genre });
            }

            artist.Songs = songsToDb;
            
            var parsedAlbum = new Album() { Title = albumProjection.AlbumTitle,  ReleaseDate = albumProjection.ReleaseDate, Songs = songsToDb, Artist = artist, Label = label };

            foreach (var song in songsToDb)
            {
                song.Album = parsedAlbum;
            }

            artist.Albums.Add(parsedAlbum);

            //if (albumProjection.LabelId != default(int))
            //{
            //    label.LabelID = albumProjection.LabelId;
            //}

            //if (albumProjection.ArtistId != default(int))
            //{
            //    artist.ArtistID = albumProjection.ArtistId;
            //}

            return parsedAlbum;
        }

        /// <summary>
        /// Checks if any of the objects parsed from the MongoDb database already exists in the SQL Server database.
        /// </summary>
        private Album ValidateAlbumInfo(Album album)
        {
            var artistInDb = this.SqlServerContext.Artists.FirstOrDefault(a => a.Name == album.Artist.Name);
            var labelInDb = this.SqlServerContext.Lables.FirstOrDefault(l => l.LegalName == album.Label.LegalName);

            if (artistInDb != null)
            {
                album.Artist = artistInDb;
                foreach (var song in album.Songs)
                {
                    song.Artist = artistInDb;
                }
            }

            if (labelInDb != null)
            {
                album.Label = labelInDb;
                album.Artist.Lable = labelInDb;
            }

            var genresInDb = this.SqlServerContext.Genres.ToList();

            foreach (var song in album.Songs)
            {
                var genreInDb = genresInDb.FirstOrDefault(g => g.Name == song.Genre.Name);

                if (genreInDb != null)
                {
                    song.Genre = genreInDb;
                }
            }
            
            return album;
        }

        public void TransferSingleRecord()
        {
            var mongoDbAlbum = this.MongoDbPersister.GetSingleAlbum();
            var parsedAlbum = this.ParseMongoDbRecord(mongoDbAlbum);
            parsedAlbum = this.ValidateAlbumInfo(parsedAlbum);

            SqlServerContext.Albums.Add(parsedAlbum);
            this.SqlServerContext.SaveChanges();
        }

        //TODO Fix: mutiple queries to the sql server
        public void TransferAllRecords()
        {
            var mongoDbAlbums = this.MongoDbPersister.GetAllAlbums();
            foreach (var mongoDbAlbum in mongoDbAlbums)
            {
                var parsedAlbum = this.ParseMongoDbRecord(mongoDbAlbum);
                parsedAlbum = this.ValidateAlbumInfo(parsedAlbum);

                SqlServerContext.Albums.Add(parsedAlbum);
                this.SqlServerContext.SaveChanges();
            }

            //TODO Remove!!!
            var country = new Country() { Name = "bulgaria" };
            var address = new Address() { AddressText = "addrszz", Country = country };
            var store = new Store() { Name = "Zmeyovo", Address = address };

            this.SqlServerContext.Stores.Add(store);
            this.SqlServerContext.SaveChanges();


        }
    }
}