using MusicFactory.Reporters.Templates;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicFactory.Reporters
{
    public class XmlReporter : SalesReporter
    {
        public override void GenerateReport(int year)
        {
            string connectionString = "Server=LYUBENPC; " +
            "Database=MusicFactoryTest; Integrated Security=true";

            SqlConnection musicFactoryDbConnection = new SqlConnection(connectionString);

            musicFactoryDbConnection.Open();

            using (musicFactoryDbConnection)
            {
                SqlCommand salesByArtistCommand = new SqlCommand("SELECT artists.Name, SUM(orders.TotalSum) AS [Sales] FROM Orders AS orders JOIN Albums AS albums ON orders.AlbumID = albums.AlbumID	JOIN Artists AS artists ON albums.ArtistID = artists.ArtistID WHERE YEAR(orders.OrderDate) = @year GROUP BY artists.Name ORDER BY artists.Name", musicFactoryDbConnection);

                salesByArtistCommand.Parameters.AddWithValue("@year", year);

                var dataAdapter = new SqlDataAdapter(salesByArtistCommand);
                var salesByArtistDataTable = new DataTable("Sales By Artist");

                dataAdapter.Fill(salesByArtistDataTable);

                salesByArtistDataTable.WriteXml(@"..\..\..\Reports\report.xml");

                Console.WriteLine("XML Report has been successfully generated");
            }
        }
    }
}
