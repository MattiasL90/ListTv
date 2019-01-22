using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ListTv.Models;
using ListTv.ViewModels;


namespace ListTv.Controllers
{
    public class VMController : Controller
    {
        private object db;

        //public ActionResult Days()
        //{
        //    return View();
        //}
        
        public object UserName { get; }

        public object PassWord { get; }

        public object PuffList { get; } 

        public ActionResult PrivateList(string uname, string pword)
        {
            ProgramsController pc = new ProgramsController();
            var proglist = pc.SendList();
            PersonalListsController pl = new PersonalListsController();
            var plist = pl.SendList();
            List<ProgramVM> personlist = new List<ProgramVM>();
            var dateAndTime = DateTime.Now;
            var datee = dateAndTime.Date;

            ViewBag.UserName = uname;
            ViewBag.PassWord = pword;

            if (Login(uname, pword) == 1)
            {
                PuffsController puc = new PuffsController();
                List<Puff> puffen = puc.SendList();
                ViewBag.PuffList = puffen;
                return View("Admin");
            }
            else if (Login(uname, pword) == 2)
            {
                    foreach (var l in plist)
                    {
                    foreach (var p in proglist)
                    {
                        string x = GetChannel(p.ChannelId.Value);
                        if (l.Channel == GetChannel(p.ChannelId.Value) && l.Username == uname)
                        {
                            ProgramVM o = new ProgramVM();
                            if (p.Date == datee)
                            {
                                o.Id = p.Id;
                                o.ProgramName = p.ProgramName;
                                o.Time = p.Time;
                                o.ChannelId = p.ChannelId.Value;
                                o.Date = p.Date;
                                o.Length = p.Length;
                                o.Info = p.Info;
                                personlist.Add(o);
                            }
                        }
                    }
                    
                }
                return View(personlist);
            }
            else
            {
                return View("Fail");
            }

        }

        
        public ActionResult Days(string date)
        {
            DateTime datum = Convert.ToDateTime(date);
            ProgramsController pc = new ProgramsController();
            List<ProgramVM> progtables = new List<ProgramVM>();
            var program = pc.SendList();
            ViewBag.puffList = GetPuff();

            foreach (var p in program)
            {
                ProgramVM o = new ProgramVM();
                if (p.Date == datum)
                {
                    o.Id = p.Id;
                    o.ProgramName = p.ProgramName;
                    o.Time = p.Time;
                    o.ChannelId = p.ChannelId.Value;
                    o.Date = p.Date;
                    o.Length = p.Length;
                    o.Info = p.Info;
                }
                progtables.Add(o);
            }
            return View(SortList(progtables));
        }

        public ActionResult ShowMore(int id)
        {
            List<ShowMore> LstMore = new List<ShowMore>();
            ProgramsController pc = new ProgramsController();
            var programs = pc.SendList();
            foreach (var s in programs)
            {
                if (s.Id == id)
                {
                    LstMore.Add(new ShowMore { Id = s.Id, ProgramName = s.ProgramName, Time = s.Time, Category = s.Category, ChannelName = GetChannel(s.ChannelId.Value), Length = s.Length, Date = s.Date, Info = s.Info });
                }
            }
            return View(LstMore);
        }

        public string GetChannel(int id)
        {
            string chan = "";
            if (id == 1)
            {
                chan = "Svt";
            }
            else if (id == 2)
            {
                chan = "Tv3";
            }
            else if (id == 4)
            {
                chan = "Tv4";
            }
            else if (id == 5)
            {
                chan = "Tv6";
            }
            else
            {
                chan = "Kanal 5";
            }
            return chan;
        }

        public int Login(string uname, string pword)
        {
            int lType = 0;
            LoginsController lc = new LoginsController();
            var logins = lc.SendList();
            string test = "";
            foreach (var l in logins)
            {
                test = l.Username;
                if (l.Username == uname && l.Password == pword)
                {
                    lType = l.Type;
                    break;
                }
                else
                {
                    l.Type = 0;
                }
            }
            return lType;
        }

        public List<ProgramVM> SortList(List<ProgramVM> list)
        {
            list = list.OrderByDescending(x => x.ChannelId).ToList();
            return list;
        }

