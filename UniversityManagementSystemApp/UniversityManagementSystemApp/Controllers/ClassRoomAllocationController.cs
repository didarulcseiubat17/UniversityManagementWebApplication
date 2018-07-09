using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UniversityManagementSystemApp.Models;

namespace UniversityManagementSystemApp.Controllers
{
    public class ClassRoomAllocationController : Controller
    {
        private UniversityDbContext db = new UniversityDbContext();

        // GET: /ClassRoomAllocation/
        public ActionResult Index()
        {
            var classroomallocations = db.ClassRoomAllocations.Include(c => c.Course).Include(c => c.Day).Include(c => c.Department).Include(c => c.Room);
            return View(classroomallocations.ToList());
        }

        // GET: /ClassRoomAllocation/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassRoomAllocation classroomallocation = db.ClassRoomAllocations.Find(id);
            if (classroomallocation == null)
            {
                return HttpNotFound();
            }
            return View(classroomallocation);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "CourseCode");
            ViewBag.DayId = new SelectList(db.Days, "Id", "Name");
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "DeptCode");
            ViewBag.RoomId = new SelectList(db.Rooms, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,DepartmentId,CourseId,RoomId,DayId,StartTime,EndTime,RoomStatus")] ClassRoomAllocation classroomallocation)
        {
            if (ModelState.IsValid)
            {
                db.ClassRoomAllocations.Add(classroomallocation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseId = new SelectList(db.Courses, "Id", "CourseCode", classroomallocation.CourseId);
            ViewBag.DayId = new SelectList(db.Days, "Id", "Name", classroomallocation.DayId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "DeptCode", classroomallocation.DepartmentId);
            ViewBag.RoomId = new SelectList(db.Rooms, "Id", "Name", classroomallocation.RoomId);
            return View(classroomallocation);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassRoomAllocation classroomallocation = db.ClassRoomAllocations.Find(id);
            if (classroomallocation == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "CourseCode", classroomallocation.CourseId);
            ViewBag.DayId = new SelectList(db.Days, "Id", "Name", classroomallocation.DayId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "DeptCode", classroomallocation.DepartmentId);
            ViewBag.RoomId = new SelectList(db.Rooms, "Id", "Name", classroomallocation.RoomId);
            return View(classroomallocation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,DepartmentId,CourseId,RoomId,DayId,StartTime,EndTime,RoomStatus")] ClassRoomAllocation classroomallocation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(classroomallocation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "CourseCode", classroomallocation.CourseId);
            ViewBag.DayId = new SelectList(db.Days, "Id", "Name", classroomallocation.DayId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "DeptCode", classroomallocation.DepartmentId);
            ViewBag.RoomId = new SelectList(db.Rooms, "Id", "Name", classroomallocation.RoomId);
            return View(classroomallocation);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassRoomAllocation classroomallocation = db.ClassRoomAllocations.Find(id);
            if (classroomallocation == null)
            {
                return HttpNotFound();
            }
            return View(classroomallocation);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ClassRoomAllocation classroomallocation = db.ClassRoomAllocations.Find(id);
            db.ClassRoomAllocations.Remove(classroomallocation);
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
    }
}
