using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Cataloges cataloges)
        {
            if (ModelState.IsValid)
            {
                session.Save(cataloges);
            }

            return View(cataloges);
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
            return View(catalog);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [System.Web.Services.WebMethod]

        public ActionResult EditConfirm([Bind(Include = "Id,Name")] Cataloges cataloges, int IdParent = -1)
        {
            if (ModelState.IsValid)
            {
                if(IdParent!= -1)
                cataloges.Parent = session.Query<Cataloges>().Where(c => c.Id == IdParent).FirstOrDefault();
                session.Update(cataloges, cataloges.Id);
                session.Flush();
            }

            return PartialView("Cataloges", "Home");
        }

        // public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Cataloges cataloges = db.Cataloges.Find(id);
        //    if (cataloges == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(cataloges);
        //}

        //// POST: Cataloges/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Cataloges cataloges = db.Cataloges.Find(id);
        //    db.Cataloges.Remove(cataloges);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
