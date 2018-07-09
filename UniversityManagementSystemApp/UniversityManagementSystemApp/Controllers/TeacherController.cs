using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using UniversityManagementSystemApp.Models;

namespace UniversityManagementSystemApp.Controllers
{
    public class TeacherController : Controller
    {
        private UniversityDbContext db = new UniversityDbContext();

        public ActionResult Index()
        {
            var teachers = db.Teachers.Include(t => t.Department).Include(t => t.Designation);
            return View(teachers.ToList());
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // GET: /Teacher/Create
        public ActionResult Create()
        {
            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Massage"];
                ViewBag.MessageType = TempData["MessageType"];
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "DeptCode");
            ViewBag.DesignationId = new SelectList(db.Designations, "Id", "DesignationName");
            ViewBag.Departmets = db.Departments.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="Id,TeacherName,TeacherAddress,TeacherEmail,TeacherContactNo,DesignationId,DepartmentId,CreditTaken,CreditLeft")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                teacher.CreditLeft = teacher.CreditTaken;
                db.Teachers.Add(teacher);
                if (await db.SaveChangesAsync() > 0)
                {
                    TempData["Massage"] = string.Format("Teacher <b> {0} </b> is Save Successfully", teacher.TeacherName);
                    TempData["MassageType"] = "success";
                }
                return RedirectToAction("Create");
            }

            //ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "DeptCode", teacher.DepartmentId);
            
            //ViewBag.DesignationId = new SelectList(db.Designations, "Id", "DesignationName", teacher.DesignationId);
            return View(teacher);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "DeptCode", teacher.DepartmentId);
            ViewBag.DesignationId = new SelectList(db.Designations, "Id", "DesignationName", teacher.DesignationId);
            return View(teacher);
        }

        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,TeacherName,TeacherAddress,TeacherEmail,TeacherContactNo,DesignationId,DepartmentId,CreditTaken,CreditLeft")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teacher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "DeptCode", teacher.DepartmentId);
            ViewBag.DesignationId = new SelectList(db.Designations, "Id", "DesignationName", teacher.DesignationId);
            return View(teacher);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Teacher teacher = db.Teachers.Find(id);
            db.Teachers.Remove(teacher);
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

        public JsonResult IsEmailExists(string TeacherEmail)
        {
            return Json(!db.Teachers.Any(x => x.TeacherEmail == TeacherEmail), JsonRequestBehavior.AllowGet);

        }
    }
}
