using Microsoft.Data.SqlClient;
using University_Management_System_APIs_Global.DTOs.Course;
using University_Management_System_APIs_Global.DTOs.Enrollment;
using University_Management_System_APIs_Global.DTOs.Person;
using University_Management_System_APIs_Global.DTOs.Student;
using University_Management_System_APIs_Global.Enums;

namespace University_Management_System_APIs_DAL.Extensions
{
    internal static class SqlDataReaderExtensions
    {
        #region Person DTOs
        public static PersonDTO ToPersonDTO(this SqlDataReader reader)
        {
            return new PersonDTO
            (
                reader.GetInt32(reader.GetOrdinal("PersonID")), // PersonID
                reader.GetString(reader.GetOrdinal("FirstName")), // FirstName
                reader.GetString(reader.GetOrdinal("SecondName")), // SecondName
                reader.IsDBNull(reader.GetOrdinal("ThirdName")) ? null : reader.GetString(reader.GetOrdinal("ThirdName")), // ThirdName
                reader.GetString(reader.GetOrdinal("LastName")), // LastName
                (GenderEnum)reader.GetByte(reader.GetOrdinal("Gender")), // Gender
                reader.GetString(reader.GetOrdinal("PhoneNumber")), // PhoneNumber
                reader.IsDBNull(reader.GetOrdinal("Email")) ? null : reader.GetString(reader.GetOrdinal("Email")), // Email
                reader.GetDateTime(reader.GetOrdinal("DateOfBirth")), // DateOfBirth
                reader.GetString(reader.GetOrdinal("NationalNumber")), // NationalNumber
                reader.GetInt32(reader.GetOrdinal("NationalCountryID")), // NationalCountryID
                reader.GetDateTime(reader.GetOrdinal("CreatedDate")), // CreatedDate
                reader.GetDateTime(reader.GetOrdinal("LastModifiedDate")), // LastModifiedDate
                reader.GetBoolean(reader.GetOrdinal("IsDeleted")) // IsDeleted
            );
        }
        #endregion

        #region Student DTOs
        public static StudentDTO ToStudentDTO(this SqlDataReader reader)
        {
            return new StudentDTO
            (
                reader.GetInt32(reader.GetOrdinal("StudentID")), // StudentID
                reader.GetString(reader.GetOrdinal("StudentNo")), // StudentNo
                reader.GetString(reader.GetOrdinal("StudentMajor")), // StudentMajor
                reader.GetByte(reader.GetOrdinal("StudyYear")), // StudyYear
                reader.GetByte(reader.GetOrdinal("StudySemester")), // StudySemester
                reader.GetInt32(reader.GetOrdinal("CreatedByUserID")), // CreatedByUserID
                ToPersonDTO(reader) // PersonDTO
            );
        }

        public static StudentsListDTO ToStudentsListDTO(this SqlDataReader reader)
        {
            return new StudentsListDTO
            (
                reader.GetString(reader.GetOrdinal("StudentNo")),
                reader.GetString(reader.GetOrdinal("FullName")).Replace("  ", " "),
                reader.GetString(reader.GetOrdinal("StudentMajor")),
                reader.GetByte(reader.GetOrdinal("StudyYear")),
                reader.GetByte(reader.GetOrdinal("StudySemester")),
                reader.GetString(reader.GetOrdinal("Gender")),
                reader.GetByte(reader.GetOrdinal("Age")),
                reader.GetString(reader.GetOrdinal("CountryName")),
                reader.GetString(reader.GetOrdinal("NationalNumber"))
            );
        }
        #endregion

        #region Course DTOs
        public static CourseDTO ToCourseDTO(this SqlDataReader reader)
        {
            return new CourseDTO
            (
                reader.GetInt32(reader.GetOrdinal("CourseID")),
                reader.GetString(reader.GetOrdinal("CourseName")),
                reader.GetString(reader.GetOrdinal("CourseCode")),
                reader.GetByte(reader.GetOrdinal("Credits")),
                reader.GetByte(reader.GetOrdinal("MaxCapacity")),
                reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                reader.GetDateTime(reader.GetOrdinal("ModifiedDate")),
                reader.GetInt32(reader.GetOrdinal("CreatedByUserID")),
                reader.GetBoolean(reader.GetOrdinal("IsDeleted"))
            );
        }
        
        public static CoursesListDTO ToCoursesListDTO(this SqlDataReader reader)
        {
            return new CoursesListDTO
            (
                reader.GetString(reader.GetOrdinal("CourseName")),
                reader.GetString(reader.GetOrdinal("CourseCode")),
                reader.GetByte(reader.GetOrdinal("Credits")),
                reader.GetByte(reader.GetOrdinal("MaxCapacity"))
            );
        }
        #endregion

        #region Enrollment DTOs
        public static EnrollmentDTO ToEnrollmentDTO(this SqlDataReader reader)
        {
            return new EnrollmentDTO
            (
                reader.GetInt32(reader.GetOrdinal("EnrollmentID")),
                reader.GetInt32(reader.GetOrdinal("StudentID")),
                reader.GetInt32(reader.GetOrdinal("CourseID")),
                reader.GetDateTime(reader.GetOrdinal("EnrollmentDate")),
                reader.IsDBNull(reader.GetOrdinal("Grade")) ? null : reader.GetDecimal(reader.GetOrdinal("Grade")),
                reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                reader.GetDateTime(reader.GetOrdinal("ModifiedDate")),
                reader.GetInt32(reader.GetOrdinal("CreatedByUserID")),
                reader.GetBoolean(reader.GetOrdinal("IsDeleted"))
            );
        }

        public static EnrollmentsListDTO ToEnrollmentsListDTO(this SqlDataReader reader)
        {
            return new EnrollmentsListDTO
            (
                reader.GetInt32(reader.GetOrdinal("EnrollmentID")),
                reader.GetString(reader.GetOrdinal("StudentFullName")).Replace("  ", " "),
                reader.GetString(reader.GetOrdinal("CourseName")),
                reader.GetDateTime(reader.GetOrdinal("EnrollmentDate")),
                reader.IsDBNull(reader.GetOrdinal("Grade")) ? null : reader.GetDecimal(reader.GetOrdinal("Grade"))
            );
        }
        #endregion

    }
}
