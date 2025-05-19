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
    public class ExamStagesController : Controller
    {
        private ExamSchedulerDbEntities db = new ExamSchedulerDbEntities();

        // GET: ExamStages
        public ActionResult Index()
        {
            return View(db.ExamStages.ToList());
        }

        // GET: ExamStages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExamStage examStage = db.ExamStages.Find(id);
            if (examStage == null)
            {
                return HttpNotFound();
            }
            return View(examStage);
        }

        // GET: ExamStages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ExamStages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ExamStageID,Name")] ExamStage examStage)
        {
            if (ModelState.IsValid)
            {
                db.ExamStages.Add(examStage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(examStage);
        }

        // GET: ExamStages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExamStage examStage = db.ExamStages.Find(id);
            if (examStage == null)
            {
                return HttpNotFound();
            }
            return View(examStage);
        }

        // POST: ExamStages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ExamStageID,Name")] ExamStage examStage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(examStage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(examStage);
        }

        // GET: ExamStages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExamStage examStage = db.ExamStages.Find(id);
            if (examStage == null)
            {
                return HttpNotFound();
            }
            return View(examStage);
        }

        // POST: ExamStages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ExamStage examStage = db.ExamStages.Find(id);
            db.ExamStages.Remove(examStage);
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
