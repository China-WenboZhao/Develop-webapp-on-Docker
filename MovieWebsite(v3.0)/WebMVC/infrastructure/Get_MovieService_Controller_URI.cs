using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.infrastructure
{
    public class Get_MovieService_Controller_URI
    {
        public static class Movie
        {
            public static string baseURI = "http://moviesservice:5000/MoviesService";
            public static string ShowMovies = $"{baseURI}/ShowMovies";
            public static string CreateMovie = $"{baseURI}/GetSelectList";
            public static string CreateMoviePost = $"{baseURI}/CreateMovie";
            public static string EditMovie(int? id) => $"{baseURI}/ShowMovie?id={id}";
            public static string EditMoviePost() => $"{baseURI}/UpdateMovie";
            public static string DeleteConfirmed(int? id) => $"{baseURI}/DeleteMovie?id={id}";
            public static string DeleteMovie(int? id) => $"{baseURI}/ShowMovie?id={id}";
            public static string GetMovieDetail(int? id) => $"{baseURI}/ShowMovie?id={id}";

        }

        public static class Basket
        {
            public static string baseURI = "http://basketservice:5002/BasketService";
            public static string GetBasket(string  id) => $"{baseURI}/GetBasket?userid={id}";
            public static string UpdateBasket => $"{baseURI}/UpdateBasket";
            public static string DeleteBasket(string id) => $"{baseURI}/DeleteBasket?id={id}";
        }
    }
}
