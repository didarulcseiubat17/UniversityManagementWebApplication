using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityManagementSystemApp.Models
{
    public class CourseAssign
    {
        public int Id { get; set; }

        [Required]
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        [Required]
        public int TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; }

        [Editable(false)]
        [DefaultValue(0.0)]
        [Display(Name = "Credit to be Taken")]
        public double CreditTaken { get; set; }

        [Editable(false)]
        [DefaultValue(0.0)]
        [Display(Name = "Remaining Credit")]
        public double CreditLeft { get; set; }

        [Required(ErrorMessage = "Please Select A Course Name")]
        public int CourseID { get; set; }
        public virtual Course Course { get; set; }


        public string Name { get; set; }

        public double Credit { get; set; }
    }
}