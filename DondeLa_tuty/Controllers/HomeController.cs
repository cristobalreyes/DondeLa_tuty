using DondeLa_tuty.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace IdentitySample.Controllers
{
    public class HomeController : Controller
    {
        ShoppingStoreEntities storeDB = new ShoppingStoreEntities();

        //selecciona los items mas elegidos
        private List<Item> GetTopSellingItems(int count)
        {
            return storeDB.Items.OrderByDescending(i => i.OrderDetails.Count())
                .Take(count)
                .ToList();
        }

        [HttpGet]
        public ActionResult Index()
        {
            //productos mas vendidos
            var items = GetTopSellingItems(10);

            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
