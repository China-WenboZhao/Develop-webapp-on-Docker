using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Models
{
    public class PublishCompany
    {
        
        [Key]
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }

          
    }
}
