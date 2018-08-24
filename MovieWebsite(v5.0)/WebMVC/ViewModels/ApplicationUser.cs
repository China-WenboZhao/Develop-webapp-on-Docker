using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.ViewModels
{
    public class ApplicationUser:IdentityUser
    {
        string Userid { get; set; }
    }
}
