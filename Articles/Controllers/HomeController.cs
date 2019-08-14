using Articles.Interfaces;
using Articles.Items;
using Articles.Models;
using NHibernate;
using Ninject;
using System.Linq;
using System.Threading.Tasks;
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

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var model = await this.GetView();
            return this.View(model);
         }


        [HttpGet]
        public async Task<ActionResult> Clauses(string ClausesId)
        {
            var  Id = int.Parse(ClausesId);
            var model = await this.GetView(Id);
            return PartialView("Clauses", model);
        }


        private async Task<Clauses> GetView(int ClausesId = 0)
        {
            UpdateMenu();
            return FindClauses(ClausesId); 
        }

        private void UpdateMenu()
        {
            HtmlGenerator generator = new HtmlGenerator(parent);
            ViewBag.Meny = new HtmlString(generator.GenerateMeny());
        }

        private Clauses FindClauses(int id)
        {
            return session.Query<Clauses>().Where(c => c.Id == id).FirstOrDefault();
        }



    }
}