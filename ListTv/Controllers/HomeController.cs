using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ListTv.Models;
using ListTv.ViewModels;


namespace ListTv.Controllers
{
    public class HomeController : Controller
    {
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Index()
        {
            string date = "2019-01-21";
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
                    o.ChannelId = p.ChannelId.Value;
                    o.Date = p.Date;
                    o.Length = p.Length;
                }
                progtables.Add(o);
            }
            return View(progtables);
        }
    }
}