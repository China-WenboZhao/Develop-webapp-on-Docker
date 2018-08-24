using BasketService.Data;
using BasketService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace BasketService.Controllers
{
    [Authorize]

    [Route("BasketService")]
    public class BasketController:Controller
    {
        private readonly IBasketRepository _repository;
        public BasketController(IBasketRepository repository)
        {
            _repository = repository;
        }

        [Route("GetBasket")]
        public async Task<IActionResult> GetorCreateBasket(string userid)
        {
            var basket = await _repository.GetorCreateBasketAsync(userid);
            System.Console.WriteLine(basket);
            if (basket == null)
            {
                return Ok(new Basket(userid) { });
            }

            return Ok(basket);
        }

        // POST /value
        [HttpPost]
        [Route("UpdateBasket")]
        public async Task<IActionResult> UpdateBasket([FromBody]Basket value)
        {
            var basket = await _repository.UpdateBasketAsync(value);

            return Ok(basket);
        }


        //leave this method  for further using.
        [Route("CheckOut")]
        [HttpPost]
        public async Task<IActionResult> CheckOut([FromBody]BasketCheckout basketCheckout, [FromHeader(Name = "x-requestid")] string requestId)
        {
           
            return Accepted();
        }

        /*
         * not needed by now, leave it for further using
         */
        // DELETE api/values/5
        [HttpDelete("{id}")]
        [Route("DeleteBasket")]
        public void DeleteBasket(string userid)
        {
            _repository.DeleteBasketAsync(userid);
        }
    }
}
