using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebMVC.infrastructure;
using WebMVC.Models;
using WebMVC.ViewModels;

namespace WebMVC.Services
{
    public class BasketService: Controller,IBasketService
    {
       
       
        public BasketService()
        {
           
        }


        public async Task<Basket> GetorCreateBasket(string userid, HttpClient _apiClient)
        {
            var uri = Get_MovieService_Controller_URI.Basket.GetBasket(userid);

            var responseString = await _apiClient.GetStringAsync(uri);

            return string.IsNullOrEmpty(responseString) ?
                new Basket(userid) :
                JsonConvert.DeserializeObject<Basket>(responseString);
        }

        public async Task<Basket> UpdateBasket(Basket basket, HttpClient _apiClient)
        {
            var uri = Get_MovieService_Controller_URI.Basket.UpdateBasket;

            var basketContent = new StringContent(JsonConvert.SerializeObject(basket), System.Text.Encoding.UTF8, "application/json");

            var response = await _apiClient.PostAsync(uri, basketContent);

            response.EnsureSuccessStatusCode();

            return basket;
        }

        

        public void AddItemtoBasket(Basket basket,Movie movie)
        {
            var BasketItem = new BasketItem()
            {
                Id = movie.ID,
                Title = movie.Title,
                Price = movie.Price,
                CompanyName = movie.PublishCompany.CompanyName,
                Quantity = 1

            };
            basket.Items.Add(BasketItem);
        }


    }
}
