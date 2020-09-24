using CourseAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseAPI.Contexts
{
    public class CoursesContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }

        public CoursesContext(DbContextOptions<CoursesContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().HasData(
                new Course { ID = 1, Credits = 20, Name = "Kurssi1" },
                new Course { ID = 2, Credits = 10, Name = "Kurssi2" },
                new Course { ID = 3, Credits = 5, Name = "Kurssi3" }
                );
        }
    }
}
