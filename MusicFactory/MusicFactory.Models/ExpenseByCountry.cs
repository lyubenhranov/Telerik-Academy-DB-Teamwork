namespace MusicFactory.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ExpenseByCountry
    {
        public string CountryName { get; set; }
        public decimal Expenses { get; set; }
        public int Year { get; set; }
    }
}
