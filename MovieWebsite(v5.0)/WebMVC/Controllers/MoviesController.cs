
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebMVC.Services;
using WebMVC.Models;
using WebMVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;
using System;

namespace WebMVC.Controllers
{
    [Route("Movies")]
    public class MoviesController : Controller
    {
        private readonly IMoviesService _movieSvc;
       
        public MoviesController(IMoviesService movieSvc)
        {
            _movieSvc = movieSvc;
        }

        [Route("Index")]
        // GET: Movies
        public async Task<IActionResult> Index()
        {
            return View(await _movieSvc.ShowMovies());
            
        }

   
        // GET: Movies/Details/5
        [Route("Details")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var movieitemVM = await _movieSvc.GetMovieDetail(id);
            if (movieitemVM == null)
            {
                return NotFound();
            }
            return View(movieitemVM);
        }

        // GET: Movies/Create
        [Route("Create")]
        public async Task<IActionResult> Create()
        {
           MovieItemViewModel movieitemVM = await _movieSvc.CreateMovie();
            return View(movieitemVM);
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Create")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,ReleaseDate,Genre,Price,CompanyID")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                await _movieSvc.CreateMoviePost(movie);
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Edit/5
        [Route("Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var movieitemVM = await _movieSvc.EditMovie(id);
            if (movieitemVM.Movie == null)
            {
                return NotFound();
            }
            return View(movieitemVM);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Edit")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("ID,Title,ReleaseDate,Genre,Price,CompanyID")]Movie movie)
        {
            //if (id != movie.ID)
            //{
            //    return NotFound();
            //}
            if (ModelState.IsValid)
            {
                string companyname = await _movieSvc.GetCompanyName(movie.CompanyID);
                movie.PublishCompany = new PublishCompany()
                {
                    CompanyID = movie.CompanyID,
                    CompanyName = companyname
                };
                await _movieSvc.EditMoviePost(movie);
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        [Route("Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var movieItemViewModel = await _movieSvc.DeleteMovie(id);
            if (movieItemViewModel.Movie == null)
            {
                return NotFound();
            }
            return View(movieItemViewModel);
        }

        // POST: Movies/Delete
        [HttpPost]
        [Route("DeleteConfirmed")]
       // [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed([Bind("ID")]Movie movie)
        {
            await _movieSvc.DeleteConfirmed(movie.ID);
            return RedirectToAction(nameof(Index));
        }
        
        [Route("AddtoCart")]
        public async Task<IActionResult> AddtoCart(int ? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var movieitemVM = await _movieSvc.GetMovieDetail(id);
            var movie = movieitemVM.Movie;
            var value = JsonConvert.SerializeObject(movie);
            if (movieitemVM == null)
            {
                return NotFound();
            }
            /*
             * This function pass values in different controllers, which are in different process(different processes can not share variables, so values want to used must be copied). 
             * note that this function is shallow copy, so the object company in the movie will be not if transfer the object 
             */
            TempData["added_movie"] = value;
            return RedirectToAction("AddtoCart","Basket");
        }
     
    }
}
