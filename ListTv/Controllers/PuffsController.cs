using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ListTv.Controllers;
using ListTv.Models;
using ListTv.ViewModels;

namespace ListTv.Models
{
    public class PuffsController : Controller
    {
        private DataBaseTvEntities db = new DataBaseTvEntities();

        // GET: Puffs
        public ActionResult Index()
        {
            var puff = db.Puff.Include(p => p.Program);
            return View(puff.ToList());
        }
        public List<Puff> SendList()
        {
            var puff = db.Puff.Include(p => p.Program);
            return puff.ToList();
        }

        public List<PuffEdit> GetPuffList()
        {
            List<PuffEdit> puffEditList = new List<PuffEdit>();
            ProgramsController pc = new ProgramsController();
            var prog = pc.SendList();
            var puff = db.Puff.Include(p => p.Program);

            foreach (var l in puff)
            {
                foreach (var p in prog)
                {
                    PuffEdit o = new PuffEdit();
                    if (l.Progid == p.Id)
                    {
                        o.Pid = l.Pid;
                        o.Progid = p.Id;
                        o.ProgramName = p.ProgramName;
                        o.Time = p.Time;
                        o.Date = p.Date;
                        o.ChannelId = p.ChannelId.Value;
                        o.Category = p.Category;
                        o.Length = p.Length;
                        o.Info = p.Info;
                        puffEditList.Add(o);
                    }
                }
            }
            return puffEditList;
        }


        // GET: Puffs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Puff puff = db.Puff.Find(id);
            if (puff == null)
            {
                return HttpNotFound();
            }
            return View(puff);
        }

        // GET: Puffs/Create
        public ActionResult Create()
        {
            ViewBag.Progid = new SelectList(db.Program, "Id", "ProgramName");
            return View();
        }

        // POST: Puffs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Pid,Progid")] Puff puff)
        {
            if (ModelState.IsValid)
            {
                db.Puff.Add(puff);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Progid = new SelectList(db.Program, "Id", "ProgramName", puff.Progid);
            return View(puff);
        }

        // GET: Puffs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Puff puff = db.Puff.Find(id);
            if (puff == null)
            {
                return HttpNotFound();
            }
            ViewBag.Progid = new SelectList(db.Program, "Id", "ProgramName", "Date", puff.Progid);
            return View(puff);
        }
        
        // POST: Puffs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Pid,Progid")] Puff puff)
        {
            if (ModelState.IsValid)
            {
                db.Entry(puff).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Progid = new SelectList(db.Program, "Id", "ProgramName", "Date", puff.Progid);
            return View(puff);
        }

        //public string GetCname(int progid)
        //{
        //    ProgramsController pc = new ProgramsController();
        //    var proglist = pc.SendList();
        //    string puppy = "";

        //    foreach (var item in proglist)
        //    {
        //        if (item.Id == progid)
        //        {
        //            puppy = Convert.ToString(item.Time);
        //        }
        //    }
        //    return puppy;
        //}

        // GET: Puffs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Puff puff = db.Puff.Find(id);
            if (puff == null)
            {
                return HttpNotFound();
            }
            return View(puff);
        }

        // POST: Puffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Puff puff = db.Puff.Find(id);
            db.Puff.Remove(puff);
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
