
namespace University_Management_System_APIs_Global.DTOs.Enrollment
{
    public class AddEnrollmentDTO
    {
        public int StudentID { get; set; }
        public int CourseID { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public int CreatedByUserID { get; set; }

        public AddEnrollmentDTO(int studentID, int courseID, DateTime enrollmentDate, int createdByUserID)
        {
            this.StudentID = studentID;
            this.CourseID = courseID;
            this.EnrollmentDate = enrollmentDate;
            this.CreatedByUserID = createdByUserID;
        }
    }
}
