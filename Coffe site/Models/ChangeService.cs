
using System.Diagnostics.Eventing.Reader;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Org.BouncyCastle.Asn1.Ocsp;

namespace Coffe_site.Models
{
    public class ChangeService : IChangeService
    {

        readonly AppDbContext _context;
        readonly IAuthService _authService;

        public ChangeService(AppDbContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        public async Task<ChangeResponse> AddAddress(string id, AddAddressRequest address)
        {

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id.ToString() == id.ToString());
            var AllAddresses = await _context.Address.Where(x => x.User_Id == user.Id).ToListAsync();

            bool isDuplicated = false;

            foreach (var adr in AllAddresses)
            {
                if (adr.Address_Value == address.address)
                {
                    isDuplicated = true;
                    break;
                }
            }
            if (isDuplicated == false)
            {


                if (user != null)
                {
                    bool is_First = false;

                    var addressOfUser = await _context.Address.FirstOrDefaultAsync(x => x.User_Id == user.Id);






                    if (addressOfUser == null)
                    {

                        if (address.lat != "" && address.lng != "")
                        {



                            var newAddress = new Address
                            {
                                User_Id = user.Id,

                                Address_Value = address.address,

                                Is_Default = true,

                                Lat = address.lat,

                                Long = address.lng



                            };


                            _context.Address.Add(newAddress);
                            _context.SaveChanges();
                            return new ChangeResponse
                            {
                                Message = "Добавлен новый адрес для пользователя",
                                Success = true
                            };
                        }
                        else
                        {
                            return new ChangeResponse
                            {
                                Message = "Проверьте правильность введения по подсказке",
                                Success = false
                            };
                        }



                    }
                    else
                    {
                        if (address.lat != "" && address.lng != "")
                        {
                            var anotherAddress = new Address
                            {
                                User_Id = user.Id,

                                Address_Value = address.address,

                                Is_Default = false,

                                Lat = address.lat,

                                Long = address.lng




                            };


                            _context.Address.Add(anotherAddress);
                            _context.SaveChanges();


                            return new ChangeResponse
                            {
                                Message = "Добавлен новый адрес для пользователя",
                                Success = true
                            };

                        }
                        else
                        {
                            return new ChangeResponse
                            {
                                Message = "Проверьте правильность введения по подсказке",
                                Success = false
                            };
                        }
                    }
                }
                else
                {


                    return new ChangeResponse
                    {
                        Message = "Необходимо войти или зарегистрироваться",
                        Success = false
                    };
                }


            }
            else
            {
                return new ChangeResponse
                {
                    Message = "Данный адрес уже добавлен в аккаунт",
                    Success = false
                };
            }
        }

        public async Task<ChangeResponse> IsOrdersTableEmpty(int user_id)
        {


            bool IsEmpty = false;

            var orders = await _context.Orders.FirstOrDefaultAsync(x => x.user_id == user_id);

            if (orders == null)
            {
                IsEmpty = true;
            }

            return new ChangeResponse
            {

                Success = IsEmpty
            };

        }

        public async Task<ChangeResponse> ChangePhone(string Userphone, string Userid, ChangePhoneRequest newPhone)
        {

            var userFind = await _context.Users.FirstOrDefaultAsync(u => u.Id.ToString() == Userid);

            var phoneFind = await _context.Users.FirstOrDefaultAsync(u => u.Phone == newPhone.newphone);

            if (userFind != null)
            {
                if (phoneFind != null)
                {
                    return new ChangeResponse
                    {
                        Success = false,
                        Message = "Данный номер уже используется"


                    };


                }
                else
                {


                    userFind.Phone = newPhone.newphone;
                    _context.SaveChanges();



                    return new ChangeResponse
                    {
                        Success = true,
                        Message = "Телефон изменён",
                        Token = _authService.GenerateJwtToken(userFind)

                    };

                }
            }


            return new ChangeResponse
            {

                Success = false,
                Message = "Пользователь не найден",


            };
        }

