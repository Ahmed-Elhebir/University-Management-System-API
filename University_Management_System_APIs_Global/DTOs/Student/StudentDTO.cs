using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University_Management_System_APIs_Global.DTOs.Person;

namespace University_Management_System_APIs_Global.DTOs.Student
{
    public class StudentDTO
    {
        // Primary Key
        public int StudentID { get; set; }

        // Properties
        public string StudentNo { get; set; }
        public string StudentMajor { get; set; }
        public byte StudyYear { get; set; }
        public byte StudySemester { get; set; }
        public int CreatedByUserID { get; set; }
        public PersonDTO PersonDTO { get; set; }

        // Parameterized Constructor
        public StudentDTO(int studentID, string studentNo, string studentMajor,
            byte studyYear, byte studySemester, int createdByUserID, PersonDTO personDTO)
        {
            StudentID = studentID;
            StudentNo = studentNo;
            StudentMajor = studentMajor;
            StudyYear = studyYear;
            StudySemester = studySemester;
            CreatedByUserID = createdByUserID;
            PersonDTO = personDTO;
        }
    }
}
