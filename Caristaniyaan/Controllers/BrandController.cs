using Caristaniyaan.Dto;
using Caristaniyaan.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Caristaniyaan.Controllers
{
    public class BrandController : Controller
    {
        // GET: Brand
        private ApplicationDbContext _context;
        public BrandController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [HttpGet]
        [AllowAnonymous]
        public JsonResult GetBrand(int id)
        {
            try
            {
                var brand = _context.Brands.Include(c=>c.SubCategory).SingleOrDefault(c => c.Id == id);
                if (brand == null)
                {
                    return Json(new { responseText = "Brand Not Found 404" }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { data = brand }, JsonRequestBehavior.AllowGet);
            }
            catch (JsonException jx)
            {
                throw new JsonException("Unable to get brand", jx);
            }

        }

        [HttpGet]
        [AllowAnonymous]
        public JsonResult GetBrands()
        {
            try
            {

                int startRec = Convert.ToInt32(Request["start"]);
                int pageSize = Convert.ToInt32(Request["length"]);
                string search = Request["search[value]"].ToString();
                int totalRecord = _context.Brands.Count();

                if (!string.IsNullOrWhiteSpace(search))
                {
                    return Json(new { data = _context.Brands.Where(x => x.name.ToLower().Contains(search.ToLower())) }, JsonRequestBehavior.AllowGet);
                }

                var brand = _context.Brands.OrderBy(od => od.Id).Skip(startRec).Take(pageSize).Include(c=>c.SubCategory).ToList();

                return Json(new { data = brand, draw = Request["draw"], recordsTotal = totalRecord, recordsFiltered = totalRecord }, JsonRequestBehavior.AllowGet);
            }
            catch (JsonException jx)
            {
                throw new JsonException("Unable to load brands", jx);
            }

        }


        [HttpPost]
        public JsonResult Add(brandViewModal model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { success = false, responseText = "Sorry! There was error perfoming your action." }, JsonRequestBehavior.AllowGet);
                }

                bool _exists = _context.Brands.Any(c => c.name == model.name);

                if (!_exists)
                {
                    Brand brand = new Brand();
                    brand.name = model.name;
                    brand.SubCategoryId = model.SubCategoryId;
                    brand.date = DateTime.Now;

                    _context.Brands.Add(brand);
                    _context.SaveChanges();

                    var brand_new = _context.Brands.Include(c=>c.SubCategory).SingleOrDefault(c => c.Id == brand.Id);



                    return Json(new { data = brand_new, success = true, responseText = "Brand " + model.name + " has been successfuly added!" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, responseText = "Brand " + model.name + " already exists!" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (JsonException jx)
            {
                throw new JsonException("Unable to add Brand", jx);
            }



        }

        [HttpPost]
        public JsonResult Update(brandViewModal model, int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { success = false, responseText = "Sorry! There was error perfoming your action." }, JsonRequestBehavior.AllowGet);
                }

                var brandInDB = _context.Brands.SingleOrDefault(c => c.Id == id);
                if (brandInDB != null)
                {

                    brandInDB.name = model.name;
                    brandInDB.SubCategoryId = model.SubCategoryId;

                    _context.SaveChanges();
                    var cat_new = _context.Brands.SingleOrDefault(c => c.Id == id);

                    return Json(new { cat = cat_new, success = true, responseText = "Brand " + model.name + " has been successfuly Updated!" }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json(new { success = false, responseText = "Brand " + model.name + " doesnot exists!" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (JsonException jx)
            {
                throw new JsonException("Unable to Update Brand", jx);
            }


        }

        [HttpPost]
        public JsonResult Delete(int Id)
        {
            try
            {
                var brandinDB = _context.Brands.SingleOrDefault(c => c.Id == Id);

                if (brandinDB != null)
                {
                    _context.Brands.Remove(brandinDB);
                    _context.SaveChanges();
                    return Json(new { success = true, responseText = "Brand " + brandinDB.name + " has been Deleted!" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, responseText = "Brand " + brandinDB.name + " doesnot exists" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (JsonException jx)
            {
                throw new JsonException("Unable to Delete Brand", jx);
            }


        }
    }
}