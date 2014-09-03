namespace MusicFactory.Models
{
    using System;

    public class OrderDetail
    {
        public Guid OrderDetailId { get; set; }

       

        public OrderDetail()
        {
            this.OrderDetailId = Guid.NewGuid();
        }
    }
}