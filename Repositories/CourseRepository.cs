using CourseAPI.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseAPI.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private List<Course> courses;

        public CourseRepository(ILogger<CourseRepository> logger)
        {
            courses = new List<Course>();
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

        public void AddCourse(Course course)
        {
            if (courses.Count == 0)
                course.ID = 0;
            else
                course.ID = courses.Last().ID + 1;

            courses.Add(course);
        }

        // Useless
        public void DeleteCourse(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteCourse(Course course)
        {
            courses.Remove(course);
        }

        public Course GetCourse(int id)
        {
            return courses.FirstOrDefault(c => c.ID == id);
        }

        public List<Course> GetCourses()
        {
            return courses;
        }

        public void UpdateCourse(int id, Course updatedCourse)
        {
            Course course = courses.First(c => c.ID == id);
            int index = courses.IndexOf(course);

            courses[index] = updatedCourse;
        }
    }
}
