
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.IdentityModel.Abstractions;
using Newtonsoft.Json;

namespace Coffe_site.Models
{
    public class CartService : ICartService
    {

        private readonly AppDbContext _context;
        private readonly IConfiguration _logger;


        public CartService(IConfiguration logger,AppDbContext context) {
        
            _context = context;
            _logger = logger;
        
        }

        public async Task<Cart> AddItemToDbCart(ChangePhoneRequest id, int userId)
        {

            var item = await _context.Items.FirstOrDefaultAsync(x => x.Product_Id.ToString() == id.newphone);

            var newCartItem = new Cart
            {
              
                count = 1,
                product_Id = item.Product_Id,
                user_Id = userId

            };

            _context.Carts.Add(newCartItem);

            _context.SaveChanges();

            return newCartItem;

        }

        public async Task<ChangeResponse> BrowseCartFromStorageToDb(string cartItemToAdd, int userId)
        {
            
            Cart cart = JsonConvert.DeserializeObject<Cart>(cartItemToAdd);

            if(cart.count >= 30)
            {

                cart.count = 30;
            }



                
                var newItemToCart = new Cart()
                {
                    product_Id = cart.product_Id,
                    user_Id = userId,
                    count = cart.count,
                    user = cart.user,
                    item = cart.item

                };


            var searchForEqualRow = await _context.Carts.FirstOrDefaultAsync(x=>x.user_Id == userId && x.product_Id == cart.product_Id);

            if (searchForEqualRow != null)
            {

                if(searchForEqualRow.count >= 30)
                {
                    return new ChangeResponse
                    {
                        Success = true,
                        Message = "Данный товар уже есть в бд"


                    };

                }
                else
                {

                    if (searchForEqualRow.count + cart.count < 30)
                    {

                        var newValue = (searchForEqualRow.count + cart.count);

                        searchForEqualRow.count = newValue;

                        await _context.SaveChangesAsync();

                        return new ChangeResponse
                        {
                            Success = true,
                            Message = "Товар пополнен в корзине"


                        };
                    }
                    else
                    {
                        searchForEqualRow.count = 30;
                        await _context.SaveChangesAsync();

                        return new ChangeResponse
                        {
                            Success = true,
                            Message = "Товар пополнен в корзине до лимита"


                        };

                    }
                }


            }
             _context.Carts.Add(newItemToCart);
              
             await _context.SaveChangesAsync();

            return new ChangeResponse
            {
                Success = true,
                Message = "Товар загружен в бд"
               

            };

        }

        public async Task<Item> GetItem()
        {
            
            var tart = await _context.Items.FirstOrDefaultAsync(x=>x.Product_Id == 6);

            return tart; 

        }

        public async Task<Cart> GetItemById(ChangePhoneRequest id)
        {
           
            var item = await _context.Items.FirstOrDefaultAsync(x=>x.Product_Id.ToString() == id.newphone);

            return new Cart()
            {
                product_Id = item.Product_Id,
                count = 1
               

            };



        }


        public async Task<object[]> GetCartOfUser(int userId)
        {

            

            var cart =  from c in _context.Carts

                       where c.user_Id == userId
            select c;
                       

           var list = new List<object>();

          

            if (cart != null)
            {
               

                foreach(var item in cart)
                {
                    var ItemToDisplay = new Cart()
                    {

                         product_Id = item.product_Id,
                         count = item.count,
                         user = item.user,
                         user_Id = userId,
                         item = item.item

                    };

                    list.Add(ItemToDisplay);

                    var product = await _context.Items.FirstOrDefaultAsync(x => x.Product_Id == ItemToDisplay.product_Id);

                   list.Add(product);
                  
                }
                return list.ToArray();
            }
            else
            {
                return list.ToArray();

            }

        }

        public async Task<ChangeResponse> DeleteFromDb(int userId, int product_id)
        {
            var rowsToDelete = await _context.Carts.FirstOrDefaultAsync(x=> x.product_Id == product_id && x.user_Id == userId);

            
            if (rowsToDelete == null)
            {
                return new ChangeResponse()
                {
                    Success = false,
                    Message = "Товар не найден"

                };

            }
            else
            {
                
                _context.Carts.Remove(rowsToDelete);
                await _context.SaveChangesAsync();

                return new ChangeResponse()
                {
                    Success = true,
                    Message = "Товар удалён из корзины пользователя"

                };
            }


        }

