using Microsoft.EntityFrameworkCore.Sqlite;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;


namespace Coffe_site.Models
{
    public class User
    {

        public int Id { get; set; }


        [Required]
        public string Phone { get; set; }

        [Required]
        public string Password { get; set; }

        public string? Email { get; set; }


        public ICollection<Order> Orders { get; set; }

    }
}
