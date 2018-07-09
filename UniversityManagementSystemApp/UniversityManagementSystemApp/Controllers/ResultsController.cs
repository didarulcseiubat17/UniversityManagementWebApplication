using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using UniversityManagementSystemApp.Models;

namespace UniversityManagementSystemApp.Controllers
{
    public class ResultsController : Controller
    {
        private UniversityDbContext db = new UniversityDbContext();

        // GET: /Results/
        public ActionResult Index()
        {
            var results = db.Results.Include(r => r.Course).Include(r => r.Grade);
            return View(results.ToList());
        }

        // GET: /Results/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Result result = db.Results.Find(id);
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        // GET: /Results/Create
        public ActionResult Create()
        {
            //ViewBag.CourseId = new SelectList(db.Courses, "Id", "CourseCode");
            //ViewBag.GradeId = new SelectList(db.Grades, "Id", "GradeName");
            ViewBag.Grades = db.Grades.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RegistrationNumber,Name,Email,Department,CourseId,GradeId,ResultStatus")] Result result)
        {
            if (ModelState.IsValid)
            {
                db.Results.Add(result);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.CourseId = new SelectList(db.Courses, "Id", "CourseCode", result.CourseId);
            //ViewBag.GradeId = new SelectList(db.Grades, "Id", "GradeName", result.GradeId);
            ViewBag.Grades = db.Grades.ToList();
            return View(result);
        }

        // GET: /Results/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Result result = db.Results.Find(id);
            if (result == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "CourseCode", result.CourseId);
            ViewBag.GradeId = new SelectList(db.Grades, "Id", "GradeName", result.GradeId);
            return View(result);
        }

        // POST: /Results/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RegistrationNumber,Name,Email,Department,CourseId,GradeId,ResultStatus")] Result result)
        {
            if (ModelState.IsValid)
            {
                db.Entry(result).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "CourseCode", result.CourseId);
            ViewBag.GradeId = new SelectList(db.Grades, "Id", "GradeName", result.GradeId);
            return View(result);
        }

        // GET: /Results/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Result result = db.Results.Find(id);
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        // POST: /Results/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Result result = db.Results.Find(id);
            db.Results.Remove(result);
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

        public ActionResult ViewResult()
        {
            return View();

        }


        public JsonResult GetStudentInfoByRegistrationNumber(string registrationNum)
        {
            var student = db.Students.FirstOrDefault(x => x.StudentRegNo == registrationNum);
            if (student != null) return Json(student, JsonRequestBehavior.AllowGet);
            else return Json(false, JsonRequestBehavior.AllowGet);
        }


        //may be this method is unuseful...
        public JsonResult GetEnrollAndResultInfoByRegNumber(string registrationNum)
        {
            var enrollmentInfo = db.Enrollments.Where(x => x.RegistrationNumber == registrationNum).ToList();
            if (enrollmentInfo.Count > 0)
            {
                return Json(enrollmentInfo, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetResultInfoByRegNo(string registrationNumber)
        {
            var resInfo = db.Enrollments.Where(x => x.RegistrationNumber == registrationNumber).ToList();
            if (resInfo.Count > 0)
            {
                return Json(resInfo, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveResult(Result result)
        {
            var check =
                db.Results.Where(x => x.CourseId == result.CourseId && x.RegistrationNumber == result.RegistrationNumber)
                    .ToList();
            if (check.Count > 0)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else
            {

                var enroll = db.Enrollments.FirstOrDefault(x => x.RegistrationNumber == result.RegistrationNumber && x.ResultStatus == "");
                if (enroll != null)
                {
                    result.EnrollmentId = enroll.Id;
                    enroll.GradeId = result.GradeId;
                    enroll.ResultStatus = "Published";
                    db.Enrollments.AddOrUpdate(enroll);
                }
                db.Results.Add(result);
                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }


        }
    }
}
