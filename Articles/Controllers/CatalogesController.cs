using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Articles.Interfaces;
using Articles.Models;
using NHibernate;

namespace Articles.Controllers
{
    public class CatalogesController : Controller
    {
        ISession session;

        public CatalogesController()
        {
            session = NHibernateHelper.OpenSession();
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
        public PartialViewResult CreateConfirm([Bind(Include = "Id,Name")] Cataloges cataloges,string idParent)
        {
            if (ModelState.IsValid)
            {
                cataloges.Parent = session.Query<Cataloges>().Where(c => c.Id == int.Parse(idParent)).FirstOrDefault();
                session.Save(cataloges);
            }

            return  PartialView("~/Views/Clauses/ClausesShow.cshtml");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var  catalog = session.Query<Cataloges>().Where(c => c.Id == id).FirstOrDefault();
            if (catalog == null)
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
            return View(catalog);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [System.Web.Services.WebMethod]

        public  ActionResult EditConfirm([Bind(Include = "Id,Name")] Cataloges cataloges, string idParent)
        {
            if (ModelState.IsValid)
            {
                if(idParent != null)
                    cataloges.Parent = session.Query<Cataloges>().Where(c => c.Id == int.Parse(idParent)).FirstOrDefault();
                session.Update(cataloges, cataloges.Id);
                session.Flush();
             }

            return  PartialView("~/Views/Clauses/ClausesShow.cshtml");
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cataloges clauses = session.Query<Cataloges>().Where(c => c.Id == id).FirstOrDefault();
            if (clauses == null)
            {
                return HttpNotFound();
            }
            return View(clauses);
        }

        [HttpPost]
        public ActionResult DeleteConfirm(int id)
        {

            Cataloges clauses = session.Query<Cataloges>().Where(c => c.Id == id).FirstOrDefault();

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
