namespace MusicFactory.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Song
    {
        public int SongID { get; set; }

        [StringLength(100)]
        public string Title { get; set; }

        public int ArtistID { get; set; }

        public int AlbumID { get; set; }

        public int? Duration { get; set; }

        public virtual Album Album { get; set; }

        public virtual Artist Artist { get; set; }
    }
}
