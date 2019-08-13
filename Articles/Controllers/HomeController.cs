using Articles.Abstractions;
using Articles.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Articles.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //var session = NHibernateHelper.OpenSession();
            //session.Save(new Cataloges()
            //{
            //    Name = "root1"
            //});


            //HtmlGenerator generator = new HtmlGenerator()
           // ViewBag.Meny

            return View();
        }

 
    }
}