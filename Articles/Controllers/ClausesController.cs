using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Articles.Models;
using NHibernate;

namespace Articles.Controllers
{
    public class ClausesController : Controller
    {
         ISession session;

        public ClausesController()
        {
            session = NHibernateHelper.OpenSession();

        }

        [HttpGet]
        public async Task<ActionResult> ClausesShow(string clausesId)
        {
            var Id = int.Parse(clausesId);
            var model = await this.GetView(Id);
            return PartialView("ClausesShow", model);
        }


        private async Task<Clauses> GetView(int ClausesId = 0)
        {
            return  session.Query<Clauses>().Where(c => c.Id == ClausesId).FirstOrDefault();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Text")] Clauses clauses)
        {
            if (ModelState.IsValid)
            {
                session.Save(clauses);
            }

            return View(clauses);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clauses clauses = session.Query<Clauses>().Where(c => c.Id == id).FirstOrDefault();
            if (clauses == null)
            {
                return HttpNotFound();
            }
            return View(clauses);
        }

        [HttpPost]
        public ActionResult EditConfirm([Bind(Include = "Id,Name,Text")] Clauses clauses, int IdParent)
        {
            if (ModelState.IsValid)
            {
                clauses.Cataloges = session.Query<Cataloges>().Where(c => c.Id == IdParent).FirstOrDefault();
                session.Update(clauses, clauses.Id);
                session.Flush();
            }

            return PartialView("ClausesShow", clauses);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                session.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
