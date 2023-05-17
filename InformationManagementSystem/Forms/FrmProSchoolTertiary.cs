using System;
using System.Data.OleDb;
using System.Windows.Forms;

namespace InformationManagementSystem
{
    public partial class FrmProSchoolTertiary : Form
    {
        private readonly OleDbConnection _connection = new OleDbConnection();
        private OleDbCommand _command = new OleDbCommand();
        private readonly FrmNewProfessional _professional;

        public FrmProSchoolTertiary(FrmNewProfessional professional)
        {
            InitializeComponent();
            _connection.ConnectionString = DatabaseHelper.GetConnection();
            _professional = professional;
        }

        private void BtnCurrentAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbxTertiarySchool.Text))
            {
                MessageBox.Show("Some fields are missing");
            }
            else
            {
                try
                {
                    var _queryInsert = "INSERT INTO tbl_ProfessionalTertiarySchool (TertiarySchoolName) VALUES (@TertiarySchoolName)";

                    _connection.Open();
                    _command.Connection = _connection;
                    _command = new OleDbCommand(_queryInsert, _connection);
                    _command.Parameters.AddWithValue("@TertiarySchoolName", tbxTertiarySchool.Text);
                    _command.ExecuteNonQuery();
                    _connection.Close();
                    _professional.LoadTertiarySchool();
                    ClearFields();
                    MessageBox.Show("New record has been added");
                }
                catch (Exception)
                {
                    _connection.Close();
                    MessageBox.Show("Connection Failed");
                }
            }
        }

        private void ClearFields()
        {
            tbxTertiarySchool.Clear();
        }
    }
}
