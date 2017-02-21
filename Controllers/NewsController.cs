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
    public class NewsController : Controller
    {
        // GET: Pictures
        public async Task<ActionResult> Index()
        {
            var model = await NewsHandler.GetNewsAsync();
            return View(model);
        }

        // GET: Pictures/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var model = await NewsHandler.GetNewsAsync(id);
            return View(model);
        }

        // GET: Pictures/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pictures/Create
        [HttpPost]
        public async Task<ActionResult> Create(libraryNaturguiden.News model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var status = await NewsHandler.CreateNewsAsync(model);

                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    ViewBag.Error = e.Message;
                    return View(model);
                }
            }
            ViewBag.Error = "One or more fields was not filled in correctley";
            return View(model);
        }

        // GET: Pictures/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var model = await NewsHandler.GetNewsAsync(id);
            return View(model);
        }

        // POST: Pictures/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, libraryNaturguiden.News model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var status = await NewsHandler.UpdateNewsAsync(model);

                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    ViewBag.Error = e.Message;
                    return View(model);
                }
            }
            ViewBag.Error = "One or more fields was not filled in correctley";
            return View(model);
        }

        // GET: Pictures/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var model = await NewsHandler.GetNewsAsync(id);
            return View(model);
        }

        // POST: Pictures/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id, libraryNaturguiden.News model)
        {
            try
            {
                await NewsHandler.DeleteNewsAsync(id);

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View(model);
            }
        }
    }
}