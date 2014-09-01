namespace MusicFactory.Models
{
    using System;

    public class OrderDetail
    {
        public Guid OrderDetailId { get; set; }

        public int? AlbumId { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public virtual Album Album { get; set; }

        public OrderDetail()
        {
            this.OrderDetailId = Guid.NewGuid();
        }
    }
}