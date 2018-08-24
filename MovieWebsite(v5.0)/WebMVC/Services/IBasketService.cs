using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebMVC.Models;
using WebMVC.ViewModels;

namespace WebMVC.Services
{
    public  interface IBasketService
    {

        Task<Basket> GetorCreateBasket(string userid, HttpClient _apiClient);
        Task<Basket> UpdateBasket(Basket basket, HttpClient _apiClient);
        //  Task Checkout(Basket basket);
        void AddItemtoBasket(Basket basket, Movie movie);
    }
}
