using System;
using System.Data.OleDb;
using System.Windows.Forms;

namespace InformationManagementSystem
{
    public partial class FrmStudentSeniorHigh : Form
    {
        private readonly FrmNewStudent _student;
        private OleDbConnection _connection = new OleDbConnection();
        private OleDbCommand _command = new OleDbCommand();

        public FrmStudentSeniorHigh(FrmNewStudent student)
        {
            InitializeComponent();
            _connection.ConnectionString = DatabaseConnection.GetConnection();
            _student = student;
        }

        private void btnCurrentAdd_Click(object sender, EventArgs e)
        {
            try
            {
                var _queryInsert = "INSERT INTO tbl_StudentHighSchool (HighSchoolName) VALUES (@HighSchoolName)";

                _connection.Open();
                _command.Connection = _connection;
                _command = new OleDbCommand(_queryInsert, _connection);
                _command.Parameters.AddWithValue("@HighSchoolName", tbxSeniorHighSchool.Text);
                _command.ExecuteNonQuery();
                _connection.Close();
                _student.LoadSeniorHigh();
                MessageBox.Show("New record has been added");
                ClearFields();
            }
            catch (Exception ex)
            {
                _connection.Close();
                MessageBox.Show("Connection Failed" + ex.Message);
            }
        }

        private void ClearFields()
        {
            tbxSeniorHighSchool.Clear();
        }
    }
}