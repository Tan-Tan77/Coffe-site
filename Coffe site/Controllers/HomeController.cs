using System.Diagnostics;
using Coffe_site.Models;
using Microsoft.AspNetCore.Mvc;

namespace Coffe_site.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;




        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {


            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Main() {
           
            return View();
        
        }

        public IActionResult Desserts() {
            
            return View();
        }

        public IActionResult Sells() {

            return View();
        }


    

        public IActionResult Cart()
        {

            return View();
        }

        public IActionResult Account()
        {

            return View();
        }

        public IActionResult OrderStory()
        {

            return View();
        }


        public IActionResult OrderDone()
        {

            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



    }
}
