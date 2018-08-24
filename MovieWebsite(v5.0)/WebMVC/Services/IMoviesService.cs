using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Models;
using WebMVC.ViewModels;

namespace WebMVC.Services
{
    public interface IMoviesService
    {
        Task<MovieItemViewModel> CreateMovie();
        Task<Movie> CreateMoviePost(Movie movie);
        Task<MovieItemViewModel> EditMovie(int ?id);
        Task EditMoviePost(Movie movie);
        // in fact, this method is not the true delete method, it only show the information for user to confirm.
        Task<MovieItemViewModel> DeleteMovie(int ?id);
        Task DeleteConfirmed(int ? id);
        //correspond to /index
        Task<List<Movie>> ShowMovies();
        Task<MovieItemViewModel> GetMovieDetail(int ? id);

        Task<string> GetCompanyName(int? CompanyID);
    }
}
