using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityManagementSystem.DTOs;
using UniversityManagementSystem.EF;

namespace UniversityManagementSystem.Controllers
{
    public class EnrollmentController : Controller
    {
        // GET: Enrollment

        UMSEntities2 db = new UMSEntities2();


        public ActionResult ListEnrollment()
        {
            if (Session["info"] == null || (Session["info"] as User)?.Role != "Admin")
            {
                TempData["msg"] = "Unauthorized access. Please log in as an admin.";
                return RedirectToAction("Index", "Login");
            }
            var enrollmentList = from e in db.Enrollments
                                 join s in db.Users on e.StudentId equals s.UserId
                                 join c in db.Courses on e.CourseId equals c.CourseId
                                 join i in db.Users on c.InstructorId equals i.UserId
                                 select new EnrollmentDTO
                                 {
                                     EnrollmentId = e.EnrollmentId,
                                     StudentId = e.StudentId,
                                     CourseId = e.CourseId,
                                     StudentName = s.Name,
                                     StudentEmail = s.Email,
                                     CourseName = c.CourseName,
                                     CourseDuration = c.Duration,
                                     InstructorName = i.Name
                                 };

            return View(enrollmentList.ToList());




            
        }

        public ActionResult FilterEnrollments(int? studentId, int? courseId)
        {
            if (Session["info"] == null || (Session["info"] as User)?.Role != "Admin")
            {
                TempData["msg"] = "Unauthorized access. Please log in as an admin.";
                return RedirectToAction("Index", "Login");
            }



            // Base query for enrollments
            var query = from e in db.Enrollments
                        join s in db.Users on e.StudentId equals s.UserId
                        join c in db.Courses on e.CourseId equals c.CourseId
                        join i in db.Users on c.InstructorId equals i.UserId
                        
                        select new EnrollmentDTO
                        {
                            EnrollmentId = e.EnrollmentId,
                            StudentId = e.StudentId,
                            CourseId = e.CourseId,
                            StudentName = s.Name,
                            StudentEmail = s.Email,
                            CourseName = c.CourseName,
                            CourseDuration = c.Duration,
                            InstructorName = i.Name,
                            
                        };

            // Apply filters
            if (studentId.HasValue)
                query = query.Where(e => e.StudentId == studentId);

            if (courseId.HasValue)
                query = query.Where(e => e.CourseId == courseId);

            // Populate dropdown lists for filtering
            ViewBag.Students = new SelectList(
                db.Users.Where(u => u.Role == "Student"),
                "UserId", "Name");

            ViewBag.Courses = new SelectList(
                db.Courses,
                "CourseId", "Name");

            return View(query.ToList());





        }


        [HttpGet]
        public ActionResult DeleteEnrollment(int? id)
        {
            if (Session["info"] == null || (Session["info"] as User)?.Role != "Admin")
            {
                TempData["msg"] = "Unauthorized access. Please log in as an admin.";
                return RedirectToAction("Index", "Login");
            }

            // Check if id is provided
            if (!id.HasValue)
            {
                TempData["msg"] = "Invalid user ID.";
                return RedirectToAction("ListUsers");
            }

            var enrollment = db.Enrollments.Find(id.Value);
            if (enrollment == null)
            {
                TempData["msg"] = "Enrollment not found.";
                return RedirectToAction("ListEnrollment");
            }

            // Pass user details to the view
            return View(enrollment);

        }

        [HttpPost]
        public ActionResult ConfirmEnrollment(int? id)
        {
            if (Session["info"] == null || (Session["info"] as User)?.Role != "Admin")
            {
                TempData["msg"] = "Unauthorized access. Please log in as an admin.";
                return RedirectToAction("Index", "Login");
            }

            // Check if id is provided
            if (!id.HasValue)
            {
                TempData["msg"] = "Invalid user ID.";
                return RedirectToAction("ListUsers");
            }

            var enrollment = db.Enrollments.Find(id.Value);
            if (enrollment == null)
            {
                TempData["msg"] = "Courses not found.";
                return RedirectToAction("ListEnrollment");
            }


            //Remove related enrollments and progress records
            var relatedEnrollments = db.Enrollments.Where(e => e.CourseId == id.Value);
            db.Enrollments.RemoveRange(relatedEnrollments);

            //var relatedProgress = db.StudentProgresses.Where(p => p.CourseId == id.Value);
            //db.StudentProgresses.RemoveRange(relatedProgress);

            db.Enrollments.Remove(enrollment);
            db.SaveChanges();

            TempData["SuccessMsg"] = "Enrollments deleted successfully.";
            return RedirectToAction("ListEnrollment");
        }







    }
}