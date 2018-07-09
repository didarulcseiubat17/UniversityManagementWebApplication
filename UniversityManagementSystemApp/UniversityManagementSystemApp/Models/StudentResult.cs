using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityManagementSystemApp.Models
{
    public class StudentResult
    {
        public int Id { get; set; }


        [Required]
        public int StudentId { get; set; }
        public virtual Student Student { get; set; }

        [Display(Name = "Registration Number")]
        public string RegistrationNumber { get; set; }

        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        [Display(Name = "Department")]
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        [Required]
        [Display(Name = "Course")]
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }

        [Required]
        [Display(Name = "Grade")]
        public int GradeId { get; set; }
        public virtual Grade Grade { get; set; }

    }
}