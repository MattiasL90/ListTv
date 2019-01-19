using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ListTv.Models
{
    public class MoreInfoesController : Controller
    {
        private DataBaseTvEntities db = new DataBaseTvEntities();

        // GET: MoreInfoes
        public ActionResult Index()
        {
            var moreInfo = db.MoreInfo.Include(m => m.Program);
            return View(moreInfo.ToList());
        }

        // GET: MoreInfoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MoreInfo moreInfo = db.MoreInfo.Find(id);
            if (moreInfo == null)
            {
                return HttpNotFound();
            }
            return View(moreInfo);
        }

        // GET: MoreInfoes/Create
        public ActionResult Create()
        {
            ViewBag.ProgramId = new SelectList(db.Program, "Id", "ProgramName");
            return View();
        }

        // POST: MoreInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProgramId,Info")] MoreInfo moreInfo)
        {
            if (ModelState.IsValid)
            {
                db.MoreInfo.Add(moreInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProgramId = new SelectList(db.Program, "Id", "ProgramName", moreInfo.ProgramId);
            return View(moreInfo);
        }

        // GET: MoreInfoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MoreInfo moreInfo = db.MoreInfo.Find(id);
            if (moreInfo == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProgramId = new SelectList(db.Program, "Id", "ProgramName", moreInfo.ProgramId);
            return View(moreInfo);
        }

        // POST: MoreInfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProgramId,Info")] MoreInfo moreInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(moreInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProgramId = new SelectList(db.Program, "Id", "ProgramName", moreInfo.ProgramId);
            return View(moreInfo);
        }

        // GET: MoreInfoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MoreInfo moreInfo = db.MoreInfo.Find(id);
            if (moreInfo == null)
            {
                return HttpNotFound();
            }
            return View(moreInfo);
        }

        // POST: MoreInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MoreInfo moreInfo = db.MoreInfo.Find(id);
            db.MoreInfo.Remove(moreInfo);
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
