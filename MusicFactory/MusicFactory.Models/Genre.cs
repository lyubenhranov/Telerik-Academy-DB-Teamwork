namespace MusicFactory.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Genre
    {
        public Genre()
        {
            Albums = new HashSet<Album>();
            Artists = new HashSet<Artist>();
        }

        public int GenreID { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        public virtual ICollection<Album> Albums { get; set; }

        public virtual ICollection<Artist> Artists { get; set; }
    }
}
