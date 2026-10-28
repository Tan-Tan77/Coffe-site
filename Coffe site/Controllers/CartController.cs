using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Nodes;
using Coffe_site.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using Org.BouncyCastle.Crypto.Operators;
using Org.BouncyCastle.Crypto.Prng;


namespace Coffe_site.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        
        private readonly ICartService _cartService;
        private readonly AppDbContext _appDbContext;

        public CartController(ICartService cartService, AppDbContext appDbContext)
        {

            _cartService = cartService;
            _appDbContext = appDbContext;

        }

        [HttpPost("get")]
        public async Task<ActionResult<Item>> GetItem()
        {

           var result = await _cartService.GetItem();

            return Ok(result);

        }



        [HttpPost("add-to-cart")]

        public async Task<ActionResult<Cart>> AddItemToCart([FromBody] ChangePhoneRequest id)
        {
            var result = await _cartService.GetItemById(id);

            return Ok(result);
        }


        [Authorize]
        [HttpPost("add-to-cart-auth")]
        public async Task<ActionResult<Cart>> AddItemToCartByUser([FromBody] ChangePhoneRequest id)
        {

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            int.TryParse(userId, out int user);

            var result = await _cartService.AddItemToDbCart(id, user);

            return Ok(result);

        }


        [Authorize]
        [HttpPost("add-to-db-from-cookie")]
        public async Task<ActionResult<ChangeResponse>> LoadToDb([FromBody] JsonElement cart)
        {
            string jsonString = cart.GetRawText();

            var user = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
 
            int.TryParse(user, out int userId);

            var result = await _cartService.BrowseCartFromStorageToDb(jsonString, userId);

            if (result.Success)
            {

                return Ok(result);
            }

            return BadRequest(result);
        }


        [Authorize]
        [HttpPost("cart-from-db")]

        public async Task<ActionResult<object[]>> LoadCart()
        {

            var user = int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out  int userId);


            var result = await _cartService.GetCartOfUser(userId);


            return result;

        }


        [Authorize]
        [HttpPost("delete")]

        public async Task<ActionResult<ChangeResponse>> DeleteProductFromDb([FromBody] string Id)
        {
            
            int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userId);

            int.TryParse(Id, out int productId);

            var result = await _cartService.DeleteFromDb(userId,productId);

            if (result.Success == true) { return Ok(result); }
            else
            {
                result.Message += " " + productId + " ";
                result.Message += userId + " ";
                return BadRequest(result);
            }

        }


        [HttpPost("find-item")]
        public async Task<ActionResult<Item>> GetFromDb([FromBody] JsonElement id)
        {
            string jsonString = id.GetRawText();

            int.TryParse(jsonString, out int item_Id);

            var result = await _cartService.GetItemsForLocalStorage(item_Id);

            return Ok(result);

        }

        [Authorize]
        [HttpPost("subtract")]

        public async Task<ActionResult<ChangeResponse>> DecreaseAmountInCart([FromBody] ChangePhoneRequest productid)
        {
            
            int.TryParse (productid.newphone, out int product_Id);
            int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userId);

            var result = await _cartService.MinusCountInCart(userId, product_Id);

            if (result.Success == true) { return Ok(result); }

            result.Message += product_Id.ToString();

            return BadRequest(result);

        }

        [Authorize]
        [HttpPost("add")]

        public async Task<ActionResult<ChangeResponse>> AddOneAmountInCart([FromBody] ChangePhoneRequest productid)
        {

            int.TryParse(productid.newphone, out int product_Id);
            int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userId);

            var result = await _cartService.ChangeCountInCart(userId, product_Id);



            if (result.Success == true) { return Ok(result); }



            return BadRequest(result);

        }

        [Authorize]
        [HttpPost("buy")]

        public async Task<ActionResult<ChangeResponse>> LoadCartToOrderTable([FromBody] OrderRequest req)
        {

            int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int user_Id);

            bool b = false;


            if (req.pickUp == "+")
            {
                b = true;

            }
            else
            {

                b = false;
            }

            decimal.TryParse(req.deliveryCost, out decimal delivery);

            var loadToOrders = await _cartService.MakeAnOrder(user_Id, b, delivery);

           

            if (loadToOrders.Success)
            {

                return Ok(loadToOrders);
            }
            else
            {
                return BadRequest(loadToOrders);
            }
        }


        [Authorize]
        [HttpPost("get-location")]

        public async Task<ActionResult<AddAddressRequest>> GetLocation([FromBody] ChangePhoneRequest address)
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            int.TryParse(user, out int id);

            var result = await _cartService.GetLocation(id,address.newphone);
            
            if(result.address == "")
            {
                return BadRequest(result);
            }else
            
             
                return Ok(result);
            }


       


    }


    }


