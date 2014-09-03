namespace MusicFactory.Models.SQLite
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Data.SQLite;

    public class SQLiteRepository
    {
        public void CreateDatabase(string databaseName)
        {
            SQLiteConnection.CreateFile(System.Configuration.ConfigurationManager.AppSettings["DatabaseFolderPath"] + databaseName + ".sqlite");
        }

        public void FillDatabaseWithData()
        {
            SQLiteConnection libraryDatabase = new SQLiteConnection("Data Source=..\\..\\..\\..\\Databases\\additionalProductInfo.sqlite;Version=3;");

            libraryDatabase.Open();

            using (libraryDatabase)
            {
                SQLiteCommand createTableCommand = new SQLiteCommand("CREATE TABLE `ExpensesByCountry` (`ExpensesID` INTEGER PRIMARY KEY AUTOINCREMENT,`CountryName` VARCHAR(100) NOT NULL,`Expenses` INTEGER NOT NULL, `Year` INTEGER NOT NULL)", libraryDatabase);

                createTableCommand.ExecuteNonQuery();

                SQLiteCommand fillTableWithDataCommand = new SQLiteCommand("INSERT INTO `ExpensesByCountry` VALUES (1, 'Bulgaria', 500, 2014), (2, 'France', 800, 2014), (3, 'Germany', 900, 2014), (4, 'USA', 1000, 2014), (5, 'UK', 300, 2014);", libraryDatabase);

                fillTableWithDataCommand.ExecuteNonQuery();
            }
        }
    }
}
