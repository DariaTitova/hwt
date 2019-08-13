using Articles.Abstractions;
using Articles.interfaces;
using Articles.Interfaces;
using Articles.Items;
using Articles.Models;
using Articles.Scripts;
using NHibernate;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
 using System.Web;
using System.Web.Mvc;

namespace Articles.Controllers
{
    public class HomeController : Controller
    {
        IParentItem parent;
        ISession session;
        public HomeController()
        {
           session = NHibernateHelper.OpenSession();

            IKernel ninjectKernel = new StandardKernel();
            ninjectKernel.Bind<IParentItem>().To<CatalogesItems>()
                .WithConstructorArgument("cataloge", session.Query<Cataloges>().ToList().First()); 
            parent = ninjectKernel.Get<IParentItem>();
        }
        public ActionResult Index()
        {
            session = NHibernateHelper.OpenSession();
            //session.Save(new Cataloges()
            //{
            //    Name = "root1"
            //});

  
            HtmlGenerator generator = new HtmlGenerator(parent);

            ViewBag.Meny = new HtmlString(generator.GenerateMeny());


            Clauses clause = session.Query<Clauses>().ToList().First();
            IShownItem item = new ClausesItems(clause);
            HtmlGeneratorArticles generator2 = new HtmlGeneratorArticles(item);

            ViewBag.Clauses = new HtmlString(generator2.GenerateArticle());

            return View();
        }

 
    }
}