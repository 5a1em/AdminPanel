using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;
using AdminPanel.Models;

namespace AdminPanel.Controllers
{
    public class HomeController : Controller
    {
        private DatabaseEntities db = new DatabaseEntities();

        public ActionResult Index()
        {
            var tours = db.Tours.Include(t => t.City).Include(t => t.City1);
            return View(tours.ToList());
        }

        [HttpPost]
        public ActionResult Index(string fromCity, string toWhere, DateTime? startDate = null, int days = 0)
        {
            if(startDate == null && days == 0)
            {
                var tours = db.Tours.Include(t => t.City).Include(t => t.City1).Where(t => t.City.Name == fromCity &&
                (t.City1.Name == toWhere || t.City1.Region.Name == toWhere || t.City1.Region.Country.Name == toWhere));

                return View(tours.ToList());
            }
            else if(startDate == null && days != 0)
            {
                var tours = db.Tours.Include(t => t.City).Include(t => t.City1).Where(t => t.City.Name == fromCity &&
                (t.City1.Name == toWhere || t.City1.Region.Name == toWhere || t.City1.Region.Country.Name == toWhere));

                return View(tours.ToList().Where(t => (SubDate(t.StartDate, t.EndDate) == days)));
            }
            else if(startDate != null && days == 0)
            {
                var tours = db.Tours.Include(t => t.City).Include(t => t.City1).Where(t => t.City.Name == fromCity &&
                (t.City1.Name == toWhere || t.City1.Region.Name == toWhere || t.City1.Region.Country.Name == toWhere) &&
                (t.StartDate == startDate));

                return View(tours.ToList());
            }
            else
            {
                var tours = db.Tours.Include(t => t.City).Include(t => t.City1).Where(t => t.City.Name == fromCity &&
                (t.City1.Name == toWhere || t.City1.Region.Name == toWhere || t.City1.Region.Country.Name == toWhere) &&
                (t.StartDate == startDate));

                return View(tours.ToList().Where(t => (SubDate(t.StartDate, t.EndDate) == days)));
            }
        }

        private static int SubDate(DateTime startDate, DateTime endDate)
        {
            TimeSpan sub = endDate - startDate;
            return sub.Days;
        }
    }
}