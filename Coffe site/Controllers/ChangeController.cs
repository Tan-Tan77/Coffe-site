using System.Security.Claims;
using Coffe_site.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Coffe_site.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ChangeController : ControllerBase
    {

        readonly IChangeService _changeService;
        readonly AppDbContext _appDbContext;

        public ChangeController(IChangeService changeService, AppDbContext appContext)
        {
            _changeService = changeService;
            _appDbContext = appContext;

        }

        [HttpPost("change-phone")]
        public async Task<ActionResult<ChangeResponse>> ChangePhone([FromBody] ChangePhoneRequest newPhone)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var userPhone = User.FindFirst(ClaimTypes.MobilePhone)?.Value;

            var result = await _changeService.ChangePhone(userPhone, userId, newPhone);

           

            if (!result.Success)
            {

                return BadRequest(result);
            }
            return Ok(result);

        }

        [HttpPost("get-phone")]
        public IActionResult GetPhone()
        {
            var userPhone = User.FindFirst(ClaimTypes.MobilePhone)?.Value;

            var u = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var us = int.TryParse(u, out int user);

            var phone = _appDbContext.Users.FirstOrDefault(x => x.Phone == userPhone);

            var searchAddresses = from ad in _appDbContext.Address
                                  where ad.User_Id == user
                                  where ad.Is_Default == true
                                  select ad.Address_Value;


            var allAddressses = from adresses in _appDbContext.Address
                                where adresses.User_Id == user
                                where adresses.Is_Default == false
                                select adresses.Address_Value;

          var listOfAddresses =  allAddressses.ToList();


            if (searchAddresses.FirstOrDefault() != null)
            {

              var fisrtAddress = searchAddresses.FirstOrDefault();

              var address_val = fisrtAddress;

                return Ok(new
                {
                    phone = phone.Phone,

                    address = address_val,

                    list = listOfAddresses

                });

            }else
            {

                return Ok(new
                {
                    phone = phone.Phone,

                    address = "Адреса отсутствуют",

                    list = listOfAddresses

                });


            }



            

        }


        [HttpPost("add-address")]

        public async Task<ActionResult<ChangeResponse>> AddAddress([FromBody] AddAddressRequest address)
        {
            var user_id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var result = await _changeService.AddAddress(user_id, address);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }



        [HttpPost("change-default-address")]

        public async Task<ActionResult<ChangeResponse>> ChangeDefaultAddress([FromBody] ChangePhoneRequest newDefaultAddress)
        {
            var user_id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var result = await _changeService.SetNewDefaultAddress(user_id, newDefaultAddress);

            if (result.Success)

            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }

        }


        [HttpPost("is-orders-empty")]

        public async Task<ActionResult<ChangeResponse>> IsOrdersTableEmpty()
        {
            var user_id =int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int id);

            var result = await _changeService.IsOrdersTableEmpty(id);


            if (result.Success == false)

            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }

        }
        [HttpPost("load-mail")]

        public async Task<ActionResult<ChangeResponse>> LoadMail()
        {
            var user_id = int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int id);    

            var WasAddedMail = await _changeService.ReturnMailOfUser(id);

            if (!WasAddedMail.Success)
            {
                return BadRequest(WasAddedMail);
            }
            else
            {
                return Ok(WasAddedMail);
            }
        }

        [HttpPost("refresh-or-add-mail")]

        public async Task<ActionResult<ChangeResponse>> MailActions(ChangePhoneRequest mail)
        {
            var userId =  int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value,out int id);

            var result = await _changeService.AddOrRefreshMail(id, mail.newphone);

            if (result.Success == false)
            {
                return BadRequest(result);
            }
            else
            {
                return Ok(result);
            }
        }


        [HttpPost("entry-password")]

        public async Task<ActionResult<ChangeResponse>> TestPassword(ChangePhoneRequest pass)
        {

            var userId = int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value,out int id);

            var result = await _changeService.IsPasswordCorrect(id,pass.newphone);

            if (!result.Success)
            {
                return BadRequest(result);
            }
            else {
                return Ok(result);
            }
           
        }


        [HttpPost("new-password")]

         public async Task<ActionResult<ChangeResponse>> SetNewPassword(ChangePhoneRequest pass)
        {

            var user = int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value,out int id);

            var result = await _changeService.ChangePassword(id,pass.newphone);

            if (!result.Success)
            {
                return BadRequest(result);
            }
            else
            {
                return Ok(result);
            }

        }

    }
}
