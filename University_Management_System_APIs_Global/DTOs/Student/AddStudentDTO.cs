using University_Management_System_APIs_Global.DTOs.Person;

namespace University_Management_System_APIs_Global.DTOs.Student
{
    public class AddStudentDTO
    {
        // Properties
        public AddPersonDTO AddPersonDTO { get; set; }
        public string StudentMajor { get; set; }
        public int StudyYear { get; set; }
        public int StudySemester { get; set; }
        public int CreatedByUserID { get; set; }


        // Parameterized Constructor
        public AddStudentDTO(string studentMajor, int studyYear, int studySemester, int createdByUserID, AddPersonDTO addPersonDTO)
        {
            this.AddPersonDTO = addPersonDTO;
            this.StudentMajor = studentMajor;
            this.StudyYear = studyYear;
            this.StudySemester = studySemester;
            this.CreatedByUserID = createdByUserID;
        }
    }
}
