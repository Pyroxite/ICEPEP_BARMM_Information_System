using System;
using System.Data.OleDb;
using System.Windows.Forms;

namespace InformationManagementSystem
{
    public partial class FrmStudentCurrentSchool : Form
    {
        private readonly FrmNewStudent _student;
        private OleDbConnection _connection = new OleDbConnection();
        private OleDbCommand _command = new OleDbCommand();

        public FrmStudentCurrentSchool(FrmNewStudent student)
        {
            InitializeComponent();
            _connection.ConnectionString = DatabaseHelper.GetConnection();
            _student = student;
        }

        private void btnCurrentAdd_Click(object sender, EventArgs e)
        {
            try
            {
                var _queryInsert = "INSERT INTO tbl_StudentCurrentSchool (CurrentSchool, CurrentSchoolAddress) VALUES (@CurrentSchool, @CurrentSchoolAddress)";

                _connection.Open();
                _command.Connection = _connection;
                _command = new OleDbCommand(_queryInsert, _connection);
                _command.Parameters.AddWithValue("@CurrentSchool", tbxCurrentSchool.Text);
                _command.Parameters.AddWithValue("@CurrentSchoolAddress", tbxCurrentSchoolAddress.Text);
                _command.ExecuteNonQuery();
                _connection.Close();
                _student.LoadCurrentSchool();
                ClearFields();
                MessageBox.Show("New record has been added");
            }
            catch (Exception)
            {
                _connection.Close();
                MessageBox.Show("Connection Failed");
            }
        }

        private void ClearFields()
        {
            tbxCurrentSchool.Clear();
            tbxCurrentSchoolAddress.Clear();
        }
    }
}