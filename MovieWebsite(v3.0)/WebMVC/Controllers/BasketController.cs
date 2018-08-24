using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebMVC.infrastructure;
using WebMVC.Models;
using WebMVC.Services;
using WebMVC.ViewModels;

namespace WebMVC.Controllers
{
    [Route("Basket")]
    [Authorize]
    public class BasketController:Controller
    {

        private readonly IBasketService _basketSvc;
        private readonly myHttpClient _apiClient;

        public  BasketController(IBasketService basketSvc, myHttpClient myHttpClient)
        {
            _basketSvc = basketSvc;
            _apiClient = myHttpClient;
        }
  
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            _apiClient.SetBearerToken(accessToken);

            var user = HttpContext.User;
            var vm = await _basketSvc.GetorCreateBasket(user.Claims.Where(c => c.Type == "name").First().Value,_apiClient);
            return View(vm);
        }

       
        [Route("AddtoCart")]
        public async Task<IActionResult> AddtoCart()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            _apiClient.SetBearerToken(accessToken);

            var value = (string)TempData["added_movie"];
            var movie = JsonConvert.DeserializeObject<Movie>(value);
            if (movie?.ID != null)
            {
                var user = HttpContext.User;
                var basket = await _basketSvc.GetorCreateBasket(user.Claims.Where(c => c.Type == "name").First().Value,_apiClient);
                foreach (var item in basket.Items)
                {
                    if (item.Id.Equals(movie.ID))
                    {
                        return RedirectToAction("Index", "Movies");
                    }
                }
                 _basketSvc.AddItemtoBasket(basket, movie);
                await _basketSvc.UpdateBasket(basket,_apiClient);
            }
            return RedirectToAction("Index", "Movies");
           
        }

        [Route("RemovefromCart")]
        public async Task<IActionResult> RemovefromCart(BasketItem basketitem)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            _apiClient.SetBearerToken(accessToken); 

            var user = HttpContext.User;
            var basket = await _basketSvc.GetorCreateBasket(user.Claims.Where(c => c.Type == "name").First().Value,_apiClient);
            int index = 0;
            foreach (var item in basket.Items){
                if (item.Id.Equals(basketitem.Id))
                {
                    break;
                }
                index++;
            }
            basket.Items.RemoveAt(index);
            //basket.Items.Remove(basketitem);
            await _basketSvc.UpdateBasket(basket,_apiClient);

            return RedirectToAction("Index");
        }

    }
}
