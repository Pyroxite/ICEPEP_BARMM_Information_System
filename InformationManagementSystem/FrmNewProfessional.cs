using System;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace InformationManagementSystem
{
    public partial class FrmNewProfessional : Form
    {
        private readonly OleDbConnection _connection = new OleDbConnection();
        private OleDbCommand _command = new OleDbCommand();
        private OleDbDataReader _reader;
        private readonly FrmMain _main;
        private readonly SingleInstanceForm _singleInstance = new SingleInstanceForm();

        public FrmNewProfessional(FrmMain main)
        {
            InitializeComponent();
            _connection.ConnectionString = DatabaseHelper.GetConnection();
            _main = main;
            LockDateTime();
            AutoIncrementProfessioanlID();
            LoadJobTitle();
            LoadCurrentEmployerAndAddress();
            LoadTertiarySchool();
            LoadDegree();
            lblProfessionalDateID.Text = DateTime.Now.ToString("yyyy");
            tbxProfessionalRegionChapter.Text = "BARMM";
            btnProfessionalUpdate.Enabled = false;
        }

        private void AutoIncrementProfessioanlID()
        {
            var autoIncrement = new AutoIncrementID();
            var query = "SELECT tbl_ProfessionalInformation.ProfessionalID FROM tbl_ProfessionalInformation";
            var queryID = "ProfessionalID";
            autoIncrement.IncrementID(tbxProfessionalID, query, queryID);
            autoIncrement.GetIncrementedID(tbxProfessionalID.Text);
        }

        private void AddImage()
        {
            var openImage = new OpenFileDialog
            {
                Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp, *.png)|*.jpg; *.jpeg; *.gif; *.bmp, *.png"
            };
            if (openImage.ShowDialog() == DialogResult.OK)
                pbxProfessionalPicture.Image = new Bitmap(openImage.FileName);
        }

        private void LockDateTime()
        {
            if (chxProfessionalRegularOneYear.Checked == false && chxProfessionalRegularThreeYears.Checked == false && chxProfessionalAsscociateOneYear.Checked == false && chxProfessionalAsscociateThreeYear.Checked == false)
                dtpProfessionalDateSigned.Enabled = false;
        }

        private void UnlockDateTime()
        {
            dtpProfessionalDateSigned.Enabled = true;
        }

        public void LoadDegree()
        {
            try
            {
                cbxProfessionalDegree.Items.Clear();

                var selectQuary = "SELECT tbl_ProfessionalDegree.DegreeName FROM tbl_ProfessionalDegree";

                _connection.Open();
                _command.Connection = _connection;
                _command = new OleDbCommand(selectQuary, _connection);
                _reader = _command.ExecuteReader();

                while (_reader.Read())
                {
                    cbxProfessionalDegree.Items.Add(_reader["DegreeName"].ToString());
                }
                _reader.Close();
                _connection.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to load current school list");
            }
        }

        public void LoadTertiarySchool()
        {
            try
            {
                cbxProfessionalTertiarySchool.Items.Clear();

                var selectQuary = "SELECT tbl_ProfessionalTertiarySchool.TertiarySchoolName FROM tbl_ProfessionalTertiarySchool";

                _connection.Open();
                _command.Connection = _connection;
                _command = new OleDbCommand(selectQuary, _connection);
                _reader = _command.ExecuteReader();

                while (_reader.Read())
                {
                    cbxProfessionalTertiarySchool.Items.Add(_reader["TertiarySchoolName"].ToString());
                }
                _reader.Close();
                _connection.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to load current school list");
            }
        }

        public void LoadJobTitle()
        {
            try
            {
                cbxProfessionalJobTitle.Items.Clear();

                var selectQuary = "SELECT tbl_ProfessionalJobTitle.JobTitle FROM tbl_ProfessionalJobTitle WHERE JobITitleD";

                _connection.Open();
                _command.Connection = _connection;
                _command = new OleDbCommand(selectQuary, _connection);
                _reader = _command.ExecuteReader();

                while (_reader.Read())
                {
                    cbxProfessionalJobTitle.Items.Add(_reader["JobTitle"].ToString());
                }
                _reader.Close();
                _connection.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to load current school list");
            }
        }

        public void LoadCurrentEmployerAndAddress()
        {
            try
            {
                cbxProfessionalCurrentEmployer.Items.Clear();

                var selectQuary = "SELECT tbl_ProfessionalEmployer.EmployerName FROM tbl_ProfessionalEmployer WHERE EmployerID";

                _connection.Open();
                _command.Connection = _connection;
                _command = new OleDbCommand(selectQuary, _connection);
                _reader = _command.ExecuteReader();

                while (_reader.Read())
                {
                    cbxProfessionalCurrentEmployer.Items.Add(_reader["EmployerName"].ToString());
                }
                _reader.Close();
                _connection.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to load current school list");
            }
        }

        private void ClearFields()
        {
            tbxProfessionalFirstName.Clear();
            tbxProfessionalMiddleName.Clear();
            tbxProfessionalLastName.Clear();
            tbxProfessionalSuffixName.Clear();
            tbxProfessionalID.Clear();
            tbxProfessionalEmailAddress.Clear();
            tbxProfessionalRegionChapter.Clear();
            tbxProfessionalContact.Clear();
            tbxProfessionalPresentAddress.Clear();

            Image image;
            var profilePath = "default.jpg";
            var applicationPath = Application.StartupPath + @"\Pictures\";
            var getProfile = Path.Combine(applicationPath, profilePath);

            using (Stream stream = File.OpenRead(getProfile))
                image = Image.FromStream(stream);

            pbxProfessionalPicture.Image = image;

            cbxProfessionalCurrentEmployer.Text = "";
            cbxProfessionalJobTitle.Text = "";
            tbxProfessionalEmployeeAddress.Text = "";
            tbxProfessionalSpecializations.Clear();
            cbxProfessionalTertiarySchool.Text = "";
            cbxProfessionalDegree.Text = "";
            dtpProfessionalYearGraduated.Value = DateTime.Now;
            dtpProfessionalDateSigned.Value = DateTime.Now;
            chxProfessionalRegularOneYear.Checked = false;
            chxProfessionalRegularThreeYears.Checked = false;
            chxProfessionalAsscociateOneYear.Checked = false;
            chxProfessionalAsscociateThreeYear.Checked = false;
            chxProfessionalTransferee.Checked = false;
        }

        public void InsertData()
        {
            if (string.IsNullOrWhiteSpace(tbxProfessionalID.Text))
            {
                MessageBox.Show("Some fields are missing");
            }
            else
            {
                try
                {
                    var professionalID = lblProfessionalBarmmID.Text + lblProfessionalIndentOne.Text + lblProfessionalDateID.Text + lblProfessionalIndentTwo.Text + tbxProfessionalID.Text;
                    var checkQuary = "SELECT COUNT(*) FROM tbl_ProfessionalInformation WHERE ProfessionalID = @ProfessionalID";
                    var queryID = "@ProfessionalID";

                    _connection.Open();
                    _command.Connection = _connection;
                    _command = new OleDbCommand(checkQuary, _connection);
                    _command.Parameters.AddWithValue(queryID, professionalID);

                    var exist = Convert.ToInt32(_command.ExecuteScalar());

                    if (exist == 0)
                    {
                        _connection.Close();
                    }
                    else
                    {
                        _connection.Close();
                        MessageBox.Show("This professional id is already exist");
                        return;
                    }

                    pbxProfessionalPicture.Image.Save(Application.StartupPath + @"\Pictures\Professional\" + professionalID + ".jpg");

                    pbxProfessionalPicture.Image.Dispose();

                    var imagePath = Application.StartupPath + @"\Pictures\Professional\" + professionalID + ".jpg";

                    var _queryInsert = "INSERT INTO tbl_ProfessionalInformation " +
                        "(ProfessionalID," +
                        "ProfessionalFirstName," +
                        "ProfessionalMiddleName," +
                        "ProfessionalLastName," +
                        "ProfessionalSuffixName," +
                        "ProfessionalEmailAddress," +
                        "ProfessionalRegionChapter," +
                        "ProfessionalContactNumber," +
                        "ProfessionalPresentAddress," +
                        "ProfessionalPicture," +
                        "ProfessionalCurrentEmployer," +
                        "ProfessionalJobTitle," +
                        "ProfessionalEmployerAddress," +
                        "ProfessionalSpecialization," +
                        "ProfessionalSchool," +
                        "ProfessionalDegree," +
                        "ProfessionalYearGraduated," +
                        "ProfessionalStatus," +
                        "ProfessionalDateSigned," +
                        "ProfessionalValidUntil," +
                        "ProfessionalIsActive," +
                        "ProfessionalIsTransferee)" +
                        "VALUES " +
                        "(@ProfessionalID, " +
                        "@ProfessionalFirstName," +
                        "@ProfessionalMiddleName," +
                        "@ProfessionalLastName," +
                        "@ProfessionalSuffixName," +
                        "@ProfessionalEmailAddress," +
                        "@ProfessionalRegionChapter," +
                        "@ProfessionalContactNumber," +
                        "@ProfessionalPresentAddress," +
                        "@ProfessionalPicture," +
                        "@ProfessionalCurrentEmployer," +
                        "@ProfessionalJobTitle," +
                        "@ProfessionalEmployerAddress," +
                        "@ProfessionalSpecialization," +
                        "@ProfessionalSchool," +
                        "@ProfessionalDegree, " +
                        "@ProfessionalYearGraduated, " +
                        "@ProfessionalStatus, " +
                        "@ProfessionalDateSigned, " +
                        "@ProfessionalValidUntil, " +
                        "@ProfessionalIsActive, " +
                        "@ProfessionalIsTransferee)";

                    _connection.Open();
                    _command.Connection = _connection;
                    _command = new OleDbCommand(_queryInsert, _connection);
                    _command.Parameters.AddWithValue("@ProfessionalID", professionalID);
                    _command.Parameters.AddWithValue("@ProfessionalFirstName", tbxProfessionalFirstName.Text);
                    _command.Parameters.AddWithValue("@ProfessionalMiddleName", tbxProfessionalMiddleName.Text);
                    _command.Parameters.AddWithValue("@ProfessionalLastName", tbxProfessionalLastName.Text);
                    _command.Parameters.AddWithValue("@ProfessionalSuffixName", tbxProfessionalSuffixName.Text);
                    _command.Parameters.AddWithValue("@ProfessionalEmailAddress", tbxProfessionalEmailAddress.Text);
                    _command.Parameters.AddWithValue("@ProfessionalRegionChapter", tbxProfessionalRegionChapter.Text);
                    _command.Parameters.AddWithValue("@ProfessionalContactNumber", tbxProfessionalContact.Text);
                    _command.Parameters.AddWithValue("@ProfessionalPresentAddress", tbxProfessionalPresentAddress.Text);
                    _command.Parameters.AddWithValue("@ProfessionalPicture", imagePath);
                    _command.Parameters.AddWithValue("@ProfessionalCurrentEmployer", cbxProfessionalCurrentEmployer.Text);
                    _command.Parameters.AddWithValue("@ProfessionalJobTitle", cbxProfessionalJobTitle.Text);
                    _command.Parameters.AddWithValue("@ProfessionalEmployerAddress", tbxProfessionalEmployeeAddress.Text);
                    _command.Parameters.AddWithValue("@ProfessionalSpecialization", tbxProfessionalSpecializations.Text);
                    _command.Parameters.AddWithValue("@ProfessionalSchool", cbxProfessionalTertiarySchool.Text);
                    _command.Parameters.AddWithValue("@ProfessionalDegree", cbxProfessionalDegree.Text);
                    _command.Parameters.AddWithValue("@ProfessionalYearGraduated", dtpProfessionalYearGraduated.Value.ToString("MM/dd/yyyy"));

                    //Status
                    if (chxProfessionalRegular.Checked == true)
                        _command.Parameters.AddWithValue("@ProfessionalStatus", "Regular");
                    else if (chxProfessionalAssociate.Checked == true)
                        _command.Parameters.AddWithValue("@ProfessionalStatus", "Associate");

                    _command.Parameters.AddWithValue("@ProfessionalDateSigned", dtpProfessionalDateSigned.Value.ToString("MM/dd/yyyy"));

                    //Valid Time
                    if (chxProfessionalRegularOneYear.Checked == true)
                        _command.Parameters.AddWithValue("@ProfessionalValidUntil", ProfessionalExpirationDate.RegularOneYearValidTime(dtpProfessionalDateSigned));
                    else if (chxProfessionalRegularThreeYears.Checked == true)
                        _command.Parameters.AddWithValue("@ProfessionalValidUntil", ProfessionalExpirationDate.RegularThreeYearValidTime(dtpProfessionalDateSigned));
                    else if (chxProfessionalAsscociateOneYear.Checked == true)
                        _command.Parameters.AddWithValue("@ProfessionalValidUntil", ProfessionalExpirationDate.AssociateOneYearValidTime(dtpProfessionalDateSigned));
                    else if (chxProfessionalAsscociateThreeYear.Checked == true)
                        _command.Parameters.AddWithValue("@ProfessionalValidUntil", ProfessionalExpirationDate.AssociateThreeYearValidTime(dtpProfessionalDateSigned));
                    else if (chxProfessionalRegularLifetime.Checked == true)
                        _command.Parameters.AddWithValue("@ProfessionalValidUntil", "Lifetime");
                    else if (chxProfessionalAsscociateLifetime.Checked == true)
                        _command.Parameters.AddWithValue("@ProfessionalValidUntil", "Lifetime");

                    _command.Parameters.AddWithValue("@ProfessionalIsActive", "Yes");

                    if (chxProfessionalTransferee.Checked == true)
                        _command.Parameters.AddWithValue("@ProfessionalIsTransferee", "Yes");
                    else if (chxProfessionalTransferee.Checked == false)
                        _command.Parameters.AddWithValue("@ProfessionalIsTransferee", "No");

                    _command.ExecuteNonQuery();
                    _connection.Close();
                    _main.LoadListOfProfessionals();
                    ClearFields();

                    MessageBox.Show("New record has been added");
                }
                catch (Exception ex)
                {
                    _connection.Close();
                    MessageBox.Show("Failed to add this member " + ex.Message);
                }
            }
        }

        private void btnProfessionalSave_Click(object sender, EventArgs e)
        {
            InsertData();
        }

        private void btnProfessionalUpdate_Click(object sender, EventArgs e)
        {
            UpdateProfessionalMember();
        }

        private void UpdateProfessionalMember()
        {
            try
            {
                var existingID = lblProfessionalBarmmID.Text + lblProfessionalIndentOne.Text + lblProfessionalDateID.Text + lblProfessionalIndentTwo.Text + tbxProfessionalID.Text;

                var imagePath = Application.StartupPath + @"\Pictures\Professional\" + existingID + ".jpg";

                Bitmap newImage = new Bitmap(pbxProfessionalPicture.Image);
                if (File.Exists(imagePath))
                    File.Delete(imagePath);
                newImage.Save(imagePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                newImage.Dispose();

                var _queryUpdate = "UPDATE tbl_ProfessionalInformation SET ProfessionalFirstName = @ProfessionalFirstName, ProfessionalMiddleName = @ProfessionalMiddleName, ProfessionalLastName = @ProfessionalLastName, ProfessionalSuffixName = @ProfessionalSuffixName, ProfessionalEmailAddress = @ProfessionalEmailAddress, ProfessionalRegionChapter = @ProfessionalRegionChapter, ProfessionalContactNumber = @ProfessionalContactNumber, ProfessionalPresentAddress = @ProfessionalPresentAddress, ProfessionalPicture = @ProfessionalPicture, ProfessionalCurrentEmployer = @ProfessionalCurrentEmployer, ProfessionalJobTitle = @ProfessionalJobTitle, ProfessionalEmployerAddress = @ProfessionalEmployerAddress, ProfessionalSpecialization = @ProfessionalSpecialization, ProfessionalSchool = @ProfessionalSchool, ProfessionalDegree = @ProfessionalDegree, ProfessionalYearGraduated = @ProfessionalYearGraduatet WHERE ProfessionalID LIKE @ProfessionalID";

                _connection.Open();
                _command.Connection = _connection;
                _command = new OleDbCommand(_queryUpdate, _connection);
                _command.Parameters.AddWithValue("@ProfessionalFirstName", tbxProfessionalFirstName.Text);
                _command.Parameters.AddWithValue("@ProfessionalMiddleName", tbxProfessionalMiddleName.Text);
                _command.Parameters.AddWithValue("@ProfessionalLastName", tbxProfessionalLastName.Text);
                _command.Parameters.AddWithValue("@ProfessionalSuffixName", tbxProfessionalSuffixName.Text);
                _command.Parameters.AddWithValue("@ProfessionalEmailAddress", tbxProfessionalEmailAddress.Text);
                _command.Parameters.AddWithValue("@ProfessionalRegionChapter", tbxProfessionalRegionChapter.Text);
                _command.Parameters.AddWithValue("@ProfessionalContactNumber", tbxProfessionalContact.Text);
                _command.Parameters.AddWithValue("@ProfessionalPresentAddress", tbxProfessionalPresentAddress.Text);
                _command.Parameters.AddWithValue("@ProfessionalPicture", imagePath);
                _command.Parameters.AddWithValue("@ProfessionalCurrentEmployer", cbxProfessionalCurrentEmployer.Text);
                _command.Parameters.AddWithValue("@ProfessionalJobTitle", cbxProfessionalJobTitle.Text);
                _command.Parameters.AddWithValue("@ProfessionalEmployerAddress", tbxProfessionalEmployeeAddress.Text);
                _command.Parameters.AddWithValue("@ProfessionalSpecialization", tbxProfessionalSpecializations.Text);
                _command.Parameters.AddWithValue("@ProfessionalSchool", cbxProfessionalTertiarySchool.Text);
                _command.Parameters.AddWithValue("@ProfessionalDegree", cbxProfessionalDegree.Text);
                _command.Parameters.AddWithValue("@ProfessionalYearGraduated", dtpProfessionalYearGraduated.Value.ToString("MM/dd/yyyy"));
                _command.Parameters.AddWithValue("@ProfessionalID", existingID);

                _command.ExecuteNonQuery();
                _connection.Close();
                _main.LoadListOfProfessionals();
                ClearFields();
                MessageBox.Show("This member has been updated");
                Dispose();
            }
            catch (Exception)
            {
                _connection.Close();
                MessageBox.Show("Failed to update");
            }
        }

        private void btnProfessioanlCancel_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        #region CheckBoxes

        private void chxProfessionalRegularMemberOneYear_CheckedChanged(object sender, EventArgs e)
        {
            if (chxProfessionalRegularOneYear.Checked == true)
            {
                UnlockDateTime();
                chxProfessionalRegularThreeYears.Checked = false;
                chxProfessionalRegularThreeYears.CheckState = CheckState.Unchecked;
                chxProfessionalRegularLifetime.Checked = false;
                chxProfessionalRegularLifetime.CheckState = CheckState.Unchecked;

                chxProfessionalAssociate.Checked = false;
                chxProfessionalAssociate.CheckState = CheckState.Unchecked;
                chxProfessionalAsscociateOneYear.Checked = false;
                chxProfessionalAsscociateOneYear.CheckState = CheckState.Unchecked;
                chxProfessionalAsscociateThreeYear.Checked = false;
                chxProfessionalAsscociateThreeYear.CheckState = CheckState.Unchecked;
                chxProfessionalAsscociateLifetime.Checked = false;
                chxProfessionalAsscociateLifetime.CheckState = CheckState.Unchecked;
            }
            else
            {
                LockDateTime();
            }
        }

        private void chxProfessionalRegularMemberThreeYears_CheckedChanged(object sender, EventArgs e)
        {
            if (chxProfessionalRegularThreeYears.Checked == true)
            {
                UnlockDateTime();
                chxProfessionalRegularOneYear.Checked = false;
                chxProfessionalRegularOneYear.CheckState = CheckState.Unchecked;
                chxProfessionalRegularLifetime.Checked = false;
                chxProfessionalRegularLifetime.CheckState = CheckState.Unchecked;

                chxProfessionalAssociate.Checked = false;
                chxProfessionalAssociate.CheckState = CheckState.Unchecked;
                chxProfessionalAsscociateOneYear.Checked = false;
                chxProfessionalAsscociateOneYear.CheckState = CheckState.Unchecked;
                chxProfessionalAsscociateThreeYear.Checked = false;
                chxProfessionalAsscociateThreeYear.CheckState = CheckState.Unchecked;
                chxProfessionalAsscociateLifetime.Checked = false;
                chxProfessionalAsscociateLifetime.CheckState = CheckState.Unchecked;
            }
            else
            {
                LockDateTime();
            }
        }

        private void chxProfessionalAsscociateMemberOneYear_CheckedChanged(object sender, EventArgs e)
        {
            if (chxProfessionalAsscociateOneYear.Checked == true)
            {
                UnlockDateTime();
                chxProfessionalAsscociateThreeYear.Checked = false;
                chxProfessionalAsscociateThreeYear.CheckState = CheckState.Unchecked;
                chxProfessionalAsscociateLifetime.Checked = false;
                chxProfessionalAsscociateLifetime.CheckState = CheckState.Unchecked;

                chxProfessionalRegular.Checked = false;
                chxProfessionalRegular.CheckState = CheckState.Unchecked;
                chxProfessionalRegularOneYear.Checked = false;
                chxProfessionalRegularOneYear.CheckState = CheckState.Unchecked;
                chxProfessionalRegularThreeYears.Checked = false;
                chxProfessionalRegularThreeYears.CheckState = CheckState.Unchecked;
                chxProfessionalRegularLifetime.Checked = false;
                chxProfessionalRegularLifetime.CheckState = CheckState.Unchecked;
            }
            else
            {
                LockDateTime();
            }
        }

        private void chxProfessionalAsscociateMemberThreeYear_CheckedChanged(object sender, EventArgs e)
        {
            if (chxProfessionalAsscociateThreeYear.Checked == true)
            {
                UnlockDateTime();
                chxProfessionalAsscociateOneYear.Checked = false;
                chxProfessionalAsscociateOneYear.CheckState = CheckState.Unchecked;
                chxProfessionalAsscociateLifetime.Checked = false;
                chxProfessionalAsscociateLifetime.CheckState = CheckState.Unchecked;

                chxProfessionalRegular.Checked = false;
                chxProfessionalRegular.CheckState = CheckState.Unchecked;
                chxProfessionalRegularOneYear.Checked = false;
                chxProfessionalRegularOneYear.CheckState = CheckState.Unchecked;
                chxProfessionalRegularThreeYears.Checked = false;
                chxProfessionalRegularThreeYears.CheckState = CheckState.Unchecked;
                chxProfessionalRegularLifetime.Checked = false;
                chxProfessionalRegularLifetime.CheckState = CheckState.Unchecked;
            }
            else
            {
                LockDateTime();
            }
        }

        private void chxProfessionalLifetime_CheckedChanged(object sender, EventArgs e)
        {
            if (chxProfessionalRegularLifetime.Checked == true)
            {
                chxProfessionalRegularOneYear.Checked = false;
                chxProfessionalRegularOneYear.CheckState = CheckState.Unchecked;
                chxProfessionalRegularThreeYears.Checked = false;
                chxProfessionalRegularThreeYears.CheckState = CheckState.Unchecked;

                chxProfessionalAssociate.Checked = false;
                chxProfessionalAssociate.CheckState = CheckState.Unchecked;
                chxProfessionalAsscociateOneYear.Checked = false;
                chxProfessionalAsscociateOneYear.CheckState = CheckState.Unchecked;
                chxProfessionalAsscociateThreeYear.Checked = false;
                chxProfessionalAsscociateThreeYear.CheckState = CheckState.Unchecked;
                chxProfessionalAsscociateLifetime.Checked = false;
                chxProfessionalAsscociateLifetime.CheckState = CheckState.Unchecked;
                UnlockDateTime();
            }
            else
            {
                LockDateTime();
            }
        }

        private void chxProfessionalAsscociateLifetime_CheckedChanged(object sender, EventArgs e)
        {
            if (chxProfessionalAsscociateLifetime.Checked == true)
            {
                chxProfessionalRegular.Checked = false;
                chxProfessionalRegular.CheckState = CheckState.Unchecked;
                chxProfessionalRegularOneYear.Checked = false;
                chxProfessionalRegularOneYear.CheckState = CheckState.Unchecked;
                chxProfessionalRegularThreeYears.Checked = false;
                chxProfessionalRegularThreeYears.CheckState = CheckState.Unchecked;
                chxProfessionalRegularLifetime.Checked = false;
                chxProfessionalRegularLifetime.CheckState = CheckState.Unchecked;

                chxProfessionalAsscociateOneYear.Checked = false;
                chxProfessionalAsscociateOneYear.CheckState = CheckState.Unchecked;
                chxProfessionalAsscociateThreeYear.Checked = false;
                chxProfessionalAsscociateThreeYear.CheckState = CheckState.Unchecked;
                UnlockDateTime();
            }
            else
            {
                LockDateTime();
            }
        }

        private void chxProfessionalRegular_CheckedChanged(object sender, EventArgs e)
        {
            if (chxProfessionalRegular.Checked == true)
            {
                chxProfessionalAssociate.Checked = false;
                chxProfessionalAssociate.CheckState = CheckState.Unchecked;
                chxProfessionalAsscociateOneYear.Checked = false;
                chxProfessionalAsscociateOneYear.CheckState = CheckState.Unchecked;
                chxProfessionalAsscociateThreeYear.Checked = false;
                chxProfessionalAsscociateThreeYear.CheckState = CheckState.Unchecked;
                chxProfessionalAsscociateLifetime.Checked = false;
                chxProfessionalAsscociateLifetime.CheckState = CheckState.Unchecked;
                UnlockDateTime();
            }
            else
            {
                chxProfessionalRegularOneYear.Checked = false;
                chxProfessionalRegularOneYear.CheckState = CheckState.Unchecked;
                chxProfessionalRegularThreeYears.Checked = false;
                chxProfessionalRegularThreeYears.CheckState = CheckState.Unchecked;
                chxProfessionalRegularLifetime.Checked = false;
                chxProfessionalRegularLifetime.CheckState = CheckState.Unchecked;
                LockDateTime();
            }
        }

        private void chxProfessionalAssociate_CheckedChanged(object sender, EventArgs e)
        {
            if (chxProfessionalAssociate.Checked == true)
            {
                chxProfessionalRegular.Checked = false;
                chxProfessionalRegular.CheckState = CheckState.Unchecked;
                chxProfessionalRegularOneYear.Checked = false;
                chxProfessionalRegularOneYear.CheckState = CheckState.Unchecked;
                chxProfessionalRegularThreeYears.Checked = false;
                chxProfessionalRegularThreeYears.CheckState = CheckState.Unchecked;
                chxProfessionalRegularLifetime.Checked = false;
                chxProfessionalRegularLifetime.CheckState = CheckState.Unchecked;
                UnlockDateTime();
            }
            else
            {
                chxProfessionalAsscociateOneYear.Checked = false;
                chxProfessionalAsscociateOneYear.CheckState = CheckState.Unchecked;
                chxProfessionalAsscociateThreeYear.Checked = false;
                chxProfessionalAsscociateThreeYear.CheckState = CheckState.Unchecked;
                chxProfessionalAsscociateLifetime.Checked = false;
                chxProfessionalAsscociateLifetime.CheckState = CheckState.Unchecked;
                LockDateTime();
            }
        }

        #endregion CheckBoxes

        private void btnProfessionalBrowse_Click(object sender, EventArgs e)
        {
            AddImage();
        }

        private void BtnProfessionalAddCurrentEmployer_Click(object sender, EventArgs e)
        {
            _singleInstance.OpenForm(new FrmProCurrentEmployer(this));
        }

        private void CbxProfessionalCurrentEmployer_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var selectQuary = "SELECT DISTINCT tbl_ProfessionalEmployer.EmployerAddress FROM tbl_ProfessionalEmployer WHERE tbl_ProfessionalEmployer.EmployerName LIKE @EmployerName";

                _connection.Open();
                _command.Connection = _connection;
                _command = new OleDbCommand(selectQuary, _connection);
                _command.Parameters.AddWithValue("@EmployerName", cbxProfessionalCurrentEmployer.Text);
                _reader = _command.ExecuteReader();

                while (_reader.Read())
                {
                    tbxProfessionalEmployeeAddress.Text = _reader["EmployerAddress"].ToString();
                }
                _reader.Close();
                _connection.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to load current school address");
            }
        }

        private void BtnProfessionalAddJobTitle_Click(object sender, EventArgs e)
        {
            _singleInstance.OpenForm(new FrmProJobTitle(this));
        }

        private void BtnProfessionalSchool_Click(object sender, EventArgs e)
        {
            _singleInstance.OpenForm(new FrmProSchoolTertiary(this));
        }
    }
}