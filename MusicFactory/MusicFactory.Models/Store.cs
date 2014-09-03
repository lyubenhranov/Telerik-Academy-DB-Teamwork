namespace MusicFactory.Models
{
    using System;
    using System.Collections.Generic;

    public class Store
    {
        public int StoreId { get; set; }

        public string Owner { get; set; }

        public string Name { get; set; }

        public int AddressId { get; set; }

        public virtual Address Address { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public Store()
        {
            //this.StoreId = Guid.NewGuid();
            this.Orders = new HashSet<Order>();
        }
    }
}