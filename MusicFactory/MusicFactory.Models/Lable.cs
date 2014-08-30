namespace MusicFactory.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Lable
    {
        public Lable()
        {
            Artists = new HashSet<Artist>();
        }

        [Key]
        public int LabelID { get; set; }

        [StringLength(100)]
        public string LegalName { get; set; }

        public int? Country { get; set; }

        public virtual ICollection<Artist> Artists { get; set; }
    }
}
