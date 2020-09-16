using System;
using System.Collections.Generic;
using System.Linq;
using CourseAPI.Models;
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
        private static List<Course> courses;

        public CoursesController(ILogger<CoursesController> logger)
        {
            _logger = logger;
            AddDummyCourses();
        }

        private void AddDummyCourses()
        {
            courses = new List<Course>();
            courses.Add(new Course
            {
                ID = 0,
                Name = "ASPNET",
                Credits = 10
            });

            courses.Add(new Course
            {
                ID = 1,
                Name = "Python",
                Credits = 5
            });

            courses.Add(new Course
            {
                ID = 2,
                Name = "Javascript",
                Credits = 2
            });
        }

        [HttpPut("{id:int}")]
        public ActionResult<List<Course>> Put(int id, [FromBody] Course course)
        {
            if (course.Name is null || course.Name == string.Empty)
                return BadRequest(); // return 405 not allowed

            var updatedCourses = courses.Select(c => (c.ID == id) ? course : c).ToList();
            courses = updatedCourses;
            return Ok(updatedCourses);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<List<Course>> Delete(int id)
        {
            courses = courses.Where(c => c.ID != id).ToList();
            return Ok(courses);
        }
        
        [HttpPatch("{id:int}")]
        public ActionResult<List<Course>> Patch(int id, [FromBody] JsonPatchDocument<Course> coursePatch)
        {
            Course course;

            try
            {
                course = courses.First(c => c.ID == id);
            }
            catch (Exception e)
            {
                return BadRequest();
            }

            coursePatch.ApplyTo(course);
            return courses;
        }
    }
}
