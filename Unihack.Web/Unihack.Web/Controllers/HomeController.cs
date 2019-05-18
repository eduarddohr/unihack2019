using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Unihack.Web.Models;

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
        public PartialViewResult GetBins()
        {
            List<BinViewModel> binsList = new List<BinViewModel>
            {
                new BinViewModel()
                {
                     Name ="E1",
                     Picture="/Content/Images/bin.jpg",
                     Capacity=50f,
                     Manager="Andrei"
                    
                },
                new BinViewModel()
                {
                     Name ="E2",
                     Picture="/Content/Images/bin.jpg",
                     Capacity=30f,
                     Manager="Andrei"

                },
                new BinViewModel()
                {
                     Name ="E3",
                     Picture="/Content/Images/bin.jpg",
                     Capacity=70f,
                     Manager="Andrei"

                },

                 new BinViewModel()
                {
                     Name ="E4",
                     Picture="/Content/Images/bin.jpg",
                     Capacity=20f,
                     Manager="Andrei"

                },

                  new BinViewModel()
                {
                     Name ="E5",
                     Picture="/Content/Images/bin.jpg",
                     Capacity=26f,
                     Manager="Andrei"

                }
            };
            return PartialView("~/Views/Home/_BinsList.cshtml", binsList);
        }
    }
}