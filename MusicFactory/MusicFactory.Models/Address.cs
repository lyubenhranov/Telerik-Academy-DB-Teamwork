namespace MusicFactory.Models
{
    using System;

    public class Address
    {
        public Guid AddressId { get; set; }

        public Guid CountryId { get; set; }

        public virtual Country Country { get; set; }

        public Address()
        {
            this.AddressId = Guid.NewGuid();
        }
    }
}