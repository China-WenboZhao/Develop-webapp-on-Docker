using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Models
{
    public class Basket
    {
        public string Userid { get; set; }
        public List<BasketItem> Items { get; set; }

        public Basket(string userid)
        {
            Userid = userid;
            Items = new List<BasketItem>();
        }
    }
}
