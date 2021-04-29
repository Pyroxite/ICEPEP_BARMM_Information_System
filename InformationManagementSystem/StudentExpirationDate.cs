using System;
using System.Windows.Forms;

namespace InformationManagementSystem
{
    public static class StudentExpirationDate
    {
        public static string ValidTime(DateTimePicker validTime)
        {
            var dateSigned = validTime.Value.ToString("MM/dd/yyyy");
            var validUntil = DateTime.Parse(dateSigned);
            return validUntil.AddYears(1).ToString("MM/dd/yyyy");
        }
    }
}