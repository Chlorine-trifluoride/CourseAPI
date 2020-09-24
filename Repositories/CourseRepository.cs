using CourseAPI.Contexts;
using CourseAPI.Models;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseAPI.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ILogger<CourseRepository> _logger;
        private readonly CoursesContext _context;

        public CourseRepository(ILogger<CourseRepository> logger, CoursesContext coursesContext)
        {
            _logger = logger;
            _context = coursesContext;
        }

        public void AddCourse(Course course)
        {
            if (_context.Courses.Count() == 0)
                course.ID = 0;
            else
                course.ID = _context.Courses.ToList().Last().ID + 1;

            _context.Add(course);
            _context.SaveChanges();
        }

        // Useless
        public void DeleteCourse(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteCourse(Course course)
        {
            _context.Remove(course);
            _context.SaveChanges();
        }

        public Course GetCourse(int id)
        {
            return _context.Courses.FirstOrDefault(c => c.ID == id);
        }

        public List<Course> GetCourses()
        {
            return _context.Courses.ToList();
        }

        public void UpdateCourse(int id, Course updatedCourse)
        {
            Course course = _context.Courses.First(c => c.ID == id);

            course.Credits = updatedCourse.Credits;
            course.Name = updatedCourse.Name;

            _context.Update(course);
            _context.SaveChanges();
        }
    }
}
