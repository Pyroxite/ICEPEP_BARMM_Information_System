using System.IO;
using System.Windows.Forms;

namespace InformationManagementSystem
{
    public static class DatabaseHelper
    {
        public static string GetConnection()
        {
            string _connection = @"Provider = Microsoft.Jet.OLEDB.4.0; Data Source = " + Application.StartupPath + @"\ICEPEP.mdb; Jet OLEDB:Database Password = 121798";

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