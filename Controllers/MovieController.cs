using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieMvc.Data;
using MovieMvc.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Linq;

namespace MovieMvc.Controllers
{
    public class MovieController : Controller
    {
        private readonly AppDb _db;

        public MovieController(AppDb db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var movies = await _db.Movies.ToListAsync();
            return View(movies);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Movies model, IFormFile fileUpload)
        {
            if (model.Duration < 1)
            {
                ModelState.AddModelError("errDuration", "The duration field is required.");
                return View(model);
            }

            if (fileUpload == null)
            {
                ModelState.AddModelError("errFileUpload", "The file upload field is required.");
                return View(model);
            }

            if (ModelState.IsValid)
            {
                string pathImgMovie = "/images/movie/";
                string pathSave = $"wwwroot{pathImgMovie}";

                if (!Directory.Exists(pathSave))
                    Directory.CreateDirectory(pathSave);

                string fileName = $"{DateTime.Now:dd-MM-yyyy}-{fileUpload.FileName}";
                string fullPath = Path.Combine(Directory.GetCurrentDirectory(), pathSave, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await fileUpload.CopyToAsync(stream);
                }

                model.CoverImg = pathImgMovie + fileName;
                model.CreateDate = DateTime.Now;
                model.ModifyDate = DateTime.Now;

                _db.Movies.Add(model);
                await _db.SaveChangesAsync();

                TempData["Success"] = "เพิ่มข้อมูลภาพยนตร์สำเร็จแล้ว!";
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var movie = await _db.Movies.FindAsync(id);
            if (movie == null)
            {
                TempData["Error"] = "ไม่พบข้อมูลภาพยนตร์ที่ต้องการแก้ไข";
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Movies model, IFormFile fileUpload)
        {
            var oldMovie = await _db.Movies.FindAsync(model.Id);
            if (oldMovie == null)
            {
                TempData["Error"] = "ไม่พบข้อมูลภาพยนตร์ที่ต้องการแก้ไข";
                return RedirectToAction("Index");
            }

            oldMovie.Title = model.Title;
            oldMovie.Duration = model.Duration;
            oldMovie.Genre = model.Genre;
            oldMovie.ReleaseDate = model.ReleaseDate;
            oldMovie.ModifyDate = DateTime.Now;

            if (ModelState.IsValid)
            {
                if (fileUpload != null)
                {
                    string pathImgMovie = "/images/movie/";
                    string pathSave = $"wwwroot{pathImgMovie}";

                    if (!Directory.Exists(pathSave))
                        Directory.CreateDirectory(pathSave);

                    string fileName = $"{DateTime.Now:dd-MM-yyyy}-{fileUpload.FileName}";
                    string fullPath = Path.Combine(Directory.GetCurrentDirectory(), pathSave, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await fileUpload.CopyToAsync(stream);
                    }

                    oldMovie.CoverImg = pathImgMovie + fileName;
                }

                _db.Movies.Update(oldMovie);
                await _db.SaveChangesAsync();

                TempData["Success"] = "แก้ไขข้อมูลภาพยนตร์สำเร็จแล้ว!";
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var movie = await _db.Movies.FindAsync(id);
            if (movie == null)
            {
                TempData["Error"] = "ไม่พบข้อมูลภาพยนตร์ที่ต้องการลบ";
                return RedirectToAction("Index");
            }

            var pathPic = $"wwwroot{movie.CoverImg}";
            if (System.IO.File.Exists(pathPic))
            {
                System.IO.File.Delete(pathPic);
            }

            _db.Movies.Remove(movie);
            await _db.SaveChangesAsync();

            TempData["Success"] = "ลบข้อมูลภาพยนตร์สำเร็จแล้ว!";
            return RedirectToAction("Index");
        }

    }
}
