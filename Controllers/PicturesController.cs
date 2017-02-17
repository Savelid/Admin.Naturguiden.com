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
        [HttpPost]
        public async Task<ActionResult> ReformatPicture(libraryNaturguiden.Picture picture)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var status = await PictureHandler.ReformatPictureAsync(picture);
                    TempData["Message"] = status.ToString();
                }
                catch (Exception e)
                {
                    ViewBag.Error = e.Message;
                }
            }

            return RedirectToAction(nameof(Edit), new { id = picture.Id});
        }

        // GET: Pictures
        public async Task<ActionResult> Index()
        {
            var pictures = await PictureHandler.GetPicturesAsync();
            ViewBag.Message = (string)TempData["Message"];
            return View(pictures);
        }

        // GET: Pictures/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var picture = await PictureHandler.GetPictureAsync(id);
            return View(picture);
        }

        // GET: Pictures/Create
        public async Task<ActionResult> Create()
        {
            var viewModel = new PictureGroup();
            viewModel.Categories = await PictureHandler.GetCategoriesAsync();
            viewModel.Formats = new string[] { "Album", "News" };
            return View(viewModel);
        }

        // POST: Pictures/Create
        [HttpPost]
        public async Task<ActionResult> Create(PictureGroup model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var status = await PictureHandler.CreatePictureAsync(model.Picture);
                    TempData["Message"] = status.ToString();
                    return RedirectToAction("Index");
                }
                catch(Exception e)
                {
                    ViewBag.Error = e.Message;
                    return View();
                }
            }
            ViewBag.Error = "One or more fields was not filled in correctley";
            return View(model);
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
                    TempData["Message"] = status.ToString();
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
                var status = await PictureHandler.DeletePictureAsync(id);
                TempData["Message"] = status.ToString();
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
