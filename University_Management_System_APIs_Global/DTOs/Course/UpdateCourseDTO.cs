using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University_Management_System_APIs_Global.DTOs.Course
{
    public class UpdateCourseDTO
    {
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public byte Credits { get; set; }
        public byte MaxCapacity { get; set; }

        public UpdateCourseDTO(string courseName, string courseCode, byte credits, byte maxCapacity)
        {
            this.CourseName = courseName;
            this.CourseCode = courseCode;
            this.Credits = credits;
            this.MaxCapacity = maxCapacity;
        }
    }
}
