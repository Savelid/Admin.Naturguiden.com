using adminNaturguiden.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace adminNaturguiden.Controllers
{
    public class PicturesController : Controller
    {

        public string UploadImage()
        {
            string returnStr = PictureHandler.SaveImage(WebImage.GetImageFromRequest());
           
            return returnStr;
        }

        // GET: Pictures
        public async Task<ActionResult> Index()
        {
            var pictures = await PictureHandler.GetPicturesAsync();
            return View(pictures);
        }

        // GET: Pictures/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var picture = await PictureHandler.GetPictureAsync(id);
            return View(picture);
        }

        // GET: Pictures/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pictures/Create
        [HttpPost]
        public async Task<ActionResult> Create(libraryNaturguiden.Picture picture)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var status = await PictureHandler.CreatePictureAsync(picture);

                    return RedirectToAction("Index");
                }
                catch(Exception e)
                {
                    ViewBag.Error = e.Message;
                    return View();
                }
            }
            ViewBag.Error = "One or more fields was not filled in correctley";
            return View(picture);
        }

        // GET: Pictures/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var picture = await PictureHandler.GetPictureAsync(id);
            return View(picture);
        }

        // POST: Pictures/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, libraryNaturguiden.Picture picture)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var status = await PictureHandler.UpdatePictureAsync(picture);

                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    ViewBag.Error = e.Message;
                    return View();
                }
            }
            ViewBag.Error = "One or more fields was not filled in correctley";
            return View(picture);
        }

        // GET: Pictures/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var picture = await PictureHandler.GetPictureAsync(id);
            return View(picture);
        }

        // POST: Pictures/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id, libraryNaturguiden.Picture picture)
        {
            try
            {
                await PictureHandler.DeletePictureAsync(id);

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
