using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.IO;

using OfficeOpenXml;
//using MySql.Data.MySqlClient;

namespace MusicFactory.Models.Repositories
{
    public class MySqlRepository
    {
        public void GenerateExcelReports()
        {
            FileInfo newFile = new FileInfo("../../../../Reports/ExcelFromMySQL/Music Factory.xlsx");
            using (ExcelPackage xlPackage = new ExcelPackage(newFile))
            {
                ExcelWorksheet bugsWorkSheet = xlPackage.Workbook.Worksheets.Add("MusicFactory");
                bugsWorkSheet.Cell(1, 1).Value = "CountryId";
                bugsWorkSheet.Cell(1, 2).Value = "Sales";
                bugsWorkSheet.Cell(1, 3).Value = "Year";

                xlPackage.Save();
            }
        }



        /*  FILLING IN
        private const string ExcelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=..\\..\\..\\Excel Reports\\Report.xlsx;Extended Properties=Excel 12.0;";
        private const string MySqlToExcelTransferSuccessMessage = "Data transferred from MySql in Excel successfully. Check out in solution folder Excel Reports.";
        
        public void WriteFromMySqlInExcel()
        {
            using (var context = new MusicFactoryDbContext())
            {
                var connection = new OleDbConnection(ExcelConnectionString);
                connection.Open();

                // TO FINISH LATER
            }
        }
        */
    }
}
