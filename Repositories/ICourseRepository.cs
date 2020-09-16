using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseAPI.Models;

namespace CourseAPI.Repositories
{
    public interface ICourseRepository
    {
        Course GetCourse(int id);
        List<Course> GetCourses();
        void AddCourse(Course course);
        void UpdateCourse(int id, Course updatedCourse);
        void DeleteCourse(int id);
        void DeleteCourse(Course course);
    }
}
