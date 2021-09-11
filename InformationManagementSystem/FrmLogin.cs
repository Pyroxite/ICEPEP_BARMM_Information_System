using System;
using System.Data.OleDb;
using System.Windows.Forms;

namespace InformationManagementSystem
{
    public partial class FrmLogin : Form
    {
        private readonly OleDbConnection _connection = new OleDbConnection();
        private OleDbCommand _command = new OleDbCommand();
        private OleDbDataReader _reader;

        public FrmLogin()
        {
            InitializeComponent();
            _connection.ConnectionString = DatabaseHelper.GetConnection();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            FrmMain _main = new FrmMain(this);
            if (!string.IsNullOrWhiteSpace(tbxLoginUsername.Text) || !string.IsNullOrWhiteSpace(tbxLoginPassword.Text))
            {
                try
                {
                    var _querySelect = "SELECT * FROM tbl_Account WHERE Username=@Username AND Password=@Password";

                    _connection.Open();
                    _command.Connection = _connection;
                    _command = new OleDbCommand(_querySelect, _connection);
                    _command.Parameters.AddWithValue("@Username", tbxLoginUsername.Text);
                    _command.Parameters.AddWithValue("@Password", tbxLoginPassword.Text);
                    _reader = _command.ExecuteReader();

                    if (_reader.Read())
                    {
                        _main.Show();
                        _main.tspUser.Text = tbxLoginUsername.Text;
                        ClearFields();
                        Hide();
                    }
                    else
                        MessageBox.Show("Username or Password are not available");

                    _reader.Close();
                    _connection.Close();
                }
                catch (Exception ex)
                {
                    _connection.Close();
                    MessageBox.Show("Failed to login " , ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Missing Fields");
            }
        }

        private void ClearFields()
        {
            tbxLoginUsername.Clear();
            tbxLoginPassword.Clear();
        }
    }
}