        public async Task<ChangeResponse> SetNewDefaultAddress(string id, ChangePhoneRequest newDefaultAddress)
        {

            var userId = await _context.Address.FirstOrDefaultAsync(x => x.User_Id.ToString() == id);

            if (userId != null)
            {

                var searchDefault = from address in _context.Address
                                    where address.User_Id == userId.User_Id
                                    where address.Is_Default == true
                                    select address;

                if (searchDefault.Any())
                {

                    var OldDefault = searchDefault.FirstOrDefault();

                    OldDefault.Is_Default = false;

                    var NewDefault = newDefaultAddress.newphone;

                    var searchForNewAddress = from add in _context.Address
                                              where add.Address_Value == NewDefault
                                              where add.User_Id == userId.User_Id
                                              select add;
                    ;

                    var newDefaultAd = searchForNewAddress.FirstOrDefault();

                    newDefaultAd.Is_Default = true;

                    _context.SaveChanges();

                    return new ChangeResponse
                    {
                        Success = true,
                        Message = "Адрес успешно установлен"

                    };
                }
                else
                {
                    return new ChangeResponse
                    {
                        Success = true,
                        Message = "Пользователь не имеет дефолтного адреса"

                    };
                }


            }
            else
            {
                return new ChangeResponse
                {
                    Success = true,
                    Message = "Пользователь не имеет адресов"

                };

            }


        }

        public async Task<ChangeResponse> AddOrRefreshMail(int user_id, string mail)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x=>x.Id== user_id);

            if(mail == "")
            {
                return new ChangeResponse()
                {

                    Success = false,
                    Message = "Почта не добавлена"

                };

            }


            if (user != null)
            {
                if (mail == user.Email)
                {
                    return new ChangeResponse()
                    {

                        Success = false,
                        Message = "Электронная почта не изменена"

                    };
                }
                else
                {
                    user.Email = mail;

                    await _context.SaveChangesAsync();

                    return new ChangeResponse()
                    {

                        Success = true,
                        Message = "Электронная почта изменена"

                    };
                }
            }
            else
            {
                return new ChangeResponse() { 
                
                    Success =false,
                    Message = "Пользователь не найден"

                };

            }

        }

        public async Task<ChangeResponse> ReturnMailOfUser(int user_id)
        {
            var mail = await _context.Users.FirstOrDefaultAsync(x => x.Id == user_id);

            if (mail == null)
            {
                return new ChangeResponse
                {
                    Success = false,
                    Message = ""
                };
            }
            else
            {
                return new ChangeResponse
                {
                    Success = true,
                    Message = mail.Email
                };

            }
        }

        public async Task<ChangeResponse> IsPasswordCorrect(int user_id, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x=>x.Id == user_id);

            if (user == null) {
                return new ChangeResponse
                {
                    Success = true,
                    Message = "Пользователь не найден"
                };

            }
            else
            {
                var isPasswordCorrect =  BCrypt.Net.BCrypt.Verify(password, user.Password);
                if (isPasswordCorrect)
                {
                    return new ChangeResponse
                    {
                        Success = true,
                        Message = "Пароль введён верно"
                    };
                }
                else
                {
                    return new ChangeResponse
                    {
                        Success = false,
                        Message = "Неверный пароль"
                    };
                }
            }
        }

        public async Task<ChangeResponse> ChangePassword(int user_id, string password)
        {
           
            var user = await _context.Users.FirstOrDefaultAsync(x=>x.Id == user_id);

            if (user == null)
            {
                return new ChangeResponse
                {
                    Success = false,
                    Message = "Пользователь не найден"
                };
            }
            else
            {
               string pass = BCrypt.Net.BCrypt.HashPassword(password);

                _authService.GenerateJwtToken(user);

                user.Password = pass;

                _context.SaveChangesAsync();

                return new ChangeResponse
                {
                    Success = true,
                    Message = "Пароль был успешно изменён",
                    Token = _authService.GenerateJwtToken(user)    
                };

            }

        }
    }
}
