using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using University_Management_System_APIs_Global.Enums;

namespace University_Management_System_APIs_BLL
{
    internal class Validation
    {
        // Validates email format if email is not null or empty, otherwise returns true (assuming email is optional)
        public static bool ValidateEmail(string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                try
                {
                    var addr = new MailAddress(email);
                    return addr.Address == email;
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }

        // Calculates age based on the provided date of birth
        private static int CalculateAge(DateTime date)
        {
            byte age = Convert.ToByte(DateTime.Today.Year - date.Year);
            if (date.Date > DateTime.Today.AddYears(-age))
                age--;

            return age;
        }

        // Validates student age against specified minimum and maximum age limits
        public static bool ValidateStudentAge(DateTime dateOfBirth, byte minAge = 16, byte maxAge = 100)
        {
            var studentAge = CalculateAge(dateOfBirth);

            return studentAge >= minAge && studentAge <= maxAge;
        }

        // Validates that the current year of study does not exceed the maximum allowed years
        private static bool CheckYears(byte currentYear, byte maxYears)
        {
            return currentYear <= maxYears;
        }

        // Validates the current year of study based on the degree level, using predefined maximum years for each degree type
        public static bool ValidateStudyYears(byte currentYear, DegreeLevelEnum degreeLevel = DegreeLevelEnum.Bachelor)
        {
            byte maxYears = 0;

            switch (degreeLevel)
            {
                case DegreeLevelEnum.Diploma:
                    maxYears = 5;
                    break;
                case DegreeLevelEnum.Bachelor:
                    maxYears = 6;
                    break;
                case DegreeLevelEnum.Master:
                    maxYears = 4;
                    break;
                case DegreeLevelEnum.PhD:
                    maxYears = 10;
                    break;
            }

            return CheckYears(currentYear, maxYears);

        }

        // Validates that the current semester number falls within the specified minimum and maximum semester numbers
        public static bool ValidateSemesterSystemPerYear(byte currentSemester, byte minNo = 1, byte maxNo = 2)
        {
            return (currentSemester >= minNo && currentSemester <= maxNo);
        }

        // Validates that the current course credits fall within the specified minimum and maximum credit limits
        public static bool ValidateCourseCredits(byte currentCredits, byte minCredits = 1, byte maxCredits = 6)
        {
            return currentCredits >= minCredits && currentCredits <= maxCredits;
        }

        // Validates that the current course maximum capacity falls within the specified minimum and maximum capacity limits
        public static bool ValidateCourseMaxCapacity(byte currentMaxCapacity, byte minCapacity = 5, byte maxCapacity = 100)
        {
            return currentMaxCapacity >= minCapacity && currentMaxCapacity <= maxCapacity;
        }

        // Validates that the current grade falls within the specified minimum and maximum grade limits
        public static bool ValidateGrade(decimal currentGrade, decimal minGrade = 0.00m, decimal maxGrade = 100.00m)
        {
            return currentGrade >= minGrade && currentGrade <= maxGrade;
        }

        // Validates that the provided ID is a positive integer
        public static bool ValidateID(int ID)
        {
            return ID > 0;
        }

        // Validates that the provided date is not in the future, ensuring it is either today or a past date
        public static bool ValidateDate(DateTime date)
        {
            return date.Date <= DateTime.Today;
        }

        // Validates that the provided gender value is defined within the GenderEnum, ensuring it is a valid
        public static bool ValidateGender(GenderEnum genderValue)
        {
            return Enum.IsDefined(typeof(GenderEnum), genderValue);
        }

        // Validates that the provided phone number is either 10 or 12 digits long (after removing any '+' characters) and contains only digits
        public static bool ValidatePhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
                return false;

            string cleanedPhoneNumber = phoneNumber.Replace("+", "");

            if (!cleanedPhoneNumber.All(char.IsDigit))
                return false;

            return cleanedPhoneNumber.Length == 10 || cleanedPhoneNumber.Length == 12;
        }


    }
}
