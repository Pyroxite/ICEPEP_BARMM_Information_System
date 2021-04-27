using System;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace InformationManagementSystem
{
    public partial class FrmNewStudent : Form
    {
        private readonly OleDbConnection _connection = new OleDbConnection();
        private OleDbCommand _command = new OleDbCommand();
        private OleDbDataReader _reader;
        private readonly FrmMain _main;
        private AutoIncrementID _autoIncrement = new AutoIncrementID();

        public FrmNewStudent(FrmMain main)
        {
            InitializeComponent();
            _connection.ConnectionString = DatabaseConnection.GetConnection();
            _main = main;
            LockDateTime();
            LoadCurrentSchool();
            LoadSeniorHigh();
            LoadCourse();
            LoadYearLevel();
            AutoIncrementStudentID();
            lblStudentDateID.Text = DateTime.Now.ToString("yyyy");
            tbxStudentRegionChapter.Text = "BARMM";
        }

        private void AutoIncrementStudentID()
        {
            var query = "SELECT tbl_StudentInformation.StudentID FROM tbl_StudentInformation";
            var queryID = "StudentID";
            _autoIncrement.Increment(tbxStudentID, query, queryID);
            _autoIncrement.GetIncrementedID(tbxStudentID.Text);
        }

        public void BackupDataBase()
        {
            var dateBaseName = "ICEPEP.mdb";
            var sourcePath = Application.StartupPath;
            var targetPath = Application.StartupPath + @"\Backup\";

            var sourceFile = Path.Combine(sourcePath, dateBaseName);
            var destinationFile = Path.Combine(targetPath, dateBaseName);

            File.Copy(sourceFile, destinationFile, true);
        }

        private void ClearFields()
        {
            tbxStudentFirstName.Clear();
            tbxStudentMiddleName.Clear();
            tbxStudentLastName.Clear();
            tbxStudentSuffixName.Clear();
            tbxStudentID.Clear();
            tbxStudentEmailAddress.Clear();
            tbxStudentRegionChapter.Text = "";
            tbxStudentContact.Clear();
            tbxStudentPresentAddress.Clear();

            Image image;
            var profilePath = "default.jpg";
            var applicationPath = Application.StartupPath + @"\Pictures\";
            var getProfile = Path.Combine(applicationPath, profilePath);

            using (Stream stream = File.OpenRead(getProfile))
                image = Image.FromStream(stream);

            pbxStudentPicture.Image = image;

            cbxStudentCurrentSchool.Text = "";
            cbxStudentCourse.Text = "";
            cbxStudentYear.Text = "";
            tbxStudentSchoolAddress.Clear();
            tbxStudentSpecializations.Clear();
            cbxStudentHighSchool.Text = "";
            tbxStudentStrand.Clear();
            dtpStudentYearGraduated.Value = DateTime.Now;
            chxStudentRegularMember.Checked = false;
            chxStudentAssociateMember.Checked = false;
            dtpStudentDateSigned.Value = DateTime.Now;
            chxStudentTransferee.Checked = false;
        }

        private void LockDateTime()
        {
            if (chxStudentRegularMember.Checked == false && chxStudentAssociateMember.Checked == false)
                dtpStudentDateSigned.Enabled = false;
        }

        private void UnlockDateTime()
        {
            dtpStudentDateSigned.Enabled = true;
        }

        private void LoadCourse()
        {
            try
            {
                cbxStudentCourse.Items.Clear();
                var selectQuary = "SELECT tbl_StudentCourse.CourseName FROM tbl_StudentCourse WHERE CourseID";

                _connection.Open();
                _command.Connection = _connection;
                _command = new OleDbCommand(selectQuary, _connection);
                _reader = _command.ExecuteReader();

                while (_reader.Read())
                {
                    cbxStudentCourse.Items.Add(_reader["CourseName"].ToString());
                }
                _reader.Close();
                _connection.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to load course");
            }
        }

        private void LoadYearLevel()
        {
            try
            {
                cbxStudentYear.Items.Clear();
                var selectQuary = "SELECT tbl_StudentYearLevel.YearLevelName FROM tbl_StudentYearLevel WHERE YearLevelID";

                _connection.Open();
                _command.Connection = _connection;
                _command = new OleDbCommand(selectQuary, _connection);
                _reader = _command.ExecuteReader();

                while (_reader.Read())
                {
                    cbxStudentYear.Items.Add(_reader["YearLevelName"].ToString());
                }
                _reader.Close();
                _connection.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to load year level");
            }
        }

        public void LoadSeniorHigh()
        {
            try
            {
                cbxStudentHighSchool.Items.Clear();
                var selectQuary = "SELECT tbl_StudentHighSchool.HighSchoolName FROM tbl_StudentHighSchool WHERE HighSchoolID";

                _connection.Open();
                _command.Connection = _connection;
                _command = new OleDbCommand(selectQuary, _connection);
                _reader = _command.ExecuteReader();

                while (_reader.Read())
                {
                    cbxStudentHighSchool.Items.Add(_reader["HighSchoolName"].ToString());
                }
                _reader.Close();
                _connection.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to load senior high school");
            }
        }

        public void LoadCurrentSchool()
        {
            try
            {
                cbxStudentCurrentSchool.Items.Clear();

                var selectQuary = "SELECT tbl_StudentCurrentSchool.CurrentSchool FROM tbl_StudentCurrentSchool WHERE CurrentSchoolID";

                _connection.Open();
                _command.Connection = _connection;
                _command = new OleDbCommand(selectQuary, _connection);
                _reader = _command.ExecuteReader();

                while (_reader.Read())
                {
                    cbxStudentCurrentSchool.Items.Add(_reader["CurrentSchool"].ToString());
                }
                _reader.Close();
                _connection.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to load current school list");
            }
        }

        private string ValidTime()
        {
            var dateSigned = dtpStudentDateSigned.Value.ToString("MM/dd/yyyy");
            var validUntil = DateTime.Parse(dateSigned);
            return validUntil.AddYears(1).ToString("MM/dd/yyyy");
        }

        private void InsertData()
        {
            if (string.IsNullOrWhiteSpace(tbxStudentID.Text) || string.IsNullOrWhiteSpace(tbxStudentFirstName.Text) || string.IsNullOrWhiteSpace(tbxStudentMiddleName.Text) || string.IsNullOrWhiteSpace(tbxStudentLastName.Text) || string.IsNullOrWhiteSpace(tbxStudentEmailAddress.Text) || string.IsNullOrWhiteSpace(tbxStudentRegionChapter.Text) || string.IsNullOrWhiteSpace(tbxStudentContact.Text) || string.IsNullOrWhiteSpace(tbxStudentPresentAddress.Text) || string.IsNullOrWhiteSpace(cbxStudentCurrentSchool.Text) || string.IsNullOrWhiteSpace(cbxStudentCourse.Text) || string.IsNullOrWhiteSpace(cbxStudentYear.Text) || string.IsNullOrWhiteSpace(tbxStudentSchoolAddress.Text) || chxStudentRegularMember.Checked == false && chxStudentAssociateMember.Checked == false)
            {
                MessageBox.Show("Some fields are missing");
                return;
            }
            else
            {
                try
                {
                    pbxStudentPicture.Image.Save(Application.StartupPath + @"\Pictures\Student\" + lblStudentBarmmID.Text + "-" + lblStudentDateID.Text + "-" + tbxStudentID.Text + ".jpg");
                    pbxStudentPicture.Image.Dispose();

                    var studentID = lblStudentBarmmID.Text + lblStudentIndentOne.Text + lblStudentDateID.Text + lblStudentIndentTwo.Text + tbxStudentID.Text;
                    var imagePath = Application.StartupPath + @"\Pictures\Student\" + lblStudentBarmmID.Text + "-" + lblStudentDateID.Text + "-" + tbxStudentID.Text + ".jpg";

                    var _queryInsert = "INSERT INTO tbl_StudentInformation " +
                        "(StudentID," +
                        "StudentFirstName," +
                        "StudentMiddleName," +
                        "StudentLastName," +
                        "StudentSuffixName," +
                        "StudentEmailAddress," +
                        "StudentRegionChapter," +
                        "StudentContactNumber," +
                        "StudentPresentAddress," +
                        "StudentPicture," +
                        "StudentCurrentSchool," +
                        "StudentCourse," +
                        "StudentYear," +
                        "StudentSchoolAddress," +
                        "StudentSpecialization," +
                        "StudentHighSchool," +
                        "StudentStrand," +
                        "StudentYearGraduated," +
                        "StudentStatus," +
                        "StudentDateSigned," +
                        "StudentValidUntil," +
                        "StudentIsActive, " +
                        "StudentIsTransferee)" +
                        "VALUES " +
                        "(@StudentID, " +
                        "@StudentFirstName," +
                        "@StudentMiddleName," +
                        "@StudentLastName," +
                        "@StudentSuffixName," +
                        "@StudentEmailAddress," +
                        "@StudentRegionChapter," +
                        "@StudentContactNumber," +
                        "@StudentPresentAddress," +
                        "@StudentPicture," +
                        "@StudentCurrentSchool," +
                        "@StudentCourse," +
                        "@StudentYear," +
                        "@StudentSchoolAddress," +
                        "@StudentSpecialization," +
                        "@StudentHighSchool, " +
                        "@StudentStrand, " +
                        "@StudentYearGraduated, " +
                        "@StudentStatus, " +
                        "@StudentDateSigned, " +
                        "@StudentValidUntil, " +
                        "@StudentIsActive, " +
                        "@StudentIsTransferee)";

                    _connection.Open();
                    _command.Connection = _connection;
                    _command = new OleDbCommand(_queryInsert, _connection);
                    _command.Parameters.AddWithValue("@StudentID", studentID);
                    _command.Parameters.AddWithValue("@StudentFirstName", tbxStudentFirstName.Text);
                    _command.Parameters.AddWithValue("@StudentMiddleName", tbxStudentMiddleName.Text);
                    _command.Parameters.AddWithValue("@StudentLastName", tbxStudentLastName.Text);
                    _command.Parameters.AddWithValue("@StudentSuffixName", tbxStudentSuffixName.Text);
                    _command.Parameters.AddWithValue("@StudentEmailAddress", tbxStudentEmailAddress.Text);
                    _command.Parameters.AddWithValue("@StudentRegionChapter", tbxStudentRegionChapter.Text);
                    _command.Parameters.AddWithValue("@StudentContactNumber", tbxStudentContact.Text);
                    _command.Parameters.AddWithValue("@StudentPresentAddress", tbxStudentPresentAddress.Text);
                    _command.Parameters.AddWithValue("@StudentPicture", imagePath);
                    _command.Parameters.AddWithValue("@StudentCurrentSchool", cbxStudentCurrentSchool.Text);
                    _command.Parameters.AddWithValue("@StudentCourse", cbxStudentCourse.Text);
                    _command.Parameters.AddWithValue("@StudentYear", cbxStudentYear.Text);
                    _command.Parameters.AddWithValue("@StudentSchoolAddress", tbxStudentSchoolAddress.Text);
                    _command.Parameters.AddWithValue("@StudentSpecialization", tbxStudentSpecializations.Text);
                    _command.Parameters.AddWithValue("@StudentHighSchool", cbxStudentHighSchool.Text);
                    _command.Parameters.AddWithValue("@StudentStrand", tbxStudentStrand.Text);
                    _command.Parameters.AddWithValue("@StudentYearGraduated", dtpStudentYearGraduated.Value.ToString("MM/dd/yyyy"));

                    if (chxStudentRegularMember.Checked == true)
                        _command.Parameters.AddWithValue("@StudentStatus", "Regular");
                    else if (chxStudentAssociateMember.Checked == true)
                        _command.Parameters.AddWithValue("@StudentStatus", "Associate");

                    _command.Parameters.AddWithValue("@StudentDateSigned", dtpStudentDateSigned.Value.ToString("MM/dd/yyyy"));
                    _command.Parameters.AddWithValue("@StudentValidUntil", ValidTime());
                    _command.Parameters.AddWithValue("@StudentIsActive", "Yes");

                    if (chxStudentTransferee.Checked == true)
                        _command.Parameters.AddWithValue("@StudentIsTransferee", "Yes");
                    else if (chxStudentTransferee.Checked == false)
                        _command.Parameters.AddWithValue("@StudentIsTransferee", "No");

                    _command.ExecuteNonQuery();
                    _connection.Close();
                    _main.LoadListOfStudents();
                    ClearFields();

                    MessageBox.Show("New record has been added");
                }
                catch (Exception)
                {
                    _connection.Close();
                    MessageBox.Show("Connection Failed");
                }
            }
        }

        private void UpdateMember()
        {
            try
            {
                var existingID = lblStudentBarmmID.Text + lblStudentIndentOne.Text + lblStudentDateID.Text + lblStudentIndentTwo.Text + tbxStudentID.Text;

                var imagePath = Application.StartupPath + @"\Pictures\Student\" + existingID + ".jpg";

                Bitmap newImage = new Bitmap(pbxStudentPicture.Image);
                if (File.Exists(imagePath))
                    File.Delete(imagePath);
                newImage.Save(imagePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                newImage.Dispose();

                var _queryUpdate = "UPDATE tbl_StudentInformation SET StudentFirstName = @StudentFirstName, StudentMiddleName = @StudentMiddleName, StudentLastName = @StudentLastName, StudentSuffixName = @StudentSuffixName, StudentEmailAddress = @StudentEmailAddress, StudentRegionChapter = @StudentRegionChapter, StudentContactNumber = @StudentContactNumber, StudentPresentAddress = @StudentPresentAddress, StudentPicture = @StudentPicture, StudentCurrentSchool = @StudentCurrentSchool, StudentCourse = @StudentCourse, StudentYear = @StudentYear, StudentSchoolAddress = @StudentSchoolAddress, StudentSpecialization = @StudentSpecialization, StudentHighSchool = @StudentHighSchool, StudentStrand = @StudentStrand, StudentYearGraduated = @StudentYearGraduated WHERE StudentID LIKE @StudentID";

                _connection.Open();
                _command.Connection = _connection;
                _command = new OleDbCommand(_queryUpdate, _connection);
                _command.Parameters.AddWithValue("@StudentFirstName", tbxStudentFirstName.Text);
                _command.Parameters.AddWithValue("@StudentMiddleName", tbxStudentMiddleName.Text);
                _command.Parameters.AddWithValue("@StudentLastName", tbxStudentLastName.Text);
                _command.Parameters.AddWithValue("@StudentSuffixName", tbxStudentSuffixName.Text);
                _command.Parameters.AddWithValue("@StudentEmailAddress", tbxStudentEmailAddress.Text);
                _command.Parameters.AddWithValue("@StudentRegionChapter", tbxStudentRegionChapter.Text);
                _command.Parameters.AddWithValue("@StudentContactNumber", tbxStudentContact.Text);
                _command.Parameters.AddWithValue("@StudentPresentAddress", tbxStudentPresentAddress.Text);
                _command.Parameters.AddWithValue("@StudentPicture", imagePath);
                _command.Parameters.AddWithValue("@StudentCurrentSchool", cbxStudentCurrentSchool.Text);
                _command.Parameters.AddWithValue("@StudentCourse", cbxStudentCourse.Text);
                _command.Parameters.AddWithValue("@StudentYear", cbxStudentYear.Text);
                _command.Parameters.AddWithValue("@StudentSchoolAddress", tbxStudentSchoolAddress.Text);
                _command.Parameters.AddWithValue("@StudentSpecialization", tbxStudentSpecializations.Text);
                _command.Parameters.AddWithValue("@StudentHighSchool", cbxStudentHighSchool.Text);
                _command.Parameters.AddWithValue("@StudentStrand", tbxStudentStrand.Text);
                _command.Parameters.AddWithValue("@StudentYearGraduated", dtpStudentYearGraduated.Value.ToString("MM/dd/yyyy"));
                _command.Parameters.AddWithValue("@StudentID", existingID);

                _command.ExecuteNonQuery();
                _connection.Close();
                _main.LoadListOfStudents();
                ClearFields();
                MessageBox.Show("This member has been updated");
            }
            catch (Exception)
            {
                _connection.Close();
                MessageBox.Show("Failed to update");
            }
        }

        private void CheckIDNumber()
        {
            try
            {
                var existingID = lblStudentBarmmID.Text + lblStudentIndentOne.Text + lblStudentDateID.Text + lblStudentIndentTwo.Text + tbxStudentID.Text;
                var checkQuary = "SELECT COUNT(*) FROM tbl_StudentInformation WHERE StudentID = @StudentID";
                _connection.Open();
                _command.Connection = _connection;
                _command = new OleDbCommand(checkQuary, _connection);
                _command.Parameters.AddWithValue("@StudentID", existingID);

                var exist = Convert.ToInt32(_command.ExecuteScalar());

                if (exist == 0)
                {
                    _connection.Close();
                    InsertData();
                }
                else
                {
                    _connection.Close();
                    MessageBox.Show("This ID Number is already exist");
                }
                _connection.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Error in checking the id");
            }
        }

        private void AddImage()
        {
            var openImage = new OpenFileDialog
            {
                Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp, *.png)|*.jpg; *.jpeg; *.gif; *.bmp, *.png"
            };
            if (openImage.ShowDialog() == DialogResult.OK)
                pbxStudentPicture.Image = new Bitmap(openImage.FileName);
        }

        private void btnStudentSave_Click(object sender, EventArgs e)
        {
            CheckIDNumber();
            BackupDataBase();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            AddImage();
        }

        private void chxRegularMember_CheckedChanged(object sender, EventArgs e)
        {
            if (chxStudentRegularMember.Checked == true)
            {
                UnlockDateTime();
                chxStudentAssociateMember.Checked = false;
                chxStudentAssociateMember.CheckState = CheckState.Unchecked;
            }
            else
            {
                LockDateTime();
            }
        }

        private void chxAssociateMember_CheckedChanged(object sender, EventArgs e)
        {
            if (chxStudentAssociateMember.Checked == true)
            {
                UnlockDateTime();
                chxStudentRegularMember.Checked = false;
                chxStudentRegularMember.CheckState = CheckState.Unchecked;
            }
            else
            {
                LockDateTime();
            }
        }

        private void btnStudentAddCurrentSchool_Click(object sender, EventArgs e)
        {
            var addCurrentSchool = new FrmStudentCurrentSchool(this);
            addCurrentSchool.Show();
        }

        private void cbxStudentCurrentSchool_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var selectQuary = "SELECT tbl_StudentCurrentSchool.CurrentSchoolAddress FROM tbl_StudentCurrentSchool WHERE CurrentSchool LIKE @CurrentSchool";

                _connection.Open();
                _command.Connection = _connection;
                _command = new OleDbCommand(selectQuary, _connection);
                _command.Parameters.AddWithValue("@CurrentSchool", cbxStudentCurrentSchool.Text);
                _reader = _command.ExecuteReader();

                while (_reader.Read())
                {
                    tbxStudentSchoolAddress.Text = _reader["CurrentSchoolAddress"].ToString();
                }
                _reader.Close();
                _connection.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to load current school address");
            }
        }

        private void btnStudentAddHighSchool_Click(object sender, EventArgs e)
        {
            var seniorHigh = new FrmStudentSeniorHigh(this);
            seniorHigh.Show();
        }

        private void btnStudentCancel_Click(object sender, EventArgs e)
        {
            ClearFields();
            Dispose();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateMember();
            BackupDataBase();
        }
    }
}