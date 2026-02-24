using University_Management_System_APIs_Global.DTOs.Person;

namespace University_Management_System_APIs_Global.DTOs.Student
{
    public class UpdateStudentDTO
    {
        // Properties
        public string StudentMajor { get; set; }
        public int StudyYear { get; set; }
        public int StudySemester { get; set; }
        public UpdatePersonDTO UpdatePersonDTO { get; set; }


        // Parameterized Constructor
        public UpdateStudentDTO(string studentMajor, int studyYear, int studySemester, UpdatePersonDTO updatePersonDTO)
        {
            StudentMajor = studentMajor;
            StudyYear = studyYear;
            StudySemester = studySemester;
            UpdatePersonDTO = updatePersonDTO;
        }
    }
}
