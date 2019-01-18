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
                    o.ProgramName = p.ProgramName;
                    o.Time = p.Time;
                    o.ChannelId = p.ChannelId;
                    o.Date = p.Date;
                    o.Length = p.Length;
                }
                progtables.Add(o);
            }
            return View(progtables);
        }

        public ActionResult News(int id)
        {




            return View();
        }
    }
}