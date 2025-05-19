using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ExamScheduler;

namespace ExamScheduler.Controllers
{
    public class ScheduledExamStudentsController : Controller
    {
        private ExamSchedulerDbEntities db = new ExamSchedulerDbEntities();

        // GET: ScheduledExamStudents
        public ActionResult Index()
        {
            var scheduledExamUsers = db.ScheduledExamStudents.Include(s => s.ScheduledExam).Include(s => s.User);
            var scheduledExamStudents = scheduledExamUsers.Where(s => s.User.RoleID == 3).ToList();

            return View(scheduledExamStudents.ToList());
        }

        // GET: ScheduledExamStudents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ScheduledExamStudent scheduledExamStudent = db.ScheduledExamStudents.Find(id);
            if (scheduledExamStudent == null)
            {
                return HttpNotFound();
            }
            return View(scheduledExamStudent);
        }

        // GET: ScheduledExamStudents/Create
        public ActionResult Create()
        {
            ViewBag.ScheduledExamID = new SelectList(db.ScheduledExams, "ScheduledExamID", "ScheduledExamID");
            ViewBag.UserID = new SelectList(db.Users.Where(s=>s.RoleID==3).ToList(), "UserID", "Name");
            return View();
        }

        // POST: ScheduledExamStudents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ScheduledExamStudentID,UserID,ScheduledExamID")] ScheduledExamStudent scheduledExamStudent)
        {
            if (ModelState.IsValid)
            {
                db.ScheduledExamStudents.Add(scheduledExamStudent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ScheduledExamID = new SelectList(db.ScheduledExams, "ScheduledExamID", "ScheduledExamID", scheduledExamStudent.ScheduledExamID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Name", scheduledExamStudent.UserID);
            return View(scheduledExamStudent);
        }

        // GET: ScheduledExamStudents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ScheduledExamStudent scheduledExamStudent = db.ScheduledExamStudents.Find(id);
            if (scheduledExamStudent == null)
            {
                return HttpNotFound();
            }
            ViewBag.ScheduledExamID = new SelectList(db.ScheduledExams, "ScheduledExamID", "ScheduledExamID", scheduledExamStudent.ScheduledExamID);
            ViewBag.UserID = new SelectList(db.Users.Where(s => s.RoleID == 3).ToList(), "UserID", "Name", scheduledExamStudent.UserID);
            return View(scheduledExamStudent);
        }

        // POST: ScheduledExamStudents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ScheduledExamStudentID,UserID,ScheduledExamID")] ScheduledExamStudent scheduledExamStudent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(scheduledExamStudent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ScheduledExamID = new SelectList(db.ScheduledExams, "ScheduledExamID", "ScheduledExamID", scheduledExamStudent.ScheduledExamID);
            ViewBag.UserID = new SelectList(db.Users.Where(s => s.RoleID == 3).ToList(), "UserID", "Name", scheduledExamStudent.UserID);
            return View(scheduledExamStudent);
        }

        // GET: ScheduledExamStudents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ScheduledExamStudent scheduledExamStudent = db.ScheduledExamStudents.Find(id);
            if (scheduledExamStudent == null)
            {
                return HttpNotFound();
            }
            return View(scheduledExamStudent);
        }

        // POST: ScheduledExamStudents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ScheduledExamStudent scheduledExamStudent = db.ScheduledExamStudents.Find(id);
            db.ScheduledExamStudents.Remove(scheduledExamStudent);
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
