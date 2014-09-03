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
        public void CreateDatabase()
        {
            SQLiteConnection.CreateFile(@"..\..\..\..\Databases\additionalProductInfo.sqlite");
        }

        public void FillDatabaseWithData()
        {
            SQLiteConnection libraryDatabase = new SQLiteConnection("Data Source=..\\..\\..\\..\\Databases\\additionalProductInfo.sqlite;Version=3;");

            libraryDatabase.Open();

            using (libraryDatabase)
            {
                SQLiteCommand createTableCommand = new SQLiteCommand("CREATE TABLE `ExpensesByCountry` (`ExpensesID` INTEGER PRIMARY KEY AUTOINCREMENT,`CountryID` INTEGER NOT NULL,`Expenses` INTEGER NOT NULL)", libraryDatabase);

                createTableCommand.ExecuteNonQuery();

                SQLiteCommand fillTableWithDataCommand = new SQLiteCommand("INSERT INTO `ExpensesByCountry` VALUES (1, 1, 500), (2, 2, 800), (3, 3, 900), (4, 4, 1000), (5, 5, 300);", libraryDatabase);

                fillTableWithDataCommand.ExecuteNonQuery();
            }
        }
    }
}
