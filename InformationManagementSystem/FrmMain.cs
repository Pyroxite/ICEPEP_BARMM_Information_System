using System;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace InformationManagementSystem
{
    public partial class FrmMain : Form
    {
        private OleDbConnection _connection = new OleDbConnection();
        private OleDbCommand _command = new OleDbCommand();
        private OleDbDataReader _reader;
        private FrmNewStudent _student;
        private readonly DirectoryHelper _helper = new DirectoryHelper();

        public FrmMain()
        {
            InitializeComponent();
            _connection.ConnectionString = DatabaseHelper.GetConnection();
            _helper.CreateDirectory();
            LoadListOfStudents();
            LoadRegularStudents();
            LoadAssociateStudents();
            LoadRegularStudents();
            LoadListOfProfessionals();
        }

        #region Clear

        private void ClearFields()
        {
            tbxStudentBasicID.Clear();
            tbxStudentBasicFirstName.Clear();
            tbxStudentBasicLastName.Clear();
            tbxStudentBasicCourse.Clear();
            tbxStudentBasicYear.Clear();
            tbxStudentBasicContact.Clear();
            tbxStudentBasicEmailAddress.Clear();
            tbxStudentBasicPresentAddress.Clear();
            tbxStudentBasicSpecializations.Clear();
            tbxStudentBasicDateSigned.Clear();
            tbxStudentBasicValidUntil.Clear();
            chxStudentBasicActive.Checked = false;

            Image image;
            var profilePath = "default.jpg";
            var applicationPath = Application.StartupPath + @"\Pictures\";
            var getProfile = Path.Combine(applicationPath, profilePath);

            using (Stream stream = File.OpenRead(getProfile))
                image = Image.FromStream(stream);

            pbxStudentBasicPicture.Image = image;
        }

        #endregion Clear

        #region TotalMembers

        private void LoadRegularStudents()
        {
            try
            {
                var isRegular = "Regular";
                var selectQuary = "SELECT COUNT(*) FROM tbl_StudentInformation WHERE StudentStatus LIKE @StudentStatus";
                var regularCount = 0;

                _connection.Open();
                _command.Connection = _connection;
                _command = new OleDbCommand(selectQuary, _connection);
                _command.Parameters.AddWithValue("StudentStatus", isRegular);
                regularCount = Convert.ToInt32(_command.ExecuteScalar());
                tsslTotalStudents.Text = regularCount.ToString();
                _reader.Close();
                _connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load the total of regular students " + ex.Message);
            }
        }

        private void LoadAssociateStudents()
        {
            try
            {
                var isAssociate = "Associate";
                var selectQuary = "SELECT COUNT(*) FROM tbl_StudentInformation WHERE StudentStatus LIKE @StudentStatus";
                var associateCount = 0;

                _connection.Open();
                _command.Connection = _connection;
                _command = new OleDbCommand(selectQuary, _connection);
                _command.Parameters.AddWithValue("StudentStatus", isAssociate);
                associateCount = Convert.ToInt32(_command.ExecuteScalar());
                tsslTotalAssociateStudents.Text = associateCount.ToString();
                _reader.Close();
                _connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load the total of regular students " + ex.Message);
            }
        }

        #endregion TotalMembers

        #region StudentTab

        public void LoadListOfStudents()
        {
            try
            {
                dgvStudents.Rows.Clear();
                var rowIndex = 0;
                var selectQuary = "SELECT tbl_StudentInformation.StudentID, tbl_StudentInformation.StudentFirstName, tbl_StudentInformation.StudentLastName, tbl_StudentInformation.StudentEmailAddress, tbl_StudentInformation.StudentContactNumber, tbl_StudentInformation.StudentPresentAddress, tbl_StudentInformation.StudentStatus, tbl_StudentInformation.StudentDateSigned, tbl_StudentInformation.StudentValidUntil, tbl_StudentInformation.StudentIsActive, tbl_StudentInformation.StudentCourse, tbl_StudentInformation.StudentYear, tbl_StudentInformation.StudentSpecialization, tbl_StudentInformation.StudentIsTransferee FROM tbl_StudentInformation ORDER BY tbl_StudentInformation.StudentID";

                _connection.Open();
                _command.Connection = _connection;
                _command = new OleDbCommand(selectQuary, _connection);
                _reader = _command.ExecuteReader();

                while (_reader.Read())
                {
                    rowIndex++;
                    dgvStudents.Rows.Add(rowIndex,
                        _reader["StudentID"].ToString(),
                        _reader["StudentFirstName"].ToString(),
                        _reader["StudentLastName"].ToString(),
                        _reader["StudentStatus"].ToString(),
                        _reader["StudentValidUntil"].ToString(),
                        _reader["StudentCourse"].ToString(),
                        _reader["StudentYear"].ToString(),
                        _reader["StudentContactNumber"].ToString(),
                        _reader["StudentEmailAddress"].ToString(),
                        _reader["StudentPresentAddress"].ToString(),
                        _reader["StudentSpecialization"].ToString(),
                        _reader["StudentDateSigned"].ToString(),
                        _reader["StudentIsActive"].ToString(),
                        _reader["StudentIsTransferee"].ToString());
                }
                _reader.Close();
                _connection.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to load students list");
            }
        }

        private void DeleteStudentMember()
        {
            if (string.IsNullOrWhiteSpace(tbxStudentBasicID.Text))
            {
                MessageBox.Show("Select a member to delete");
            }
            else
            {
                if (MessageBox.Show("Are you sure you want to delete this member?", "ICEPEP", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    try
                    {
                        var imagePath = Application.StartupPath + @"\Pictures\Student\" + tbxStudentBasicID.Text + ".jpg";
                        var _queryDelete = "DELETE FROM tbl_StudentInformation WHERE StudentID=@StudentID";

                        _connection.Open();
                        _command.Connection = _connection;
                        _command = new OleDbCommand(_queryDelete, _connection);
                        _command.Parameters.AddWithValue("@StudentID", tbxStudentBasicID.Text);
                        _command.ExecuteNonQuery();
                        _connection.Close();
                        LoadListOfStudents();

                        if (File.Exists(imagePath))
                            File.Delete(imagePath);
                        else
                            return;

                        ClearFields();
                    }
                    catch (Exception)
                    {
                        _connection.Close();
                        MessageBox.Show("Failed to delete member");
                    }
                }
            }
        }

        #region AddNewStudent

        private void tspMenuStudent_Click(object sender, EventArgs e)
        {
            var newStudents = new FrmNewStudent(this);
            newStudents.Show();
        }

        #endregion AddNewStudent

        #region StudentBasicInfo

        private void dgvStudents_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                tbxStudentBasicID.Text = dgvStudents.Rows[e.RowIndex].Cells[1].Value.ToString();
                tbxStudentBasicFirstName.Text = dgvStudents.Rows[e.RowIndex].Cells[2].Value.ToString();
                tbxStudentBasicLastName.Text = dgvStudents.Rows[e.RowIndex].Cells[3].Value.ToString();
                tbxStudentBasicValidUntil.Text = dgvStudents.Rows[e.RowIndex].Cells[5].Value.ToString();
                tbxStudentBasicCourse.Text = dgvStudents.Rows[e.RowIndex].Cells[6].Value.ToString();
                tbxStudentBasicYear.Text = dgvStudents.Rows[e.RowIndex].Cells[7].Value.ToString();
                tbxStudentBasicContact.Text = dgvStudents.Rows[e.RowIndex].Cells[8].Value.ToString();
                tbxStudentBasicEmailAddress.Text = dgvStudents.Rows[e.RowIndex].Cells[9].Value.ToString();
                tbxStudentBasicPresentAddress.Text = dgvStudents.Rows[e.RowIndex].Cells[10].Value.ToString();
                tbxStudentBasicSpecializations.Text = dgvStudents.Rows[e.RowIndex].Cells[11].Value.ToString();
                tbxStudentBasicDateSigned.Text = dgvStudents.Rows[e.RowIndex].Cells[12].Value.ToString();

                if (dgvStudents.Rows[e.RowIndex].Cells[13].Value.ToString() == "Yes")
                    chxStudentBasicActive.Checked = true;
                else
                    chxStudentBasicActive.Checked = false;

                Image image;
                var profilePath = tbxStudentBasicID.Text + ".jpg";
                var applicationPath = Application.StartupPath + @"\Pictures\Student\";
                var getProfile = Path.Combine(applicationPath, profilePath);

                using (Stream stream = File.OpenRead(getProfile))
                    image = Image.FromStream(stream);

                pbxStudentBasicPicture.Image = image;
            }
            catch (Exception)
            {
                return;
            }
        }

        #endregion StudentBasicInfo

        #region StudentSearchBox

        private void tbxStudentSearch_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cbxStudentFilter.Text))
            {
                try
                {
                    var rowIndex = 0;
                    var _querySearch = "SELECT tbl_StudentInformation.StudentID, tbl_StudentInformation.StudentFirstName, tbl_StudentInformation.StudentLastName, tbl_StudentInformation.StudentEmailAddress, tbl_StudentInformation.StudentContactNumber, tbl_StudentInformation.StudentPresentAddress, tbl_StudentInformation.StudentStatus, tbl_StudentInformation.StudentDateSigned, tbl_StudentInformation.StudentValidUntil, tbl_StudentInformation.StudentIsActive, tbl_StudentInformation.StudentCourse, tbl_StudentInformation.StudentYear, tbl_StudentInformation.StudentSpecialization, tbl_StudentInformation.StudentIsTransferee FROM tbl_StudentInformation WHERE tbl_StudentInformation.StudentFirstName LIKE @StudentFirstName";

                    dgvStudents.Rows.Clear();
                    _connection.Open();
                    _command.Connection = _connection;
                    _command = new OleDbCommand(_querySearch, _connection);
                    _command.Parameters.AddWithValue("@StudentFirstName", tbxStudentSearch.Text + "%");
                    _reader = _command.ExecuteReader();

                    while (_reader.Read())
                    {
                        rowIndex++;
                        dgvStudents.Rows.Add(rowIndex,
                            _reader["StudentID"].ToString(),
                            _reader["StudentFirstName"].ToString(),
                            _reader["StudentLastName"].ToString(),
                            _reader["StudentStatus"].ToString(),
                            _reader["StudentValidUntil"].ToString(),
                            _reader["StudentCourse"].ToString(),
                            _reader["StudentYear"].ToString(),
                            _reader["StudentContactNumber"].ToString(),
                            _reader["StudentEmailAddress"].ToString(),
                            _reader["StudentPresentAddress"].ToString(),
                            _reader["StudentSpecialization"].ToString(),
                            _reader["StudentDateSigned"].ToString(),
                            _reader["StudentIsActive"].ToString(),
                            _reader["StudentIsTransferee"].ToString());
                    }
                    _connection.Close();
                }
                catch (Exception)
                {
                    _connection.Close();
                    MessageBox.Show("Error Search Empty");
                }
            }
            else if (cbxStudentFilter.Text == "Transferee")
            {
                try
                {
                    var rowIndex = 0;
                    var isTransferee = "Yes";
                    var _querySearch = "SELECT tbl_StudentInformation.StudentID, tbl_StudentInformation.StudentFirstName, tbl_StudentInformation.StudentLastName, tbl_StudentInformation.StudentEmailAddress, tbl_StudentInformation.StudentContactNumber, tbl_StudentInformation.StudentPresentAddress, tbl_StudentInformation.StudentStatus, tbl_StudentInformation.StudentDateSigned, tbl_StudentInformation.StudentValidUntil, tbl_StudentInformation.StudentIsActive, tbl_StudentInformation.StudentCourse, tbl_StudentInformation.StudentYear, tbl_StudentInformation.StudentSpecialization, tbl_StudentInformation.StudentIsTransferee FROM tbl_StudentInformation WHERE tbl_StudentInformation.StudentFirstName LIKE @StudentFirstName AND tbl_StudentInformation.StudentIsTransferee=@StudentIsTransferee";

                    dgvStudents.Rows.Clear();
                    _connection.Open();
                    _command.Connection = _connection;
                    _command = new OleDbCommand(_querySearch, _connection);
                    _command.Parameters.AddWithValue("@StudentFirstName", tbxStudentSearch.Text + "%");
                    _command.Parameters.AddWithValue("@StudentIsTransferee", isTransferee);
                    _reader = _command.ExecuteReader();

                    while (_reader.Read())
                    {
                        rowIndex++;
                        dgvStudents.Rows.Add(rowIndex,
                            _reader["StudentID"].ToString(),
                            _reader["StudentFirstName"].ToString(),
                            _reader["StudentLastName"].ToString(),
                            _reader["StudentStatus"].ToString(),
                            _reader["StudentValidUntil"].ToString(),
                            _reader["StudentCourse"].ToString(),
                            _reader["StudentYear"].ToString(),
                            _reader["StudentContactNumber"].ToString(),
                            _reader["StudentEmailAddress"].ToString(),
                            _reader["StudentPresentAddress"].ToString(),
                            _reader["StudentSpecialization"].ToString(),
                            _reader["StudentDateSigned"].ToString(),
                            _reader["StudentIsActive"].ToString(),
                            _reader["StudentIsTransferee"].ToString());
                    }
                    _connection.Close();
                }
                catch (Exception)
                {
                    _connection.Close();
                    MessageBox.Show("Error Search Transferee");
                }
            }
            else
            {
                try
                {
                    var rowIndex = 0;
                    var _querySearch = "SELECT tbl_StudentInformation.StudentID, tbl_StudentInformation.StudentFirstName, tbl_StudentInformation.StudentLastName, tbl_StudentInformation.StudentEmailAddress, tbl_StudentInformation.StudentContactNumber, tbl_StudentInformation.StudentPresentAddress, tbl_StudentInformation.StudentStatus, tbl_StudentInformation.StudentDateSigned, tbl_StudentInformation.StudentValidUntil, tbl_StudentInformation.StudentIsActive, tbl_StudentInformation.StudentCourse, tbl_StudentInformation.StudentYear, tbl_StudentInformation.StudentSpecialization, tbl_StudentInformation.StudentIsTransferee FROM tbl_StudentInformation WHERE tbl_StudentInformation.StudentFirstName LIKE @StudentFirstName AND tbl_StudentInformation.StudentStatus=@StudentStatus";

                    dgvStudents.Rows.Clear();
                    _connection.Open();
                    _command.Connection = _connection;
                    _command = new OleDbCommand(_querySearch, _connection);
                    _command.Parameters.AddWithValue("@StudentFirstName", tbxStudentSearch.Text + "%");
                    _command.Parameters.AddWithValue("@StudentStatus", cbxStudentFilter.Text);
                    _reader = _command.ExecuteReader();

                    while (_reader.Read())
                    {
                        rowIndex++;
                        dgvStudents.Rows.Add(rowIndex,
                            _reader["StudentID"].ToString(),
                            _reader["StudentFirstName"].ToString(),
                            _reader["StudentLastName"].ToString(),
                            _reader["StudentStatus"].ToString(),
                            _reader["StudentValidUntil"].ToString(),
                            _reader["StudentCourse"].ToString(),
                            _reader["StudentYear"].ToString(),
                            _reader["StudentContactNumber"].ToString(),
                            _reader["StudentEmailAddress"].ToString(),
                            _reader["StudentPresentAddress"].ToString(),
                            _reader["StudentSpecialization"].ToString(),
                            _reader["StudentDateSigned"].ToString(),
                            _reader["StudentIsActive"].ToString(),
                            _reader["StudentIsTransferee"].ToString());
                    }
                    _connection.Close();
                }
                catch (Exception)
                {
                    _connection.Close();
                    MessageBox.Show("Error Search Status");
                }
            }
        }

        #endregion StudentSearchBox

        private void btnStudentBasicViewFull_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbxStudentBasicID.Text))
            {
                MessageBox.Show("Select a member to view");
            }
            else
            {
                var newStudent = new FrmNewStudent(this)
                {
                    Text = "Full Information of " + tbxStudentBasicFirstName.Text
                };
                newStudent.btnStudentCancel.Text = "Exit";
                newStudent.tbxStudentFirstName.Enabled = false;
                newStudent.tbxStudentMiddleName.Enabled = false;
                newStudent.tbxStudentLastName.Enabled = false;
                newStudent.tbxStudentSuffixName.Enabled = false;
                newStudent.tbxStudentID.Enabled = false;
                newStudent.tbxStudentEmailAddress.Enabled = false;
                newStudent.tbxStudentRegionChapter.Enabled = false;
                newStudent.tbxStudentContact.Enabled = false;
                newStudent.tbxStudentPresentAddress.Enabled = false;
                newStudent.cbxStudentCurrentSchool.Enabled = false;
                newStudent.cbxStudentCourse.Enabled = false;
                newStudent.cbxStudentYear.Enabled = false;
                newStudent.tbxStudentSchoolAddress.Enabled = false;
                newStudent.tbxStudentSpecializations.Enabled = false;
                newStudent.cbxStudentHighSchool.Enabled = false;
                newStudent.tbxStudentStrand.Enabled = false;
                newStudent.dtpStudentYearGraduated.Enabled = false;
                newStudent.chxStudentRegularMember.Enabled = false;
                newStudent.chxStudentAssociateMember.Enabled = false;
                newStudent.btnStudentAddCurrentSchool.Enabled = false;
                newStudent.btnStudentAddHighSchool.Enabled = false;
                newStudent.btnStudentSave.Enabled = false;
                newStudent.btnStudentBrowse.Enabled = false;
                newStudent.pbxStudentPicture.Enabled = false;
                newStudent.chxStudentTransferee.Enabled = false;
                newStudent.btnStudentUpdate.Enabled = false;

                try
                {
                    var _querySearch = "SELECT tbl_StudentInformation.* FROM tbl_StudentInformation WHERE StudentID LIKE @STUDENTID";

                    _connection.Open();
                    _command.Connection = _connection;
                    _command = new OleDbCommand(_querySearch, _connection);
                    _command.Parameters.AddWithValue("@STUDENTID", tbxStudentBasicID.Text);
                    _reader = _command.ExecuteReader();

                    while (_reader.Read())
                    {
                        var fullID = _reader["StudentID"].ToString();
                        var idNumber = fullID.Split('-');

                        newStudent.tbxStudentFirstName.Text = _reader["StudentFirstName"].ToString();
                        newStudent.tbxStudentMiddleName.Text = _reader["StudentMiddleName"].ToString();
                        newStudent.tbxStudentLastName.Text = _reader["StudentLastName"].ToString();
                        newStudent.tbxStudentSuffixName.Text = _reader["StudentSuffixName"].ToString();
                        newStudent.tbxStudentID.Text = idNumber[3];
                        newStudent.tbxStudentEmailAddress.Text = _reader["StudentEmailAddress"].ToString();
                        newStudent.tbxStudentRegionChapter.Text = _reader["StudentRegionChapter"].ToString();
                        newStudent.tbxStudentContact.Text = _reader["StudentContactNumber"].ToString();
                        newStudent.tbxStudentPresentAddress.Text = _reader["StudentPresentAddress"].ToString();
                        newStudent.cbxStudentCurrentSchool.Text = _reader["StudentCurrentSchool"].ToString();
                        newStudent.cbxStudentCourse.Text = _reader["StudentCourse"].ToString();
                        newStudent.cbxStudentYear.Text = _reader["StudentYear"].ToString();
                        newStudent.tbxStudentSchoolAddress.Text = _reader["StudentSchoolAddress"].ToString();
                        newStudent.tbxStudentSpecializations.Text = _reader["StudentSpecialization"].ToString();
                        newStudent.cbxStudentHighSchool.Text = _reader["StudentHighSchool"].ToString();
                        newStudent.tbxStudentStrand.Text = _reader["StudentStrand"].ToString();
                        newStudent.dtpStudentYearGraduated.Text = _reader["StudentYearGraduated"].ToString();
                        newStudent.dtpStudentDateSigned.Text = _reader["StudentDateSigned"].ToString();

                        if (_reader["StudentStatus"].ToString() == "Associate")
                            newStudent.chxStudentAssociateMember.Checked = true;
                        else if (_reader["StudentStatus"].ToString() == "Regular")
                            newStudent.chxStudentRegularMember.Checked = true;

                        if (_reader["StudentIsTransferee"].ToString() == "Yes")
                            newStudent.chxStudentTransferee.Checked = true;
                        else if (_reader["StudentIsTransferee"].ToString() == "No")
                            newStudent.chxStudentTransferee.Checked = false;

                        Image image;
                        var profilePath = tbxStudentBasicID.Text + ".jpg";
                        var applicationPath = Application.StartupPath + @"\Pictures\Student\";
                        var getProfile = Path.Combine(applicationPath, profilePath);

                        using (Stream stream = File.OpenRead(getProfile))
                            image = Image.FromStream(stream);

                        newStudent.pbxStudentPicture.Image = image;

                        newStudent.dtpStudentDateSigned.Enabled = false;
                    }
                    _connection.Close();
                    newStudent.Show();
                }
                catch (Exception ex)
                {
                    _connection.Close();
                    MessageBox.Show("Error Transferring data " + ex.Message);
                }
            }
        }

        private void btnStudentBasicEdit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbxStudentBasicID.Text))
            {
                MessageBox.Show("Select a member to edit");
            }
            else
            {
                try
                {
                    var newStudent = new FrmNewStudent(this);
                    var _querySearch = "SELECT tbl_StudentInformation.* FROM tbl_StudentInformation WHERE StudentID LIKE @STUDENTID";

                    _connection.Open();
                    _command.Connection = _connection;
                    _command = new OleDbCommand(_querySearch, _connection);
                    _command.Parameters.AddWithValue("@STUDENTID", tbxStudentBasicID.Text);
                    _reader = _command.ExecuteReader();

                    while (_reader.Read())
                    {
                        var fullID = _reader["StudentID"].ToString();
                        var idNumber = fullID.Split('-');
                        newStudent.tbxStudentID.Text = idNumber[3];

                        newStudent.tbxStudentFirstName.Text = _reader["StudentFirstName"].ToString();
                        newStudent.tbxStudentMiddleName.Text = _reader["StudentMiddleName"].ToString();
                        newStudent.tbxStudentLastName.Text = _reader["StudentLastName"].ToString();
                        newStudent.tbxStudentSuffixName.Text = _reader["StudentSuffixName"].ToString();
                        newStudent.tbxStudentEmailAddress.Text = _reader["StudentEmailAddress"].ToString();
                        newStudent.tbxStudentRegionChapter.Text = _reader["StudentRegionChapter"].ToString();
                        newStudent.tbxStudentContact.Text = _reader["StudentContactNumber"].ToString();
                        newStudent.tbxStudentPresentAddress.Text = _reader["StudentPresentAddress"].ToString();
                        newStudent.cbxStudentCurrentSchool.Text = _reader["StudentCurrentSchool"].ToString();
                        newStudent.cbxStudentCourse.Text = _reader["StudentCourse"].ToString();
                        newStudent.cbxStudentYear.Text = _reader["StudentYear"].ToString();
                        newStudent.tbxStudentSchoolAddress.Text = _reader["StudentSchoolAddress"].ToString();
                        newStudent.tbxStudentSpecializations.Text = _reader["StudentSpecialization"].ToString();
                        newStudent.cbxStudentHighSchool.Text = _reader["StudentHighSchool"].ToString();
                        newStudent.tbxStudentStrand.Text = _reader["StudentStrand"].ToString();
                        newStudent.dtpStudentYearGraduated.Text = _reader["StudentYearGraduated"].ToString();
                        newStudent.dtpStudentDateSigned.Text = _reader["StudentDateSigned"].ToString();

                        if (_reader["StudentStatus"].ToString() == "Associate")
                            newStudent.chxStudentAssociateMember.Checked = true;
                        else if (_reader["StudentStatus"].ToString() == "Regular")
                            newStudent.chxStudentRegularMember.Checked = true;

                        if (_reader["StudentIsTransferee"].ToString() == "Yes")
                            newStudent.chxStudentTransferee.Checked = true;
                        else if (_reader["StudentIsTransferee"].ToString() == "No")
                            newStudent.chxStudentTransferee.Checked = false;

                        Image image;
                        var profilePath = tbxStudentBasicID.Text + ".jpg";
                        var applicationPath = Application.StartupPath + @"\Pictures\Student\";
                        var getProfile = Path.Combine(applicationPath, profilePath);

                        using (Stream stream = File.OpenRead(getProfile))
                            image = Image.FromStream(stream);

                        newStudent.pbxStudentPicture.Image = image;
                    }
                    _connection.Close();

                    newStudent.tbxStudentID.Enabled = false;
                    newStudent.chxStudentAssociateMember.Enabled = false;
                    newStudent.chxStudentRegularMember.Enabled = false;
                    newStudent.chxStudentTransferee.Enabled = false;
                    newStudent.dtpStudentDateSigned.Enabled = false;
                    newStudent.btnStudentSave.Enabled = false;
                    newStudent.Show();
                }
                catch (Exception ex)
                {
                    _connection.Close();
                    MessageBox.Show("Error Transferring data " + ex.Message);
                }
            }
        }

        #region StudentFilters

        private void cbxStudentFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cbxStudentFilter.Text))
            {
                try
                {
                    dgvStudents.Rows.Clear();
                    var rowIndex = 0;
                    var selectQuary = "SELECT tbl_StudentInformation.StudentID, tbl_StudentInformation.StudentFirstName, tbl_StudentInformation.StudentLastName, tbl_StudentInformation.StudentEmailAddress, tbl_StudentInformation.StudentContactNumber, tbl_StudentInformation.StudentPresentAddress, tbl_StudentInformation.StudentStatus, tbl_StudentInformation.StudentDateSigned, tbl_StudentInformation.StudentValidUntil, tbl_StudentInformation.StudentIsActive, tbl_StudentInformation.StudentCourse, tbl_StudentInformation.StudentYear, tbl_StudentInformation.StudentSpecialization, tbl_StudentInformation.StudentIsTransferee FROM tbl_StudentInformation";

                    _connection.Open();
                    _command.Connection = _connection;
                    _command = new OleDbCommand(selectQuary, _connection);
                    _reader = _command.ExecuteReader();

                    while (_reader.Read())
                    {
                        rowIndex++;
                        dgvStudents.Rows.Add(rowIndex,
                            _reader["StudentID"].ToString(),
                            _reader["StudentFirstName"].ToString(),
                            _reader["StudentLastName"].ToString(),
                            _reader["StudentStatus"].ToString(),
                            _reader["StudentValidUntil"].ToString(),
                            _reader["StudentCourse"].ToString(),
                            _reader["StudentYear"].ToString(),
                            _reader["StudentContactNumber"].ToString(),
                            _reader["StudentEmailAddress"].ToString(),
                            _reader["StudentPresentAddress"].ToString(),
                            _reader["StudentSpecialization"].ToString(),
                            _reader["StudentDateSigned"].ToString(),
                            _reader["StudentIsActive"].ToString(),
                            _reader["StudentIsTransferee"].ToString());
                    }
                    _reader.Close();
                    _connection.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Failed to filter");
                }
            }
            else if (cbxStudentFilter.Text == "Transferee")
            {
                try
                {
                    dgvStudents.Rows.Clear();
                    var rowIndex = 0;
                    var selectQuary = "SELECT tbl_StudentInformation.StudentID, tbl_StudentInformation.StudentFirstName, tbl_StudentInformation.StudentLastName, tbl_StudentInformation.StudentEmailAddress, tbl_StudentInformation.StudentContactNumber, tbl_StudentInformation.StudentPresentAddress, tbl_StudentInformation.StudentStatus, tbl_StudentInformation.StudentDateSigned, tbl_StudentInformation.StudentValidUntil, tbl_StudentInformation.StudentIsActive, tbl_StudentInformation.StudentCourse, tbl_StudentInformation.StudentYear, tbl_StudentInformation.StudentSpecialization, tbl_StudentInformation.StudentIsTransferee FROM tbl_StudentInformation WHERE tbl_StudentInformation.StudentIsTransferee=@StudentIsTransferee";

                    _connection.Open();
                    _command.Connection = _connection;
                    _command = new OleDbCommand(selectQuary, _connection);
                    _command.Parameters.AddWithValue("@StudentIsTransferee", "Yes");
                    _reader = _command.ExecuteReader();

                    while (_reader.Read())
                    {
                        rowIndex++;
                        dgvStudents.Rows.Add(rowIndex,
                            _reader["StudentID"].ToString(),
                            _reader["StudentFirstName"].ToString(),
                            _reader["StudentLastName"].ToString(),
                            _reader["StudentStatus"].ToString(),
                            _reader["StudentValidUntil"].ToString(),
                            _reader["StudentCourse"].ToString(),
                            _reader["StudentYear"].ToString(),
                            _reader["StudentContactNumber"].ToString(),
                            _reader["StudentEmailAddress"].ToString(),
                            _reader["StudentPresentAddress"].ToString(),
                            _reader["StudentSpecialization"].ToString(),
                            _reader["StudentDateSigned"].ToString(),
                            _reader["StudentIsActive"].ToString(),
                            _reader["StudentIsTransferee"].ToString());
                    }
                    _reader.Close();
                    _connection.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Failed to filter");
                }
            }
            else
            {
                try
                {
                    dgvStudents.Rows.Clear();
                    var rowIndex = 0;
                    var selectQuary = "SELECT tbl_StudentInformation.StudentID, tbl_StudentInformation.StudentFirstName, tbl_StudentInformation.StudentLastName, tbl_StudentInformation.StudentEmailAddress, tbl_StudentInformation.StudentContactNumber, tbl_StudentInformation.StudentPresentAddress, tbl_StudentInformation.StudentStatus, tbl_StudentInformation.StudentDateSigned, tbl_StudentInformation.StudentValidUntil, tbl_StudentInformation.StudentIsActive, tbl_StudentInformation.StudentCourse, tbl_StudentInformation.StudentYear, tbl_StudentInformation.StudentSpecialization, tbl_StudentInformation.StudentIsTransferee FROM tbl_StudentInformation WHERE tbl_StudentInformation.StudentStatus=@StudentStatus";

                    _connection.Open();
                    _command.Connection = _connection;
                    _command = new OleDbCommand(selectQuary, _connection);
                    _command.Parameters.AddWithValue("@StudentStatus", cbxStudentFilter.Text);
                    _reader = _command.ExecuteReader();

                    while (_reader.Read())
                    {
                        rowIndex++;
                        dgvStudents.Rows.Add(rowIndex,
                            _reader["StudentID"].ToString(),
                            _reader["StudentFirstName"].ToString(),
                            _reader["StudentLastName"].ToString(),
                            _reader["StudentStatus"].ToString(),
                            _reader["StudentValidUntil"].ToString(),
                            _reader["StudentCourse"].ToString(),
                            _reader["StudentYear"].ToString(),
                            _reader["StudentContactNumber"].ToString(),
                            _reader["StudentEmailAddress"].ToString(),
                            _reader["StudentPresentAddress"].ToString(),
                            _reader["StudentSpecialization"].ToString(),
                            _reader["StudentDateSigned"].ToString(),
                            _reader["StudentIsActive"].ToString(),
                            _reader["StudentIsTransferee"].ToString());
                    }
                    _reader.Close();
                    _connection.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Failed to filter");
                }
            }
        }

        #endregion StudentFilters

        private void btnStudentBasicDelete_Click(object sender, EventArgs e)
        {
            DeleteStudentMember();
        }

        private void btnStudentClearFilter_Click(object sender, EventArgs e)
        {
            cbxStudentFilter.Text = "";
            cbxStudentFilter_SelectedIndexChanged(null, null);
        }

        #endregion StudentTab

        #region ProfessionalTab

        private void DeleteProfessionalMember()
        {
            if (string.IsNullOrWhiteSpace(tbxProfessionalBasicID.Text))
            {
                MessageBox.Show("Select a member to delete");
            }
            else
            {
                if (MessageBox.Show("Are you sure you want to delete this member?", "ICEPEP", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    try
                    {
                        var imagePath = Application.StartupPath + @"\Pictures\Professional\" + tbxProfessionalBasicID.Text + ".jpg";
                        var _queryDelete = "DELETE FROM tbl_ProfessionalInformation WHERE ProfessionalID=@ProfessionalID";

                        _connection.Open();
                        _command.Connection = _connection;
                        _command = new OleDbCommand(_queryDelete, _connection);
                        _command.Parameters.AddWithValue("@ProfessionalID", tbxProfessionalBasicID.Text);
                        _command.ExecuteNonQuery();
                        _connection.Close();
                        LoadListOfStudents();

                        if (File.Exists(imagePath))
                            File.Delete(imagePath);
                        else
                            return;

                        ClearFields();
                    }
                    catch (Exception)
                    {
                        _connection.Close();
                        MessageBox.Show("Failed to delete member");
                    }
                }
            }
        }

        public void LoadListOfProfessionals()
        {
            try
            {
                dgvProfessionals.Rows.Clear();
                var rowIndex = 0;
                var selectQuary = "SELECT tbl_ProfessionalInformation.ProfessionalID, tbl_ProfessionalInformation.ProfessionalFirstName, tbl_ProfessionalInformation.ProfessionalLastName, tbl_ProfessionalInformation.ProfessionalContactNumber, tbl_ProfessionalInformation.ProfessionalPresentAddress, tbl_ProfessionalInformation.ProfessionalEmailAddress, tbl_ProfessionalInformation.ProfessionalJobTitle, tbl_ProfessionalInformation.ProfessionalSpecialization, tbl_ProfessionalInformation.ProfessionalStatus, tbl_ProfessionalInformation.ProfessionalDegree, tbl_ProfessionalInformation.ProfessionalDateSigned, tbl_ProfessionalInformation.ProfessionalValidUntil, tbl_ProfessionalInformation.ProfessionalIsActive, tbl_ProfessionalInformation.ProfessionalIsTransferee FROM tbl_ProfessionalInformation ORDER BY tbl_ProfessionalInformation.ProfessionalID";

                _connection.Open();
                _command.Connection = _connection;
                _command = new OleDbCommand(selectQuary, _connection);
                _reader = _command.ExecuteReader();

                while (_reader.Read())
                {
                    rowIndex++;
                    dgvProfessionals.Rows.Add(rowIndex,
                        _reader["ProfessionalID"].ToString(),
                        _reader["ProfessionalFirstName"].ToString(),
                        _reader["ProfessionalLastName"].ToString(),
                        _reader["ProfessionalStatus"].ToString(),
                        _reader["ProfessionalValidUntil"].ToString(),
                        _reader["ProfessionalJobTitle"].ToString(),
                        _reader["ProfessionalDegree"].ToString(),
                        _reader["ProfessionalContactNumber"].ToString(),
                        _reader["ProfessionalEmailAddress"].ToString(),
                        _reader["ProfessionalPresentAddress"].ToString(),
                        _reader["ProfessionalSpecialization"].ToString(),
                        _reader["ProfessionalDateSigned"].ToString(),
                        _reader["ProfessionalIsActive"].ToString(),
                        _reader["ProfessionalIsTransferee"].ToString());
                }
                _reader.Close();
                _connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load students list " + ex.Message);
            }
        }

        private void tspMenuProfessional_Click(object sender, EventArgs e)
        {
            var professional = new FrmNewProfessional(this);
            professional.Show();
        }

        private void dgvProfessionals_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                tbxProfessionalBasicID.Text = dgvProfessionals.Rows[e.RowIndex].Cells[1].Value.ToString();
                tbxProfessionalBasicFirstName.Text = dgvProfessionals.Rows[e.RowIndex].Cells[2].Value.ToString();
                tbxProfessionalBasicLastName.Text = dgvProfessionals.Rows[e.RowIndex].Cells[3].Value.ToString();
                tbxProfessionalBasicValidUntil.Text = dgvProfessionals.Rows[e.RowIndex].Cells[5].Value.ToString();
                tbxProfessionalBasicJobTitle.Text = dgvProfessionals.Rows[e.RowIndex].Cells[6].Value.ToString();
                tbxProfessionalBasicDegree.Text = dgvProfessionals.Rows[e.RowIndex].Cells[7].Value.ToString();
                tbxProfessionalBasicContact.Text = dgvProfessionals.Rows[e.RowIndex].Cells[8].Value.ToString();
                tbxProfessionalBasicEmail.Text = dgvProfessionals.Rows[e.RowIndex].Cells[9].Value.ToString();
                tbxProfessionalBasicPresentAddress.Text = dgvProfessionals.Rows[e.RowIndex].Cells[10].Value.ToString();
                tbxProfessionalBasicSpecialization.Text = dgvProfessionals.Rows[e.RowIndex].Cells[11].Value.ToString();
                tbxProfessionalBasicDateSigned.Text = dgvProfessionals.Rows[e.RowIndex].Cells[12].Value.ToString();

                if (dgvProfessionals.Rows[e.RowIndex].Cells[13].Value.ToString() == "Yes")
                    chxProfessionalBasicIsActive.Checked = true;
                else
                    chxProfessionalBasicIsActive.Checked = false;

                Image image;
                var profilePath = tbxProfessionalBasicID.Text + ".jpg";
                var applicationPath = Application.StartupPath + @"\Pictures\Professional\";
                var getProfile = Path.Combine(applicationPath, profilePath);

                using (Stream stream = File.OpenRead(getProfile))
                    image = Image.FromStream(stream);

                pbxProfessionalBasicPicture.Image = image;
            }
            catch (Exception)
            {
                return;
            }
        }

        private void btnProfessionalBasicDelete_Click(object sender, EventArgs e)
        {
            DeleteProfessionalMember();
        }

        private void btnProfessionalBasicViewFull_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbxProfessionalBasicID.Text))
            {
                MessageBox.Show("Select a member to view");
            }
            else
            {
                var newProfessional = new FrmNewProfessional(this)
                {
                    Text = "Full Information of " + tbxProfessionalBasicFirstName.Text
                };
                newProfessional.btnProfessioanlCancel.Text = "Exit";
                newProfessional.tbxProfessionalFirstName.Enabled = false;
                newProfessional.tbxProfessionalMiddleName.Enabled = false;
                newProfessional.tbxProfessionalLastName.Enabled = false;
                newProfessional.tbxProfessionalSuffixName.Enabled = false;
                newProfessional.tbxProfessionalID.Enabled = false;
                newProfessional.tbxProfessionalEmailAddress.Enabled = false;
                newProfessional.tbxProfessionalRegionChapter.Enabled = false;
                newProfessional.tbxProfessionalContact.Enabled = false;
                newProfessional.tbxProfessionalPresentAddress.Enabled = false;
                newProfessional.cbxProfessionalCurrentEmployer.Enabled = false;
                newProfessional.cbxProfessionalJobTitle.Enabled = false;
                newProfessional.tbxProfessionalEmployeeAddress.Enabled = false;
                newProfessional.cbxProfessionalSchool.Enabled = false;
                newProfessional.tbxProfessionalSpecializations.Enabled = false;
                newProfessional.cbxProfessionalDegree.Enabled = false;
                newProfessional.dtpProfessionalYearGraduated.Enabled = false;
                newProfessional.chxProfessionalRegularOneYear.Enabled = false;
                newProfessional.chxProfessionalRegularThreeYears.Enabled = false;
                newProfessional.chxProfessionalAsscociateOneYear.Enabled = false;
                newProfessional.chxProfessionalAsscociateThreeYear.Enabled = false;
                newProfessional.chxProfessionalRegularLifetime.Enabled = false;
                newProfessional.btnProfessionalSchool.Enabled = false;
                newProfessional.btnProfessionalSave.Enabled = false;
                newProfessional.btnProfessionalBrowse.Enabled = false;
                newProfessional.btnProfessionalAddCurrentEmployer.Enabled = false;
                newProfessional.pbxProfessionalPicture.Enabled = false;
                newProfessional.chxProfessionalTransferee.Enabled = false;
                newProfessional.btnProfessionalUpdate.Enabled = false;
                newProfessional.chxProfessionalRegular.Enabled = false;
                newProfessional.chxProfessionalAssociate.Enabled = false;
                newProfessional.chxProfessionalAsscociateLifetime.Enabled = false;

                try
                {
                    var _querySearch = "SELECT tbl_ProfessionalInformation.* FROM tbl_ProfessionalInformation WHERE ProfessionalID LIKE @ProfessionalID";

                    _connection.Open();
                    _command.Connection = _connection;
                    _command = new OleDbCommand(_querySearch, _connection);
                    _command.Parameters.AddWithValue("@ProfessionalID", tbxProfessionalBasicID.Text);
                    _reader = _command.ExecuteReader();

                    while (_reader.Read())
                    {
                        var fullID = _reader["ProfessionalID"].ToString();
                        var idNumber = fullID.Split('-');

                        newProfessional.tbxProfessionalFirstName.Text = _reader["ProfessionalFirstName"].ToString();
                        newProfessional.tbxProfessionalMiddleName.Text = _reader["ProfessionalMiddleName"].ToString();
                        newProfessional.tbxProfessionalLastName.Text = _reader["ProfessionalLastName"].ToString();
                        newProfessional.tbxProfessionalSuffixName.Text = _reader["ProfessionalSuffixName"].ToString();
                        newProfessional.tbxProfessionalID.Text = idNumber[2];
                        newProfessional.tbxProfessionalEmailAddress.Text = _reader["ProfessionalEmailAddress"].ToString();
                        newProfessional.tbxProfessionalRegionChapter.Text = _reader["ProfessionalRegionChapter"].ToString();
                        newProfessional.tbxProfessionalContact.Text = _reader["ProfessionalContactNumber"].ToString();
                        newProfessional.tbxProfessionalPresentAddress.Text = _reader["ProfessionalPresentAddress"].ToString();
                        newProfessional.cbxProfessionalCurrentEmployer.Text = _reader["ProfessionalCurrentEmployer"].ToString();
                        newProfessional.cbxProfessionalJobTitle.Text = _reader["ProfessionalJobTitle"].ToString();
                        newProfessional.tbxProfessionalEmployeeAddress.Text = _reader["ProfessionalEmployerAddress"].ToString();
                        newProfessional.cbxProfessionalSchool.Text = _reader["ProfessionalSchool"].ToString();
                        newProfessional.tbxProfessionalSpecializations.Text = _reader["ProfessionalSpecialization"].ToString();
                        newProfessional.cbxProfessionalDegree.Text = _reader["ProfessionalDegree"].ToString();
                        newProfessional.dtpProfessionalYearGraduated.Text = _reader["ProfessionalYearGraduated"].ToString();

                        if (_reader["ProfessionalStatus"].ToString() == "Regular")
                            newProfessional.chxProfessionalRegular.Checked = true;
                        else if (_reader["ProfessionalStatus"].ToString() == "Associate")
                            newProfessional.chxProfessionalAssociate.Checked = true;

                        newProfessional.dtpProfessionalDateSigned.Text = _reader["ProfessionalDateSigned"].ToString();

                        var parseTime = DateTime.Parse(_reader["ProfessionalValidUntil"].ToString());

                        var dateOne = _reader["ProfessionalDateSigned"].ToString();
                        var validOneYear = DateTime.Parse(dateOne);
                        var dateOneYear = validOneYear.AddYears(1);

                        var dateThree = _reader["ProfessionalDateSigned"].ToString();
                        var validThreeYear = DateTime.Parse(dateThree);
                        var dateThreeYear = validThreeYear.AddYears(3);

                        if (_reader["ProfessionalStatus"].ToString() == "Associate")
                        {
                            if (parseTime == dateOneYear)
                                newProfessional.chxProfessionalAsscociateOneYear.Checked = true;
                            else if (parseTime == dateThreeYear)
                                newProfessional.chxProfessionalAsscociateThreeYear.Checked = true;
                            else
                                newProfessional.chxProfessionalAsscociateLifetime.Checked = true;
                        }
                        else if (_reader["ProfessionalStatus"].ToString() == "Regular")
                        {
                            if (parseTime == dateOneYear)
                                newProfessional.chxProfessionalRegularOneYear.Checked = true;
                            else if (parseTime == dateThreeYear)
                                newProfessional.chxProfessionalRegularThreeYears.Checked = true;
                            else
                                newProfessional.chxProfessionalRegularLifetime.Checked = true;
                        }

                        if (_reader["ProfessionalIsTransferee"].ToString() == "Yes")
                            newProfessional.chxProfessionalTransferee.Checked = true;
                        else if (_reader["ProfessionalIsTransferee"].ToString() == "No")
                            newProfessional.chxProfessionalTransferee.Checked = false;

                        Image image;
                        var profilePath = tbxProfessionalBasicID.Text + ".jpg";
                        var applicationPath = Application.StartupPath + @"\Pictures\Professional\";
                        var getProfile = Path.Combine(applicationPath, profilePath);

                        using (Stream stream = File.OpenRead(getProfile))
                            image = Image.FromStream(stream);

                        newProfessional.pbxProfessionalPicture.Image = image;

                        newProfessional.dtpProfessionalDateSigned.Enabled = false;
                    }
                    _connection.Close();
                    newProfessional.Show();
                }
                catch (Exception ex)
                {
                    _connection.Close();
                    MessageBox.Show("Error Transferring data " + ex.Message);
                }
            }
        }

        private void btnProfessionalBasicEdit_Click(object sender, EventArgs e)
        {


            if (string.IsNullOrWhiteSpace(tbxProfessionalBasicID.Text))
            {
                MessageBox.Show("Select a member to view");
            }
            else
            {
                try
                {
                    var newProfessional = new FrmNewProfessional(this);
                    var _querySearch = "SELECT tbl_ProfessionalInformation.* FROM tbl_ProfessionalInformation WHERE ProfessionalID LIKE @ProfessionalID";

                    _connection.Open();
                    _command.Connection = _connection;
                    _command = new OleDbCommand(_querySearch, _connection);
                    _command.Parameters.AddWithValue("@ProfessionalID", tbxProfessionalBasicID.Text);
                    _reader = _command.ExecuteReader();

                    while (_reader.Read())
                    {
                        var fullID = _reader["ProfessionalID"].ToString();
                        var idNumber = fullID.Split('-');
                        newProfessional.tbxProfessionalID.Text = idNumber[2];

                        newProfessional.tbxProfessionalFirstName.Text = _reader["ProfessionalFirstName"].ToString();
                        newProfessional.tbxProfessionalMiddleName.Text = _reader["ProfessionalMiddleName"].ToString();
                        newProfessional.tbxProfessionalLastName.Text = _reader["ProfessionalLastName"].ToString();
                        newProfessional.tbxProfessionalSuffixName.Text = _reader["ProfessionalSuffixName"].ToString();
                        newProfessional.tbxProfessionalEmailAddress.Text = _reader["ProfessionalEmailAddress"].ToString();
                        newProfessional.tbxProfessionalRegionChapter.Text = _reader["ProfessionalRegionChapter"].ToString();
                        newProfessional.tbxProfessionalContact.Text = _reader["ProfessionalContactNumber"].ToString();
                        newProfessional.tbxProfessionalPresentAddress.Text = _reader["ProfessionalPresentAddress"].ToString();
                        newProfessional.cbxProfessionalCurrentEmployer.Text = _reader["ProfessionalCurrentEmployer"].ToString();
                        newProfessional.cbxProfessionalJobTitle.Text = _reader["ProfessionalJobTitle"].ToString();
                        newProfessional.tbxProfessionalEmployeeAddress.Text = _reader["ProfessionalEmployerAddress"].ToString();
                        newProfessional.cbxProfessionalSchool.Text = _reader["ProfessionalSchool"].ToString();
                        newProfessional.tbxProfessionalSpecializations.Text = _reader["ProfessionalSpecialization"].ToString();
                        newProfessional.cbxProfessionalDegree.Text = _reader["ProfessionalDegree"].ToString();
                        newProfessional.dtpProfessionalYearGraduated.Text = _reader["ProfessionalYearGraduated"].ToString();

                        if (_reader["ProfessionalStatus"].ToString() == "Regular")
                            newProfessional.chxProfessionalRegular.Checked = true;
                        else if (_reader["ProfessionalStatus"].ToString() == "Associate")
                            newProfessional.chxProfessionalAssociate.Checked = true;

                        newProfessional.dtpProfessionalDateSigned.Text = _reader["ProfessionalDateSigned"].ToString();

                        var parseTime = DateTime.Parse(_reader["ProfessionalValidUntil"].ToString());

                        var dateOne = _reader["ProfessionalDateSigned"].ToString();
                        var validOneYear = DateTime.Parse(dateOne);
                        var dateOneYear = validOneYear.AddYears(1);

                        var dateThree = _reader["ProfessionalDateSigned"].ToString();
                        var validThreeYear = DateTime.Parse(dateThree);
                        var dateThreeYear = validThreeYear.AddYears(3);

                        if (_reader["ProfessionalStatus"].ToString() == "Associate")
                        {
                            if (parseTime == dateOneYear)
                                newProfessional.chxProfessionalAsscociateOneYear.Checked = true;
                            else if (parseTime == dateThreeYear)
                                newProfessional.chxProfessionalAsscociateThreeYear.Checked = true;
                            else
                                newProfessional.chxProfessionalAsscociateLifetime.Checked = true;
                        }
                        else if (_reader["ProfessionalStatus"].ToString() == "Regular")
                        {
                            if (parseTime == dateOneYear)
                                newProfessional.chxProfessionalRegularOneYear.Checked = true;
                            else if (parseTime == dateThreeYear)
                                newProfessional.chxProfessionalRegularThreeYears.Checked = true;
                            else
                                newProfessional.chxProfessionalRegularLifetime.Checked = true;
                        }

                        if (_reader["ProfessionalIsTransferee"].ToString() == "Yes")
                            newProfessional.chxProfessionalTransferee.Checked = true;
                        else if (_reader["ProfessionalIsTransferee"].ToString() == "No")
                            newProfessional.chxProfessionalTransferee.Checked = false;

                        Image image;
                        var profilePath = tbxProfessionalBasicID.Text + ".jpg";
                        var applicationPath = Application.StartupPath + @"\Pictures\Professional\";
                        var getProfile = Path.Combine(applicationPath, profilePath);

                        using (Stream stream = File.OpenRead(getProfile))
                            image = Image.FromStream(stream);

                        newProfessional.pbxProfessionalPicture.Image = image;

                        newProfessional.dtpProfessionalDateSigned.Enabled = false;
                        newProfessional.btnProfessionalSave.Enabled = false;
                        newProfessional.tbxProfessionalID.Enabled = false;

                        newProfessional.chxProfessionalAssociate.Enabled = false;
                        newProfessional.chxProfessionalAsscociateOneYear.Enabled = false;
                        newProfessional.chxProfessionalAsscociateThreeYear.Enabled = false;
                        newProfessional.chxProfessionalAsscociateLifetime.Enabled = false;
                        newProfessional.chxProfessionalRegular.Enabled = false;
                        newProfessional.chxProfessionalRegularOneYear.Enabled = false;
                        newProfessional.chxProfessionalRegularThreeYears.Enabled = false;
                        newProfessional.chxProfessionalRegularLifetime.Enabled = false;
                        newProfessional.chxProfessionalTransferee.Enabled = false;
                    }
                    _connection.Close();
                    newProfessional.Show();
                }
                catch (Exception ex)
                {
                    _connection.Close();
                    MessageBox.Show("Error Transferring data " + ex.Message);
                }
            }
        }

        private void tbxProfessionalSearch_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cbxProfessionalFilter.Text))
            {
                try
                {
                    dgvProfessionals.Rows.Clear();
                    var rowIndex = 0;
                    var selectQuary = "SELECT tbl_ProfessionalInformation.ProfessionalID, tbl_ProfessionalInformation.ProfessionalFirstName, tbl_ProfessionalInformation.ProfessionalLastName, tbl_ProfessionalInformation.ProfessionalContactNumber, tbl_ProfessionalInformation.ProfessionalPresentAddress, tbl_ProfessionalInformation.ProfessionalEmailAddress, tbl_ProfessionalInformation.ProfessionalJobTitle, tbl_ProfessionalInformation.ProfessionalSpecialization, tbl_ProfessionalInformation.ProfessionalStatus, tbl_ProfessionalInformation.ProfessionalDegree, tbl_ProfessionalInformation.ProfessionalDateSigned, tbl_ProfessionalInformation.ProfessionalValidUntil, tbl_ProfessionalInformation.ProfessionalIsActive, tbl_ProfessionalInformation.ProfessionalIsTransferee FROM tbl_ProfessionalInformation WHERE tbl_ProfessionalInformation.ProfessionalFirstName LIKE @ProfessionalFirstName";

                    _connection.Open();
                    _command.Connection = _connection;
                    _command = new OleDbCommand(selectQuary, _connection);
                    _command.Parameters.AddWithValue("@ProfessionalFirstName", tbxProfessionalSearch.Text + "%");
                    _reader = _command.ExecuteReader();

                    while (_reader.Read())
                    {
                        rowIndex++;
                        dgvProfessionals.Rows.Add(rowIndex,
                            _reader["ProfessionalID"].ToString(),
                            _reader["ProfessionalFirstName"].ToString(),
                            _reader["ProfessionalLastName"].ToString(),
                            _reader["ProfessionalStatus"].ToString(),
                            _reader["ProfessionalValidUntil"].ToString(),
                            _reader["ProfessionalJobTitle"].ToString(),
                            _reader["ProfessionalDegree"].ToString(),
                            _reader["ProfessionalContactNumber"].ToString(),
                            _reader["ProfessionalEmailAddress"].ToString(),
                            _reader["ProfessionalPresentAddress"].ToString(),
                            _reader["ProfessionalSpecialization"].ToString(),
                            _reader["ProfessionalDateSigned"].ToString(),
                            _reader["ProfessionalIsActive"].ToString(),
                            _reader["ProfessionalIsTransferee"].ToString());
                    }
                    _connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to load students list " + ex.Message);
                }
            }
            else if (cbxProfessionalFilter.Text == "Transferee")
            {
                try
                {
                    dgvProfessionals.Rows.Clear();
                    var rowIndex = 0;
                    var selectQuary = "SELECT tbl_ProfessionalInformation.ProfessionalID, tbl_ProfessionalInformation.ProfessionalFirstName, tbl_ProfessionalInformation.ProfessionalLastName, tbl_ProfessionalInformation.ProfessionalContactNumber, tbl_ProfessionalInformation.ProfessionalPresentAddress, tbl_ProfessionalInformation.ProfessionalEmailAddress, tbl_ProfessionalInformation.ProfessionalJobTitle, tbl_ProfessionalInformation.ProfessionalSpecialization, tbl_ProfessionalInformation.ProfessionalStatus, tbl_ProfessionalInformation.ProfessionalDegree, tbl_ProfessionalInformation.ProfessionalDateSigned, tbl_ProfessionalInformation.ProfessionalValidUntil, tbl_ProfessionalInformation.ProfessionalIsActive, tbl_ProfessionalInformation.ProfessionalIsTransferee FROM tbl_ProfessionalInformation WHERE tbl_ProfessionalInformation.ProfessionalFirstName LIKE @ProfessionalFirstName AND tbl_ProfessionalInformation.ProfessionalIsTransferee=@ProfessionalIsTransferee";

                    _connection.Open();
                    _command.Connection = _connection;
                    _command = new OleDbCommand(selectQuary, _connection);
                    _command.Parameters.AddWithValue("@ProfessionalFirstName", tbxProfessionalSearch.Text + "%");
                    _command.Parameters.AddWithValue("@ProfessionalIsTransferee", "Yes");
                    _reader = _command.ExecuteReader();

                    while (_reader.Read())
                    {
                        rowIndex++;
                        dgvProfessionals.Rows.Add(rowIndex,
                            _reader["ProfessionalID"].ToString(),
                            _reader["ProfessionalFirstName"].ToString(),
                            _reader["ProfessionalLastName"].ToString(),
                            _reader["ProfessionalStatus"].ToString(),
                            _reader["ProfessionalValidUntil"].ToString(),
                            _reader["ProfessionalJobTitle"].ToString(),
                            _reader["ProfessionalDegree"].ToString(),
                            _reader["ProfessionalContactNumber"].ToString(),
                            _reader["ProfessionalEmailAddress"].ToString(),
                            _reader["ProfessionalPresentAddress"].ToString(),
                            _reader["ProfessionalSpecialization"].ToString(),
                            _reader["ProfessionalDateSigned"].ToString(),
                            _reader["ProfessionalIsActive"].ToString(),
                            _reader["ProfessionalIsTransferee"].ToString());
                    }
                    _connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to load students list " + ex.Message);
                }
            }
        }

        private void btnProfessionalClearFilter_Click(object sender, EventArgs e)
        {
            cbxProfessionalFilter.Text = "";
            cbxProfessionalFilter_SelectedIndexChanged(null, null);
        }

        private void cbxProfessionalFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cbxProfessionalFilter.Text))
            {
                try
                {
                    dgvProfessionals.Rows.Clear();
                    var rowIndex = 0;
                    var selectQuary = "SELECT tbl_ProfessionalInformation.ProfessionalID, tbl_ProfessionalInformation.ProfessionalFirstName, tbl_ProfessionalInformation.ProfessionalLastName, tbl_ProfessionalInformation.ProfessionalContactNumber, tbl_ProfessionalInformation.ProfessionalPresentAddress, tbl_ProfessionalInformation.ProfessionalEmailAddress, tbl_ProfessionalInformation.ProfessionalJobTitle, tbl_ProfessionalInformation.ProfessionalSpecialization, tbl_ProfessionalInformation.ProfessionalStatus, tbl_ProfessionalInformation.ProfessionalDegree, tbl_ProfessionalInformation.ProfessionalDateSigned, tbl_ProfessionalInformation.ProfessionalValidUntil, tbl_ProfessionalInformation.ProfessionalIsActive, tbl_ProfessionalInformation.ProfessionalIsTransferee FROM tbl_ProfessionalInformation";

                    _connection.Open();
                    _command.Connection = _connection;
                    _command = new OleDbCommand(selectQuary, _connection);
                    _reader = _command.ExecuteReader();

                    while (_reader.Read())
                    {
                        rowIndex++;
                        dgvProfessionals.Rows.Add(rowIndex,
                            _reader["ProfessionalID"].ToString(),
                            _reader["ProfessionalFirstName"].ToString(),
                            _reader["ProfessionalLastName"].ToString(),
                            _reader["ProfessionalStatus"].ToString(),
                            _reader["ProfessionalValidUntil"].ToString(),
                            _reader["ProfessionalJobTitle"].ToString(),
                            _reader["ProfessionalDegree"].ToString(),
                            _reader["ProfessionalContactNumber"].ToString(),
                            _reader["ProfessionalEmailAddress"].ToString(),
                            _reader["ProfessionalPresentAddress"].ToString(),
                            _reader["ProfessionalSpecialization"].ToString(),
                            _reader["ProfessionalDateSigned"].ToString(),
                            _reader["ProfessionalIsActive"].ToString(),
                            _reader["ProfessionalIsTransferee"].ToString());
                    }
                    _reader.Close();
                    _connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to load students list " + ex.Message);
                }
            }
            else if (cbxProfessionalFilter.Text == "Transferee")
            {
                try
                {
                    dgvProfessionals.Rows.Clear();
                    var rowIndex = 0;
                    var selectQuary = "SELECT tbl_ProfessionalInformation.ProfessionalID, tbl_ProfessionalInformation.ProfessionalFirstName, tbl_ProfessionalInformation.ProfessionalLastName, tbl_ProfessionalInformation.ProfessionalContactNumber, tbl_ProfessionalInformation.ProfessionalPresentAddress, tbl_ProfessionalInformation.ProfessionalEmailAddress, tbl_ProfessionalInformation.ProfessionalJobTitle, tbl_ProfessionalInformation.ProfessionalSpecialization, tbl_ProfessionalInformation.ProfessionalStatus, tbl_ProfessionalInformation.ProfessionalDegree, tbl_ProfessionalInformation.ProfessionalDateSigned, tbl_ProfessionalInformation.ProfessionalValidUntil, tbl_ProfessionalInformation.ProfessionalIsActive, tbl_ProfessionalInformation.ProfessionalIsTransferee FROM tbl_ProfessionalInformation WHERE tbl_ProfessionalInformation.ProfessionalIsTransferee=@ProfessionalIsTransferee";

                    _connection.Open();
                    _command.Connection = _connection;
                    _command = new OleDbCommand(selectQuary, _connection);
                    _command.Parameters.AddWithValue("@ProfessionalIsTransferee", "Yes");
                    _reader = _command.ExecuteReader();

                    while (_reader.Read())
                    {
                        rowIndex++;
                        dgvProfessionals.Rows.Add(rowIndex,
                            _reader["ProfessionalID"].ToString(),
                            _reader["ProfessionalFirstName"].ToString(),
                            _reader["ProfessionalLastName"].ToString(),
                            _reader["ProfessionalStatus"].ToString(),
                            _reader["ProfessionalValidUntil"].ToString(),
                            _reader["ProfessionalJobTitle"].ToString(),
                            _reader["ProfessionalDegree"].ToString(),
                            _reader["ProfessionalContactNumber"].ToString(),
                            _reader["ProfessionalEmailAddress"].ToString(),
                            _reader["ProfessionalPresentAddress"].ToString(),
                            _reader["ProfessionalSpecialization"].ToString(),
                            _reader["ProfessionalDateSigned"].ToString(),
                            _reader["ProfessionalIsActive"].ToString(),
                            _reader["ProfessionalIsTransferee"].ToString());
                    }
                    _reader.Close();
                    _connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to load students list " + ex.Message);
                }
            }
            else
            {
                try
                {
                    dgvProfessionals.Rows.Clear();
                    var rowIndex = 0;
                    var selectQuary = "SELECT tbl_ProfessionalInformation.ProfessionalID, tbl_ProfessionalInformation.ProfessionalFirstName, tbl_ProfessionalInformation.ProfessionalLastName, tbl_ProfessionalInformation.ProfessionalContactNumber, tbl_ProfessionalInformation.ProfessionalPresentAddress, tbl_ProfessionalInformation.ProfessionalEmailAddress, tbl_ProfessionalInformation.ProfessionalJobTitle, tbl_ProfessionalInformation.ProfessionalSpecialization, tbl_ProfessionalInformation.ProfessionalStatus, tbl_ProfessionalInformation.ProfessionalDegree, tbl_ProfessionalInformation.ProfessionalDateSigned, tbl_ProfessionalInformation.ProfessionalValidUntil, tbl_ProfessionalInformation.ProfessionalIsActive, tbl_ProfessionalInformation.ProfessionalIsTransferee FROM tbl_ProfessionalInformation WHERE tbl_ProfessionalInformation.ProfessionalStatus=@ProfessionalStatus";

                    _connection.Open();
                    _command.Connection = _connection;
                    _command = new OleDbCommand(selectQuary, _connection);
                    _command.Parameters.AddWithValue("@ProfessionalStatus", cbxProfessionalFilter.Text);
                    _reader = _command.ExecuteReader();

                    while (_reader.Read())
                    {
                        rowIndex++;
                        dgvProfessionals.Rows.Add(rowIndex,
                            _reader["ProfessionalID"].ToString(),
                            _reader["ProfessionalFirstName"].ToString(),
                            _reader["ProfessionalLastName"].ToString(),
                            _reader["ProfessionalStatus"].ToString(),
                            _reader["ProfessionalValidUntil"].ToString(),
                            _reader["ProfessionalJobTitle"].ToString(),
                            _reader["ProfessionalDegree"].ToString(),
                            _reader["ProfessionalContactNumber"].ToString(),
                            _reader["ProfessionalEmailAddress"].ToString(),
                            _reader["ProfessionalPresentAddress"].ToString(),
                            _reader["ProfessionalSpecialization"].ToString(),
                            _reader["ProfessionalDateSigned"].ToString(),
                            _reader["ProfessionalIsActive"].ToString(),
                            _reader["ProfessionalIsTransferee"].ToString());
                    }
                    _reader.Close();
                    _connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to load students list " + ex.Message);
                }
            }
        }

        #endregion ProfessionalTab

        #region About

        private void tspAbout_Click(object sender, EventArgs e)
        {
            var about = new FrmAbout();
            about.Show();
        }

        #endregion About

        #region Account

        private void tspMenuAccount_Click(object sender, EventArgs e)
        {
            var account = new FrmUserAccount();
            account.Show();
        }

        #endregion Account
    }
}