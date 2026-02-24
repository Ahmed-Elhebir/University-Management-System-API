using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using University_Management_System_APIs_BLL;
using University_Management_System_APIs_Global;
using University_Management_System_APIs_Global.DTOs.Enrollment;

namespace University_Management_System_APIs.Controllers
{
    [Route("api/Enrollments")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        [HttpGet("All", Name = "GetAllEnrollments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<EnrollmentsListDTO>> GetAllEnrollments()
        {
            var result = EnrollmentService.GetAllEnrollments();
            return (result.IsSuccess) ? Ok(result.Data) : NotFound(result.Message);
        }

        [HttpGet("EnrollmentID/{ID}", Name = "GetEnrollmentByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<EnrollmentDTO> GetEnrollmentByID(int ID)
        {
            if (ID < 1)
                return BadRequest(OperationResult.Failure("Invalid enrollment id"));

            var result = EnrollmentService.Find(ID);

            if (!result.IsSuccess || result.Data == null)
                return NotFound(result.Message);

            return Ok(result.Data.EDTO);
        }

        [HttpGet("StudentID/{ID}", Name = "GetEnrollmentByStudentID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<EnrollmentsListDTO> GetStudentEnrollments(int ID)
        {
            if (ID < 1)
                return BadRequest(OperationResult.Failure("Invalid student id"));

            var result = EnrollmentService.FindByStudentID(ID);

            if (!result.IsSuccess || result.Data == null || result.Data.Count == 0)
                return NotFound(result.Message);

            return Ok(result.Data);
        }

        [HttpGet("CourseID/{ID}", Name = "GetEnrollmentByCourseID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<EnrollmentsListDTO> GetEnrollmentsByCourseID(int ID)
        {
            if (ID < 1)
                return BadRequest(OperationResult.Failure("Invalid course id"));

            var result = EnrollmentService.FindByCourseID(ID);

            if (!result.IsSuccess || result.Data == null || result.Data.Count == 0)
                return NotFound(result.Message);

            return Ok(result.Data);
        }

        [HttpGet("StudentGPA/{ID}", Name = "GetStudentGPA")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult GetStudentGPA(int ID)
        {
            if (ID < 1)
                return BadRequest(OperationResult.Failure("Invalid student id"));

            var result = EnrollmentService.StudentGPA(ID);

            if (!result.IsSuccess)
                return NotFound(result.Message);

            return Ok(result.Data);
        }

        [HttpGet("EnrollmentCounts/{courseID}", Name = "GetEnrollmentCountsByCourseID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult GetEnrollmentCountsByCourseID(int courseID)
        {
            if (courseID < 1)
                return BadRequest(OperationResult.Failure("Invalid course id"));

            var result = EnrollmentService.GetNumberOfEnrollmentsForCourse(courseID);

            if (!result.IsSuccess)
                return NotFound(result.Message);

            return Ok(result.Data);
        }

        [HttpPost(Name = "AddNewEnrollment")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<OperationResult> AddNewEnrollment([FromBody] AddEnrollmentDTO enrollmentDTO)
        {
            if (enrollmentDTO == null)
                return BadRequest(OperationResult.Failure("Enrollment data is required"));

            var enrollment = EnrollmentService.CreateFromDTO(enrollmentDTO);

            if (enrollment == null)
                return BadRequest(OperationResult.Failure("Failed to add enrollment"));

            var saveResult = enrollment.Save();

            if (!saveResult.IsSuccess)
                return BadRequest(saveResult.Message);

            return CreatedAtRoute("GetEnrollmentByID", new { ID = enrollment.EnrollmentID }, enrollment.EDTO);
        }

        [HttpPut("UpdateEnrollment/{ID}", Name = "UpdateEnrollment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<OperationResult> UpdateEnrollment(int ID, [FromBody] UpdateEnrollmentDTO updateEnrollmentDTO)
        {
            if (ID < 1 || updateEnrollmentDTO == null)
                return BadRequest(OperationResult.Failure("Invalid enrollment id"));

            var updateEnrollment = EnrollmentService.Find(ID);

            if (!updateEnrollment.IsSuccess || updateEnrollment.Data == null)
                return NotFound(OperationResult.Failure(updateEnrollment.Message));

            var updateResult = updateEnrollment.Data.Update(updateEnrollmentDTO);

            if (!updateResult.IsSuccess)
                return BadRequest(OperationResult.Failure(updateResult.Message));

            return Ok(OperationResult.Success(updateResult.Message));

        }

        [HttpPut("UpdateGrade/{ID}", Name = "UpdateGrade")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<OperationResult> UpdateGrade(int ID, [FromBody] decimal grade)
        {
            if (ID < 1)
                return BadRequest(OperationResult.Failure("Invalid enrollment id"));

            var result = EnrollmentService.UpdateGrade(ID, grade);

            if (!result.IsSuccess)
                return BadRequest(OperationResult.Failure(result.Message));

            return Ok(OperationResult.Success(result.Message));
        }

        [HttpDelete("DeleteEnrollment/{ID}", Name = "DeleteEnrollment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<OperationResult> DeleteEnrollment(int ID)
        {
            if (ID < 1)
                return BadRequest(OperationResult.Failure("Invalid enrollment id"));

            var result = EnrollmentService.Delete(ID);

            if (!result.IsSuccess)
                return BadRequest(OperationResult.Failure(result.Message));

            return Ok(OperationResult.Success(result.Message));

        }

    }
}
