using MusicFactory.Reporters.Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicFactory.Reporters
{
    public class XmlReporter : IReporter
    {
        public void GenerateReport()
        {
            string connectionString = "Server=LYUBENPC; " +
            "Database=Northwind; Integrated Security=true";

            SqlConnection dbConnection = new SqlConnection(connectionString);

            dbConnection.Open();

            using (dbConnection)
            {
                SqlCommand employeesCommand = new SqlCommand("SELECT TOP 10 EmployeeId AS ID, FirstName + ' ' + LastName AS [Full Name], Title FROM Employees", dbConnection);

                var dataAdapter = new SqlDataAdapter(employeesCommand);
                var employeesDataTable = new DataTable("Employees");

                dataAdapter.Fill(employeesDataTable);

                employeesDataTable.WriteXml(@"..\..\..\Reports\report.xml");

                Console.WriteLine("XML Report has been successfully generated");
            }
        }
    }
}
