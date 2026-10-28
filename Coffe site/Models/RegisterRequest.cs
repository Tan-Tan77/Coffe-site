using System.ComponentModel.DataAnnotations;

namespace Coffe_site.Models
{
    public class RegisterRequest
    {

        [Required]
        public string Phone { get; set; }
        [Required]
        public string Password { get; set; }



    }
}
