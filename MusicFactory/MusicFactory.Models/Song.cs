namespace MusicFactory.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Song
    {
        [Key]
        public Guid SongID { get; set; }

        [StringLength(100)]
        public string Title { get; set; }

        // public Guid? ArtistID { get; set; }
        public Guid? AlbumID { get; set; }

        public int? Duration { get; set; }

        public Guid? GenreID { get; set; }

        public virtual Album Album { get; set; }

        public virtual Artist Artist { get; set; }

        public virtual Genre Genre { get; set; }

        public Song()
        {
            this.SongID = Guid.NewGuid();
        }
    }
}