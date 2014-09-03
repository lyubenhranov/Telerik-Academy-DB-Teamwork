namespace MusicFactory.Models
{
    using System;
    using System.Collections.Generic;

    public class Order
    {
        public Guid OrderID { get; set; }

        public DateTime OrderDate { get; set; }

        public Guid? AlbumId { get; set; }

        public Guid StoreId { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public decimal TotalSum { get; set; }

        public virtual Album Album { get; set; }

        public virtual Store Store { get; set; }

        public Order()
        {
            this.OrderID = Guid.NewGuid();
        }
    }
}