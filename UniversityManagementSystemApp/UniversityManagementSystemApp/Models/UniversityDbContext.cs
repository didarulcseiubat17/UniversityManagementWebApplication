using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace UniversityManagementSystemApp.Models
{
    public class UniversityDbContext: DbContext
    {
        public DbSet<Department> Departments { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Semester> Semesters { get; set; }

        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<Designation> Designations { get; set; }
        public DbSet<CourseAssign> CourseAssigns { get; set; }
        public DbSet<Student> Students { get; set; }

        public DbSet<ClassRoomAllocation> ClassRoomAllocations { get; set; }

        public DbSet<Day> Days { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<Grade> Grades { get; set; }


        public DbSet<Enrollment> Enrollments { get; set; }

        public System.Data.Entity.DbSet<UniversityManagementSystemApp.Models.Result> Results { get; set; }
        
    }
}