namespace MusicFactory.Models
{
    using System;
    using System.Collections.Generic;

    public class Order
    {
        public Guid OrderID { get; set; }

        public DateTime OrderDate { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public Order()
        {
            this.OrderID = Guid.NewGuid();
            this.OrderDetails = new HashSet<OrderDetail>();
        }
    }
}