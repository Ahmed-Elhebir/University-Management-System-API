using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using University_Management_System_APIs_BLL;
using University_Management_System_APIs_Global;
using University_Management_System_APIs_Global.DTOs.Course;
using University_Management_System_APIs_Global.DTOs.Student;

namespace University_Management_System_APIs.Controllers
{
    [Route("api/Course")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        [HttpGet("All", Name = "GetAllCourses")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<CoursesListDTO>> GetAllCourses()
        {
            var result = CourseService.GetAllCourses();
            return (result.IsSuccess) ? Ok(result.Data) : NotFound(result.Message);
        }


        [HttpGet("CourseID/{courseID}", Name = "GetCourseByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<CourseDTO> GetCourseByID(int courseID)
        {
            if (courseID < 1)
                return BadRequest(OperationResult.Failure("Invalid course id"));

            var result = CourseService.Find(courseID);

            if (!result.IsSuccess)
                return NotFound(result.Message);

            if (result.Data != null)
                return Ok(result.Data.CDTO);

            return NotFound(result.Message);
        }


        [HttpGet("CourseCode/{courseCode}", Name = "GetCourseByCourseCode")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<CoursesListDTO> GetCourseByCourseCode(string courseCode)
        {
            if (string.IsNullOrWhiteSpace(courseCode))
                return BadRequest(OperationResult.Failure("Course code is required."));

            var result = CourseService.Find(courseCode);

            return result.IsSuccess ? Ok(result.Data) : NotFound(result.Message);
        }

        [HttpPost(Name = "AddNewCourse")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<OperationResult> AddNewCourse([FromBody] AddCourseDTO courseDTO)
        {
            if (courseDTO == null)
                return BadRequest(OperationResult.Failure("course data is required."));

            var course = CourseService.CreateFromDTO(courseDTO);

            if (course == null)
                return BadRequest(OperationResult.Failure("Failed to add course"));

            var saveResult = course.Save();

            if (!saveResult.IsSuccess)
                return BadRequest($"{saveResult.Message}");

            return CreatedAtRoute("GetCourseByID", new { courseID = course.CourseID }, course.CDTO);
        }

        [HttpPut("{courseID}", Name = "UpdateCourseInfo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<OperationResult> UpdateCourse(int courseID, [FromBody] UpdateCourseDTO updateCourseDTO)
        {
            if (courseID < 1 || updateCourseDTO == null)
                return BadRequest(OperationResult.Failure("Invalid request data"));

            var updateCourse = CourseService.Find(courseID);
            if (!updateCourse.IsSuccess)
                return NotFound(OperationResult.Failure(updateCourse.Message));

            if (updateCourse.Data != null)
            {
                var updateResult = updateCourse.Data.Update(updateCourseDTO);
                if (updateResult.IsSuccess)
                    return Ok(OperationResult.Success(updateResult.Message));
            }

            return BadRequest(OperationResult.Failure(updateCourse.Message));

        }

        [HttpDelete("Delete", Name = "DeleteCourse")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<OperationResult> DeleteCourse(int courseID)
        {
            if (courseID < 1)
                return BadRequest(OperationResult.Failure("Invalid course ID"));

            var result = CourseService.Delete(courseID);

            if (!result.IsSuccess)
                return NotFound(OperationResult.Failure(result.Message));

            return Ok(OperationResult.Success(result.Message));
        }

    }
}
