namespace MusicFactory.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public class MusicFactoryDbContext : DbContext
    {
        public MusicFactoryDbContext()
            : base("name=MusicFactoryModels")
        {
        }

        public virtual DbSet<Album> Albums { get; set; }
        public virtual DbSet<Artist> Artists { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Lable> Lables { get; set; }
        public virtual DbSet<Song> Songs { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Album>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<Album>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            //modelBuilder.Entity<Album>()
            //    .HasMany(e => e.Songs)
            //    .WithRequired(e => e.Album)
            //    .HasForeignKey(e => e.ArtistID)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Album>()
            //    .HasMany(e => e.Genres)
            //    .WithMany(e => e.Albums)
            //    .Map(m => m.ToTable("Albums.Genres").MapLeftKey("AlbumID").MapRightKey("GenreID"));

            modelBuilder.Entity<Artist>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Artist>()
                .HasMany(e => e.Albums)
                .WithRequired(e => e.Artist)
                .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Artist>()
            //    .HasMany(e => e.Songs)
            //    .WithRequired(e => e.Artist)
            //    .WillCascadeOnDelete(false);


            //modelBuilder.Entity<Artist>()
            //    .HasMany(e => e.Genres)
            //    .WithMany(e => e.Artists)
            //    .Map(m => m.ToTable("Artists.Genres").MapLeftKey("ArtistID").MapRightKey("GenreID"));

            modelBuilder.Entity<Genre>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Lable>()
                .Property(e => e.LegalName)
                .IsUnicode(false);

            modelBuilder.Entity<Lable>()
                .HasMany(e => e.Artists)
                .WithOptional(e => e.Lable)
                .HasForeignKey(e => e.LableID);

            modelBuilder.Entity<Song>()
                .Property(e => e.Title)
                .IsUnicode(false);
        }
    }
}
