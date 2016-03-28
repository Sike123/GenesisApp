using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GenApp.Repository;
using GenApp.Web.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions.Impl;

namespace GenApp.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            return View();
            
        }


     

    }
}
