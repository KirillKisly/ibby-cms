using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ibby_cms.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Страница описания вашего приложения.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Ваша контактная страница.";

            return View();
        }
    }
}