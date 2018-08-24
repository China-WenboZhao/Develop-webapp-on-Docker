
using System.ComponentModel.DataAnnotations;


namespace MoviesService.Models
{
    public class PublishCompany
    {
        
        [Key]
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
    }
}
