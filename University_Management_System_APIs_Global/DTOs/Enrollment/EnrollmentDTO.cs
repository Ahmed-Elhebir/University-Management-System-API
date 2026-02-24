
namespace University_Management_System_APIs_Global.DTOs.Enrollment
{
    public class EnrollmentDTO
    {
        public int EnrollmentID { get; set; }
        public int StudentID { get; set; }
        public int CourseID { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public decimal? Grade { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int CreatedByUserID { get; set; }
        public bool IsDeleted { get; set; }

        public EnrollmentDTO(int enrollmentID, int studentID, int courseID, DateTime enrollmentDate, decimal? grade,
                DateTime createdDate, DateTime modifiedDate, int createdByUserID, bool isDeleted)
        {
            this.EnrollmentID = enrollmentID;
            this.StudentID = studentID;
            this.CourseID = courseID;
            this.EnrollmentDate = enrollmentDate;
            this.Grade = grade;
            this.CreatedDate = createdDate;
            this.ModifiedDate = modifiedDate;
            this.CreatedByUserID = createdByUserID;
            this.IsDeleted = isDeleted;
        }
    }
}
