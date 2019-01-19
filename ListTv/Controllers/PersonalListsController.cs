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
    public class PersonalListsController : Controller
    {
        private DataBaseTvEntities db = new DataBaseTvEntities();

        // GET: PersonalLists
        public ActionResult Index()
        {
            var personalList = db.PersonalList.Include(p => p.Login);
            return View(personalList.ToList());
        }
        public List<PersonalList> SendList()
        {
            var personalList = db.PersonalList.Include(p => p.Login);
            return personalList.ToList();
        }

        // GET: PersonalLists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonalList personalList = db.PersonalList.Find(id);
            if (personalList == null)
            {
                return HttpNotFound();
            }
            return View(personalList);
        }

        // GET: PersonalLists/Create
        public ActionResult Create()
        {
            ViewBag.Username = new SelectList(db.Login, "Username", "Password");
            return View();
        }

        // POST: PersonalLists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Username,Channel")] PersonalList personalList)
        {
            if (ModelState.IsValid)
            {
                db.PersonalList.Add(personalList);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Username = new SelectList(db.Login, "Username", "Password", personalList.Username);
            return View(personalList);
        }

        // GET: PersonalLists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonalList personalList = db.PersonalList.Find(id);
            if (personalList == null)
            {
                return HttpNotFound();
            }
            ViewBag.Username = new SelectList(db.Login, "Username", "Password", personalList.Username);
            return View(personalList);
        }

        // POST: PersonalLists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Username,Channel")] PersonalList personalList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(personalList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Username = new SelectList(db.Login, "Username", "Password", personalList.Username);
            return View(personalList);
        }

        // GET: PersonalLists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonalList personalList = db.PersonalList.Find(id);
            if (personalList == null)
            {
                return HttpNotFound();
            }
            return View(personalList);
        }

        // POST: PersonalLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PersonalList personalList = db.PersonalList.Find(id);
            db.PersonalList.Remove(personalList);
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
