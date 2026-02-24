using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University_Management_System_APIs_Global.DTOs.Course
{
    public class CourseDTO
    {
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public byte Credits { get; set; }
        public byte MaxCapacity { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int CreatedByUserID { get; set; }
        public bool IsDeleted { get; set; }

        public CourseDTO(int courseID, string courseName, string courseCode, byte credits, byte maxCapacity,
                DateTime createdDate, DateTime modifiedDate, int createdByUserID, bool isDeleted)
        {
            this.CourseID = courseID;
            this.CourseName = courseName;
            this.CourseCode = courseCode;
            this.Credits = credits;
            this.MaxCapacity = maxCapacity;
            this.CreatedDate = createdDate;
            this.ModifiedDate = modifiedDate;
            this.CreatedByUserID = createdByUserID;
            this.IsDeleted = isDeleted;
        }
    }
}
