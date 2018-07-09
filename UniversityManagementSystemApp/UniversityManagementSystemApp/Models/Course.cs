using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UniversityManagementSystemApp.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Course Code Is A Mandatory Field.")]
        [Display(Name = "Code")]
        [StringLength(5, ErrorMessage = "Course Code Cannot Be Greater Than Five Character Long.")]
        [Remote("IsExistCourseCode", "Course", ErrorMessage = "Course Code Already Exist.")]
        public string CourseCode { get; set; }

        [Required(ErrorMessage = "Course Name is Mandatory Field")]
        [Display(Name = "Name")]
        [Remote("IsExistCourseName", "Course", ErrorMessage = "Course Name Already Exist.")]
        public string CourseName { get; set; }

        [Required]
        [Display(Name = "Credit")]
        [Range(0.5, 5.0, ErrorMessage = "Credit must be within 0.5 to 5.0")]
        public double CourseCredit { get; set; }

        [Required]
        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string CourseDescription { get; set; }
        public string CourseAssignTo { get; set; }
        public bool CourseStatus { get; set; }

        [Display(Name = "Department")]
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        [Display(Name = "Semester")]
        public int SemesterId { get; set; }
        public virtual Semester Semester { get; set; }
    }
}