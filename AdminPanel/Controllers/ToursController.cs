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
    public class ToursController : Controller
    {
        private DatabaseEntities db = new DatabaseEntities();

        // GET: Tours
        public ActionResult Index()
        {
            var tours = db.Tours.Include(t => t.City).Include(t => t.City1);
            return View(tours.ToList());
        }

        // GET: Tours/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tour tour = db.Tours.Find(id);
            if (tour == null)
            {
                return HttpNotFound();
            }
            return View(tour);
        }

        // GET: Tours/Create
        public ActionResult Create()
        {
            ViewBag.CountryId = new SelectList(db.Countries.Where(t => t.Regions.Count != 0), "CountryId", "Name");
            ViewBag.FromCityId = new SelectList(db.Cities, "CityId", "Name");
            ViewBag.ToCityId = new SelectList(db.Cities, "CityId", "Name");
            return View();
        }

        // POST: Tours/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "TourId,Price,StartDate,EndDate,Image")] Tour tour, int[] CityId)
        {
            tour.ToCityId = CityId[0];
            tour.FromCityId = CityId[1];

            if (ModelState.IsValid)
            {
                db.Tours.Add(tour);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CountryId = new SelectList(db.Countries.Where(t => t.Regions.Count != 0), "CountryId", "Name");
            ViewBag.FromCityId = new SelectList(db.Cities, "FromCityId", "Name", tour.FromCityId);
            ViewBag.ToCityId = new SelectList(db.Cities, "ToCityId", "Name", tour.ToCityId);

            return View(tour);
        }

        // GET: Tours/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tour tour = db.Tours.Find(id);
            if (tour == null)
            {
                return HttpNotFound();
            }
            ViewBag.CountryId = new SelectList(db.Countries.Where(t => t.Regions.Count != 0), "CountryId", "Name");
            ViewBag.FromCityId = new SelectList(db.Cities, "CityId", "Name", tour.FromCityId);
            ViewBag.ToCityId = new SelectList(db.Cities, "CityId", "Name", tour.ToCityId);
            return View(tour);
        }

        // POST: Tours/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit([Bind(Include = "TourId,Price,StartDate,EndDate,Image")] Tour tour, int[] CityId)
        {
            tour.ToCityId = CityId[0];
            tour.FromCityId = CityId[1];

            if (ModelState.IsValid)
            {
                db.Entry(tour).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CountryId = new SelectList(db.Countries.Where(t => t.Regions.Count != 0), "CountryId", "Name");
            ViewBag.FromCityId = new SelectList(db.Cities, "CityId", "Name", tour.FromCityId);
            ViewBag.ToCityId = new SelectList(db.Cities, "CityId", "Name", tour.ToCityId);
            return View(tour);
        }

        // POST: Tours/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            Tour tour = db.Tours.Find(id);
            db.Tours.Remove(tour);
            db.SaveChanges();

            return PartialView("ToursTable", db.Tours.Include(t => t.City).Include(t => t.City1).ToList());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult CountryIsChangedDropDownList(int countryId)
        {
            ViewBag.RegionId = new SelectList(db.Regions.Where(t => t.CountryId == countryId && t.Cities.Count != 0), "RegionId", "Name");
            return PartialView("RegionDropDownList");
        }

        public ActionResult RegionIsChangedDropDownList(int regionId)
        {
            ViewBag.CityId = new SelectList(db.Cities.Where(t => t.RegionId == regionId), "CityId", "Name");
            return PartialView("CityDropDownList");
        }

    }
}
