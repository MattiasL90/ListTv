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

        // GET: VM
        public ActionResult Index()
        {
            DataBaseTvEntities dbtv = new DataBaseTvEntities();
            List<ProgramVM> ProgramVMList = new List<ProgramVM>();
            var proglist = (from cnl in dbtv.Channel
                            join prg in dbtv.Program on cnl.Id equals prg.ChannelId
                            select new { cnl.ChannelName, prg.ProgramName, prg.Time, prg.Date, prg.Length }).ToList();

            foreach (var m in proglist)
            {
                ProgramVM pm = new ProgramVM();
                pm.ChannelName = m.ChannelName;
                pm.ProgramName = m.ProgramName;
                pm.Time = m.Time;
                pm.Date = m.Date;
                pm.Length = m.Length;
                ProgramVMList.Add(pm);
            }
            return View(ProgramVMList);
        }

        public ActionResult Days(DateTime date)
        {
            ProgramsController pc = new ProgramsController();
            List<ProgTable> progtables = new List<ProgTable>();
            var program = pc.SendList();

            foreach (var p in program)
            {
                ProgTable o = new ProgTable();
                if (o.Date == date)
                {
                    o.ProgramName = p.ProgramName;
                    o.Time = p.Time;
                }
                progtables.Add(o);
            }
            return View(progtables);
        }
    }
}