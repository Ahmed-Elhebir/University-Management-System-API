using System.Data;
using University_Management_System_APIs_DAL.Extensions;
using University_Management_System_APIs_Global;
using University_Management_System_APIs_Global.DTOs.Enrollment;

namespace University_Management_System_APIs_DAL
{
    public class EnrollmentData
    {
        public static OperationResult<List<EnrollmentsListDTO>> GetAllEnrollments()
        {
            return DBHelper.ExecuteQuery("SP_GetAllEnrollmentsList", null, reader => reader.ToEnrollmentsListDTO());
        }

        public static OperationResult<EnrollmentDTO> GetEnrollmentByID(int enrollmentID)
        {
            var parameter = new Dictionary<string, object>
            {
                { "@EnrollmentID", enrollmentID }
            };
            return DBHelper.ExecuteSingleRow("SP_GetEnrollmentByID", parameter, reader => reader.ToEnrollmentDTO());
        }

        public static OperationResult<(List<EnrollmentDTO> Data, Dictionary<string, object> OutputValues)> GetEnrollmentByStudentID(int studentID)
        {
            var inputParameter = new Dictionary<string, object>
            {
                { "@StudentID", studentID }
            };

            var outputParameter = new Dictionary<string, OutputParameter>
            {
                { "@RETURN_VALUE", new OutputParameter(SqlDbType.Int) }
            };

            return DBHelper.ExecuteNonQueryWithOutput("SP_GetEnrollmentByStudentID", inputParameter, outputParameter, reader => reader.ToEnrollmentDTO());
        }

        public static OperationResult<(List<EnrollmentDTO> Data, Dictionary<string, object> OutputValues)> GetEnrollmentByCourseID(int courseID)
        {
            var inputParameter = new Dictionary<string, object>
            {
                { "@CourseID", courseID }
            };

            var outputParameter = new Dictionary<string, OutputParameter>
            {
                { "@RETURN_VALUE", new OutputParameter(SqlDbType.Int) }
            };

            return DBHelper.ExecuteNonQueryWithOutput("SP_GetEnrollmentByCourseID", inputParameter, outputParameter, reader => reader.ToEnrollmentDTO());
        }

        public static OperationResult<EnrollmentDataResult> GetStudentGPA(int studentID)
        {
            var inputParameter = new Dictionary<string, object>
            {
                { "@StudentID", studentID }
            };

            var outputParameter = new Dictionary<string, OutputParameter>
            {
                { "@GPA", new OutputParameter(SqlDbType.Decimal, precision: 5, scale: 2) },
                { "@RETURN_VALUE", new OutputParameter(SqlDbType.Int) }
            };

            var result = DBHelper.ExecuteNonQueryWithOutput("SP_CalculateGPA", inputParameter, outputParameter);

            if (!result.IsSuccess || result.Data == null || result.Data.Count == 0)
                return OperationResult<EnrollmentDataResult>.Failure(result.Message);

            var enrollmentDataResult = new EnrollmentDataResult
            (
                studentGPA: result.Data["@GPA"] == DBNull.Value ? null : Convert.ToDecimal(result.Data["@GPA"]),
                returnValue: Convert.ToInt32(result.Data["@RETURN_VALUE"])
            );

            return OperationResult<EnrollmentDataResult>.Success(enrollmentDataResult);

        }

