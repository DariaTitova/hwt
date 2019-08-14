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
            session = NHibernateHelper.OpenSession();
            roots = new List<IParentItem>();
            IKernel ninjectKernel = new StandardKernel();
            foreach(var cataloge in session.Query<Cataloges>().Where(c => c.Parent == null).ToList())
            {
                ninjectKernel.Bind<IParentItem>().To<CatalogesItems>()
                   .WithConstructorArgument("cataloge", cataloge);
                roots.Add(ninjectKernel.Get<IParentItem>());
            }

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
            HtmlGeneratorMeny generator = new HtmlGeneratorMeny(roots);
            ViewBag.Meny = new HtmlString(generator.GenerateMeny());
        }

        private Clauses FindClauses(int id)
        {
            return session.Query<Clauses>().Where(c => c.Id == id).FirstOrDefault();
        }



    }
}