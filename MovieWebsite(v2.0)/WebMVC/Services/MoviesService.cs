using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebMVC.infrastructure;
using WebMVC.Models;
using WebMVC.ViewModels;
using System;

namespace WebMVC.Services
{
    public class MoviesService : IMoviesService
    {

        private readonly HttpClient _apiClient;

        public MoviesService(myHttpClient myHttpClient)
        {
            //_apiClient = myHttpClient;
            _apiClient = new HttpClient();
            _apiClient.Timeout = TimeSpan.FromMinutes(10);
        }

        public async Task<MovieItemViewModel> CreateMovie()
        {
            var uri = Get_MovieService_Controller_URI.Movie.CreateMovie;
            var responseString = await _apiClient.GetStringAsync(uri);
            var response = JsonConvert.DeserializeObject<List<SelectListItem>>(responseString);
            MovieItemViewModel movieitemVM = new MovieItemViewModel
            {
                Movie=new Movie(),
                Companys=response
            };
            return movieitemVM;
        }

        public async Task<Movie> CreateMoviePost(Movie movie)
        {
            var uri = Get_MovieService_Controller_URI.Movie.CreateMoviePost;
            var content = new StringContent(JsonConvert.SerializeObject(movie), System.Text.Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
            return movie;
        }

        public async Task DeleteConfirmed(int ? id)
        {
            var uri = Get_MovieService_Controller_URI.Movie.DeleteConfirmed(id);
            var content = new StringContent(JsonConvert.SerializeObject(id), System.Text.Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(uri,content);
            response.EnsureSuccessStatusCode();
        }

        public async Task<MovieItemViewModel> DeleteMovie(int? id)
        {
            var uri = Get_MovieService_Controller_URI.Movie.DeleteMovie(id);
            var responseString = await _apiClient.GetStringAsync(uri);
            var response = JsonConvert.DeserializeObject<MovieItemViewModel>(responseString);
            return response;
        }

        public async Task<MovieItemViewModel> EditMovie(int ? id)
        {
            var uri = Get_MovieService_Controller_URI.Movie.EditMovie(id);
            var responseString = await _apiClient.GetStringAsync(uri);
            var response = JsonConvert.DeserializeObject<MovieItemViewModel>(responseString);
            return response;
        }

        public async Task EditMoviePost(Movie movie)
        {
            var uri = Get_MovieService_Controller_URI.Movie.EditMoviePost();
            var response = await _apiClient.PostAsync(uri,null);
            response.EnsureSuccessStatusCode();
        }

        public async Task<MovieItemViewModel> GetMovieDetail(int? id)
        {
            var uri = Get_MovieService_Controller_URI.Movie.GetMovieDetail(id);
            var responseString = await _apiClient.GetStringAsync(uri);
            var response = JsonConvert.DeserializeObject<MovieItemViewModel>(responseString);
            return response;
        }

        public async Task<List<Movie>> ShowMovies()
        {
            try
            {
                var uri = Get_MovieService_Controller_URI.Movie.ShowMovies;
                var responseString = await _apiClient.GetStringAsync(uri);
                var response = JsonConvert.DeserializeObject<List<Movie>>(responseString);
                return response;
            }
            catch (Exception e)
            {
                throw e;
            }
           
        }
    }
}
