using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University_Management_System_APIs_Global.DTOs.Course
{
    public class AddCourseDTO
    {
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public byte Credits { get; set; }
        public byte MaxCapacity { get; set; }
        public int CreatedByUserID { get; set; }


        public AddCourseDTO(string courseName, string courseCode, byte credits, byte maxCapacity, int createdByUserID)
        {
            CourseName = courseName;
            CourseCode = courseCode;
            Credits = credits;
            MaxCapacity = maxCapacity;
            CreatedByUserID = createdByUserID;
        }


    }
}
