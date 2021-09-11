using System.Windows.Forms;

namespace InformationManagementSystem
{
    public class SingleInstanceForm
    {
        private Form currentForm;

        public void OpenForm(Form form)
        {
            if (currentForm != null)
                currentForm.Close();

            currentForm = form;
            form.BringToFront();
            form.Show();
        }
    }
}
