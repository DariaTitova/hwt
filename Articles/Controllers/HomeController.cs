using Articles.Interfaces;
using Articles.Items;
using Articles.Models;
using NHibernate;
using System.Collections.Generic;
using System.Linq;
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
        public  ActionResult Index()
        {

            roots = new List<IParentItem>();

            foreach (var cataloge in session.Query<Cataloges>().Where(c => c.Parent == null).ToList())
            {
                roots.Add(new CatalogesItems(cataloge));
            }

            UpdateMenu();
            FindAllEditable();
            return View();
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
            ViewBag.Meny = new HtmlString(new HtmlGeneratorMeny(roots).GenerateMeny());
        }
    }
}