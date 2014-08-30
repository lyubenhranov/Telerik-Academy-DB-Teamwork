namespace MusicFactory.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Album
    {
        public Album()
        {
            Songs = new HashSet<Song>();
            Genres = new HashSet<Genre>();
        }

        public int AlbumID { get; set; }

        [StringLength(100)]
        public string Title { get; set; }

        public int ArtistID { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ReleaseDate { get; set; }

        [Column(TypeName = "money")]
        public decimal? Price { get; set; }

        public int? LabelID { get; set; }

        public virtual Artist Artist { get; set; }

        public virtual ICollection<Song> Songs { get; set; }

        public virtual ICollection<Genre> Genres { get; set; }
    }
}