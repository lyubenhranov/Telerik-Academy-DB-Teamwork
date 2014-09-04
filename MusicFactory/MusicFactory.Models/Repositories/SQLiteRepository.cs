namespace MusicFactory.Models.SQLite
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Data.SQLite;
    using System.Data;

    public class SQLiteRepository
    {
        public void CreateDatabase(string databaseName)
        {
            SQLiteConnection.CreateFile(System.Configuration.ConfigurationManager.AppSettings["DatabaseFolderPath"] + databaseName + ".sqlite");
        }

        public void FillDatabaseWithData()
        {
            SQLiteConnection expensesDatabase = new SQLiteConnection("Data Source=..\\..\\..\\..\\Databases\\additionalProductInfo.sqlite;Version=3;");

            expensesDatabase.Open();

            using (expensesDatabase)
            {
                SQLiteCommand createTableCommand = new SQLiteCommand("CREATE TABLE `ExpensesByCountry` (`ExpensesID` INTEGER PRIMARY KEY AUTOINCREMENT,`CountryName` TEXT NOT NULL,`Expenses` INTEGER NOT NULL,`Year` INTEGER NOT NULL)", expensesDatabase);

                createTableCommand.ExecuteNonQuery();

                SQLiteCommand fillTableWithDataCommand = new SQLiteCommand("INSERT INTO `ExpensesByCountry` VALUES (1, 'Italy', 500, 2013), (2, 'France', 800, 2013), (3, 'UK', 900, 2013), (4, 'USA', 1000, 2013), (5, 'Bulgaria', 300, 2013), (6, 'Italy', 500, 2014), (7, 'France', 800, 2014), (8, 'UK', 900, 2014), (9, 'USA', 1000, 2014), (10, 'Bulgaria', 300, 2014);", expensesDatabase);

                fillTableWithDataCommand.ExecuteNonQuery();
            }
        }

        public List<ExpenseByCountry> GetExpensesData()
        {
            SQLiteConnection expensesDatabase = new SQLiteConnection("Data Source=..\\..\\..\\..\\Databases\\additionalProductInfo.sqlite;Version=3;");

            List<ExpenseByCountry> expenses = new List<ExpenseByCountry>();

            expensesDatabase.Open();

            using (expensesDatabase)
            {
                SQLiteCommand createTableCommand = new SQLiteCommand("SELECT * FROM ExpensesByCountry", expensesDatabase);
                
                var reader = createTableCommand.ExecuteReader();

                string countryName;
                decimal expense;
                int year;

                while (reader.Read())
                {
                    countryName = (string)reader["CountryName"];
                    expense = (long)reader["Expenses"];
                    year = (int)(long)reader["Year"];

                    expenses.Add(new ExpenseByCountry { CountryName = countryName, Expenses = expense, Year = year });
                }
            }

            return expenses;
        }
    }
}
