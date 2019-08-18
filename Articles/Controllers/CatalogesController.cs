using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Articles.Models;

namespace Articles.Controllers
{
    public class CatalogesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Cataloges
        public ActionResult Index()
        {
            return View(db.Cataloges.ToList());
        }

        // GET: Cataloges/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cataloges cataloges = db.Cataloges.Find(id);
            if (cataloges == null)
            {
                return HttpNotFound();
            }
            return View(cataloges);
        }

        // GET: Cataloges/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cataloges/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Cataloges cataloges)
        {
            if (ModelState.IsValid)
            {
                db.Cataloges.Add(cataloges);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cataloges);
        }

        // GET: Cataloges/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cataloges cataloges = db.Cataloges.Find(id);
            if (cataloges == null)
            {
                return HttpNotFound();
            }
            return View(cataloges);
        }

        // POST: Cataloges/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Cataloges cataloges)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cataloges).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cataloges);
        }

        // GET: Cataloges/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cataloges cataloges = db.Cataloges.Find(id);
            if (cataloges == null)
            {
                return HttpNotFound();
            }
            return View(cataloges);
        }

        // POST: Cataloges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cataloges cataloges = db.Cataloges.Find(id);
            db.Cataloges.Remove(cataloges);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
