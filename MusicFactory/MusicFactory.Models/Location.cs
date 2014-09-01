namespace MusicFactory.Models
{
    using System;

    public class Location
    {
        public Guid LocationId { get; set; }

        public string Description { get; set; }

        public Location()
        {
            this.LocationId = Guid.NewGuid();
        }
    }
}