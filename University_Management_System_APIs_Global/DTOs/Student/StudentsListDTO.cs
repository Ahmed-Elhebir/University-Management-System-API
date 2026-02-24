using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University_Management_System_APIs_Global.Enums;

namespace University_Management_System_APIs_Global.DTOs.Student
{
    public class StudentsListDTO
    {
        public string StudentNo { get; set; }
        public string FullName { get; set; }
        public string StudentMajor { get; set; }
        public byte StudyYear { get; set; }
        public byte StudySemester { get; set; }
        public string Gender { get; set; }
        public byte Age { get; set; }
        public string CountryName { get; set; }
        public string NationalNumber { get; set; }

        public StudentsListDTO(string studentNo, string fullName, string studentMajor, byte studyYear, byte studySemester,
            string gender, byte age, string countryName, string nationalNumber)
        {
            this.StudentNo = studentNo;
            this.FullName = fullName;
            this.StudentMajor = studentMajor;
            this.StudyYear = studyYear;
            this.StudySemester = studySemester;
            this.Gender = gender;
            this.Age = age;
            this.CountryName = countryName;
            this.NationalNumber = nationalNumber;
        }
    }
}
