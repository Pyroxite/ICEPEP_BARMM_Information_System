using System.Windows.Forms;

namespace InformationManagementSystem
{
    public static class DatabaseConnection
    {
        public static string GetConnection()
        {
            string _connection = @"Provider = Microsoft.Jet.OLEDB.4.0; Data Source = " + Application.StartupPath + @"\ICEPEP.mdb; Jet OLEDB:Database Password = 121798";

            return _connection;
        }
    }
}