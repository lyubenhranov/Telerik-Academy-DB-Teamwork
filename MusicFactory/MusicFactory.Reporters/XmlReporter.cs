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
        public override void GenerateReport(int year, string fileName)
        {
            SqlConnection musicFactoryDbConnection = this.GetDatabaseConnection();

            this.TransferDataToFile(year, fileName, musicFactoryDbConnection);

            Console.WriteLine("XML report has been successfully generated");
        }

        protected override void TransferDataToFile(int year, string fileName, SqlConnection musicFactoryDbConnection)
        {
            musicFactoryDbConnection.Open();

            using (musicFactoryDbConnection)
            {
                SqlCommand salesByArtistCommand = this.GetSqlCommand(year, musicFactoryDbConnection);

                var dataAdapter = new SqlDataAdapter(salesByArtistCommand);
                var salesByArtistDataTable = new DataTable("Sales By Artist");

                dataAdapter.Fill(salesByArtistDataTable);

                salesByArtistDataTable.WriteXml(System.Configuration.ConfigurationManager.AppSettings["ReportsFolderPath"] + fileName + ".xml");
            }
        }
    }
}
