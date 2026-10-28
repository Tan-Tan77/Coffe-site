using System.Security.Claims;
using Coffe_site.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Coffe_site.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly AppDbContext _appDbContext;

        public OrderController(IOrderService cartService, AppDbContext appDbContext)
        {

            _orderService = cartService;
            _appDbContext = appDbContext;

        }

        [Authorize]
        [HttpPost("order-from-db")]

        public async Task<ActionResult<object[]>> LoadOrders()
        {

            var user = int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userId);


            var result = await _orderService.GetListOfOrders(userId);


            return result;

        }

    }
}
