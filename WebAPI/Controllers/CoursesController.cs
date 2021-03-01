using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Business.Entities;
using Business.Services;
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
        private readonly ICourseRepository _courseRepository;

        public CoursesController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        [SwaggerResponse(statusCode: 201, description: "Success, course created")]
        [SwaggerResponse(statusCode: 401, description: "Eror: Not authorized")]
        [HttpPost]
        [Route("add")]
        public IActionResult Add(CourseViewModelInput courseViewModelInput)
        {
            var userCode = int.Parse(User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
            
            var course = new Course();
            course.Name = courseViewModelInput.Name;
            course.Description = courseViewModelInput.Description;
            course.UserId = userCode;

            _courseRepository.Add(course);
            _courseRepository.Commit();

            return Created("", courseViewModelInput);
        }

        [SwaggerResponse(statusCode: 201, description: "Success, course created")]
        [SwaggerResponse(statusCode: 401, description: "Eror: Not authorized")]
        [HttpGet]
        [Route("get")]
        public IActionResult GetAll()
        {
            var courses = _courseRepository.GetAll();

            return Ok(courses);
        }

        [HttpGet]
        [Route("getById")]
        public IActionResult GetById(int id)
        {
            var courses = _courseRepository.GetById(id);

            return Ok(courses);
        }
    }
}