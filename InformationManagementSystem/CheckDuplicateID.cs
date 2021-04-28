using System;
using System.Data.OleDb;
using System.Windows.Forms;

namespace InformationManagementSystem
{
    public class CheckDuplicateID
    {
        private readonly OleDbConnection _connection = new OleDbConnection();
        private OleDbCommand _command = new OleDbCommand();


        public void CheckDuplicate(string _existingID, string _queryCheck, string _queryCheckID)
        {
            _connection.ConnectionString = DatabaseHelper.GetConnection();

            if (_queryCheckID == "@StudentID")
            {
                try
                {
                    var _student = new FrmNewStudent(null);
                    var existingID = _existingID;
                    var checkQuary = _queryCheck;
                    _connection.Open();
                    _command.Connection = _connection;
                    _command = new OleDbCommand(checkQuary, _connection);
                    _command.Parameters.AddWithValue(_queryCheckID, existingID);

                    var exist = Convert.ToInt32(_command.ExecuteScalar());

                    if (exist == 0)
                    {
                        _connection.Close();
                        _student.InsertData();
                    }
                    else
                    {
                        _connection.Close();
                        MessageBox.Show("This ID Number is already exist");
                    }
                    _connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error in checking the id " + ex.Message);
                }
            }
            else
            {
                try
                {
                    var _professional = new FrmNewProfessional(null);
                    var existingID = _existingID;
                    var checkQuary = _queryCheck;
                    _connection.Open();
                    _command.Connection = _connection;
                    _command = new OleDbCommand(checkQuary, _connection);
                    _command.Parameters.AddWithValue(_queryCheckID, existingID);

                    var exist = Convert.ToInt32(_command.ExecuteScalar());

                    if (exist == 0)
                    {
                        _connection.Close();
                        _professional.InsertData();
                    }
                    else
                    {
                        _connection.Close();
                        MessageBox.Show("This ID Number is already exist");
                    }
                    _connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error in checking the id " + ex.Message);
                }
            }
        }
    }
}