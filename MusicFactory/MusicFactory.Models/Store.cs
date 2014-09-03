namespace MusicFactory.Models
{
    using System;
    using System.Collections.Generic;

    public class Store
    {
        public Guid StoreId { get; set; }

        public Retailer Owner { get; set; }
        
        public string Location { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public Store()
        {
            this.StoreId = Guid.NewGuid();
            this.Orders = new HashSet<Order>();
        }
    }
}