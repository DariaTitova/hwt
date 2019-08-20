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
        static public List<IParentItem> roots = new List<IParentItem>();
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
        public ActionResult Index()
        {
            UpdateAddItems();
              return View();
        }


        public static List<IParentItem> GetAllParents()
        {
            var list = new List<IParentItem>();
            foreach (var parent in roots)
            {
                list.Add(parent);
                list = list.Concat(GetAllChildren(parent)).ToList();
            }
            return list;
        }

        public static List<IParentItem> GetAllChildren(IParentItem parent)
        {
            var list = new List<IParentItem>();

            foreach (var child in parent.ToList())
            {
                if (child is IParentItem)
                {
                    list.Add((IParentItem)child);
                    list = list.Concat(GetAllChildren((IParentItem)child)).ToList();
                }
            }

            return list;
        }


        [System.Web.Services.WebMethod]

        private void UpdateAddItems()
        {
            //Сдаюсь. я не придумала как это сделать с помощью наследования
            Dictionary<string, string> list = new Dictionary<string, string>()
            {
                {CatalogesItems.Name(),CatalogesItems.AddView() },
                {ClausesItems.Name(),ClausesItems.AddView() }

            };

            ViewBag.Createble = list;


        }



      
        [System.Web.Services.WebMethod]
        public ActionResult MenyPartial()
        {
            UpdateAddItems();

            roots = new List<IParentItem>();
            foreach (var cataloge in NHibernateHelper.OpenSession().Query<Cataloges>().Where(c => c.Parent == null).ToList())
            {
                roots.Add(new CatalogesItems(cataloge));
            }

            ViewBag.Meny = new HtmlString(new HtmlGeneratorMeny(roots).GenerateMeny(Request.IsAuthenticated));


             return PartialView("MenyPartial");
        }

    }
}