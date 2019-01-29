using Caristaniyaan.Dto;
using Caristaniyaan.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Caristaniyaan.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        private ApplicationDbContext _context;
        public ProductController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [HttpGet]
        [AllowAnonymous]
        public JsonResult GetProduct(int id)
        {
            try
            {
                var product = _context.Products.Include(b => b.Brand).SingleOrDefault(c => c.Id == id);
                if (product == null)
                {
                    return Json(new { responseText = "Product Not Found 404" }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { data = product }, JsonRequestBehavior.AllowGet);
            }
            catch (JsonException jx)
            {
                throw new JsonException("Unable to get product", jx);
            }

        }

        [HttpGet]
        [AllowAnonymous]
        public JsonResult GetProducts()
        {
            try
            {

                int startRec = Convert.ToInt32(Request["start"]);
                int pageSize = Convert.ToInt32(Request["length"]);
                string search = Request["search[value]"].ToString();
                int totalRecord = _context.Products.Count();

                if (!string.IsNullOrWhiteSpace(search))
                {
                    return Json(new { data = _context.Products.Where(x => x.name.ToLower().Contains(search.ToLower())) }, JsonRequestBehavior.AllowGet);
                }

                var product = _context.Products.OrderBy(od => od.Id).Skip(startRec).Take(pageSize).ToList();

                return Json(new { data = product, draw = Request["draw"], recordsTotal = totalRecord, recordsFiltered = totalRecord }, JsonRequestBehavior.AllowGet);
            }
            catch (JsonException jx)
            {
                throw new JsonException("Unable to load products", jx);
            }

        }


        [HttpPost]
        public JsonResult Add(productViewModal model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { success = false, responseText = "Sorry! There was error perfoming your action." }, JsonRequestBehavior.AllowGet);
                }

                bool _exists = _context.Products.Any(c => c.name == model.name);

                if (!_exists)
                {
                    Product product = new Product();
                    product.BrandId = model.BrandId;
                    product.color = model.color;
                    product.date = DateTime.Now;
                    product.details = model.details;
                    var filename = Path.GetFileName(model.image.FileName);
                    model.image.SaveAs(Server.MapPath("../Images/product/" + filename));
                    product.image_url = "/Images/product/" + filename;
                    product.model = model.car;
                    product.name = model.name;
                    product.price = model.price;
                    product.priority = model.price;
                    product.quantity = model.quantity;
                    product.status = model.status;
                    product.whileSalePrice = model.whileSalePrice;

                    _context.Products.Add(product);
                    _context.SaveChanges();

                    var product_new = _context.Products.Include(c => c.Brand).SingleOrDefault(c => c.Id == product.Id);



                    return Json(new { data = product_new, success = true, responseText = "Product " + model.name + " has been successfuly added!" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, responseText = "Product " + model.name + " already exists!" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (JsonException jx)
            {
                throw new JsonException("Unable to add Product", jx);
            }



            
        }

       
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            try
            {
                var productinDB = _context.Products.SingleOrDefault(c => c.Id == Id);

                if (productinDB != null)
                {
                    System.IO.File.Delete(Server.MapPath(productinDB.image_url));
                    _context.Products.Remove(productinDB);
                    _context.SaveChanges();
                    return Json(new { success = true, responseText = "Product " + productinDB.name + " has been Deleted!" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, responseText = "Product " + productinDB.name + " doesnot exists" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (JsonException jx)
            {
                throw new JsonException("Unable to Delete Brand", jx);
            }


        }

        [HttpPost]
        public JsonResult Update(productViewModal model, int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { success = false, responseText = "Sorry! There was error perfoming your action." }, JsonRequestBehavior.AllowGet);
                }

                var productInDB = _context.Products.Include(pr=> pr.Brand).SingleOrDefault(c => c.Id == id);
                if (productInDB != null)
                {

                    productInDB.BrandId = model.BrandId;
                    productInDB.color = model.color;
                    productInDB.date = DateTime.Now;
                    productInDB.details = model.details;
                    var filename = Path.GetFileName(model.image.FileName);
                    model.image.SaveAs(Server.MapPath("../../Images/product" + filename));
                    productInDB.image_url = "/Images/product/" + filename;
                    productInDB.model = model.car;
                    productInDB.name = model.name;
                    productInDB.price = model.price;
                    productInDB.priority = model.price;
                    productInDB.quantity = model.quantity;
                    productInDB.status = model.status;
                    productInDB.whileSalePrice = model.whileSalePrice;

                    _context.SaveChanges();
                    var cat_new = _context.Products.SingleOrDefault(c => c.Id == id);

                    return Json(new { cat = cat_new, success = true, responseText = "Product " + model.name + " has been successfuly Updated!" }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json(new { success = false, responseText = "Product " + model.name + " doesnot exists!" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (JsonException jx)
            {
                throw new JsonException("Unable to Update Product", jx);
            }


        }
    }
}