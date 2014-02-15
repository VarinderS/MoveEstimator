using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MoveEstimator.Models;

namespace MoveEstimator.Controllers
{
    public class TestController : Controller
    {
        private Db db = new Db();

        //
        // GET: /Test/

        public ActionResult Index()
        {
            var estimates = db.Estimates.Include(e => e.FromLocation).Include(e => e.ToLocation);
            return View(estimates.ToList());
        }

        //
        // GET: /Test/Details/5

        public ActionResult Details(int id = 0)
        {
            Estimate estimate = db.Estimates.Find(id);
            if (estimate == null)
            {
                return HttpNotFound();
            }
            return View(estimate);
        }

        //
        // GET: /Test/Create

        public ActionResult Create()
        {
            ViewBag.FromLocationId = new SelectList(db.Locations, "Id", "Name");
            ViewBag.ToLocationId = new SelectList(db.Locations, "Id", "Name");
            return View();
        }

        //
        // POST: /Test/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Estimate estimate)
        {
            if (ModelState.IsValid)
            {
                db.Estimates.Add(estimate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FromLocationId = new SelectList(db.Locations, "Id", "Name", estimate.FromLocationId);
            ViewBag.ToLocationId = new SelectList(db.Locations, "Id", "Name", estimate.ToLocationId);
            return View(estimate);
        }

        //
        // GET: /Test/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Estimate estimate = db.Estimates.Find(id);
            if (estimate == null)
            {
                return HttpNotFound();
            }
            ViewBag.FromLocationId = new SelectList(db.Locations, "Id", "Name", estimate.FromLocationId);
            ViewBag.ToLocationId = new SelectList(db.Locations, "Id", "Name", estimate.ToLocationId);
            return View(estimate);
        }

        //
        // POST: /Test/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Estimate estimate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estimate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FromLocationId = new SelectList(db.Locations, "Id", "Name", estimate.FromLocationId);
            ViewBag.ToLocationId = new SelectList(db.Locations, "Id", "Name", estimate.ToLocationId);
            return View(estimate);
        }

        //
        // GET: /Test/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Estimate estimate = db.Estimates.Find(id);
            if (estimate == null)
            {
                return HttpNotFound();
            }
            return View(estimate);
        }

        //
        // POST: /Test/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Estimate estimate = db.Estimates.Find(id);
            db.Estimates.Remove(estimate);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}