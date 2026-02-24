using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University_Management_System_APIs_Global
{
    public class EnrollmentDataResult
    {
        public int ReturnValue { get; set; }
        public int? EnrollmentCount { get; set; }
        public int? NewEnrollmentID { get; set; }
        public decimal? StudentGPA { get; set; }

        public EnrollmentDataResult(int returnValue, int? enrollmentCount = -1, int? newEnrollmentID = -1, decimal? studentGPA = -1m)
        {
            this.ReturnValue = returnValue;
            this.EnrollmentCount = enrollmentCount;
            this.NewEnrollmentID = newEnrollmentID;
            this.StudentGPA = studentGPA;
        }
    }
}
