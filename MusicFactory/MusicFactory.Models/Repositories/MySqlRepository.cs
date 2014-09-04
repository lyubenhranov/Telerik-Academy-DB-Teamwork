using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.IO;
using MySql.Data.MySqlClient;
using OfficeOpenXml;

namespace MusicFactory.Models.Repositories
{
    public class MySqlRepository
    {
        public List<SalesByCountry> GetExpensesData()
        {
            string connectionString = "Server=localhost;Port=3306;Database=musicfactory;Uid=root;Pwd=;";

            MySqlConnection expensesDatabase = new MySqlConnection(connectionString);

            MySqlDataReader salesReader;

            expensesDatabase.Open();

            List<SalesByCountry> salesByCountry = new List<SalesByCountry>();

            using (expensesDatabase)
            {
                MySqlCommand selectBooksCommand = new MySqlCommand("SELECT * FROM salesbycountry", expensesDatabase);

                salesReader = selectBooksCommand.ExecuteReader();

                string countryName;
                int year;
                decimal sales;

                while (salesReader.Read())
                {
                    countryName = (string)salesReader["CountryName"];
                    year = (int)salesReader["Year"];
                    sales = (decimal)salesReader["Sales"];

                    salesByCountry.Add(new SalesByCountry { CountryName = countryName, Year = year, Sales = sales });
                }
            }

            return salesByCountry;
        }
    }
}
