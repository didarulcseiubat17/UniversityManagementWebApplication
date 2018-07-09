using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace UniversityManagementSystemApp.Models
{
    public class Result
    {
        public int Id { get; set; }
        [Required]
        public string RegistrationNumber { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Department { get; set; }

        [Required(ErrorMessage = "Please Select Course")]
        public int CourseId { get; set; }

        public virtual Course Course { get; set; }

        [Required(ErrorMessage = "Please Select Grade")]
        public int GradeId { get; set; }

        public virtual Grade Grade { get; set; }
        

        public int EnrollmentId { get; set; }
        public virtual Enrollment Enrollment { get; set; }
    }
}