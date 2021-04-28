using System.IO;
using System.Windows.Forms;

namespace InformationManagementSystem
{
    public class DirectoryHelper
    {
        private DirectoryInfo _hideDirectory;
        private readonly string _directoryImage = Application.StartupPath + @"\Pictures\";
        private readonly string _directoryBackup = Application.StartupPath + @"\Backup\";
        private readonly string _directoryStudent = Application.StartupPath + @"\Pictures\Student\";
        private readonly string _directoryProfessional = Application.StartupPath + @"\Pictures\Professional\";

        public void CreateDirectory()
        {
            if (!File.Exists(_directoryImage) || !File.Exists(_directoryBackup) || !File.Exists(_directoryStudent) || !File.Exists(_directoryProfessional))
            {
                Directory.CreateDirectory(_directoryImage);
                _hideDirectory = new DirectoryInfo(_directoryImage)
                {
                    Attributes = FileAttributes.Normal
                };

                Directory.CreateDirectory(_directoryBackup);
                _hideDirectory = new DirectoryInfo(_directoryBackup)
                {
                    Attributes = FileAttributes.Hidden
                };
                Directory.CreateDirectory(_directoryStudent);
                _hideDirectory = new DirectoryInfo(_directoryStudent)
                {
                    Attributes = FileAttributes.Normal
                };

                Directory.CreateDirectory(_directoryProfessional);
                _hideDirectory = new DirectoryInfo(_directoryProfessional)
                {
                    Attributes = FileAttributes.Normal
                };
            }
            else
                return;
        }
    }
}