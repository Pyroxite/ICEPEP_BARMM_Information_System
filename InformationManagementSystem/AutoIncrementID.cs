using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;

namespace InformationManagementSystem
{
    public class AutoIncrementID
    {
        private readonly OleDbConnection _connection = new OleDbConnection();
        private OleDbCommand _command = new OleDbCommand();
        private OleDbDataReader _reader;
        private readonly List<int> _splittedID = new List<int>();

        private void InitializedConnection()
        {
            _connection.ConnectionString = DatabaseConnection.GetConnection();
        }

        public void IncrementID(TextBox tbxID, string query, string quaryID)
        {
            InitializedConnection();
            try
            {
                var querySearch = query;
                _connection.Open();
                _command.Connection = _connection;
                _command = new OleDbCommand(querySearch, _connection);
                _reader = _command.ExecuteReader();

                while (_reader.Read())
                {
                    var fullID = _reader[quaryID].ToString();
                    var idNumber = fullID.Split('-');
                    var convertedID = Convert.ToInt32(idNumber[2]);
                    _splittedID.Add(convertedID);
                }
                _splittedID.Sort();
                foreach (var id in _splittedID)
                {
                    var increment = id + 1;
                    var returnID = tbxID.Text = increment.ToString();
                    GetIncrementedID(returnID);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to increment id " + ex.Message);
            }
        }

        public string GetIncrementedID(string returnID)
        {
            var id = returnID;
            return id;
        }
    }
}