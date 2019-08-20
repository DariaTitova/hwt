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



            var root = new SelectListItem();
            root.Text = "корень";
            root.Value = "-1";
            root.Selected = (catalog.Parent == null);


            var parents = new List<SelectListItem>()
            { root};


            List<SelectListItem> list = HomeController.GetAllParents()
               .Where(p => p.Id() != (int)id)
               .Select(p =>
               {
                   var sellist = new SelectListItem();
                   sellist.Text = p.MenyText();
                   sellist.Value = p.Id().ToString();

                   if(catalog.Parent != null)
                   sellist.Selected = (catalog.Parent.Id == p.Id());

                   return sellist;
               }).ToList();



            parents = parents.Concat(list).ToList();

           

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
                if(idParent != null&& int.Parse(idParent) != -1)
                {
                    cataloges.Parent = session.Query<Cataloges>().Where(c => c.Id == int.Parse(idParent)).FirstOrDefault();

                }
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


                CascadeDelete(clauses);


                session.Delete(clauses);

                session.FlushAsync();


            if (clauses.Children.Count >0)
            {
                session.Delete(clauses.Children.Last());
                session.FlushAsync();
            }

            try
            {
                if (clauses.Clauses.Count > 0)
                {
                    session.Delete(clauses.Clauses.Last());
                    session.FlushAsync();
                }
            }
            catch
            {

            }


            return PartialView("~/Views/Clauses/ClausesShow.cshtml");
        }


        private void CascadeDelete(Cataloges cat)
        {
            cat.Clauses.DefaultIfEmpty();
            cat.Children.DefaultIfEmpty();


            foreach (var child in cat.Children)
            {
                CascadeDelete(child);                
                session.Delete(child);
                session.FlushAsync();

            }

            try{
                foreach (var child in cat.Clauses)
                {
                    session.Delete(child);
                    session.FlushAsync();
                } }
            catch
            {

            }

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
