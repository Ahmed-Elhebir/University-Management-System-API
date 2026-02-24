using System.Data;
using University_Management_System_APIs_DAL.Extensions;
using University_Management_System_APIs_Global;
using University_Management_System_APIs_Global.DTOs.Course;
using University_Management_System_APIs_Global.DTOs.Student;

namespace University_Management_System_APIs_DAL
{
    public class CourseData
    {
        public static OperationResult<List<CoursesListDTO>> GetAllStudents()
        {
            return DBHelper.ExecuteQuery("SP_GetAllCoursesList", null, reader => reader.ToCoursesListDTO());
        }

        public static OperationResult<CourseDTO> GetCourseByID(int courseID)
        {
            var parameter = new Dictionary<string, object>
            {
                { "@CourseID", courseID }
            };

            return DBHelper.ExecuteSingleRow<CourseDTO>("SP_GetCourseByID", parameter, reader => reader.ToCourseDTO());
        }

        public static OperationResult<CoursesListDTO> GetCourseByCourseCode(string courseCode)
        {
            var parameter = new Dictionary<string, object>
            {
                { "@CourseCode", courseCode }
            };

            return DBHelper.ExecuteSingleRow<CoursesListDTO>("SP_GetCourseByCourseCode", parameter, reader => reader.ToCoursesListDTO());
        }

        public static OperationResult<CourseDataResult> AddNewCourse(AddCourseDTO addCourseDTO)
        {
            var inputParameters = new Dictionary<string, object>
            {
                { "@CourseName", addCourseDTO.CourseName },
                { "@CourseCode", addCourseDTO.CourseCode },
                { "@Credits", addCourseDTO.Credits },
                { "@MaxCapacity", addCourseDTO.MaxCapacity },
                { "@CreatedByUserID", addCourseDTO.CreatedByUserID }
            };

            var ouputParameters = new Dictionary<string, OutputParameter>()
            {
                { "@NewCourseID", new OutputParameter(SqlDbType.Int) },
                { "@RETURN_VALUE", new OutputParameter(SqlDbType.Int) }
            };

            var result = DBHelper.ExecuteNonQueryWithOutput("SP_AddNewCourse", inputParameters, ouputParameters);

            if (!result.IsSuccess || result.Data == null)
                return OperationResult<CourseDataResult>.Failure(result.Message);
            
            var courseDataResult = new CourseDataResult
            (
                courseID: Convert.ToInt32(result.Data["@NewCourseID"]),
                returnValue: Convert.ToInt32(result.Data["@RETURN_VALUE"])
            );

            return OperationResult<CourseDataResult>.Success(courseDataResult);

        }

        public static OperationResult UpdateCourse(int courseID, CourseDTO courseDTO)
        {
            var inputParameters = new Dictionary<string, object>()
            {
                { "@CourseID", courseID },
                { "@CourseName", courseDTO.CourseName },
                { "@CourseCode", courseDTO.CourseCode },
                { "@Credits", courseDTO.Credits },
                { "@MaxCapacity", courseDTO.MaxCapacity }
            };

            return DBHelper.ExecuteNonQuery("SP_UpdateCourse", inputParameters);
        }

        public static OperationResult<object> DeleteCourse(int courseID)
        {
            var inputParameter = new Dictionary<string, object>
            {
                { "@CourseID", courseID }
            };

            var outputParameter = new Dictionary<string, OutputParameter>
            {
                { "@RETURN_VALUE", new OutputParameter(SqlDbType.Int) }
            };

            var result = DBHelper.ExecuteNonQueryWithOutput("SP_DeleteCourse", inputParameter, outputParameter);

            if (result.Data != null)
                return OperationResult.Success(result.Data["@RETURN_VALUE"], result.Message);
            
            return OperationResult.Failure(result.Message);
        }

    }
}
