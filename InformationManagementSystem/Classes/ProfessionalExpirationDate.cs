using System;
using System.Windows.Forms;

namespace InformationManagementSystem
{
    public static class ProfessionalExpirationDate
    {
        public static string AssociateOneYearValidTime(DateTimePicker validTime)
        {
            var dateSigned = validTime.Value.ToString("MM/dd/yyyy");
            var validUntil = DateTime.Parse(dateSigned);
            return validUntil.AddYears(1).ToString("MM/dd/yyyy");
        }

        public static string AssociateThreeYearValidTime(DateTimePicker validTime)
        {
            var dateSigned = validTime.Value.ToString("MM/dd/yyyy");
            var validUntil = DateTime.Parse(dateSigned);
            return validUntil.AddYears(3).ToString("MM/dd/yyyy");
        }

        public static string RegularOneYearValidTime(DateTimePicker validTime)
        {
            var dateSigned = validTime.Value.ToString("MM/dd/yyyy");
            var validUntil = DateTime.Parse(dateSigned);
            return validUntil.AddYears(1).ToString("MM/dd/yyyy");
        }

        public static string RegularThreeYearValidTime(DateTimePicker validTime)
        {
            var dateSigned = validTime.Value.ToString("MM/dd/yyyy");
            var validUntil = DateTime.Parse(dateSigned);
            return validUntil.AddYears(3).ToString("MM/dd/yyyy");
        }
    }
}