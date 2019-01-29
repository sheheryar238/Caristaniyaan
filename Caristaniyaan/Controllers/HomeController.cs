using Caristaniyaan.Dto;
using Caristaniyaan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Caristaniyaan.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        public ActionResult Index(int? id)
        {
            try
            {
                int pageIndex = id - 1 ?? 0;
                int pageSize = 9;

                var products = _context.Products.OrderByDescending(it => it.Id).Skip(pageIndex * pageSize).Take(pageSize).ToList();

                var viewModal = new HomeViewModal
                {
                    product = products
                };

                return View(viewModal);
            }
            catch (Exception ex)
            {
                throw new Exception("Sorry there is some error loading Product", ex);
            }
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Appointment()
        {
            return View();
        }
        public ActionResult Autopartondemand()
        {
            return View();
        }
        public ActionResult Carappointment()
        {
            return View();
        }
        public ActionResult Cardetails()
        {
            return View();
        }
        public ActionResult CarsDefault()
        {
            return View();
        }
        public ActionResult Cart()
        {
            return View();
        }
        public ActionResult Checkout()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult Partners()
        {
            return View();
        }
        public ActionResult PrrivacyPolicy()
        {
            return View();
        }
        public ActionResult Product(int id)
        {
            try
            {
                var prod = _context.Products.Where(it => it.Id == id).SingleOrDefault();
                ViewBag.Message = "Your Single item page.";

                var viewModel = new productViewModal
                {
                    Id = prod.Id,
                    name = prod.name,
                    price = prod.price,
                    whileSalePrice = prod.whileSalePrice,
                    color = prod.color,
                    quantity = prod.quantity,
                    car = prod.model,
                    details = prod.details,
                    status = prod.status,
                    imageurl = prod.image_url
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                throw new Exception("Sorry there was error loading the item", ex);
            }
        }
        public ActionResult TermCondition()
        {
            return View();
        }
        public ActionResult TrackShipment()
        {
            return View();
        }


    }
}