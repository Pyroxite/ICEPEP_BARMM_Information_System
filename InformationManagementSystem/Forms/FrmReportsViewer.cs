using Microsoft.Reporting.WinForms;
using System;
using System.Data.OleDb;
using System.Windows.Forms;
using InformationManagementSystem.DataSets;
using System.Data;
using System.Web.UI.WebControls;

namespace InformationManagementSystem
{
    public partial class FrmReportsViewer : Form
    {
        private readonly string _reportTitle;

        public FrmReportsViewer(string reportTitle)
        {
            InitializeComponent();
            _reportTitle = reportTitle;

            lblReportTitle.Text = _reportTitle;
        }

        private void FrmReportsViewer_Load(object sender, EventArgs e)
        {
            string connectionString = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = " + Application.StartupPath + @"\ICEPEP.accdb; Jet OLEDB:Database Password = 121798";
            string selectCommand = "SELECT * FROM tbl_StudentInformation";

            //using (OleDbConnection connection = new OleDbConnection(connectionString))
            //{
            //    using (OleDbCommand command = new OleDbCommand(selectCommand))
            //    {
            //        using (OleDbDataAdapter adapter = new OleDbDataAdapter())
            //        {
            //            command.Connection = connection;
            //            adapter.SelectCommand = command;

            //            using (ICEPEPDataSet studentsList = new ICEPEPDataSet())
            //            {
            //                adapter.Fill(studentsList, "StudentsList");
            //                ReportDataSource dataSource = new ReportDataSource("ICEPEPDataSet", studentsList.Tables[0]);

            //                reportViewer.LocalReport.DataSources.Clear();
            //                reportViewer.LocalReport.DataSources.Add(dataSource);
            //                reportViewer.RefreshReport();
            //            }
            //        }
            //    }
            var DataSet = new DataSet();
            DataTable table = new DataTable();
            var dataAdapter = new OleDbDataAdapter("SELECT * FROM tbl_StudentInformation",connectionString);
            dataAdapter.Fill(DataSet);
            table = DataSet.Tables[0];
            table.TableName = "ICEPEPDataSet";
            var reportDataSource = new ReportDataSource(table.TableName, table);
            ReportViewer viewer = new ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;
            viewer.LocalReport.DataSources.Add(reportDataSource);
            viewer.LocalReport.ReportPath = "C:\\Users\\King\\source\\repos\\WinForms\\FullProjects\\InformationManagementSystem\\InformationManagementSystem\\Reports\\ReportStudents.rdlc";
            viewer.RefreshReport();
            dataAdapter.Dispose();
            table.Dispose();

            }
        }
    }

