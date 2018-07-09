using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UniversityManagementSystemApp.Gateway;
using UniversityManagementSystemApp.Models;

namespace UniversityManagementSystemApp.Controllers
{
    public class CourseAssignToController : Controller
    {
        private UniversityDbContext db = new UniversityDbContext();

        public ActionResult Index()
        {
            var courseassigntoes = db.CourseAssigns.Include(c => c.Course).Include(c => c.Department).Include(c => c.Teacher);
            return View(courseassigntoes.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseAssign courseassignto = db.CourseAssigns.Find(id);
            if (courseassignto == null)
            {
                return HttpNotFound();
            }
            return View(courseassignto);
        }

        public ActionResult Create()
        {
            ViewBag.CourseID = new SelectList(db.Courses, "Id", "CourseCode");
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "DeptCode");
            ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "TeacherName");
            ViewBag.Departments = db.Departments.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CourseAssign courseassignto)
        {
            if (ModelState.IsValid)
            {
                db.CourseAssigns.Add(courseassignto);
                db.SaveChanges();
                //return RedirectToAction("Index");
            }
            ViewBag.Departments = db.Departments.ToList();
            ViewBag.CourseID = new SelectList(db.Courses, "Id", "CourseCode", courseassignto.CourseID);
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "DeptCode", courseassignto.DepartmentId);
            ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "TeacherName", courseassignto.TeacherId);
            return View(courseassignto);
        }


        public ActionResult Assigning()
        {
            ViewBag.CourseID = new SelectList(db.Courses, "Id", "CourseCode");
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "DeptCode");
            ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "TeacherName");
            ViewBag.Departments = db.Departments.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Assigning(CourseAssign courseassignto)
        {
            if (ModelState.IsValid)
            {
                db.CourseAssigns.Add(courseassignto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Departments = db.Departments.ToList();
            ViewBag.CourseID = new SelectList(db.Courses, "Id", "CourseCode", courseassignto.CourseID);
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "DeptCode", courseassignto.DepartmentId);
            ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "TeacherName", courseassignto.TeacherId);
            return View(courseassignto);
        }

        

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseAssign courseassignto = db.CourseAssigns.Find(id);
            if (courseassignto == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseID = new SelectList(db.Courses, "Id", "CourseCode", courseassignto.CourseID);
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "DeptCode", courseassignto.DepartmentId);
            ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "TeacherName", courseassignto.TeacherId);
            return View(courseassignto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CourseAssign courseassignto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(courseassignto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseID = new SelectList(db.Courses, "Id", "CourseCode", courseassignto.CourseID);
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "DeptCode", courseassignto.DepartmentId);
            ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "TeacherName", courseassignto.TeacherId);
            return View(courseassignto);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseAssign courseassignto = db.CourseAssigns.Find(id);
            if (courseassignto == null)
            {
                return HttpNotFound();
            }
            return View(courseassignto);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CourseAssign courseassignto = db.CourseAssigns.Find(id);
            db.CourseAssigns.Remove(courseassignto);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public JsonResult GetTeacherByDepartmentId(int departmentId)
        {
            var teachers = db.Teachers.Where(x => x.DepartmentId == departmentId).ToList();
            return Json(teachers, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCourseCodeByDepartmentId(int departmentId)
        {
            var deptCode = db.Courses.Where(x => x.DepartmentId == departmentId).ToList();
            return Json(deptCode, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCreditInformationByTeacherId(int teacherId)
        {
            var creditInformation = db.Teachers.Single(x => x.Id == teacherId);
            return Json(creditInformation, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCourseInformationById(int courseId)
        {
            var courseInformation = db.Courses.FirstOrDefault(x => x.Id == courseId);
            return Json(courseInformation, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsAlreadyAssignedCourse(int courseId)
        {
            var checkCourseAssign =
                db.CourseAssigns.Where(x => x.CourseID == courseId && x.Course.CourseStatus == true).ToList();
            if (checkCourseAssign.Count > 0)
                return Json(false, JsonRequestBehavior.AllowGet);
            else
                return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveCourseAssign(CourseAssign courseAssign)
        {
            var checkCourseAssign =
                db.CourseAssigns.Where(x => x.CourseID == courseAssign.CourseID && x.Course.CourseStatus == true).ToList();
            if (checkCourseAssign.Count > 0)
                return Json(false, JsonRequestBehavior.AllowGet);
            else
            {
                db.CourseAssigns.Add(courseAssign);
                db.SaveChanges();

                var teacher = db.Teachers.Single(x => x.Id == courseAssign.TeacherId);
                if (teacher != null)
                {
                    teacher.CreditLeft = courseAssign.CreditLeft;


                    db.Teachers.AddOrUpdate(teacher);
                    db.SaveChanges();

                    var course = db.Courses.Single(x => x.Id == courseAssign.CourseID);
                    if (course != null)
                    {
                        course.CourseStatus = true;
                        course.CourseAssignTo = teacher.TeacherName;
                        CourseGateway courseGateway = new CourseGateway();
                        courseGateway.UpdateCourseStatus(course);
                        return Json(true, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(false, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public ActionResult CourseStatistics()
        {
            return View();
        }

        public JsonResult GetAllDepartments()
        {
            return Json(db.Departments.ToList(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult CourseStatisticsByDepartmentId(int departmentId)
        {
            var courses = db.Courses.Where(x => x.DepartmentId == departmentId).ToList();
            return Json(courses, JsonRequestBehavior.AllowGet);
        }

        

    }
}
