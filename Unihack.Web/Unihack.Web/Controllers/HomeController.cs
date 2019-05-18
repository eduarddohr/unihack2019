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
                var response = await Startup.client.GetStringAsync("https://unihackapi.azurewebsites.net/api/Manager/GetManagers");
                list = new JavaScriptSerializer().Deserialize<List<ManagerModel>>(response);

            }
            catch (Exception ex)
            {
                var x = 1;
            }

            return View(list);
        }

        public async Task<ActionResult> Collectors()
        {
            List<CollectorViewModel> list = new List<CollectorViewModel>();

            try
            {
                var userId = Session["userId"].ToString();
                var response = await Startup.client.GetStringAsync("https://unihackapi.azurewebsites.net/api/Collector/GetCollectorsByManager"+"?Id="+userId);
                list = new JavaScriptSerializer().Deserialize<List<CollectorViewModel>>(response);

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
            List<BinModel> bins = new List<BinModel>();
            List<BinModel> binsFiltered = new List<BinModel>();
            try
            {
                var response = await Startup.client.GetStringAsync("https://unihackapi.azurewebsites.net/api/Bins/GetBins");
                bins = new JavaScriptSerializer().Deserialize<List<BinModel>>(response);

            }
            catch (Exception ex)
            {
                var x = 1;
            }
            if (Session["userRole"].ToString() == "manager")
            {
                bins.ForEach(bin =>
                {
                    if (bin.Zone == (int)Session["userZone"])
                        binsFiltered.Add(bin);
                });
            }
            binsFiltered = bins;
            return Json(bins, JsonRequestBehavior.AllowGet);

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
            List<BinModel> bins = new List<BinModel>();
            List<BinModel> binsFiltered = new List<BinModel>();
            try
            {
                var response = await Startup.client.GetStringAsync("https://unihackapi.azurewebsites.net/api/Bins/GetBins");
                bins = new JavaScriptSerializer().Deserialize<List<BinModel>>(response);

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
            if (Session["userRole"].ToString() == "MANAGER")
            {
                bins.ForEach(bin =>
                {
                    if (bin.Zone == (int)Session["userZone"])
                        binsFiltered.Add(bin);
                });
            }
            else binsFiltered = bins;
            return PartialView("~/Views/Home/_BinsList.cshtml", binsFiltered);
        }
    }
}