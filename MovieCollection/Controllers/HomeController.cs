using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieCollection.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCollection.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public readonly MovieDbContext _db;

        public HomeController(ILogger<HomeController> logger, MovieDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult EnterFilm()
        {
            return View();
        }

        [HttpPost]
        public IActionResult EnterFilm(Movie appResponse)
        {
            if (ModelState.IsValid)
            {
                //Updating Database
                ViewBag.movie = appResponse;
                _db.Movies.Add(appResponse);
                _db.SaveChanges();
                ViewBag.action = "Submitted";
            }
            return View("submitted");
        }

        [HttpGet]
        public IActionResult FilmList()
        {
            //loads the data into a list and returns view
            List<Movie> movies = new List<Movie>();
            movies = _db.Movies.ToList();
            return View(movies);
        }

        [HttpPost]
        public IActionResult FilmList(Movie appResponse, string FormType)
        {
            Movie movie = _db.Movies.Find(appResponse.MovieId);

            //Remove Movie form the database adn return the film list again
            if (FormType == "delete")
            {
                _db.Remove(appResponse);
                _db.SaveChanges();
                List<Movie> movies = new List<Movie>();
                movies = _db.Movies.ToList();
                return View(movies);
            }
            else if (FormType == "edit")
            {
                ViewBag.movie = movie;
                return View("Edit");
            }

            return View("Index");
        }

        [HttpPost]
        public IActionResult Edit(Movie appResponse, int movieID)
        {
            if (ModelState.IsValid)
            {
                //Remove origional 
                Movie movie = _db.Movies.Find(movieID);
                _db.Remove(movie);
                _db.SaveChanges();

                //Re-Add new info to the Database
                ViewBag.movie = appResponse;
                _db.Movies.Add(appResponse);
                _db.SaveChanges();
                ViewBag.action = "Updated";
            }
            return View("submitted");
        }

        public IActionResult Podcast()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
