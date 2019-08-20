using Articles.Interfaces;
using Articles.Items;
using Articles.Models;
using NHibernate;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Articles.Controllers
{
    public class HomeController : Controller
    {
        List<IParentItem> roots;
        ISession session;
        public HomeController()
        {
            //IKernel ninjectKernel = new StandardKernel();

            ////ninjectKernel.Bind<IParentItem>().To<CatalogesItems>()
            ////      // .WithConstructorArgument("cataloge", session.Query<Cataloges>().Where(c => c.Parent == null).ToList());
            ////       .WithConstructorArgument("cataloge", new Cataloges());

            //ninjectKernel.Bind<List<IParentItem>>().To<List<CatalogesItems>>()
            //    .WithConstructorArgument("cataloge", session.Query<Cataloges>());

            ////Kernel.Bind<IPersistenceStrategy<User>>().To<DynamoDBStrategy<User>>();

            session = NHibernateHelper.OpenSession();
        }


        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var model = await this.GetView();
            UpdateMenu();
            FindAllEditable();
            return this.View(model);
        }


        private void FindAllEditable()
        {
            //Сдаюсь. я не придумала как это сделать с помощью наследования
            var list = new Dictionary<string, string>()
            {
                {CatalogesItems.Name(),CatalogesItems.AddView() },
                {ClausesItems.Name(),ClausesItems.AddView() }
            };     
         
            if (list.Count > 0)
            {
                ViewBag.Createble = list;
            }
        }


        [System.Web.Services.WebMethod]
        private void UpdateMenu()
        {
            roots = new List<IParentItem>();

            foreach (var cataloge in session.Query<Cataloges>().Where(c => c.Parent == null).ToList())
            {
                roots.Add(new CatalogesItems(cataloge));
            }

            HtmlGeneratorMeny generator = new HtmlGeneratorMeny(roots);
            ViewBag.Meny = new HtmlString(generator.GenerateMeny());
        }


        public ActionResult MenyPartial()
        {
            UpdateMenu();
            return PartialView("MenyPartial");
        }


        [HttpGet]
        public async Task<ActionResult> Clauses(string ClausesId)
        {
            var Id = int.Parse(ClausesId);
            var model = await this.GetView(Id);
            return PartialView("Clauses", model);
        }


        private async Task<Clauses> GetView(int ClausesId = 0)
        {
            return session.Query<Clauses>().Where(c => c.Id == ClausesId).FirstOrDefault();
        }
    }
}