using Articles.Interfaces;
using Articles.Items;
using Articles.Models;
using NHibernate;
using Ninject;
using System.Collections.Generic;
using System.Linq;
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
            UpdateMenu();
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var model = await this.GetView();
            return this.View(model);
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


        [HttpGet]
        public async Task<ActionResult> Clauses(string ClausesId)
        {
            var  Id = int.Parse(ClausesId);
            var model = await this.GetView(Id);
            return PartialView("Clauses", model);
        }

        [HttpGet]
        public async Task<ActionResult> CreateClauses(string ClausesId)
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