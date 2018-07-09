using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityManagementSystemApp.Models
{
    public class Enrollment
    {
        public int Id { get; set; }
        public string RegistrationNumber { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public int StudentId { get; set; }
        public virtual Student Student { get; set; }

        public int GradeId { get; set; }
        public virtual Grade Grade { get; set; }

        public string ResultStatus { get; set; }
    }
}