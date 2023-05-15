using System.IO;
using System.Windows.Forms;

namespace InformationManagementSystem
{
    public static class DatabaseHelper
    {
        public static string GetConnection()
        {
            string _connection = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = " + Application.StartupPath + @"\ICEPEP.accdb; Jet OLEDB:Database Password = 121798";

            return _connection;
        }

        public static void BackupDataBase()
        {
            var dateBaseName = "ICEPEP.mdb";
            var sourcePath = Application.StartupPath;
            var targetPath = Application.StartupPath + @"\Backup\";

            var sourceFile = Path.Combine(sourcePath, dateBaseName);
            var destinationFile = Path.Combine(targetPath, dateBaseName);

            File.Copy(sourceFile, destinationFile, true);
        }
    }
}