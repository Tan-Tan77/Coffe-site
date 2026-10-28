using Microsoft.EntityFrameworkCore.Sqlite;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;


namespace Coffe_site.Models


{
    public class LoginRequest
    {
        [Required]

        public string Phone {  get; set; }

        [Required]
        public string Password { get; set; }



    }
}
