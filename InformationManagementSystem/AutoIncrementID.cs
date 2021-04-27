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
        private readonly List<int> idList = new List<int>();

        public void Increment(TextBox id, string query, string quaryID)
        {
            _connection.ConnectionString = DatabaseConnection.GetConnection();

            try
            {
                var _querySearch = query;

                _connection.Open();
                _command.Connection = _connection;
                _command = new OleDbCommand(_querySearch, _connection);
                _reader = _command.ExecuteReader();

                while (_reader.Read())
                {
                    var fullID = _reader[quaryID].ToString();
                    var idNumber = fullID.Split('-');
                    var convertedID = Convert.ToInt32(idNumber[2]);
                    idList.Add(convertedID);
                }
                idList.Sort();
                foreach (var item in idList)
                {
                    var increment = item + 1;
                    var returnID = id.Text = increment.ToString();
                    GetIncrementedID(returnID);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to increment professional id " + ex.Message);
            }
        }

        public string GetIncrementedID(string returnID)
        {
            var id = returnID;
            return id;
        }
    }
}