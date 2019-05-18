﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Unihack.Web.Models;
using unihackAPI.Models;

namespace Unihack.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

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

        [HttpGet]
        public async System.Threading.Tasks.Task<JsonResult> GetBinsAsync()
        {
            List<BinModel> issues = new List<BinModel>();
            try
            {
                var response = await Startup.client.GetStringAsync("https://unihackapi.azurewebsites.net/api/Bins/GetBins");
                issues = new JavaScriptSerializer().Deserialize<List<BinModel>>(response);

            }
            catch (Exception ex)
            {
                var x = 1;
            }

            return Json(issues, JsonRequestBehavior.AllowGet);

            //return Json(new [] {
            //    new IssueModel(){
            //        Id =Guid.NewGuid(),
            //        Latitude=45.02f,
            //        Longitude=21f
            //    } ,
            //    new IssueModel(){
            //        Id =Guid.NewGuid(),
            //        Latitude=45.1234f,
            //        Longitude=21.098f
            //    } ,
            //    new IssueModel(){
            //        Id =Guid.NewGuid(),
            //        Latitude=45.5012f,
            //        Longitude=21.4444f
            //    } 
            //},JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public async System.Threading.Tasks.Task<PartialViewResult> GetBins()
        {
            List<BinModel> issues = new List<BinModel>();
            try
            {
                var response = await Startup.client.GetStringAsync("https://unihackapi.azurewebsites.net/api/Bins/GetBins");
                issues = new JavaScriptSerializer().Deserialize<List<BinModel>>(response);

            }
            catch (Exception ex)
            {
                var x = 1;
            }

            //List<BinViewModel> binsList = new List<BinViewModel>
            //{
            //    new BinViewModel()
            //    {
            //         Name ="E1",
            //         Picture="/Content/Images/bin.jpg",
            //         Capacity=50f,
            //         Manager="Andrei"
                    
            //    },
            //    new BinViewModel()
            //    {
            //         Name ="E2",
            //         Picture="/Content/Images/bin.jpg",
            //         Capacity=30f,
            //         Manager="Andrei"

            //    },
            //    new BinViewModel()
            //    {
            //         Name ="E3",
            //         Picture="/Content/Images/bin.jpg",
            //         Capacity=70f,
            //         Manager="Andrei"

            //    },

            //     new BinViewModel()
            //    {
            //         Name ="E4",
            //         Picture="/Content/Images/bin.jpg",
            //         Capacity=20f,
            //         Manager="Andrei"

            //    },

            //      new BinViewModel()
            //    {
            //         Name ="E5",
            //         Picture="/Content/Images/bin.jpg",
            //         Capacity=26f,
            //         Manager="Andrei"

            //    }
            //};
            return PartialView("~/Views/Home/_BinsList.cshtml", issues);
        }
    }
}