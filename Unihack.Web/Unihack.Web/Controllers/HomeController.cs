using System;
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
                        Email = "dohr.eduard@zahoo.com",
                        ZoneName = "Dacia",
                        Name = "Dohr Eduard"
                    },new ManagerModel()
                    {
                        Id = "00ee0c85-e109-4b65-8a91-472eb04523e4",
                        Email = "david.mihai@zahoo.com",
                        ZoneName = "Sagului",
                        Name = "David Mihai"
                    },new ManagerModel()
                    {
                        Id = "00ee0c85-e109-4b65-8a90-472eb04523e4",
                        Email = "morar.carla@yahoo.com",
                        ZoneName = "Complex",
                        Name = "Morar Ana Carla"
                    },
                    new ManagerModel()
                    {
                        Id = "00ee0c85-ea09-4b65-8a90-472eb04523e4",
                        Email = "motiu.marcela@yahoo.com",
                        ZoneName = "Complex",
                        Name = "Motiu Marcela Daiana"
                    }
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
            //List<BinModel> issues = new List<BinModel>();
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
            List<BinModel> issues = new List<BinModel>() {
                new BinModel(){
                    Name = "Circumvalatiunii",
                    Capacity = 33.4f,
                    ManagerName = "Tudor Gabriel",
                    Id =Guid.NewGuid(),
                    Latitude=45.759f,
                    Longitude=21.213f,
                    Type = 1
                } ,
                new BinModel(){
                    Name = "Dacia",
                    Capacity = 73.4f,
                    ManagerName = "Catalin Ionescu",
                    Id =Guid.NewGuid(),
                    Latitude=45.765f,
                    Longitude=21.219f,
                    Type = 2
                } ,
                new BinModel(){
                    Name = "Iulius Towm",
                    Capacity = 33.4f,
                    ManagerName = "Dorel Popescu",
                    Id =Guid.NewGuid(),
                    Latitude=45.765f,
                    Longitude=21.227f,
                    Type = 2
                },
                new BinModel(){
                    Name = "Bastion",
                    Capacity = 90.4f,
                    ManagerName = "George Marin",
                    Id =Guid.NewGuid(),
                    Latitude=45.756f,
                    Longitude=21.234f,
                    Type = 1
                },
                new BinModel(){
                    Name = "Aleea Studentilor",
                    Capacity = 5.4f,
                    ManagerName = "Ion Ionel",
                    Id =Guid.NewGuid(),
                    Latitude=45.7471f,
                    Longitude=21.237f,
                    Type = 3
                },
                new BinModel(){
                    Name = "Olimpia Stadion",
                    Capacity = 33.4f,
                    ManagerName = "Octavian Dudas",
                    Id =Guid.NewGuid(),
                    Latitude=45.741f,
                    Longitude=21.243f,
                    Type = 2
                },
                new BinModel(){
                    Name = "Soarelui",
                    Capacity = 53.4f,
                    ManagerName = "Gigi Pop",
                    Id =Guid.NewGuid(),
                    Latitude=45.7345f,
                    Longitude=21.244f,
                    Type = 1
                }
            };

            return Json(issues, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public async System.Threading.Tasks.Task<PartialViewResult> GetBins()
        {
            List<BinModel> issues = new List<BinModel>() {
                new BinModel(){
                    Name = "Circumvalatiunii",
                    Capacity = 33.4f,
                    ManagerName = "Tudor Gabriel",
                    Id =Guid.NewGuid(),
                    Latitude=45.759f,
                    Longitude=21.213f,
                    Type = 1
                } ,
                new BinModel(){
                    Name = "Dacia",
                    Capacity = 73.4f,
                    ManagerName = "Catalin Ionescu",
                    Id =Guid.NewGuid(),
                    Latitude=45.765f,
                    Longitude=21.219f,
                    Type = 2
                } ,
                new BinModel(){
                    Name = "Iulius Towm",
                    Capacity = 33.4f,
                    ManagerName = "Dorel Popescu",
                    Id =Guid.NewGuid(),
                    Latitude=45.765f,
                    Longitude=21.227f,
                    Type = 2
                },
                new BinModel(){
                    Name = "Bastion",
                    Capacity = 90.4f,
                    ManagerName = "George Marin",
                    Id =Guid.NewGuid(),
                    Latitude=45.756f,
                    Longitude=21.234f,
                    Type = 1
                },
                new BinModel(){
                    Name = "Aleea Studentilor",
                    Capacity = 5.4f,
                    ManagerName = "Ion Ionel",
                    Id =Guid.NewGuid(),
                    Latitude=45.7471f,
                    Longitude=21.237f,
                    Type = 3
                },
                new BinModel(){
                    Name = "Olimpia Stadion",
                    Capacity = 33.4f,
                    ManagerName = "Octavian Dudas",
                    Id =Guid.NewGuid(),
                    Latitude=45.741f,
                    Longitude=21.243f,
                    Type = 2
                },
                new BinModel(){
                    Name = "Soarelui",
                    Capacity = 53.4f,
                    ManagerName = "Gigi Pop",
                    Id =Guid.NewGuid(),
                    Latitude=45.7345f,
                    Longitude=21.244f,
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