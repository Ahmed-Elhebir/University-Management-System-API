using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using University_Management_System_APIs_BLL;
using University_Management_System_APIs_Global;
using University_Management_System_APIs_Global.DTOs.Student;

namespace University_Management_System_APIs.Controllers
{
    [Route("api/Students")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        [HttpGet("All", Name = "GetAllStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<StudentsListDTO>> GetAllStudents()
        {
            var result = StudentService.GetAllStudents();
            return (result.IsSuccess) ? Ok(result.Data) : NotFound(result.Message);
        }

        [HttpGet("StudentID/{studentID}", Name = "GetStudentByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<StudentDTO> GetStudentByID(int studentID)
        {
            if (studentID < 1)
                return BadRequest(OperationResult.Failure("Invalid student id"));

            var result = StudentService.Find(studentID);

            if (!result.IsSuccess || result.Data == null)
                return NotFound(result.Message);

            return Ok(result.Data.SDTO);
        }

        [HttpGet("StudentNo/{studentNo}", Name = "GetStudentByStudentNo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<StudentsListDTO> GetStudentByStudentNo(string studentNo)
        {
            if (string.IsNullOrWhiteSpace(studentNo))
                return BadRequest(OperationResult.Failure("Student number is required."));

            var result = StudentService.Find(studentNo);

            return result.IsSuccess ? Ok(result.Data) : NotFound(result.Message);
        }

        [HttpPost(Name = "AddNewStudent")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<OperationResult> AddNewStudent([FromBody] AddStudentDTO studentDTO)
        {
            if (studentDTO == null)
                return BadRequest(OperationResult.Failure("Student data is required."));

            var student = StudentService.CreateFromDTO(studentDTO);

            if (student == null)
                return BadRequest(OperationResult.Failure("Failed to add student"));

            var saveResult = student.Save();

            if (!saveResult.IsSuccess)
                return BadRequest(saveResult.Message);

            return CreatedAtRoute("GetStudentByID", new { studentID = student.StudentID }, student.SDTO);
        }

        [HttpPut("{studentID}", Name = "UpdateStudentInfo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<OperationResult> UpdateStudent(int studentID, [FromBody] UpdateStudentDTO updateStudentDTO)
        {
            if (studentID < 1 || updateStudentDTO == null)
                return BadRequest(OperationResult.Failure("Invalid request data"));

            var updateStudent = StudentService.Find(studentID);
            if (!updateStudent.IsSuccess || updateStudent.Data == null)
                return NotFound(OperationResult.Failure(updateStudent.Message));

            var updateResult = updateStudent.Data.Update(updateStudentDTO);
            if (updateResult.IsSuccess)
                return Ok(OperationResult.Success(updateResult.Message));

            return BadRequest(OperationResult.Failure(updateStudent.Message));

        }

        [HttpDelete("Delete", Name = "DeleteStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<OperationResult> DeleteStudent(int studentID)
        {
            if (studentID < 1)
                return BadRequest(OperationResult.Failure("Invalid student ID"));

            var result = StudentService.Delete(studentID);

            if (!result.IsSuccess)
                return NotFound(OperationResult.Failure(result.Message));

            return Ok(OperationResult.Success(result.Message));
        }

    }
}
