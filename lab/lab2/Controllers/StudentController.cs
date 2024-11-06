using lab2.EF;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace lab2.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        ASPEntities1 DB = new ASPEntities1();


        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Student s)
        {
            DB.Students.Add(s);
            DB.SaveChanges();
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            var data = DB.Students.ToList();
            return View(data);
        }

        public ActionResult Details(int id)
        {
            var data = DB.Students.Find(id);
            return View(data);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var data = DB.Students.Find(id);
            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(Student fromobj)
        {
            var exobj = DB.Students.Find(fromobj.id);
            fromobj.cgpa = exobj.cgpa;
            DB.Entry(exobj).CurrentValues.SetValues(fromobj);
            DB.SaveChanges();
            return RedirectToAction("List");
        }
        [HttpGet]
        public ActionResult delete(int id)
        {
            var data = DB.Students.Find(id);
            return View(data);
        }
        [HttpPost]
        [ActionName("delete")]
        public ActionResult deleteConfirmed(int id)
        {
            var data = DB.Students.Find(id);


            DB.Students.Remove(data);
            DB.SaveChanges();

            return RedirectToAction("List");
        }
    }
}