        public static OperationResult<EnrollmentDataResult> AddNewEnrollment(AddEnrollmentDTO addEnrollmentDTO)
        {
            var inputParameters = new Dictionary<string, object>
            {
                { "@StudentID", addEnrollmentDTO.StudentID },
                { "@CourseID", addEnrollmentDTO.CourseID },
                { "@EnrollmentDate", addEnrollmentDTO.EnrollmentDate },
                { "@CreatedByUserID", addEnrollmentDTO.CreatedByUserID }
            };

            var ouputParameters = new Dictionary<string, OutputParameter>()
            {
                { "@RETURN_VALUE", new OutputParameter(SqlDbType.Int) },
                { "@NewEnrollmentID", new OutputParameter(SqlDbType.Int) }
            };

            var result = DBHelper.ExecuteNonQueryWithOutput("SP_AddNewEnrollment", inputParameters, ouputParameters);

            if (!result.IsSuccess || result.Data == null)
                return OperationResult<EnrollmentDataResult>.Failure(result.Message);

            var enrollmentDataResult = new EnrollmentDataResult
            (
                newEnrollmentID: Convert.ToInt32(result.Data["@NewEnrollmentID"]),
                returnValue: Convert.ToInt32(result.Data["@RETURN_VALUE"])
            );

            return OperationResult<EnrollmentDataResult>.Success(enrollmentDataResult);
        }

        public static OperationResult UpdateEnrollment(int enrollmentID, UpdateEnrollmentDTO updateEnrollmentDTO)
        {
            var inputParameters = new Dictionary<string, object>()
            {
                { "@EnrollmentID", enrollmentID },
                { "@EnrollmentDate", updateEnrollmentDTO.EnrollmentDate }
            };
            return DBHelper.ExecuteNonQuery("SP_UpdateEnrollment", inputParameters);
        }

        public static OperationResult<EnrollmentDataResult> UpdateGrade(int enrollmentID, decimal grade)
        {
            var inputParameters = new Dictionary<string, object>()
            {
                { "@EnrollmentID", enrollmentID },
                { "@Grade", grade }
            };

            var outputParameters = new Dictionary<string, OutputParameter>()
            {
                { "@RETURN_VALUE", new OutputParameter(SqlDbType.Int) }
            };

            var result = DBHelper.ExecuteNonQueryWithOutput("SP_UpdateGrade", inputParameters, outputParameters);

            if (!result.IsSuccess || result.Data == null)
                return OperationResult<EnrollmentDataResult>.Failure(result.Message);

            var enrollmentDataResult = new EnrollmentDataResult
            (
                Convert.ToInt32(result.Data["@RETURN_VALUE"])
            );

            return OperationResult<EnrollmentDataResult>.Success(enrollmentDataResult);
        }

        public static OperationResult<int> UnEnroll(int enrollmentID)
        {
            var inputParameter = new Dictionary<string, object>()
            {
                { "@EnrollmentID", enrollmentID }
            };

            var outputParameters = new Dictionary<string, OutputParameter>
            {
                { "@RETURN_VALUE", new OutputParameter(SqlDbType.Int) }
            };
            
            var result = DBHelper.ExecuteNonQueryWithOutput("SP_UnEnroll", inputParameter, outputParameters);

            if (!result.IsSuccess || result.Data == null)
                return OperationResult<int>.Failure(result.Message);

            return OperationResult<int>.Success(Convert.ToInt32(result.Data["@RETURN_VALUE"]));
        }

        public static OperationResult<EnrollmentDataResult> GetNumberOfEnrollmentsForCourse(int courseID)
        {
            var inputParameter = new Dictionary<string, object>()
            {
                { "@CourseID", courseID }
            };

            var outputParameter = new Dictionary<string, OutputParameter>()
            {
                { "@EnrollmentCount", new OutputParameter(SqlDbType.Int) },
                { "@RETURN_VALUE", new OutputParameter(SqlDbType.Int) }
            };

            var result = DBHelper.ExecuteNonQueryWithOutput("SP_GetNumberOfEnrollmentsForCourse", inputParameter, outputParameter);

            if (!result.IsSuccess || result.Data == null)
                return OperationResult<EnrollmentDataResult>.Failure(result.Message);

            var enrollmentDataResult = new EnrollmentDataResult
            (
                returnValue: Convert.ToInt32(result.Data["@RETURN_VALUE"]),
                enrollmentCount: Convert.ToInt32(result.Data["@EnrollmentCount"])
            );

            return OperationResult<EnrollmentDataResult>.Success(enrollmentDataResult);
        }
    }
}
