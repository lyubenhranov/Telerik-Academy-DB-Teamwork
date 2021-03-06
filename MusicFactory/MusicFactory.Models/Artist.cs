namespace MusicFactory.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Artist
    {
        public Artist()
        {
            //this.ArtistID = Guid.NewGuid();
            Albums = new HashSet<Album>();
            Songs = new HashSet<Song>();
        }

        [Key]
        public int ArtistID { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        public int? LableID { get; set; }

        public virtual ICollection<Album> Albums { get; set; }

        public virtual Lable Lable { get; set; }

        public virtual ICollection<Song> Songs { get; set; }

    }
}
