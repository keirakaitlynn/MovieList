using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieList.Models;

namespace MovieList.Controllers
{
    public class MovieKWController : Controller
    {
        private MovieContext context { get; set; }
        public MovieKWController(MovieContext ctx)
        {
            context = ctx;
        }
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            return View("Edit", new MovieKW());
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            var movie = context.Movies.Find(id);
            return View(movie);
        }
        [HttpPost]
        public IActionResult Edit(MovieKW movieKw)
        {
            if (ModelState.IsValid)
            {
                if (movieKw.MovieId == 0)
                    context.Movies.Add(movieKw);
                else
                    context.Movies.Update(movieKw);
                context.SaveChanges();
                return RedirectToAction("IndexKW", "HomeKW");
            }
            else
            {
                ViewBag.Action = (movieKw.MovieId == 0) ? "Add" : "Edit";
                return View(movieKw);
            }
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var movie = context.Movies.Find(id);
            return View(movie);
        }
        [HttpPost]
        public IActionResult Delete(MovieKW movieKw)
        {
            context.Movies.Remove(movieKw);
            context.SaveChanges();
            return RedirectToAction("" +
                                    "Index" +
                                    "KW", "HomeKW");
        }
    }
}