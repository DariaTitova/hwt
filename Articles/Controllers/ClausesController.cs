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
    public class ClausesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Clauses
        public ActionResult Index()
        {
            return View(db.Clauses.ToList());
        }

        // GET: Clauses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clauses clauses = db.Clauses.Find(id);
            if (clauses == null)
            {
                return HttpNotFound();
            }
            return View(clauses);
        }

        // GET: Clauses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clauses/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Text")] Clauses clauses)
        {
            if (ModelState.IsValid)
            {
                db.Clauses.Add(clauses);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(clauses);
        }

        // GET: Clauses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clauses clauses = db.Clauses.Find(id);
            if (clauses == null)
            {
                return HttpNotFound();
            }
            return View(clauses);
        }

        // POST: Clauses/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Text")] Clauses clauses)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clauses).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(clauses);
        }

        // GET: Clauses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clauses clauses = db.Clauses.Find(id);
            if (clauses == null)
            {
                return HttpNotFound();
            }
            return View(clauses);
        }

        // POST: Clauses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Clauses clauses = db.Clauses.Find(id);
            db.Clauses.Remove(clauses);
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
