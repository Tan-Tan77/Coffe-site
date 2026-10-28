using System.Text.Json.Nodes;

namespace Coffe_site.Models
{
    public interface ICartService
    {

        Task<Item> GetItem();


        Task<Cart> GetItemById(ChangePhoneRequest id);


        Task<Cart> AddItemToDbCart(ChangePhoneRequest id, int userId);

        Task<ChangeResponse> BrowseCartFromStorageToDb(string cartItemToAdd,int userId);

        Task<object[]>  GetCartOfUser(int userId);

        Task<ChangeResponse> DeleteFromDb(int  userId, int product_id);

        Task<Item> GetItemsForLocalStorage(int id);


        Task<ChangeResponse> ChangeCountInCart (int user_id, int productId);

        Task<ChangeResponse> MinusCountInCart(int user_id, int product__Id);


        Task<ChangeResponse> MakeAnOrder(int userId,bool pickUp,decimal deliveryCost);

        Task<AddAddressRequest> GetLocation(int userId, string address);

    }
}
