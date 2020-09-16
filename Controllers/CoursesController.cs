using System;
using System.Collections.Generic;
using System.Linq;
using CourseAPI.Models;
using CourseAPI.Repositories;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ICourseRepository _courseRepository;

        public CoursesController(ILogger<CoursesController> logger, ICourseRepository courseRepository)
        {
            _logger = logger;
            _courseRepository = courseRepository;
        }

        [HttpPut("{id:int}")]
        public ActionResult<List<Course>> Put(int id, [FromBody] Course course)
        {
            if (_courseRepository.GetCourse(id) is null)
                return NotFound();

            _courseRepository.UpdateCourse(id, course);
            return Ok(_courseRepository.GetCourses());
        }

        [HttpDelete("{id:int}")]
        public ActionResult<List<Course>> Delete(int id)
        {
            Course course = _courseRepository.GetCourse(id);

            if (course is null)
                return NotFound();

            _courseRepository.DeleteCourse(course);
            return Ok(_courseRepository.GetCourses());
        }
        
        [HttpPatch("{id:int}")]
        public ActionResult<List<Course>> Patch(int id, [FromBody] JsonPatchDocument<Course> coursePatch)
        {
            Course course = _courseRepository.GetCourse(id);

            if (course is null)
                return NotFound();

            coursePatch.ApplyTo(course);
            return Ok(_courseRepository.GetCourses());
        }

        [HttpPost]
        public ActionResult<Course> Post([FromBody] Course course)
        {
            if (course.Credits < 0 || course.Credits > 20)
                ModelState.AddModelError("Description", "Invalid Credits. Allowed range: 0-20");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _courseRepository.AddCourse(course);
            return Created($"https://localhost/api/courses/{course.ID}", course);
        }

        [HttpGet]
        public ActionResult<List<Course>> Get()
        {
            return Ok(_courseRepository.GetCourses());
        }

        [HttpGet("{id:int}")]
        public ActionResult<List<Course>> Get(int id)
        {
            Course course = _courseRepository.GetCourse(id);

            if (course is null)
                return NotFound();

            return Ok(course);
        }
    }
}
