﻿using System.Web.Mvc;
using RR.ViewModels;

namespace RR.Web.Controllers
{
    public class HomeController : Controller
    {
        [Route("")]
        [Route("Home/Index")]
        public ActionResult Index()
        {
            return View("Index");
        }

        [Route("Home/About")]
        public ActionResult About()
        {
            return View("About");
        }

        [Route("Home/Contact")]
        public ActionResult Contact()
        {
            return View("Contact");
        }

        [Route("Home/Contact")]
        [HttpPost]
        public ActionResult Contact(CreateContactViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            return View("Contact", viewModel);
        }
    }
}