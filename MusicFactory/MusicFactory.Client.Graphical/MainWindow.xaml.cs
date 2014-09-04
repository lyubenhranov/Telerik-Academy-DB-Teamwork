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

        private const string DefaultReportsFolder = "..\\..\\..\\..\\Reports\\";

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
            System.Diagnostics.Process.Start(DefaultReportsFolder + "2014-Artists-Sales-Report.pdf");
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
            System.Diagnostics.Process.Start(DefaultReportsFolder + "JSON\\");
        }

        private void GenerateExcelReportButtonClick(object sender, RoutedEventArgs e)
        {
            this.mediator.SaveReportsFromSqliteAndMySqlToExcel();
            System.Diagnostics.Process.Start(DefaultReportsFolder + "Profit and Loss Report.xlsx");
        }

        private void GenerateXmlReportClick(object sender, RoutedEventArgs e)
        {
            this.mediator.GenerateXmlReportForYear(2014, "2014-Artists-Sales-Report");
            System.Diagnostics.Process.Start(DefaultReportsFolder + "2014-Artists-Sales-Report.xml");
        }
    }
}