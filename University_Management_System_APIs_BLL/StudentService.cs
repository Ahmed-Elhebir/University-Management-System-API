using University_Management_System_APIs_Global.Enums;
using University_Management_System_APIs_Global.DTOs.Person;
using University_Management_System_APIs_Global.DTOs.Student;
using University_Management_System_APIs_Global;
using University_Management_System_APIs_DAL;

namespace University_Management_System_APIs_BLL
{
    public class StudentService
    {
        private ModeEnum _Mode = ModeEnum.AddNew;

        public AddStudentDTO ASDTO
        {
            get { return new AddStudentDTO(this.StudentMajor, this.StudyYear, this.StudySemester, this.CreatedByUserID, this.AddPersonDTO); }
        }

        public StudentDTO SDTO
        {
            get { return new StudentDTO(this.StudentID, this.StudentNo, this.StudentMajor, this.StudyYear, this.StudySemester, 
                    this.CreatedByUserID, this.PersonDTO); }
        }

        public AddPersonDTO AddPersonDTO
        {
            get {
                return new AddPersonDTO(this.PersonDTO.FirstName, this.PersonDTO.SecondName, this.PersonDTO.ThirdName, this.PersonDTO.LastName,
                this.PersonDTO.Gender, this.PersonDTO.PhoneNumber, this.PersonDTO.Email, this.PersonDTO.DateOfBirth, this.PersonDTO.NationalNumber,
                this.PersonDTO.NationalCountryID); }
        }
       

        public int StudentID { get; set; }
        public string StudentNo { get; set; }
        public string StudentMajor { get; set; }
        public byte StudyYear { get; set; }
        public byte StudySemester { get; set; }
        public int CreatedByUserID { get; set; }
        public int PersonID { get; set; }
        public PersonDTO PersonDTO { get; set; }


        public StudentService()
        {
            this.StudentID = -1;
            this.StudentNo = "";
            this.StudentMajor = "";
            this.StudyYear = 0;
            this.StudySemester = 0;
            this.CreatedByUserID = -1;
            this.PersonDTO = new PersonDTO(-1, "", "", "", "", GenderEnum.Male, "", "", DateTime.Now, "", -1, DateTime.Now, DateTime.Now, false);

            _Mode = ModeEnum.AddNew;
        }

        public StudentService(StudentDTO studentDTO, ModeEnum mode = ModeEnum.AddNew)
        {
            this.StudentID = studentDTO.StudentID;
            this.StudentNo = studentDTO.StudentNo;
            this.StudentMajor = studentDTO.StudentMajor;
            this.StudyYear = studentDTO.StudyYear;
            this.StudySemester = studentDTO.StudySemester;
            this.CreatedByUserID = studentDTO.CreatedByUserID;
            this.PersonDTO = studentDTO.PersonDTO;

            _Mode = mode;
        }

        public static OperationResult<List<StudentsListDTO>> GetAllStudents()
        {
            var result = StudentData.GetAllStudents();

            if (result != null && result.Data != null)
                return OperationResult<List<StudentsListDTO>>.Success(result.Data, "Retrieved data successfully");

            return OperationResult<List<StudentsListDTO>>.Failure("Failed to retrieve data");
        }

