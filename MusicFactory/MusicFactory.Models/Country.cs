namespace MusicFactory.Models
{
    using System;

    public class Country
    {
        public int CountryId { get; set; }

        public string Name { get; set; }

        public Country()
        {
            //this.CountryId = Guid.NewGuid();
        }
    }
}