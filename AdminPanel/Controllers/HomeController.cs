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
        public ActionResult List(string fromCity, string toWhere, DateTime? startDate = null, int days = 0)
        {
            return View();
        }
    }
}