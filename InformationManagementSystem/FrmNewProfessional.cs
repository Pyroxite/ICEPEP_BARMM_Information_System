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
            _main = main;
            lblProfessionalDateID.Text = DateTime.Now.ToString("yyyy");
            tbxProfessionalRegionChapter.Text = "BARMM";
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

        private string RegularOneYearValidTime()
        {
            var dateSigned = dtpProfessionalDateSigned.Value.ToString("MM/dd/yyyy");
            var validUntil = DateTime.Parse(dateSigned);
            return validUntil.AddYears(1).ToString("MM/dd/yyyy");
        }

        private string RegularThreeYearValidTime()
        {
            var dateSigned = dtpProfessionalDateSigned.Value.ToString("MM/dd/yyyy");
            var validUntil = DateTime.Parse(dateSigned);
            return validUntil.AddYears(3).ToString("MM/dd/yyyy");
        }

        private string AssociateOneYearValidTime()
        {
            var dateSigned = dtpProfessionalDateSigned.Value.ToString("MM/dd/yyyy");
            var validUntil = DateTime.Parse(dateSigned);
            return validUntil.AddYears(1).ToString("MM/dd/yyyy");
        }

        private string AssociateThreeYearValidTime()
        {
            var dateSigned = dtpProfessionalDateSigned.Value.ToString("MM/dd/yyyy");
            var validUntil = DateTime.Parse(dateSigned);
            return validUntil.AddYears(3).ToString("MM/dd/yyyy");
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
            if (chxProfessionalRegularMemberOneYear.Checked == false && chxProfessionalRegularMemberThreeYears.Checked == false && chxProfessionalAsscociateMemberOneYear.Checked == false && chxProfessionalAsscociateMemberThreeYear.Checked == false)
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
            chxProfessionalRegularMemberOneYear.Checked = false;
            chxProfessionalRegularMemberThreeYears.Checked = false;
            chxProfessionalAsscociateMemberOneYear.Checked = false;
            chxProfessionalAsscociateMemberThreeYear.Checked = false;
            chxProfessionalTransferee.Checked = false;
        }

        private void InsertData()
        {
            if (string.IsNullOrWhiteSpace(tbxProfessionalID.Text) || string.IsNullOrWhiteSpace(tbxProfessionalFirstName.Text) || string.IsNullOrWhiteSpace(tbxProfessionalMiddleName.Text) || string.IsNullOrWhiteSpace(tbxProfessionalLastName.Text) || string.IsNullOrWhiteSpace(tbxProfessionalEmailAddress.Text) || string.IsNullOrWhiteSpace(tbxProfessionalRegionChapter.Text) || string.IsNullOrWhiteSpace(tbxProfessionalContact.Text) || string.IsNullOrWhiteSpace(tbxProfessionalPresentAddress.Text) || string.IsNullOrWhiteSpace(cbxProfessionalCurrentEmployer.Text) || string.IsNullOrWhiteSpace(cbxProfessionalJobTitle.Text) || string.IsNullOrWhiteSpace(tbxProfessionalEmployeeAddress.Text) || string.IsNullOrWhiteSpace(dtpProfessionalDateSigned.Text) || chxProfessionalRegularMemberOneYear.Checked == false && chxProfessionalRegularMemberThreeYears.Checked == false && chxProfessionalLifetime.Checked == false && chxProfessionalAsscociateMemberOneYear.Checked == false && chxProfessionalAsscociateMemberThreeYear.Checked == false)
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
                    if (chxProfessionalRegularMemberOneYear.Checked == true)
                        _command.Parameters.AddWithValue("@ProfessionalStatus", "Regular One Year");
                    else if (chxProfessionalRegularMemberThreeYears.Checked == true)
                        _command.Parameters.AddWithValue("@ProfessionalStatus", "Regular Three Year");
                    else if (chxProfessionalAsscociateMemberOneYear.Checked == true)
                        _command.Parameters.AddWithValue("@ProfessionalStatus", "Associate One Year");
                    else if (chxProfessionalAsscociateMemberThreeYear.Checked == true)
                        _command.Parameters.AddWithValue("@ProfessionalStatus", "Associate Three Year");
                    else if (chxProfessionalLifetime.Checked == true)
                        _command.Parameters.AddWithValue("@ProfessionalStatus", "Lifetime");

                    _command.Parameters.AddWithValue("@ProfessionalDateSigned", dtpProfessionalDateSigned.Value.ToString("MM/dd/yyyy"));

                    //Valid Time
                    if (chxProfessionalRegularMemberOneYear.Checked == true)
                        _command.Parameters.AddWithValue("@ProfessionalValidUntil", RegularOneYearValidTime());
                    else if (chxProfessionalRegularMemberThreeYears.Checked == true)
                        _command.Parameters.AddWithValue("@ProfessionalValidUntil", RegularThreeYearValidTime());
                    else if (chxProfessionalAsscociateMemberOneYear.Checked == true)
                        _command.Parameters.AddWithValue("@ProfessionalValidUntil", AssociateOneYearValidTime());
                    else if (chxProfessionalAsscociateMemberThreeYear.Checked == true)
                        _command.Parameters.AddWithValue("@ProfessionalValidUntil", AssociateThreeYearValidTime());
                    else if (chxProfessionalLifetime.Checked == true)
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

        private void chxProfessionalRegularMemberOneYear_CheckedChanged(object sender, EventArgs e)
        {
            if (chxProfessionalRegularMemberOneYear.Checked == true)
            {
                UnlockDateTime();
                chxProfessionalRegularMemberThreeYears.Checked = false;
                chxProfessionalRegularMemberThreeYears.CheckState = CheckState.Unchecked;
                chxProfessionalAsscociateMemberOneYear.Checked = false;
                chxProfessionalAsscociateMemberOneYear.CheckState = CheckState.Unchecked;
                chxProfessionalAsscociateMemberThreeYear.Checked = false;
                chxProfessionalAsscociateMemberThreeYear.CheckState = CheckState.Unchecked;
                chxProfessionalLifetime.Checked = false;
                chxProfessionalLifetime.CheckState = CheckState.Unchecked;
            }
            else
            {
                LockDateTime();
            }
        }

        private void chxProfessionalRegularMemberThreeYears_CheckedChanged(object sender, EventArgs e)
        {
            if (chxProfessionalRegularMemberThreeYears.Checked == true)
            {
                UnlockDateTime();
                chxProfessionalRegularMemberOneYear.Checked = false;
                chxProfessionalRegularMemberOneYear.CheckState = CheckState.Unchecked;
                chxProfessionalAsscociateMemberOneYear.Checked = false;
                chxProfessionalAsscociateMemberOneYear.CheckState = CheckState.Unchecked;
                chxProfessionalAsscociateMemberThreeYear.Checked = false;
                chxProfessionalAsscociateMemberThreeYear.CheckState = CheckState.Unchecked;
                chxProfessionalLifetime.Checked = false;
                chxProfessionalLifetime.CheckState = CheckState.Unchecked;
            }
            else
            {
                LockDateTime();
            }
        }

        private void chxProfessionalAsscociateMemberOneYear_CheckedChanged(object sender, EventArgs e)
        {
            if (chxProfessionalAsscociateMemberOneYear.Checked == true)
            {
                UnlockDateTime();
                chxProfessionalRegularMemberOneYear.Checked = false;
                chxProfessionalRegularMemberOneYear.CheckState = CheckState.Unchecked;
                chxProfessionalRegularMemberThreeYears.Checked = false;
                chxProfessionalRegularMemberThreeYears.CheckState = CheckState.Unchecked;
                chxProfessionalAsscociateMemberThreeYear.Checked = false;
                chxProfessionalAsscociateMemberThreeYear.CheckState = CheckState.Unchecked;
                chxProfessionalLifetime.Checked = false;
                chxProfessionalLifetime.CheckState = CheckState.Unchecked;
            }
            else
            {
                LockDateTime();
            }
        }

        private void chxProfessionalAsscociateMemberThreeYear_CheckedChanged(object sender, EventArgs e)
        {
            if (chxProfessionalAsscociateMemberThreeYear.Checked == true)
            {
                UnlockDateTime();
                chxProfessionalRegularMemberOneYear.Checked = false;
                chxProfessionalRegularMemberOneYear.CheckState = CheckState.Unchecked;
                chxProfessionalRegularMemberThreeYears.Checked = false;
                chxProfessionalRegularMemberThreeYears.CheckState = CheckState.Unchecked;
                chxProfessionalAsscociateMemberOneYear.Checked = false;
                chxProfessionalAsscociateMemberOneYear.CheckState = CheckState.Unchecked;
                chxProfessionalLifetime.Checked = false;
                chxProfessionalLifetime.CheckState = CheckState.Unchecked;
            }
            else
            {
                LockDateTime();
            }
        }

        private void chxProfessionalLifetime_CheckedChanged(object sender, EventArgs e)
        {
            if (chxProfessionalLifetime.Checked == true)
            {
                chxProfessionalRegularMemberOneYear.Checked = false;
                chxProfessionalRegularMemberOneYear.CheckState = CheckState.Unchecked;
                chxProfessionalRegularMemberThreeYears.Checked = false;
                chxProfessionalRegularMemberThreeYears.CheckState = CheckState.Unchecked;
                chxProfessionalAsscociateMemberOneYear.Checked = false;
                chxProfessionalAsscociateMemberOneYear.CheckState = CheckState.Unchecked;
                chxProfessionalAsscociateMemberThreeYear.Checked = false;
                chxProfessionalAsscociateMemberThreeYear.CheckState = CheckState.Unchecked;
                UnlockDateTime();
            }
            else
            {
                LockDateTime();
            }
        }

        private void btnProfessionalBrowse_Click(object sender, EventArgs e)
        {
            AddImage();
        }
    }
}