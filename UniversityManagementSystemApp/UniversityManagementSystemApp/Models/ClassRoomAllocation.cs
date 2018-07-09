using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace UniversityManagementSystemApp.Models
{
    public class ClassRoomAllocation
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please Select Department")]
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        [Required]
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }

        [Required]
        [Display(Name = "Room")]
        public int RoomId { get; set; }
        public virtual Room Room { get; set; }

        [Required]
        [Display(Name = "Day")]
        public int DayId { get; set; }
        public virtual Day Day { get; set; }

        [Required]
        public double StartTime { get; set; }

        [Required]
        public double EndTime { get; set; }

        public string RoomStatus { get; set; }
    }
}