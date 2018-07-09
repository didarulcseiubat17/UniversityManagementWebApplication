using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityManagementSystemApp.Models;

namespace UniversityManagementSystemApp.Controllers
{
    public class EnrollmentController : Controller
    {
        UniversityDbContext db = new UniversityDbContext();
        [HttpGet]
        public ActionResult EnrollAStudent()
        {
            ViewBag.ResgistrationNumbers = db.Students.ToList();
            ViewBag.Courses = db.Courses.ToList();
            ViewBag.Grades = db.Grades.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EnrollAStudent(Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                db.Enrollments.Add(enrollment);
                db.SaveChanges();
            }
            ViewBag.ResgistrationNumbers = db.Students.ToList();
            ViewBag.Courses = db.Courses.ToList();
            ViewBag.Grades = db.Grades.ToList();
            return View(enrollment);
        }

        public ActionResult EnrollStatistics()
        {
            ViewBag.GetRegistrationNumners = db.Students.ToList();
            return View();
        }
        public JsonResult GetStudentInfoByRegistrationNumber(string prefix)
        {
            var studentInformation = db.Students.FirstOrDefault(x => x.StudentRegNo == prefix);
            return Json(studentInformation, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDepartmentById(int departmentId)
        {
            var department = db.Departments.FirstOrDefault(x => x.Id == departmentId);
            return Json(department, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveEnrollment(Enrollment enrollment)
        {
            var check =
                db.Enrollments.Where(x => x.CourseId == enrollment.CourseId && x.StudentId == enrollment.StudentId).ToList();
            if (check.Count > 0)
                return Json(false, JsonRequestBehavior.AllowGet);
            else
            {
                enrollment.GradeId = 0;
                enrollment.ResultStatus = "";
                db.Enrollments.Add(enrollment);
                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }  
        }

        public JsonResult GetCourseStattistics(string registrationNumber)
        {
            var studentEnrollmentInfo = db.Enrollments.Where(x => x.RegistrationNumber == registrationNumber).ToList();
            return new JsonResult { Data = studentEnrollmentInfo, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}