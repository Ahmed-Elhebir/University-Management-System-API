using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University_Management_System_APIs_Global.DTOs.Enrollment
{
    public class EnrollmentsListDTO
    {
        public int EnrollmentID { get; set; }
        public string StudentFullName { get; set; }
        public string CourseName { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public decimal? Grade { get; set; }

        public EnrollmentsListDTO(int enrollmentID, string studentFullName, string courseName, DateTime enrollmentDate, decimal? grade)
        {
            this.EnrollmentID = enrollmentID;
            this.StudentFullName = studentFullName;
            this.CourseName = courseName;
            this.EnrollmentDate = enrollmentDate;
            this.Grade = grade;
        }

    }
}
