using Caristaniyaan.Dto;
using Caristaniyaan.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Caristaniyaan.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        // GET: Admin

        private ApplicationDbContext _context;
        public AdminController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Category()
        {
            return View();
        }

        public ActionResult SubCategory()
        {

            try
            {
                var categories = _context.Categories.ToList();

                var viewModel = new subCategoryViewModal
                {
                    categories = categories
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to load categories,brands and products", ex);
            }
        }

        public ActionResult Brands()
        {
            try
            {
                var subCategories = _context.SubCategories.ToList();

                var viewModel = new brandViewModal
                {
                    subcategories = subCategories
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to load categories,brands and products", ex);
            }
        }

        public ActionResult Products()
        {
            try
            {
                var brands = _context.Brands.ToList();

                var viewModel = new productViewModal
                {
                    brand = brands
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to load categories,brands and products", ex);
            }
        }

        public ActionResult Demands()
        {
            return View();
        }

        public ActionResult Appointment()
        {
            return View();
        }

        public ActionResult PendingOrders()
        {
            return View();
        }

        public ActionResult Delivered()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetPendingOrders()
        {
            try
            {
                int startRec = Convert.ToInt32(Request["start"]);
                int pageSize = Convert.ToInt32(Request["length"]);
                string search = Request["search[value]"].ToString();
                int totalRecord = _context.Orders.Where(od => od.status == Status.pending).Count();

                if (!string.IsNullOrWhiteSpace(search))
                {
                    return Json(new { data = _context.Orders.Where(od => od.status == Status.pending).Where(x => x.Id.ToString().ToLower().Contains(search.ToLower())) }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { data = _context.Orders.OrderBy(o => o.Id).Skip(startRec).Take(pageSize).Where(od => od.status == Status.pending).ToList(), recordsTotal = totalRecord, recordsFiltered = totalRecord }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to load all Orders", ex);
            }

        }

        [HttpGet]
        public JsonResult GetDeliveredOrders()
        {
            try
            {
                int startRec = Convert.ToInt32(Request["start"]);
                int pageSize = Convert.ToInt32(Request["length"]);
                string search = Request["search[value]"].ToString();
                int totalRecord = _context.Orders.Where(od => od.status == Status.delivered).Count();

                if (!string.IsNullOrWhiteSpace(search))
                {
                    return Json(new { data = _context.Orders.Where(od => od.status == Status.delivered).Where(x => x.Id.ToString().ToLower().Contains(search.ToLower())) }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { data = _context.Orders.OrderBy(o => o.Id).Skip(startRec).Take(pageSize).Where(od => od.status == Status.delivered).ToList(), recordsTotal = totalRecord, recordsFiltered = totalRecord }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to load all Orders", ex);
            }

        }

        public ActionResult Order(int id)
        {
            try
            {
                var _order = _context.Orders.Where(od => od.Id == id).SingleOrDefault();

                var _orderItem = _context.OrderProducts.Where(od => od.OrderId == _order.Id).Include(it => it.Product).ToArray();

                var viewModal = new orderDetailViewModel
                {
                    Id = _order.Id,
                    fname = _order.fname,
                    lname = _order.lname,
                    email = _order.email,
                    phoneno = _order.phoneno,
                    address = _order.address,
                    city = _order.city,
                    postcode = _order.postcode,
                    province = Enum.GetName(typeof(Province), _order.province),
                    countary = Enum.GetName(typeof(Country), _order.countary),
                    status = _order.status,
                    orderProducts = _orderItem
                };
                return View(viewModal);
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to load Order", ex);
            }

        }

        public ActionResult Deliver(int id)
        {
            try
            {
                var order = _context.Orders.Where(od => od.Id == id).SingleOrDefault();
                //Order Status
                order.status = Status.delivered;
                _context.SaveChanges();

                return View("InProcess");
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to Deliver", ex);
            }

        }
    }
}