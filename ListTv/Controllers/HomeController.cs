﻿using System;
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

         public object Datumet { get; }
        
        public object PuffList { get; }
       
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

        public ActionResult Genre(string cat, string datet)
        {
            return RedirectToAction("Genre", "VM", new { cat, datet });
        }

        public ActionResult Index()
        {
            var dateAndTime = DateTime.Now;
            var datee = dateAndTime.Date;
            ProgramsController pc = new ProgramsController();
            VMController vm = new VMController();
            List<ProgramVM> progtables = new List<ProgramVM>();
            var program = pc.SendList();
            VMController vmc = new VMController();
            ViewBag.PuffList = vmc.GetPuff();
            foreach (var p in program)
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
                }
                progtables.Add(o);
            }
            return View(vm.SortList(progtables));
        }

        //public ActionResult ShowMore(int id)
        //{
        //    List<Program> LstMore = new List<Program>();
        //    ProgramsController pc = new ProgramsController();
        //    var programs = pc.SendList();
        //    foreach (var s in programs)
        //    {
        //        if (s.Id == id)
        //        {
        //            LstMore.Add(new Program { Id = s.Id, ProgramName = s.ProgramName, Time = s.Time, Category = s.Category, ChannelId = s.ChannelId, Length = s.Length, Date = s.Date });
        //        }
        //    }
        //    return View(LstMore);
        //}

    }
}