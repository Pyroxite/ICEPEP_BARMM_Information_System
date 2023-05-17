using System;
using System.Data.OleDb;
using System.Windows.Forms;

namespace InformationManagementSystem
{
    public partial class FrmProCurrentEmployer : Form
    {
        private readonly FrmNewProfessional _professional;
        private readonly OleDbConnection _connection = new OleDbConnection();
        private OleDbCommand _command = new OleDbCommand();

        public FrmProCurrentEmployer(FrmNewProfessional professional)
        {
            InitializeComponent();
            _connection.ConnectionString = DatabaseHelper.GetConnection();
            _professional = professional;
        }

        private void BtnProCEAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbxProCurrentEmployer.Text) || string.IsNullOrWhiteSpace(tbxProCurrentAddress.Text))
            {
                MessageBox.Show("Some fields are missing");
            }
            else
            {
                try
                {
                    var _queryInsert = "INSERT INTO tbl_ProfessionalEmployer (EmployerName, EmployerAddress) VALUES (@EmployerName, @EmployerAddress)";

                    _connection.Open();
                    _command.Connection = _connection;
                    _command = new OleDbCommand(_queryInsert, _connection);
                    _command.Parameters.AddWithValue("@EmployerName", tbxProCurrentEmployer.Text);
                    _command.Parameters.AddWithValue("@EmployerAddress", tbxProCurrentAddress.Text);
                    _command.ExecuteNonQuery();
                    _connection.Close();
                    _professional.LoadCurrentEmployerAndAddress();
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
            tbxProCurrentEmployer.Clear();
            tbxProCurrentAddress.Clear();
        }
    }
}
