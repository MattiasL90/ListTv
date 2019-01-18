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
    }
}