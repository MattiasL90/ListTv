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

        public ActionResult PrivateList(DateTime date, string uname, string pword)
        {
            ProgramsController pc = new ProgramsController();
            var proglist = pc.SendList();
            PersonalListsController pl = new PersonalListsController();
            var plist = pl.SendList();
            List<ProgramVM> personlist = new List<ProgramVM>();


            if (Login(uname, pword) == 1)
            {
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
                            if (p.Date == date)
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

        //public ActionResult PrivateList(DateTime date, string uname, string pword)
        //{
        //    ProgramsController pc = new ProgramsController();
        //    var proglist = pc.SendList();
        //    PersonalListsController pl = new PersonalListsController();
        //    var plist = pl.SendList();
        //    List<ProgramVM> personlist = new List<ProgramVM>();

        //    if (Login(uname, pword) == 1)
        //    {
        //        return View("Admin");
        //    }
        //    else if (Login(uname, pword) == 2)
        //    {
        //        foreach (var p in proglist)
        //        {
        //            foreach (var l in plist)
        //            {
        //                string x = GetChannel(p.ChannelId.Value);
        //                if (l.Channel == GetChannel(p.ChannelId.Value) && l.Username == uname)
        //                {
        //                    ProgramVM o = new ProgramVM();
        //                    if (p.Date == date)
        //                    {
        //                        o.Id = p.Id;
        //                        o.ProgramName = p.ProgramName;
        //                        o.Time = p.Time;
        //                        o.ChannelId = p.ChannelId.Value;
        //                        o.Date = p.Date;
        //                        o.Length = p.Length;
        //                        o.Info = p.Info;
        //                    }
        //                    personlist.Add(o);
        //                }
        //            }
        //        }
        //        return View(personlist);
        //    }
        //    else
        //    {
        //        return View("Fail");
        //    }

        //}

        public ActionResult Days(string date)
        {
            //DataBaseTvEntities dbtv = new DataBaseTvEntities(); 
            DateTime datum = Convert.ToDateTime(date);
            ProgramsController pc = new ProgramsController();
            List<ProgramVM> progtables = new List<ProgramVM>();
            var program = pc.SendList();
            //var program = (from cnl in dbtv.Channel
            //                join prg in dbtv.Program on cnl.Id equals prg.ChannelId
            //                select new { cnl.ChannelName, prg.ProgramName, prg.Time, prg.Date, prg.Length }).ToList();

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

        public ActionResult AddFavorite(string uname)
        {
            PersonalListsController plc = new PersonalListsController();
            var plist = plc.SendList();
            List<ProgTable> allist = ChannelList();
            List<ProgTable> ownlist = new List<ProgTable>();
            foreach (var ol in plist)
            {
                if (ol.Username == uname)
                {
                    ProgTable p = new ProgTable()
                    {
                        Cname = ol.Channel
                    };
                    ownlist.Add(p);
                }
            }

            foreach (var al in allist)
            {
                foreach (var ol in ownlist)
                {
                    if (al.Cname == ol.Cname)
                    {
                        allist.Remove(al);
                    }
                }
            }
            return View(allist);
        }

        public List<ProgTable> ChannelList()
        {
            List<ProgTable> allist = new List<ProgTable>
            {
                new ProgTable{Cname = "Svt"},
                new ProgTable{Cname = "Tv3"},
                new ProgTable{Cname = "Tv4"},
                new ProgTable{Cname = "Kanal 5"},
                new ProgTable{Cname = "Tv6"}
            };
            return allist;
        }

        public ActionResult EditFavorite()
        {
            return View();
        }


        //public ActionResult Login(string uname, string pword)
        //{
        //    LoginsController lc = new LoginsController();
        //    PersonalListsController pc = new PersonalListsController();
        //    ProgramsController progc = new ProgramsController();
        //    List<ProgramVM> personallist = new List<ProgramVM>();
        //    var progs = progc.SendList();
        //    var plist = pc.SendList();

        //    var logins = lc.SendList();
        //    foreach (var l in logins)
        //    {
        //        if (l.Username == uname && l.Password == pword)
        //        {
        //            foreach (var p in plist)
        //            {
        //                foreach (var y in progs)
        //                {
        //                    if (p.Channel == y.ChannelName)
        //                    {

        //                    }
        //                }
        //            }     
        //        }
        //    }
        //    return View();
        //}
    }
}