        public ActionResult AddFavorite(string uname, string pword)
        {
            ViewBag.UserName = uname;
            ViewBag.PassWord = pword;

            PersonalListsController plc = new PersonalListsController();
            var plist = plc.SendList();
            List<string> allist = ChannelList();
            List<string> ownlist = new List<string>();
            foreach (var ol in plist)
            {
                if (ol.Username == uname)
                {
                    ownlist.Add(ol.Channel);
                }
            }

            List<string> finallist = ChannelList();

            foreach (var o in ownlist)
            {
                foreach (var a in allist)
                {
                    if (a == o)
                    {
                        finallist.RemoveAll(x => ((string)x) == o);
                    }
                }
            }
            return View(finallist);
        }

        public ActionResult AddedFavorite(string uname, string pword, string channl)
        {
            DataBaseTvEntities db = new DataBaseTvEntities();
            if (ModelState.IsValid)
            {
                PersonalList perlis = new PersonalList()
                {
                    Username = uname,
                    Channel = channl
                };
                db.PersonalList.Add(perlis);
                db.SaveChanges();
                ViewBag.UserName = uname;
                ViewBag.PassWord = pword;
                return RedirectToAction("AddFavorite", new { uname , pword });
            }
            return View();
        }

        public ActionResult DeleteFavorite(string uname,  string pword, int channl)
        {
            DataBaseTvEntities db = new DataBaseTvEntities();
            string chn = GetChannel(channl);
            int cid = GetCid(uname, GetChannel(channl));
            if (ModelState.IsValid)
            {
                var del = (from c in db.PersonalList
                           where c.Id == cid
                           select c).FirstOrDefault();
                db.PersonalList.Remove(del);
                db.SaveChanges();
            }

            ProgramsController pc = new ProgramsController();
            var proglist = pc.SendList();
            PersonalListsController pl = new PersonalListsController();
            var plist = pl.SendList();
            List<ProgramVM> personlist = new List<ProgramVM>();
            var dateAndTime = DateTime.Now;
            var datee = dateAndTime.Date;

            ViewBag.UserName = uname;
            ViewBag.PassWord = pword;

            foreach (var l in plist)
            {
                foreach (var p in proglist)
                {
                    string x = GetChannel(p.ChannelId.Value);
                    if (l.Channel == GetChannel(p.ChannelId.Value) && l.Username == uname)
                    {
                        ProgramVM o = new ProgramVM();
                        if (p.Date == datee)
                        {
                            o.Id = p.Id;
                            o.ProgramName = p.ProgramName;
                            o.Time = p.Time;
                            o.ChannelId = p.ChannelId.Value;
                            o.Date = p.Date;
                            o.Length = p.Length;
                            o.Info = p.Info;
                            personlist.Add(o);
                        }
                    }
                }
            }
            return RedirectToAction("PrivateList", new { uname, pword });
        }

        public int GetCid(string uname, string chn)
        {
            int id = 0;
            PersonalListsController pl = new PersonalListsController();
            var plist = pl.SendList();

            foreach (var item in plist)
            {
                if (item.Username == uname && item.Channel == chn)
                {
                    id = item.Id;
                }
            }
            return id;
        }

        public List<string> ChannelList()
        {
            List<string> allist = new List<string>();
            allist.Add("Svt");
            allist.Add("Tv3");
            allist.Add("Tv4");
            allist.Add("Kanal 5");
            allist.Add("Tv6");
            return allist;
        }

        public List<ProgramVM> GetPuff()
        {
            List<ProgramVM> pufflist = new List<ProgramVM>();
            ProgramsController pc = new ProgramsController();
            var proglist = pc.SendList();
            PuffsController uff = new PuffsController();
            var news = uff.SendList();

            foreach (var n in news)
            {
                foreach (var p in proglist)
                {
                    if (n.Progid == p.Id)
                    {
                        ProgramVM o = new ProgramVM();
                        o.Id = p.Id;
                        o.ProgramName = p.ProgramName;
                        o.Time = p.Time;
                        o.ChannelId = p.ChannelId.Value;
                        o.Date = p.Date;
                        o.Length = p.Length;
                        o.Info = p.Info;
                        pufflist.Add(o);
                    }
                }
            }
            return pufflist;
        }

        //public void ChangePuff(int puffid, int progid)
        //{
        //    DataBaseTvEntities db = new DataBaseTvEntities();
        //    if (ModelState.IsValid)
        //    {
        //        var del = (from c in db.Puff
        //                   where c.Id == cid
        //                   select c).FirstOrDefault();
        //        db.Puff.;
        //        db.SaveChanges();
        //    }

        //}

        //public ActionResult EditFavorite()
        //{
        //    return View();
        //}
    }
}