        private OperationResult _CheckStudentValidation()
        {
            List<string> errors = new List<string>();

            if (string.IsNullOrEmpty(PersonDTO.FirstName))
                errors.Add("First name is required");

            if (string.IsNullOrEmpty(PersonDTO.SecondName))
                errors.Add("Second name is required");

            if (string.IsNullOrEmpty(PersonDTO.LastName))
                errors.Add("Last name is required");

            if (!Validation.ValidateGender(PersonDTO.Gender))
                errors.Add("Gender is invalid");

            if (!Validation.ValidatePhoneNumber(PersonDTO.PhoneNumber))
                errors.Add("Phone number is invalid");

            if (string.IsNullOrEmpty(PersonDTO.NationalNumber))
                errors.Add("National number is required");

            if (string.IsNullOrEmpty(StudentMajor))
                errors.Add("Student major is required");

            if (!Validation.ValidateEmail(PersonDTO.Email))
                errors.Add("Email format is invalid");

            if (!Validation.ValidateStudentAge(PersonDTO.DateOfBirth))
                errors.Add("Student must be at least 18 years old");

            if (!Validation.ValidateStudyYears(StudyYear))
                errors.Add("study year is invalid");

            if (!Validation.ValidateSemesterSystemPerYear(StudySemester))
                errors.Add("semester is invalid");

            if (errors.Count > 0)
                return OperationResult.Failure(errors);

            return OperationResult.Success("Passed validation");
        }

        public static OperationResult<StudentService> Find(int studentID)
        {
            if (studentID < 1)
                return OperationResult<StudentService>.Failure("Invalid student ID");

            var result = StudentData.GetStudentByID(studentID);

            if (!result.IsSuccess || result.Data == null)
                return OperationResult<StudentService>.Failure("Student not found");

            var student = new StudentService(result.Data, ModeEnum.Update);
            student.StudentID = studentID;

            return OperationResult<StudentService>.Success(student);
        }

        public static OperationResult<StudentsListDTO> Find(string studentNo)
        {
            if (string.IsNullOrWhiteSpace(studentNo))
                return OperationResult<StudentsListDTO>.Failure("Student number is required");

            var result = StudentData.GetStudentByStudentNo(studentNo);

            if (!result.IsSuccess || result.Data == null)
                return OperationResult<StudentsListDTO>.Failure("Student not found");

            return OperationResult<StudentsListDTO>.Success(result.Data, "Student found successfully");
        }

        public static OperationResult Delete(int studentID)
        {
            if (!Validation.ValidateID(studentID))
                return OperationResult.Failure("Invalid student ID");

            var studentDataResult = StudentData.GetStudentByID(studentID);

            if (!studentDataResult.IsSuccess || studentDataResult.Data == null)
                return OperationResult.Failure("Student not found");

            var personID = studentDataResult.Data.PersonDTO.PersonID;
            var deleted = StudentData.DeleteStudent(personID);
            if (deleted.IsSuccess)
                return OperationResult.Success("Student deleted successfully");

            return OperationResult.Failure("Couldn't delete student due to database error");
        }

        private void _MapDTOToProperties(UpdateStudentDTO updateDTO)
        {
            this.StudentMajor = updateDTO.StudentMajor;
            this.StudyYear = (byte)updateDTO.StudyYear;
            this.StudySemester = (byte)updateDTO.StudySemester;
            this.PersonDTO.FirstName = updateDTO.UpdatePersonDTO.FirstName;
            this.PersonDTO.SecondName = updateDTO.UpdatePersonDTO.SecondName;
            this.PersonDTO.ThirdName = updateDTO.UpdatePersonDTO.ThirdName;
            this.PersonDTO.LastName = updateDTO.UpdatePersonDTO.LastName;
            this.PersonDTO.Gender = updateDTO.UpdatePersonDTO.Gender;
            this.PersonDTO.PhoneNumber = updateDTO.UpdatePersonDTO.PhoneNumber;
            this.PersonDTO.Email = updateDTO.UpdatePersonDTO.Email;
            this.PersonDTO.DateOfBirth = updateDTO.UpdatePersonDTO.DateOfBirth;
            this.PersonDTO.NationalNumber = updateDTO.UpdatePersonDTO.NationalNumber;
            this.PersonDTO.NationalCountryID = updateDTO.UpdatePersonDTO.NationalCountryID;
        }

        public OperationResult Update(UpdateStudentDTO updateDTO)
        {
            var validation = _CheckStudentValidation();
            if (!validation.IsSuccess)
                return validation;

            _MapDTOToProperties(updateDTO);

            return Save();
        }

