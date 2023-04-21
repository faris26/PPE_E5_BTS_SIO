using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrontCours.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous] //access ouvert a tous
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous] // access limité
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [Authorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}