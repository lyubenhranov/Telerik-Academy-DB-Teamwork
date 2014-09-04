using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MusicFactory.Client.Graphical
{
    using MusicFactory.Engine;
    using System.IO;
    using System.Windows.Media.Animation;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private WorkflowMediator mediator { get; set; }

        public MainWindow() : this(new WorkflowMediator())
        {
        }

        public MainWindow(WorkflowMediator mediator)
        {
            InitializeComponent();

            this.mediator = mediator;
        }
        
        private void GeneratePdf_Click(object sender, RoutedEventArgs e)
        {
            this.mediator.GeneratePdfReportForYear(2014, "2014-Artists-Sales-Report");
            //System.Diagnostics.Process.Start(System.Configuration.ConfigurationManager.AppSettings["ReportsFolderPath"] + "2014-Artists-Sales-Report.pdf");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.mediator.FillMongoDbWithData();
            this.mediator.TransferDataFromMongoToSqlServer();
            this.mediator.TransferXmlDataToMongoAndSqlServer();
            this.mediator.TransferDataFromExcelToSqlServer();
            this.mediator.TransferReportToMySql();
        }

        private void GenerateJsonButtonClick(object sender, RoutedEventArgs e)
        {
            this.mediator.TransferReportsJson();
        }

        private void GenerateExcelReportButtonClick(object sender, RoutedEventArgs e)
        {
            this.mediator.SaveReportsFromSqliteAndMySqlToExcel();
        }

        private void GenerateXmlReportClick(object sender, RoutedEventArgs e)
        {
            this.mediator.GenerateXmlReportForYear(2014, "2014-Artists-Sales-Report");
        }
    }
}