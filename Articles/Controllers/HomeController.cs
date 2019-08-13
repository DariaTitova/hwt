﻿using Articles.Abstractions;
using Articles.interfaces;
using Articles.Items;
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
            var session = NHibernateHelper.OpenSession();
            //session.Save(new Cataloges()
            //{
            //    Name = "root1"
            //});
            Cataloges cataloge = session.Query<Cataloges>().ToList().First();
            IParentItem parent = new CatalogesItems(cataloge);
            HtmlGenerator generator = new HtmlGenerator(parent);
            ViewBag.Meny = new HtmlString(generator.GenerateMeny());

            return View();
        }

 
    }
}