        private OperationResult _CheckReturnValue(int returnedValue)
        {
            var value = (StudentReturnValueEnum)returnedValue;

            switch (value)
            {
                case StudentReturnValueEnum.SUCCESS:
                    return OperationResult.Success("Student added successfully");
                case StudentReturnValueEnum.EMAIL_EXISTS:
                    return OperationResult.Failure("Email already exists in the system");
                case StudentReturnValueEnum.UNEXPECTED_EXCEPTION:
                    return OperationResult.Failure("An unexpected error occurred. Please try again later");
                default:
                    return OperationResult.Failure($"Unknown error occured (Code: {returnedValue})");
            }
        }

        private OperationResult<StudentDataResult> _AddNewStudent()
        {
            var validationResult = _CheckStudentValidation();
            if (!validationResult.IsSuccess)
                return OperationResult<StudentDataResult>.Failure(validationResult.Message);

            var result = StudentData.AddNewStudent(ASDTO);

            if (!result.IsSuccess || result.Data == null)
                return OperationResult<StudentDataResult>.Failure(result.Message);

            var returnValueCheck = _CheckReturnValue(result.Data.ReturnValue);
            if (!returnValueCheck.IsSuccess)
                return OperationResult<StudentDataResult>.Failure(returnValueCheck.Message);

            if (!result.Data.NewPersonID.HasValue || !result.Data.NewStudentID.HasValue || string.IsNullOrEmpty(result.Data.NewStudentNo))
                return OperationResult<StudentDataResult>.Failure("Failed to retrieve new student information after insertion");

            this.PersonDTO.PersonID = (int)result.Data.NewPersonID;
            this.StudentNo = result.Data.NewStudentNo;
            this.StudentID = (int)result.Data.NewStudentID;

            return OperationResult<StudentDataResult>.Success(result.Data, "Student added successfully");
        }

        private OperationResult _UpdateStudent()
        {
            var result = StudentData.UpdateStudent(this.StudentID, SDTO);

            if (!result.IsSuccess)
                return OperationResult.Failure(result.Message);

            return OperationResult.Success("Student updated successfully.");
        }
        
        public static StudentService CreateFromDTO(AddStudentDTO addStudentDTO)
        {
            var student = new StudentService();

            student.PersonDTO.FirstName = addStudentDTO.AddPersonDTO.FirstName;
            student.PersonDTO.SecondName = addStudentDTO.AddPersonDTO.SecondName;
            student.PersonDTO.ThirdName = addStudentDTO.AddPersonDTO.ThirdName;
            student.PersonDTO.LastName = addStudentDTO.AddPersonDTO.LastName;
            student.PersonDTO.Gender = addStudentDTO.AddPersonDTO.Gender;
            student.PersonDTO.PhoneNumber = addStudentDTO.AddPersonDTO.PhoneNumber;
            student.PersonDTO.Email = addStudentDTO.AddPersonDTO.Email;
            student.PersonDTO.DateOfBirth = addStudentDTO.AddPersonDTO.DateOfBirth;
            student.PersonDTO.NationalNumber = addStudentDTO.AddPersonDTO.NationalNumber;
            student.PersonDTO.NationalCountryID = addStudentDTO.AddPersonDTO.NationalCountryID;

            student.StudentMajor = addStudentDTO.StudentMajor;
            student.StudyYear = (byte)addStudentDTO.StudyYear;
            student.StudySemester = (byte)addStudentDTO.StudySemester;
            student.CreatedByUserID = addStudentDTO.CreatedByUserID;

            return student;
        }

        public OperationResult Save()
        {
            switch (_Mode)
            {
                case ModeEnum.AddNew:
                    var result = _AddNewStudent();
                    if (result.IsSuccess)
                        _Mode = ModeEnum.Update;
                    return result.ToNonGeneric();
                case ModeEnum.Update:
                    return _UpdateStudent();
                default:
                    return OperationResult.Failure("Invalid operation mode.");
            }
        }
    }
}
