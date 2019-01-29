using Caristaniyaan.Dto;
using Caristaniyaan.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Caristaniyaan.Controllers
{
    public class RequestController : Controller
    {
        // GET: Request
        // GET: Brand
        private ApplicationDbContext _context;
        public RequestController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [HttpGet]
        public JsonResult GetAppointment(int id)
        {
            try
            {
                var app = _context.Appointments.SingleOrDefault(c => c.Id == id);
                if (app == null)
                {
                    return Json(new { responseText = "Appointment Not Found 404" }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { data = app }, JsonRequestBehavior.AllowGet);
            }
            catch (JsonException jx)
            {
                throw new JsonException("Unable to get Appointment", jx);
            }

        }

        [HttpGet]
        public JsonResult GetAppointments()
        {
            try
            {

                int startRec = Convert.ToInt32(Request["start"]);
                int pageSize = Convert.ToInt32(Request["length"]);
                string search = Request["search[value]"].ToString();
                int totalRecord = _context.Appointments.Count();

                if (!string.IsNullOrWhiteSpace(search))
                {
                    return Json(new { data = _context.Appointments.Where(x => x.name.ToLower().Contains(search.ToLower())) }, JsonRequestBehavior.AllowGet);
                }

                var app = _context.Appointments.OrderBy(od => od.Id).Skip(startRec).Take(pageSize).ToList();

                return Json(new { data = app, draw = Request["draw"], recordsTotal = totalRecord, recordsFiltered = totalRecord }, JsonRequestBehavior.AllowGet);
            }
            catch (JsonException jx)
            {
                throw new JsonException("Unable to load brands", jx);
            }

        }

        [HttpGet]
        public JsonResult GetDemand(int id)
        {
            try
            {
                var dem = _context.Demands.SingleOrDefault(c => c.Id == id);
                if (dem == null)
                {
                    return Json(new { responseText = "Demand Not Found 404" }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { data = dem}, JsonRequestBehavior.AllowGet);
            }
            catch (JsonException jx)
            {
                throw new JsonException("Unable to get Demand", jx);
            }

        }

        [HttpGet]
        public JsonResult GetDemands()
        {
            try
            {

                int startRec = Convert.ToInt32(Request["start"]);
                int pageSize = Convert.ToInt32(Request["length"]);
                string search = Request["search[value]"].ToString();
                int totalRecord = _context.Demands.Count();

                if (!string.IsNullOrWhiteSpace(search))
                {
                    return Json(new { data = _context.Demands.Where(x => x.name.ToLower().Contains(search.ToLower())) }, JsonRequestBehavior.AllowGet);
                }

                var dem = _context.Demands.OrderBy(od => od.Id).Skip(startRec).Take(pageSize).ToList();

                return Json(new { data = dem, draw = Request["draw"], recordsTotal = totalRecord, recordsFiltered = totalRecord }, JsonRequestBehavior.AllowGet);
            }
            catch (JsonException jx)
            {
                throw new JsonException("Unable to load brands", jx);
            }

        }

        [HttpPost]
        public JsonResult Appoinment(appointemnt model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { success = false, responseText = "Sorry! There was error perfoming your action." }, JsonRequestBehavior.AllowGet);
                }

                Appointment app = new Appointment();
                app.modalYear = model.modalYear;
                app.carInfo = model.carInfo;
                app.name = model.name;
                app.email = model.email;
                app.phonenumber = model.phonenumber;
                app.message = model.message;
                app.date = DateTime.Now;

                _context.Appointments.Add(app);
                _context.SaveChanges();

                var app_new = _context.Brands.SingleOrDefault(c => c.Id == app.Id);

                return Json(new { data = app_new, success = true, responseText = "Your appointment has been successfuly placed!" }, JsonRequestBehavior.AllowGet);

            }
            catch (JsonException jx)
            {
                throw new JsonException("Unable to place appoinment", jx);
            }



        }

        [HttpPost]
        public JsonResult Demand(demand model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { success = false, responseText = "Sorry! There was error perfoming your action." }, JsonRequestBehavior.AllowGet);
                }

                Demand app = new Demand();
                app.modalYear = model.modalYear;
                app.carInfo = model.carInfo;
                app.name = model.name;
                app.email = model.email;
                app.phonenumber = model.phonenumber;
                app.itemName = model.itemName;
                app.itemDetail = model.itemDetail;
                var filename = Path.GetFileName(model.itemImage.FileName);
                model.itemImage.SaveAs(Server.MapPath("../Images/appointment/" + filename));
                app.itemImage = "/Images/product/" + filename;
                app.date = DateTime.Now;

                _context.Demands.Add(app);
                _context.SaveChanges();

                var demand_new = _context.Demands.SingleOrDefault(c => c.Id == app.Id);



                return Json(new { data = demand_new, success = true, responseText = "Your demand has been successfuly placed" }, JsonRequestBehavior.AllowGet);

            }
            catch (JsonException jx)
            {
                throw new JsonException("Unable to place demand", jx);
            }



        }

        [HttpPost]
        public JsonResult Order(orderViewModel model)
        {
            try
            {
                var cart_items = JsonConvert.DeserializeObject<List<cart>>(model.cartItems);
                if (!ModelState.IsValid)
                {
                    return Json(new { success = false, responseText = "Sorry! There was error perfoming your action." }, JsonRequestBehavior.AllowGet);
                }

                
                Order order = new Order();
                order.fname = model.fname;
                order.lname = model.lname;
                order.phoneno = model.phoneno;
                order.postcode = model.postcode;
                order.province = model.province;
                order.city = model.city;
                order.countary = model.countary;
                order.email = model.email;
                order.address = model.address;
                order.status = Status.pending;
                order.date = DateTime.Now;

                _context.Orders.Add(order);
                _context.SaveChanges();

                OrderProduct orderProduct = new OrderProduct();
                foreach( var item in cart_items)
                {
                    orderProduct.OrderId = order.Id;
                    orderProduct.ProductId = item.Id;
                    orderProduct.quantity = item.Quantity;
                    orderProduct.date = DateTime.Now;

                    _context.OrderProducts.Add(orderProduct);
                    _context.SaveChanges();
                }


                var order_new = _context.Orders.SingleOrDefault(c => c.Id == order.Id);



                return Json(new { data = order_new, success = true, responseText = "Your Order has been successfuly placed" }, JsonRequestBehavior.AllowGet);

            }
            catch (JsonException jx)
            {
                throw new JsonException("Unable to place order", jx);
            }
        }
    }
}