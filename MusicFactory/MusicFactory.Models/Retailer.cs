namespace MusicFactory.Models
{
    using System;
    using System.Collections.Generic;

    public class Retailer
    {
        public Guid RetailerId { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Store> Stores { get; set; }

        public Retailer()
        {
            this.RetailerId = Guid.NewGuid();
            this.Stores = new HashSet<Store>();
        }
    }
}