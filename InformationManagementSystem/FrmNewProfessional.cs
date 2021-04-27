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
        private readonly FrmMain _main;

        public FrmNewProfessional(FrmMain main)
        {
            InitializeComponent();
            _connection.ConnectionString = DatabaseConnection.GetConnection();
            LockDateTime();
            AutoIncrementProfessioanlID();
            _main = main;
            lblProfessionalDateID.Text = DateTime.Now.ToString("yyyy");
            tbxProfessionalRegionChapter.Text = "BARMM";
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

        private void CheckIDNumber()
        {
            try
            {
                var existingID = lblProfessionalBarmmID.Text + lblProfessionalIndentOne.Text + lblProfessionalDateID.Text + lblProfessionalIndentTwo.Text + tbxProfessionalID.Text;
                var checkQuary = "SELECT COUNT(*) FROM tbl_ProfessionalInformation WHERE ProfessionalID = @ProfessionalID";
                _connection.Open();
                _command.Connection = _connection;
                _command = new OleDbCommand(checkQuary, _connection);
                _command.Parameters.AddWithValue("@ProfessionalID", existingID);

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
            catch (Exception ex)
            {
                MessageBox.Show("Error in checking the id " + ex.Message);
            }
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
            tbxProfessionalEmployeeAddress.Clear();
            tbxProfessionalSpecializations.Clear();
            cbxProfessionalSchool.Text = "";
            cbxProfessionalDegree.Text = "";
            dtpProfessionalYearGraduated.Value = DateTime.Now;
            dtpProfessionalDateSigned.Value = DateTime.Now;
            chxProfessionalRegularOneYear.Checked = false;
            chxProfessionalRegularThreeYears.Checked = false;
            chxProfessionalAsscociateOneYear.Checked = false;
            chxProfessionalAsscociateThreeYear.Checked = false;
            chxProfessionalTransferee.Checked = false;
        }

        private void InsertData()
        {
            if (string.IsNullOrWhiteSpace(tbxProfessionalID.Text) || string.IsNullOrWhiteSpace(tbxProfessionalFirstName.Text) || string.IsNullOrWhiteSpace(tbxProfessionalMiddleName.Text) || string.IsNullOrWhiteSpace(tbxProfessionalLastName.Text) || string.IsNullOrWhiteSpace(tbxProfessionalEmailAddress.Text) || string.IsNullOrWhiteSpace(tbxProfessionalRegionChapter.Text) || string.IsNullOrWhiteSpace(tbxProfessionalContact.Text) || string.IsNullOrWhiteSpace(tbxProfessionalPresentAddress.Text) || string.IsNullOrWhiteSpace(cbxProfessionalCurrentEmployer.Text) || string.IsNullOrWhiteSpace(cbxProfessionalJobTitle.Text) || string.IsNullOrWhiteSpace(tbxProfessionalEmployeeAddress.Text) || string.IsNullOrWhiteSpace(dtpProfessionalDateSigned.Text) || chxProfessionalRegularOneYear.Checked == false && chxProfessionalRegularThreeYears.Checked == false && chxProfessionalRegularLifetime.Checked == false && chxProfessionalAsscociateOneYear.Checked == false && chxProfessionalAsscociateThreeYear.Checked == false)
            {
                MessageBox.Show("Some fields are missing");
                return;
            }
            else
            {
                try
                {
                    var professionalID = lblProfessionalBarmmID.Text + lblProfessionalIndentOne.Text + lblProfessionalDateID.Text + lblProfessionalIndentTwo.Text + tbxProfessionalID.Text;

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
                    _command.Parameters.AddWithValue("@ProfessionalSchool", cbxProfessionalSchool.Text);
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
                        _command.Parameters.AddWithValue("@ProfessionalValidUntil", ProfessionalTimeValidity.RegularOneYearValidTime(dtpProfessionalDateSigned));
                    else if (chxProfessionalRegularThreeYears.Checked == true)
                        _command.Parameters.AddWithValue("@ProfessionalValidUntil", ProfessionalTimeValidity.RegularThreeYearValidTime(dtpProfessionalDateSigned));
                    else if (chxProfessionalAsscociateOneYear.Checked == true)
                        _command.Parameters.AddWithValue("@ProfessionalValidUntil", ProfessionalTimeValidity.AssociateOneYearValidTime(dtpProfessionalDateSigned));
                    else if (chxProfessionalAsscociateThreeYear.Checked == true)
                        _command.Parameters.AddWithValue("@ProfessionalValidUntil", ProfessionalTimeValidity.AssociateThreeYearValidTime(dtpProfessionalDateSigned));
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
                    MessageBox.Show("Connection Failed " + ex.Message);
                }
            }
        }

        private void btnProfessionalSave_Click(object sender, EventArgs e)
        {
            CheckIDNumber();
        }

        #region CheckBoxes

        private void chxProfessionalRegularMemberOneYear_CheckedChanged(object sender, EventArgs e)
        {
            if (chxProfessionalRegularOneYear.Checked == true)
            {
                UnlockDateTime();
                chxProfessionalRegularThreeYears.Checked = false;
                chxProfessionalRegularThreeYears.CheckState = CheckState.Unchecked;
                chxProfessionalAsscociateOneYear.Checked = false;
                chxProfessionalAsscociateOneYear.CheckState = CheckState.Unchecked;
                chxProfessionalAsscociateThreeYear.Checked = false;
                chxProfessionalAsscociateThreeYear.CheckState = CheckState.Unchecked;
                chxProfessionalRegularLifetime.Checked = false;
                chxProfessionalRegularLifetime.CheckState = CheckState.Unchecked;
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
                chxProfessionalAsscociateOneYear.Checked = false;
                chxProfessionalAsscociateOneYear.CheckState = CheckState.Unchecked;
                chxProfessionalAsscociateThreeYear.Checked = false;
                chxProfessionalAsscociateThreeYear.CheckState = CheckState.Unchecked;
                chxProfessionalRegularLifetime.Checked = false;
                chxProfessionalRegularLifetime.CheckState = CheckState.Unchecked;
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
                chxProfessionalRegularOneYear.Checked = false;
                chxProfessionalRegularOneYear.CheckState = CheckState.Unchecked;
                chxProfessionalRegularThreeYears.Checked = false;
                chxProfessionalRegularThreeYears.CheckState = CheckState.Unchecked;
                chxProfessionalAsscociateThreeYear.Checked = false;
                chxProfessionalAsscociateThreeYear.CheckState = CheckState.Unchecked;
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
                chxProfessionalRegularOneYear.Checked = false;
                chxProfessionalRegularOneYear.CheckState = CheckState.Unchecked;
                chxProfessionalRegularThreeYears.Checked = false;
                chxProfessionalRegularThreeYears.CheckState = CheckState.Unchecked;
                chxProfessionalAsscociateOneYear.Checked = false;
                chxProfessionalAsscociateOneYear.CheckState = CheckState.Unchecked;
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

        #endregion

        private void btnProfessionalBrowse_Click(object sender, EventArgs e)
        {
            AddImage();
        }
    }
}