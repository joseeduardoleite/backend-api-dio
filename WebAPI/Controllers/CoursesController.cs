using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebAPI.Models.Courses;

namespace WebAPI.Controllers
{
    [Route("api/v1/courses")]
    [ApiController]
    [Authorize]
    public class CoursesController : ControllerBase
    {
        [SwaggerResponse(statusCode: 201, description: "Success, course created")]
        [SwaggerResponse(statusCode: 401, description: "Eror: Not authorized")]
        [HttpPost]
        [Route("add")]
        public IActionResult Add(CourseViewModelInput courseViewModelInput)
        {
            var userCode = int.Parse(User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)?.Value);

            return Created("", courseViewModelInput);
        }

        [SwaggerResponse(statusCode: 201, description: "Success, course created")]
        [SwaggerResponse(statusCode: 401, description: "Eror: Not authorized")]
        [HttpGet]
        [Route("get")]
        public IActionResult Get()
        {
            var courses = new List<CourseViewModelOutput>();

            var userCode = int.Parse(User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)?.Value);

            courses.Add(new CourseViewModelOutput()
            {
                Login = userCode.ToString(),
                Description = "Test",
                Name = "TestName"
            });

            return Ok(courses);
        }
    }
}