namespace MusicFactory.Models
{
    using System;

    public class Address
    {
        public int AddressId { get; set; }

        public string AddressText { get; set; }

        public int CountryId { get; set; }

        public virtual Country Country { get; set; }

        public Address()
        {
            //this.AddressId = Guid.NewGuid();
        }
    }
}