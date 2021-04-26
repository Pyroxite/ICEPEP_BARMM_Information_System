using System;
using System.Data.OleDb;
using System.Windows.Forms;

namespace InformationManagementSystem
{
    public partial class FrmUserAccount : Form
    {
        private OleDbConnection _connection = new OleDbConnection();
        private OleDbCommand _command = new OleDbCommand();
        private OleDbDataReader _reader;
        public FrmUserAccount()
        {
            InitializeComponent();
            _connection.ConnectionString = DatabaseConnection.GetConnection();
            LoadAccount();
        }

        private void LoadAccount()
        {
            try
            {
                dgvAccounts.Rows.Clear();
                var rowIndex = 0;
                var selectQuary = "SELECT tbl_Account.Username, tbl_Account.Password FROM tbl_Account";

                _connection.Open();
                _command.Connection = _connection;
                _command = new OleDbCommand(selectQuary, _connection);
                _reader = _command.ExecuteReader();

                while (_reader.Read())
                {
                    rowIndex++;
                    dgvAccounts.Rows.Add(rowIndex,
                        _reader["Username"].ToString(),
                        _reader["Password"].ToString());
                }
                _reader.Close();
                _connection.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to load account list");
            }
        }
    }
}
