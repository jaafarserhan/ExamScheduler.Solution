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
    public class ScheduledExamProctersController : Controller
    {
        private ExamSchedulerDbEntities db = new ExamSchedulerDbEntities();

        // GET: ScheduledExamProcters
        public ActionResult Index()
        {
            var scheduledExamProcters = db.ScheduledExamProcters.Include(s => s.ScheduledExam).Include(s => s.User);
            return View(scheduledExamProcters.ToList());
        }

        // GET: ScheduledExamProcters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ScheduledExamProcter scheduledExamProcter = db.ScheduledExamProcters.Find(id);
            if (scheduledExamProcter == null)
            {
                return HttpNotFound();
            }
            return View(scheduledExamProcter);
        }

        // GET: ScheduledExamProcters/Create
        public ActionResult Create()
        {
            ViewBag.ScheduledExamID = new SelectList(db.ScheduledExams, "ScheduledExamID", "ScheduledExamID");
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Name");
            return View();
        }

        // POST: ScheduledExamProcters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ScheduledExamProcterID,UserID,ScheduledExamID")] ScheduledExamProcter scheduledExamProcter)
        {
            if (ModelState.IsValid)
            {
                db.ScheduledExamProcters.Add(scheduledExamProcter);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ScheduledExamID = new SelectList(db.ScheduledExams, "ScheduledExamID", "ScheduledExamID", scheduledExamProcter.ScheduledExamID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Name", scheduledExamProcter.UserID);
            return View(scheduledExamProcter);
        }

        // GET: ScheduledExamProcters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ScheduledExamProcter scheduledExamProcter = db.ScheduledExamProcters.Find(id);
            if (scheduledExamProcter == null)
            {
                return HttpNotFound();
            }
            ViewBag.ScheduledExamID = new SelectList(db.ScheduledExams, "ScheduledExamID", "ScheduledExamID", scheduledExamProcter.ScheduledExamID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Name", scheduledExamProcter.UserID);
            return View(scheduledExamProcter);
        }

        // POST: ScheduledExamProcters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ScheduledExamProcterID,UserID,ScheduledExamID")] ScheduledExamProcter scheduledExamProcter)
        {
            if (ModelState.IsValid)
            {
                db.Entry(scheduledExamProcter).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ScheduledExamID = new SelectList(db.ScheduledExams, "ScheduledExamID", "ScheduledExamID", scheduledExamProcter.ScheduledExamID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Name", scheduledExamProcter.UserID);
            return View(scheduledExamProcter);
        }

        // GET: ScheduledExamProcters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ScheduledExamProcter scheduledExamProcter = db.ScheduledExamProcters.Find(id);
            if (scheduledExamProcter == null)
            {
                return HttpNotFound();
            }
            return View(scheduledExamProcter);
        }

        // POST: ScheduledExamProcters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ScheduledExamProcter scheduledExamProcter = db.ScheduledExamProcters.Find(id);
            db.ScheduledExamProcters.Remove(scheduledExamProcter);
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
