using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.ViewModels
{
    public class MovieItemViewModel
    {
        
        public Models.Movie Movie { get; set; }

        public IEnumerable<SelectListItem> Companys { get; set; }

    }
}
