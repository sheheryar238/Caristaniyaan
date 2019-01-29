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
    public class SubCategoryController : Controller
    {
        // GET: SubCategory
        private ApplicationDbContext _context;
        public SubCategoryController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [HttpGet]
        [AllowAnonymous]
        public JsonResult GetSubCategory(int id)
        {
            try
            {
                var subcat = _context.SubCategories.Include(c=>c.Category).SingleOrDefault(c => c.Id == id);
                if (subcat == null)
                {
                    return Json(new { responseText = "Sub Category Not Found 404" }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { data = subcat }, JsonRequestBehavior.AllowGet);
            }
            catch (JsonException jx)
            {
                throw new JsonException("Unable to get sub category", jx);
            }

        }

        [HttpGet]
        [AllowAnonymous]
        public JsonResult GetSubCategories()
        {
            try
            {

                int startRec = Convert.ToInt32(Request["start"]);
                int pageSize = Convert.ToInt32(Request["length"]);
                string search = Request["search[value]"].ToString();
                int totalRecord = _context.SubCategories.Count();

                if (!string.IsNullOrWhiteSpace(search))
                {
                    return Json(new { data = _context.SubCategories.Where(x => x.name.ToLower().Contains(search.ToLower())) }, JsonRequestBehavior.AllowGet);
                }

                var subcat = _context.SubCategories.OrderBy(od => od.Id).Skip(startRec).Take(pageSize).Include(c=>c.Category).ToList();

                return Json(new { data = subcat, draw = Request["draw"], recordsTotal = totalRecord, recordsFiltered = totalRecord }, JsonRequestBehavior.AllowGet);
            }
            catch (JsonException jx)
            {
                throw new JsonException("Unable to load Categories", jx);
            }

        }


        [HttpPost]
        public JsonResult Add(subCategoryViewModal model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { success = false, responseText = "Sorry! There was error perfoming your action." }, JsonRequestBehavior.AllowGet);
                }

                bool _exists = _context.SubCategories.Any(c => c.name == model.name);

                if (!_exists)
                {
                    SubCategory cat = new SubCategory();
                    cat.name = model.name;
                    cat.CategoryId = model.CategoryId;
                    cat.date = DateTime.Now;

                    _context.SubCategories.Add(cat);
                    _context.SaveChanges();

                    var cat_new = _context.SubCategories.Include(c=>c.Category).SingleOrDefault(c => c.Id == cat.Id);



                    return Json(new { data = cat_new, success = true, responseText = "Sub Category " + model.name + " has been successfuly added!" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, responseText = "Sub Category " + model.name + " already exists!" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (JsonException jx)
            {
                throw new JsonException("Unable to add Sub Category", jx);
            }



        }

        [HttpPost]
        public JsonResult Update(subCategoryViewModal model, int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { success = false, responseText = "Sorry! There was error perfoming your action." }, JsonRequestBehavior.AllowGet);
                }

                var catInDB = _context.SubCategories.SingleOrDefault(c => c.Id == id);
                if (catInDB != null)
                {

                    catInDB.name = model.name;
                    catInDB.CategoryId = model.CategoryId;

                    _context.SaveChanges();
                    var cat_new = _context.SubCategories.SingleOrDefault(c => c.Id == id);

                    return Json(new { cat = cat_new, success = true, responseText = "Sub Category " + model.name + " has been successfuly Updated!" }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json(new { success = false, responseText = "Sub Category " + model.name + " doesnot exists!" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (JsonException jx)
            {
                throw new JsonException("Unable to Update Sub Category", jx);
            }


        }

        [HttpPost]
        public JsonResult Delete(int Id)
        {
            try
            {
                var catinDB = _context.SubCategories.SingleOrDefault(c => c.Id == Id);

                if (catinDB != null)
                {
                    _context.SubCategories.Remove(catinDB);
                    _context.SaveChanges();
                    return Json(new { success = true, responseText = "Sub Category " + catinDB.name + " has been Deleted!" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, responseText = "Sub Category " + catinDB.name + " doesnot exists" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (JsonException jx)
            {
                throw new JsonException("Unable to Delete Sub Category", jx);
            }


        }
    }
}
