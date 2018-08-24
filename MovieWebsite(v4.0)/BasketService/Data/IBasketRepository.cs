using BasketService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketService.Data
{
    public interface IBasketRepository
    {
        Task<Basket> GetorCreateBasketAsync(string customerId);
        IEnumerable<string> GetUsers();
        Task<Basket> UpdateBasketAsync(Basket basket);
        Task<bool> DeleteBasketAsync(string customerid);
    }
}
