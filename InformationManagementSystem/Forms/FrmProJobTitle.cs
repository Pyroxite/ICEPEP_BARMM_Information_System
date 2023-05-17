using System;
using System.Data.OleDb;
using System.Windows.Forms;

namespace InformationManagementSystem
{
    public partial class FrmProJobTitle : Form
    {
        private readonly FrmNewProfessional _professional;
        private readonly OleDbConnection _connection = new OleDbConnection();
        private OleDbCommand _command = new OleDbCommand();

        public FrmProJobTitle(FrmNewProfessional professional)
        {
            InitializeComponent();
            _connection.ConnectionString = DatabaseHelper.GetConnection();
            _professional = professional;
        }

        private void BtnProJobTitleAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbxProJobTitle.Text))
            {
                MessageBox.Show("Missing Fields");
            }
            else
            {
                try
                {
                    var _queryInsert = "INSERT INTO tbl_ProfessionalJobTitle (JobTitle) VALUES (@JobTitle)";

                    _connection.Open();
                    _command.Connection = _connection;
                    _command = new OleDbCommand(_queryInsert, _connection);
                    _command.Parameters.AddWithValue("@JobTitle", tbxProJobTitle.Text);
                    _command.ExecuteNonQuery();
                    _connection.Close();
                    _professional.LoadJobTitle();
                    ClearField();
                    MessageBox.Show("New record has been added");
                }
                catch (Exception)
                {
                    _connection.Close();
                    MessageBox.Show("Connection Failed");
                }
            }
        }

        private void ClearField()
        {
            tbxProJobTitle.Clear();
        }
    }
}
