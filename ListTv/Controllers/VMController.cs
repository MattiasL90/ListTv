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

        //public ActionResult Index()
        //{
        //    DataBaseTvEntities dbtv = new DataBaseTvEntities();
        //    List<ProgramVM> ProgramVMList = new List<ProgramVM>();
        //    var proglist = (from cnl in dbtv.Channel
        //                    join prg in dbtv.Program on cnl.Id equals prg.ChannelId
        //                    select new { cnl.ChannelName, prg.ProgramName, prg.Time, prg.Date, prg.Length }).ToList();

        //    foreach (var m in proglist)
        //    {
        //        ProgramVM pm = new ProgramVM();
        //        pm.ChannelName = m.ChannelName;
        //        pm.ProgramName = m.ProgramName;
        //        pm.Time = m.Time;
        //        pm.Date = m.Date;
        //        pm.Length = m.Length;
        //        ProgramVMList.Add(pm);
        //    }
        //    return View(ProgramVMList);
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
            return View(progtables);
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

        public ActionResult Login(string uname, string pword)
        {
            LoginsController lc = new LoginsController();
            var logins = lc.SendList();
            foreach (var l in logins)
            {
                if (l.Username == uname && l.Password == pword)
                {

                    return View();
                }
            }


            return View();
        }


    }
}