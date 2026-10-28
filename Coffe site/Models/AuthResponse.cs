using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;


namespace Coffe_site.Models


{
    public class AuthResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public string Token { get; set; }
    }
}
