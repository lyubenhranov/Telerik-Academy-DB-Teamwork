namespace MusicFactory.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class SalesByCountry
    {
        public string CountryName { get; set; }
        public decimal Sales { get; set; }
        public int Year { get; set; }
    }
}
