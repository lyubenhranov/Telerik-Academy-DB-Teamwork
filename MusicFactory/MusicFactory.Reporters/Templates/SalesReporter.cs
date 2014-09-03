namespace MusicFactory.Reporters.Templates
{
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public abstract class SalesReporter
    {
        public abstract void GenerateReport(int year, string fileName);

        protected SqlCommand GetSqlCommand(int year, SqlConnection musicFactoryDbConnection)
        {
            SqlCommand salesByArtistCommand = new SqlCommand("SELECT artists.Name, SUM(orders.TotalSum) AS [Sales] FROM Orders AS orders JOIN Albums AS albums ON orders.AlbumID = albums.AlbumID	JOIN Artists AS artists ON albums.ArtistID = artists.ArtistID WHERE YEAR(orders.OrderDate) = @year GROUP BY artists.Name ORDER BY artists.Name", musicFactoryDbConnection);

            salesByArtistCommand.Parameters.AddWithValue("@year", year);

            return salesByArtistCommand;
        }

        protected SqlConnection GetDatabaseConnection()
        {
            SqlConnection musicFactoryDbConnection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SalesReporterConnectionString"].ConnectionString);

            return musicFactoryDbConnection;
        }

        protected abstract void TransferDataToFile(int year, string fileName, SqlConnection musicFactoryDbConnection);
    }
}
