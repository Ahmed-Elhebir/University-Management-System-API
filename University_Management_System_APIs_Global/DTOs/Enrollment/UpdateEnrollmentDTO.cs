
namespace University_Management_System_APIs_Global.DTOs.Enrollment
{
    public class UpdateEnrollmentDTO
    {
        public DateTime EnrollmentDate { get; set; }

        public UpdateEnrollmentDTO(DateTime enrollmentDate)
        {
            this.EnrollmentDate = enrollmentDate;
        }
    }
}
