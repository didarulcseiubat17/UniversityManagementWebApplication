using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using UniversityManagementSystemApp.Manager;
using UniversityManagementSystemApp.Models;

namespace UniversityManagementSystemApp.Controllers
{
    public class DepartmentController : Controller
    {
        private UniversityDbContext db = new UniversityDbContext();

        public ActionResult Index()
        {
            return View(db.Departments.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        public ActionResult Create()
        {
            if (TempData["Massage"] != null)
            {
                ViewBag.Message = TempData["Massage"];
                ViewBag.MessageType = TempData["MassageType"];
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,DeptCode,DeptName")] Department department)
        {
            if (ModelState.IsValid)
            {
                db.Departments.Add(department);
                if (await db.SaveChangesAsync() > 0)
                {
                    TempData["Massage"] = string.Format("Department <b> {0} </b> is Save Successfully", department.DeptCode);
                    TempData["MassageType"] = "success";
                }
                //db.SaveChanges();
                //return RedirectToAction("Create");
            }

            return View(department);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,DeptCode,DeptName")] Department department)
        {
            if (ModelState.IsValid)
            {
                db.Entry(department).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(department);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Department department = db.Departments.Find(id);
            db.Departments.Remove(department);
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

        private bool isExist = false;
        DepartmentManager departmentManager = new DepartmentManager();
        public JsonResult IsExistDepartmentCode(string DeptCode)
        {
            isExist = departmentManager.IsExistDepartmentCode(DeptCode);
            if (isExist)
             return Json(false, JsonRequestBehavior.AllowGet);
            
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);

            }
        }

        public JsonResult IsExistDepartmentName(string DeptName)
        {
            isExist = departmentManager.IsExistDepartmentName(DeptName);
            if (isExist)
                return Json(false, JsonRequestBehavior.AllowGet);
            
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
