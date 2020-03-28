﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public ActionResult AddManager()
        {
            return View();
        }

        public async Task<ActionResult> Managers()
        {
            List<ManagerModel> list = new List<ManagerModel>();
            
            try
            {
                //var response = await Startup.client.GetStringAsync("https://unihackapi.azurewebsites.net/api/Manager/GetManagers");
                //list = new JavaScriptSerializer().Deserialize<List<ManagerModel>>(response);
                list = new List<ManagerModel>()
                {
                    new ManagerModel()
                    {
                        Id = "00ee0c85-e109-4b65-8a96-472eb04523e4",
                        Email = "mail@s.com",
                        ZoneName = "complex",
                        Name = "Gigi"
                    },new ManagerModel()
                    {
                        Id = "00ee0c85-e109-4b65-8a91-472eb04523e4",
                        Email = "mail1@s.com",
                        ZoneName = "complex",
                        Name = "Carla"
                    },new ManagerModel()
                    {
                        Id = "00ee0c85-e109-4b65-8a90-472eb04523e4",
                        Email = "mail2@s.com",
                        ZoneName = "sagului",
                        Name = "Mihai"
                    },
                };

            }
            catch (Exception ex)
            {
                var x = 1;
            }

            return View(list);
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
            //try
            //{
            //    var response = await Startup.client.GetStringAsync("https://unihackapi.azurewebsites.net/api/Bins/GetBins");
            //    issues = new JavaScriptSerializer().Deserialize<List<BinModel>>(response);

            //}
            //catch (Exception ex)
            //{
            //    var x = 1;
            //}

            //return Json(issues, JsonRequestBehavior.AllowGet);

            return Json(new[] {
                new BinModel(){
                    Name = "Piata x",
                    Capacity = 33.4f,
                    ManagerName = "Gigi",
                    Id =Guid.NewGuid(),
                    Latitude=45.02f,
                    Longitude=21f,
                    Type = 1
                } ,
                new BinModel(){
                    Name = "Piata x",
                    Capacity = 33.4f,
                    ManagerName = "Gigi",
                    Id =Guid.NewGuid(),
                    Latitude=45.1234f,
                    Longitude=21.098f,
                    Type = 1
                } ,
                new BinModel(){
                    Name = "Piata x",
                    Capacity = 33.4f,
                    ManagerName = "Gigi",
                    Id =Guid.NewGuid(),
                    Latitude=45.5012f,
                    Longitude=21.4444f,
                    Type = 1
                } 
            },JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public async System.Threading.Tasks.Task<PartialViewResult> GetBins()
        {
            List<BinModel> issues = new List<BinModel>() {
                new BinModel(){
                    Name = "Piata x",
                    Capacity = 33.4f,
                    ManagerName = "Gigi",
                    Id =Guid.NewGuid(),
                    Latitude=45.02f,
                    Longitude=21f,
                    Type = 1
                } ,
                new BinModel(){
                    Name = "Piata x",
                    Capacity = 33.4f,
                    ManagerName = "Gigi",
                    Id =Guid.NewGuid(),
                    Latitude=45.1234f,
                    Longitude=21.098f,
                    Type = 2
                } ,
                new BinModel(){
                    Name = "Piata x",
                    Capacity = 33.4f,
                    ManagerName = "Gigi",
                    Id =Guid.NewGuid(),
                    Latitude=45.5012f,
                    Longitude=21.4444f,
                    Type = 2
                }
                ,
                new BinModel(){
                    Name = "Piata x",
                    Capacity = 33.4f,
                    ManagerName = "Gigi",
                    Id =Guid.NewGuid(),
                    Latitude=45.5012f,
                    Longitude=21.4444f,
                    Type = 3
                },
                new BinModel(){
                    Name = "Piata x",
                    Capacity = 33.4f,
                    ManagerName = "Gigi",
                    Id =Guid.NewGuid(),
                    Latitude=45.5012f,
                    Longitude=21.4444f,
                    Type = 1
                },
                new BinModel(){
                    Name = "Piata x",
                    Capacity = 33.4f,
                    ManagerName = "Gigi",
                    Id =Guid.NewGuid(),
                    Latitude=45.5012f,
                    Longitude=21.4444f,
                    Type = 3
                },
                new BinModel(){
                    Name = "Piata x",
                    Capacity = 33.4f,
                    ManagerName = "Gigi",
                    Id =Guid.NewGuid(),
                    Latitude=45.5012f,
                    Longitude=21.4444f,
                    Type = 2
                },
                new BinModel(){
                    Name = "Piata x",
                    Capacity = 33.4f,
                    ManagerName = "Gigi",
                    Id =Guid.NewGuid(),
                    Latitude=45.5012f,
                    Longitude=21.4444f,
                    Type = 1
                }
            };
            //try
            //{
            //    var response = await Startup.client.GetStringAsync("https://unihackapi.azurewebsites.net/api/Bins/GetBins");
            //    issues = new JavaScriptSerializer().Deserialize<List<BinModel>>(response);

            //}
            //catch (Exception ex)
            //{
            //    var x = 1;
            //}

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