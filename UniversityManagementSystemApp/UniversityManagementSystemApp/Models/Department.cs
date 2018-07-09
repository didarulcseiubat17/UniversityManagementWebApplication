using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UniversityManagementSystemApp.Models
{
    public class Department
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Department Code is Mandatory Field")]
        [Display(Name = "Code")]
        [StringLength(7, MinimumLength = 2, ErrorMessage = "Departmrnt Code Must be 2 to  7 characters long")]
        [Remote("IsExistDepartmentCode", "Department", ErrorMessage = "Code already exists!")]
        public string DeptCode { get; set; }


        [Required(ErrorMessage = "Department Name is Mandatory Field")]
        [Display(Name = "Name")]
        [Remote("IsExistDepartmentName", "Department", ErrorMessage = "Name already exists!")]
        public string DeptName { get; set; }
    }
}