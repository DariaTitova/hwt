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
            return session.Query<Clauses>().Where(c => c.Id == ClausesId).FirstOrDefault();
        }

        public ActionResult Create()
        {
            var parents = HomeController.GetAllParents()
              .Select(p =>
              {
                  var sellist = new SelectListItem();
                  sellist.Text = p.MenyText();
                  sellist.Value = p.Id().ToString();
                  return sellist;
              });



            ViewBag.Parents = parents;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateConfirm([Bind(Include = "Id,Name,Text")] Clauses clauses, string idParent)
        {
            if (ModelState.IsValid)
            {
                clauses.Cataloges = session.Query<Cataloges>().Where(c => c.Id == int.Parse(idParent)).FirstOrDefault();

                session.Save(clauses);
            }

            return PartialView("~/Views/Clauses/ClausesShow.cshtml");
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

            var parents = HomeController.GetAllParents()
       .Select(p =>
       {
           var sellist = new SelectListItem();
           sellist.Text = p.MenyText();
           sellist.Value = p.Id().ToString();
           return sellist;
       });



            ViewBag.Parents = parents;
            return View(clauses);
        }

        [HttpPost]
        public ActionResult EditConfirm([Bind(Include = "Id,Name,Text")] Clauses clauses, string idParent)
        {
            if (ModelState.IsValid)
            {
                clauses.Cataloges = session.Query<Cataloges>().Where(c => c.Id == int.Parse(idParent)).FirstOrDefault();
                session.Update(clauses, clauses.Id);
                session.Flush();
            }

            return PartialView("~/Views/Clauses/ClausesShow.cshtml");
        }



        public ActionResult Delete(int? id)
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
        public ActionResult DeleteConfirm(int id)
        {

            Clauses clauses = session.Query<Clauses>().Where(c => c.Id == id).FirstOrDefault();
            session.Delete(clauses);
            session.Flush();

            return PartialView("~/Views/Clauses/ClausesShow.cshtml");
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