        public async Task<Item> GetItemsForLocalStorage(int id)
        {

            var item = await _context.Items.FirstOrDefaultAsync(x=>x.Product_Id == id);



            return new Item()
            {
                
                Product_Id = item.Product_Id,
                Image = item.Image,
                Title = item.Title,
                Price = item.Price,


            };
            
            
        }

        public async Task<ChangeResponse> ChangeCountInCart(int user_id, int productId)
        {
            
            var itemInCart = await _context.Carts.FirstOrDefaultAsync(x => x.product_Id == productId && x.user_Id == user_id);

            if (itemInCart != null)
            {

                itemInCart.count++;

                await _context.SaveChangesAsync();

                return new ChangeResponse()
                {

                    Success = true,
                    Message = "Количество  товара в корзине было увеличено"
                };


            }
            else
            {

                return new ChangeResponse()
                {

                    Success = false,
                    Message = "Товар не был найден"
                };


            }
        }

        public async Task<ChangeResponse> MinusCountInCart(int user_id, int product__Id)
        {

            var itemInCart = await _context.Carts.FirstOrDefaultAsync(x => x.product_Id == product__Id && x.user_Id == user_id); 

            if (itemInCart != null)
            {

                itemInCart.count--;

                await _context.SaveChangesAsync();

                if (itemInCart.count == 0)
                {

                    _context.Carts.Remove(itemInCart);

                  await  _context.SaveChangesAsync();

                    return new ChangeResponse()
                    {

                        Success = true,
                        Message = "Товар был удалён"
                    };

                }
                else
                {


                    return new ChangeResponse()
                    {

                        Success = true,
                        Message = "Количество  товара в корзине было уменьшено"
                    };

                }

            }
            else
            {

                return new ChangeResponse()
                {

                    Success = false,
                    Message = user_id.ToString() + " " + product__Id.ToString()
                };


            }

        }





        public async Task<ChangeResponse>  MakeAnOrder(int userId,bool pickUp, decimal deliveryCost)
        {
            var userAddress = from adr in _context.Address
                              where adr.User_Id == userId
                              where adr.Is_Default == true
                              select adr.Id;

            int defaultAddress = 10;


            var cartOfUser = from cart in _context.Carts
                             where cart.user_Id == userId 
                             select cart;

            if (userAddress.FirstOrDefaultAsync() == null)
            {

                return new ChangeResponse()
                {

                    Success = false,
                    Message = "Пользователь не найден"
                };

            }
            else
            {

                    foreach (Cart item in cartOfUser)
                    {

                    if (pickUp == false)
                    {
                        System.Diagnostics.Debug.WriteLine("карт юзера ", item.id.ToString());

                        var newOrder = new Order()
                        {
                            Date = DateOnly.FromDateTime(DateTime.Now),
                            user_id = userId, 
                            item_id = item.product_Id,  
                            Pickup = pickUp,
                            Count = item.count,
                            Delivery_Price = deliveryCost,
                            address_Id = userAddress.FirstOrDefault() 

                        };
                        _context.Carts.Remove(item);
                        _context.Orders.Add(newOrder);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        var newOrder2 = new Order()
                        {
                            Date = DateOnly.FromDateTime(DateTime.Now),

                            user_id = userId,

                            item_id = item.product_Id,

                            Pickup = pickUp,

                            Count = item.count,

                            Delivery_Price = 0,

                            address_Id = defaultAddress,


                        };
                        _context.Carts.Remove(item);
                        _context.Orders.Add(newOrder2);
                        await _context.SaveChangesAsync();
                    }

                  
                   



                }
                   
             

                return new ChangeResponse()
                {

                    Success = true,
                    Message = "Корзина загружена в заказы. Корзина очищена"
                };

             


            }




        }
          


        

        public async Task<AddAddressRequest> GetLocation(int userId,string address)
        {
           
            var lat = await _context.Address.FirstOrDefaultAsync(x=>x.User_Id == userId && x.Address_Value == address);

            var lng = await _context.Address.FirstOrDefaultAsync(x => x.User_Id == userId && x.Address_Value == address);


            if (lat != null && lng != null) {

                return new AddAddressRequest
                {

                    address = lat.Address_Value,
                    lat = lat.Lat,
                    lng = lng.Long
                };

            }
            else
            {
                return new AddAddressRequest
                {

                    address = "",
                    lat = "",
                    lng = ""
                };

            }

        }
    }
}
