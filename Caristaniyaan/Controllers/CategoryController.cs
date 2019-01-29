using Caristaniyaan.Dto;
using Caristaniyaan.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Caristaniyaan.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        private ApplicationDbContext _context;
        public CategoryController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [HttpGet]
        [AllowAnonymous]
        public JsonResult GetCategory(int id)
        {
            try
            {
                var cat = _context.Categories.SingleOrDefault(c => c.Id == id);
                if (cat == null)
                {
                    return Json(new { responseText = "Category Not Found 404" }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { data = cat }, JsonRequestBehavior.AllowGet);
            }
            catch (JsonException jx)
            {
                throw new JsonException("Unable to get category", jx);
            }

        }

        [HttpGet]
        [AllowAnonymous]
        public JsonResult GetCategories()
        {
            try
            {

                int startRec = Convert.ToInt32(Request["start"]);
                int pageSize = Convert.ToInt32(Request["length"]);
                string search = Request["search[value]"].ToString();
                int totalRecord = _context.Categories.Count();

                if (!string.IsNullOrWhiteSpace(search))
                {
                    return Json(new { data = _context.Categories.Where(x => x.name.ToLower().Contains(search.ToLower())) }, JsonRequestBehavior.AllowGet);
                }

                var cat = _context.Categories.OrderBy(od => od.Id).Skip(startRec).Take(pageSize).ToList();

                return Json(new { data = cat, draw = Request["draw"], recordsTotal = totalRecord, recordsFiltered = totalRecord }, JsonRequestBehavior.AllowGet);
            }
            catch (JsonException jx)
            {
                throw new JsonException("Unable to load Categories", jx);
            }

        }


        [HttpPost]
        public JsonResult Add(categoryViewModal model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { success = false, responseText = "Sorry! There was error perfoming your action." }, JsonRequestBehavior.AllowGet);
                }

                bool _exists = _context.Categories.Any(c => c.name == model.name);

                if (!_exists)
                {
                    Category cat = new Category();
                    cat.name = model.name;
                    cat.date = DateTime.Now;

                    _context.Categories.Add(cat);
                    _context.SaveChanges();

                    var cat_new = _context.Categories.SingleOrDefault(c => c.Id == cat.Id);



                    return Json(new { data = cat_new, success = true, responseText = "Category " + model.name + " has been successfuly added!" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, responseText = "Category " + model.name + " already exists!" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (JsonException jx)
            {
                throw new JsonException("Unable to add Category", jx);
            }



        }

        [HttpPost]
        public JsonResult Update(categoryViewModal model, int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { success = false, responseText = "Sorry! There was error perfoming your action." }, JsonRequestBehavior.AllowGet);
                }

                var catInDB = _context.Categories.SingleOrDefault(c => c.Id == id);
                if (catInDB != null)
                {

                    catInDB.name = model.name;

                    _context.SaveChanges();
                    var cat_new = _context.Categories.SingleOrDefault(c => c.Id == id);

                    return Json(new { cat = cat_new, success = true, responseText = "Category " + model.name + " has been successfuly Updated!" }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json(new { success = false, responseText = "Category " + model.name + " doesnot exists!" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (JsonException jx)
            {
                throw new JsonException("Unable to Update Category", jx);
            }


        }

        [HttpPost]
        public JsonResult Delete(int Id)
        {
            try
            {
                var catinDB = _context.Categories.SingleOrDefault(c => c.Id == Id);

                if (catinDB != null)
                {
                    _context.Categories.Remove(catinDB);
                    _context.SaveChanges();
                    return Json(new { success = true, responseText = "Category " + catinDB.name + " has been Deleted!" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, responseText = "Category " + catinDB.name + " doesnot exists" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (JsonException jx)
            {
                throw new JsonException("Unable to Delete Category", jx);
            }


        }
    }
}