using University_Management_System_APIs_DAL;
using University_Management_System_APIs_Global;
using University_Management_System_APIs_Global.DTOs.Course;
using University_Management_System_APIs_Global.DTOs.Person;
using University_Management_System_APIs_Global.DTOs.Student;
using University_Management_System_APIs_Global.Enums;

namespace University_Management_System_APIs_BLL
{
    public class CourseService
    {
        private ModeEnum _Mode = ModeEnum.AddNew;

        public CourseDTO CDTO
        {
            get { return new CourseDTO(this.CourseID, this.CourseName, this.CourseCode, this.Credits, this.MaxCapacity, 
                    this.CreatedDate, this.ModifiedDate, this.CreatedByUserID, this.IsDeleted); }
        }

        public AddCourseDTO ACDTO
        {
            get { return new AddCourseDTO(this.CourseName, this.CourseCode, this.Credits, this.MaxCapacity, this.CreatedByUserID); }
        }

        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public byte Credits { get; set; }
        public byte MaxCapacity { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int CreatedByUserID { get; set; }
        public bool IsDeleted { get; set; }


        public CourseService()
        {
            this.CourseID = -1;
            this.CourseName = "";
            this.CourseCode = "";
            this.Credits = 0;
            this.MaxCapacity = 0;
            this.CreatedDate = DateTime.Now;
            this.ModifiedDate = DateTime.Now;
            this.CreatedByUserID = -1;
            this.IsDeleted = false;

            _Mode = ModeEnum.AddNew;
        }

        public CourseService(CourseDTO courseDTO, ModeEnum Mode = ModeEnum.AddNew)
        {
            this.CourseID = courseDTO.CourseID;
            this.CourseName = courseDTO.CourseName;
            this.CourseCode = courseDTO.CourseCode;
            this.Credits = courseDTO.Credits;
            this.MaxCapacity = courseDTO.MaxCapacity;
            this.CreatedDate = courseDTO.CreatedDate;
            this.ModifiedDate = courseDTO.ModifiedDate;
            this.CreatedByUserID = courseDTO.CreatedByUserID;
            this.IsDeleted = courseDTO.IsDeleted;

            _Mode = Mode;
        }

        public static OperationResult<List<CoursesListDTO>> GetAllCourses()
        {
            var result = CourseData.GetAllStudents();

            if (result != null && result.Data != null)
                return OperationResult<List<CoursesListDTO>>.Success(result.Data, "Retrieved data successfully");

            return OperationResult<List<CoursesListDTO>>.Failure("Failed to retrieve data");
        }

        private OperationResult _CheckCourseValidation()
        {
            List<string> errors = new List<string>();

            if (string.IsNullOrEmpty(CourseName))
                errors.Add("Course name is required");

            if (string.IsNullOrEmpty(CourseCode))
                errors.Add("Course code is required");

            if (!Validation.ValidateCourseCredits(Credits))
                errors.Add("Credits are invalid");

            if (!Validation.ValidateCourseMaxCapacity(MaxCapacity))
                errors.Add("Capacity is invalid");

            if (errors.Count > 0)
                return OperationResult.Failure(errors);

            return OperationResult.Success("Passed validation");
        }

        public static OperationResult<CourseService> Find(int courseID)
        {
            if (courseID < 1)
                return OperationResult<CourseService>.Failure("Invalid course ID");

            var result = CourseData.GetCourseByID(courseID);

            if (!result.IsSuccess)
                return OperationResult<CourseService>.Failure("Course not found");

            if (result.Data != null)
            {
                var course = new CourseService(result.Data, ModeEnum.Update);
                course.CourseID = courseID;
                return OperationResult<CourseService>.Success(course);
            }

            return OperationResult<CourseService>.Failure("No data found");
        }

        public static OperationResult<CoursesListDTO> Find(string courseCode)
        {
            if (string.IsNullOrWhiteSpace(courseCode))
                return OperationResult<CoursesListDTO>.Failure("Course code is required");

            var result = CourseData.GetCourseByCourseCode(courseCode);

            if (result.IsSuccess && result.Data != null)
                return OperationResult<CoursesListDTO>.Success(result.Data, "Course found successfully");

            return OperationResult<CoursesListDTO>.Failure("Course not found");
        }

        public static OperationResult Delete(int courseID)
        {
            if (courseID < 1)
                return OperationResult.Failure("Invalid course ID");

            var courseDataResult = CourseData.GetCourseByID(courseID);

            if (!courseDataResult.IsSuccess || courseDataResult.Data == null)
                return OperationResult.Failure("Course not found");

            var deletedCourseResult = CourseData.DeleteCourse(courseDataResult.Data.CourseID);
            
            var checkReturnValue = _CheckReturnValue(Convert.ToInt32(deletedCourseResult.Data), successMessage: "Course deleted successfully");

            if (!checkReturnValue.IsSuccess)
                return OperationResult.Failure(checkReturnValue.Message);
            
            return OperationResult.Success(checkReturnValue.Message);
        }

        private void _MapDTOToProperties(UpdateCourseDTO updateDTO)
        {
            this.CourseName = updateDTO.CourseName;
            this.CourseCode = updateDTO.CourseCode;
            this.Credits = (byte)updateDTO.Credits;
            this.MaxCapacity = (byte)updateDTO.MaxCapacity;
        }

        public OperationResult Update(UpdateCourseDTO updateDTO)
        {
            var validation = _CheckCourseValidation();
            if (!validation.IsSuccess)
                return validation;

            _MapDTOToProperties(updateDTO);

            return Save();
        }

        private static OperationResult _CheckReturnValue(int returnValue, string successMessage)
        {
            var value = (CourseReturnValueEnum)returnValue;

            switch (value)
            {
                case CourseReturnValueEnum.SUCCESS:
                    return OperationResult.Success(successMessage);
                case CourseReturnValueEnum.COURSE_EXISTS:
                    return OperationResult.Failure("Course already exists");
                case CourseReturnValueEnum.COURSE_CODE_EXISTS:
                    return OperationResult.Failure("Course code already exists");
                case CourseReturnValueEnum.UNEXPECTED_EXCEPTION:
                    return OperationResult.Failure("An unexpected error occurred. Please try again later");
                case CourseReturnValueEnum.COURSE_HAS_ENROLLMENT:
                    return OperationResult.Failure("Course can't be deleted due to having enrollment");
                default:
                    return OperationResult.Failure($"Unknown error occured (Code: {returnValue})");
            }
        }

        private OperationResult<CourseDataResult> _AddNewStudent()
        {
            var validationResult = _CheckCourseValidation();
            if (!validationResult.IsSuccess)
                return OperationResult<CourseDataResult>.Failure(validationResult.Message);

            var result = CourseData.AddNewCourse(ACDTO);

            if (!result.IsSuccess || result.Data == null)
                return OperationResult<CourseDataResult>.Failure(result.Message);

            var checkReturnValue = _CheckReturnValue(result.Data.ReturnValue, "Course added successfully");
            if (!checkReturnValue.IsSuccess)
                return OperationResult<CourseDataResult>.Failure(checkReturnValue.Message);

            this.CourseID = result.Data.CourseID;

            return OperationResult<CourseDataResult>.Success(result.Data, "Course added successfully");
        }

        private OperationResult _UpdateStudent()
        {
            var result = CourseData.UpdateCourse(this.CourseID, CDTO);

            if (!result.IsSuccess)
                return OperationResult.Failure(result.Message);

            return OperationResult.Success("Course updated successfully.");
        }

        public static CourseService CreateFromDTO(AddCourseDTO addCourseDTO)
        {
            var course = new CourseService();

            course.CourseName = addCourseDTO.CourseName;
            course.CourseCode = addCourseDTO.CourseCode;
            course.Credits = addCourseDTO.Credits;
            course.MaxCapacity = addCourseDTO.MaxCapacity;
            course.CreatedByUserID = addCourseDTO.CreatedByUserID;

            return course;
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
