using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace InformationManagementSystem
{
    public partial class FrmReportsViewer : Form
    {
        private readonly string _reportTitle;
        public FrmReportsViewer(string reportTitle)
        {
            InitializeComponent();
            _reportTitle = reportTitle;
        }

        private void FrmReportsViewer_Load(object sender, EventArgs e)
        {
            switch (_reportTitle)
            {
                case "STUDENTS_FULL":
                    StudentsReport();
                    break;
                default:
                    break;
            }
        }

        #region StudentsReport

        private void StudentsReport()
        {
            lblReportTitle.Text = "STUDENTS LIST";

            var studentsReportPath = AppContext.BaseDirectory + @"\Reports\ReportStudents.rdlc";
            StudentsReportViewer.ProcessingMode = ProcessingMode.Local;
            StudentsReportViewer.LocalReport.ReportPath = studentsReportPath;

            var studentsDataSet = GetStudentsList();
            if (studentsDataSet.Tables[0].Rows.Count > 0)
            {
                var studentsDataSource = new ReportDataSource("StudentsDataSet", studentsDataSet.Tables[0]);
                StudentsReportViewer.LocalReport.DataSources.Clear();
                StudentsReportViewer.LocalReport.DataSources.Add(studentsDataSource);
                StudentsReportViewer.LocalReport.Refresh();
                StudentsReportViewer.RefreshReport();
            }
        }

        private DataSet GetStudentsList()
        {
            var selectCommand = "SELECT * FROM tbl_StudentInformation";
            using (OleDbConnection connection = new OleDbConnection(DatabaseHelper.GetConnection()))
            {
                using (OleDbCommand command = new OleDbCommand(selectCommand))
                {
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter())
                    {
                        try
                        {
                            connection.Open();
                            command.Connection = connection;
                            adapter.SelectCommand = command;
                            var dataSet = new DataSet();
                            adapter.Fill(dataSet);
                            connection.Close();
                            return dataSet;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error " + ex.Message);
                            connection.Close();
                            return null;
                        }
                    }
                }
            }
        }

        #endregion
    }
}

