using adminNaturguiden.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace adminNaturguiden.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class PictureCategoriesController : Controller
    {
        // GET: Pictures
        public async Task<ActionResult> Index()
        {
            var categories = await PictureHandler.GetCategoriesAsync();
            return View(categories);
        }

        // GET: Pictures/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var category = await PictureHandler.GetCategoryAsync(id);
            return View(category);
        }

        // GET: Pictures/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pictures/Create
        [HttpPost]
        public async Task<ActionResult> Create(libraryNaturguiden.PictureCategory category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var status = await PictureHandler.CreateCategoryAsync(category);

                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    ViewBag.Error = e.Message;
                    return View();
                }
            }
            ViewBag.Error = "One or more fields was not filled in correctley";
            return View(category);
        }

        // GET: Pictures/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var category = await PictureHandler.GetCategoryAsync(id);
            return View(category);
        }

        // POST: Pictures/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, libraryNaturguiden.PictureCategory category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var status = await PictureHandler.UpdateCategoryAsync(category);

                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    ViewBag.Error = e.Message;
                    return View();
                }
            }
            ViewBag.Error = "One or more fields was not filled in correctley";
            return View(category);
        }

        // GET: Pictures/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var category = await PictureHandler.GetCategoryAsync(id);
            return View(category);
        }

        // POST: Pictures/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id, libraryNaturguiden.PictureCategory category)
        {
            try
            {
                await PictureHandler.DeleteCategoryAsync(id);

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View();
            }
        }
    }
}