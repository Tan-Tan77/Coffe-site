using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Coffe_site.Models
{
    public class OrderRequest
    {
        [Required]
        public string pickUp { get; set; }
        [Required]
        public string deliveryCost { get; set; }

        
    }

    
}
