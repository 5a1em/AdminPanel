using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AdminPanel.Models;

namespace AdminPanel.Controllers
{
    public class CitiesController : Controller
    {
        private DatabaseEntities db = new DatabaseEntities();

        // GET: Cities
        public ActionResult Index()
        {
            var cities = db.Cities.Include(c => c.Region);
            return View(cities.ToList());
        }

        // GET: Cities/Create
        public ActionResult Create()
        {
            ViewBag.CountryId = new SelectList(db.Countries, "CountryId", "Name");
            ViewBag.RegionId = new SelectList(db.Regions, "RegionId", "Name");
            return View();
        }

        // POST: Cities/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "CityId,Name,RegionId")] City city)
        {
            if (ModelState.IsValid)
            {
                db.Cities.Add(city);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RegionId = new SelectList(db.Regions, "RegionId", "Name", city.RegionId);
            return View(city);
        }

        // GET: Cities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            City city = db.Cities.Find(id);
            if (city == null)
            {
                return HttpNotFound();
            }

            ViewBag.CountryId = new SelectList(db.Countries, "CountryId", "Name");
            ViewBag.RegionId = new SelectList(db.Regions, "RegionId", "Name", city.RegionId);
            return View(city);
        }

        // POST: Cities/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit([Bind(Include = "CityId,Name,RegionId")] City city)
        {
            if (ModelState.IsValid)
            {
                db.Entry(city).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
  
            ViewBag.RegionId = new SelectList(db.Regions, "RegionId", "Name", city.RegionId);
            return View(city);
        }

        // POST: Cities/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            City city = db.Cities.Find(id);

            var dependentTours = db.Tours.Include(t => t.City).Include(t => t.City1).Where(t => t.City.CityId == id || t.City1.CityId == id);
            db.Tours.RemoveRange(dependentTours);
            db.SaveChanges();

            db.Cities.Remove(city);
            db.SaveChanges();

            return PartialView("CitiesTable", db.Cities.Include(c => c.Region).ToList());
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
