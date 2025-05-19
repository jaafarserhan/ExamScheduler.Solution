using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using ExamScheduler;

namespace ExamScheduler.Controllers
{
    public class ScheduledExamsController : Controller
    {
        private ExamSchedulerDbEntities db = new ExamSchedulerDbEntities();

        // GET: ScheduledExams
        public ActionResult Index()
        {
            var scheduledExams = db.ScheduledExams.Include(s => s.Campu).Include(s => s.Course).Include(s => s.ExamStage).Include(s => s.Room);
            return View(scheduledExams.ToList());
        }

        // GET: ScheduledExams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ScheduledExam scheduledExam = db.ScheduledExams.Find(id);
            if (scheduledExam == null)
            {
                return HttpNotFound();
            }
            return View(scheduledExam);
        }

        // GET: ScheduledExams/Create
        public ActionResult Create()
        {
            ViewBag.CampusID = new SelectList(db.Campus1, "CampusID", "Name");
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Name");
            ViewBag.ExamStageID = new SelectList(db.ExamStages, "ExamStageID", "Name");
            ViewBag.RoomID = new SelectList(db.Rooms, "RoomID", "Name");
            return View();
        }

        // POST: ScheduledExams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ScheduledExamID,CourseID,RoomID,Name,CampusID,ExamStageID,Date,StartTime,EndTime")] ScheduledExam scheduledExam)
        {
            if (ModelState.IsValid)
            {
                db.ScheduledExams.Add(scheduledExam);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            scheduledExam.Name = scheduledExam.Course.Name + "-" + scheduledExam.ExamStage.Name + "-" + scheduledExam.Campu.Name;

            ViewBag.CampusID = new SelectList(db.Campus1, "CampusID", "Name", scheduledExam.CampusID);
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Name", scheduledExam.CourseID);
            ViewBag.ExamStageID = new SelectList(db.ExamStages, "ExamStageID", "Name", scheduledExam.ExamStageID);
            ViewBag.RoomID = new SelectList(db.Rooms, "RoomID", "Name", scheduledExam.RoomID);
            return View(scheduledExam);
        }

        // GET: ScheduledExams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ScheduledExam scheduledExam = db.ScheduledExams.Find(id);
            if (scheduledExam == null)
            {
                return HttpNotFound();
            }
            ViewBag.CampusID = new SelectList(db.Campus1, "CampusID", "Name", scheduledExam.CampusID);
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Name", scheduledExam.CourseID);
            ViewBag.ExamStageID = new SelectList(db.ExamStages, "ExamStageID", "Name", scheduledExam.ExamStageID);
            ViewBag.RoomID = new SelectList(db.Rooms, "RoomID", "Name", scheduledExam.RoomID);
            return View(scheduledExam);
        }

        // POST: ScheduledExams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ScheduledExamID,CourseID,RoomID,Name,CampusID,ExamStageID,Date,StartTime,EndTime")] ScheduledExam scheduledExam)
        {
            if (ModelState.IsValid)
            {
                db.Entry(scheduledExam).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            scheduledExam.Name = scheduledExam.Course.Name + "-" + scheduledExam.ExamStage.Name + "-" + scheduledExam.Campu.Name ;

            ViewBag.CampusID = new SelectList(db.Campus1, "CampusID", "Name", scheduledExam.CampusID);
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Name", scheduledExam.CourseID);
            ViewBag.ExamStageID = new SelectList(db.ExamStages, "ExamStageID", "Name", scheduledExam.ExamStageID);
            ViewBag.RoomID = new SelectList(db.Rooms, "RoomID", "Name", scheduledExam.RoomID);
            return View(scheduledExam);
        }

        // GET: ScheduledExams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ScheduledExam scheduledExam = db.ScheduledExams.Find(id);
            if (scheduledExam == null)
            {
                return HttpNotFound();
            }
            return View(scheduledExam);
        }

        // POST: ScheduledExams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ScheduledExam scheduledExam = db.ScheduledExams.Find(id);
            db.ScheduledExams.Remove(scheduledExam);
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
