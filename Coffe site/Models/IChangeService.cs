using System.Security.Claims;

namespace Coffe_site.Models
{
    public interface IChangeService
    {

        Task<ChangeResponse> ChangePhone(string phone, string id, ChangePhoneRequest newPhone);

        Task<ChangeResponse> AddAddress(string id, AddAddressRequest address);


        Task<ChangeResponse> SetNewDefaultAddress(string id,ChangePhoneRequest newDefaultAddress);
        
        Task<ChangeResponse> IsOrdersTableEmpty(int user_id);

        Task<ChangeResponse> AddOrRefreshMail(int user_id,string mail);

        Task<ChangeResponse> ReturnMailOfUser(int user_id);
        
        Task<ChangeResponse> IsPasswordCorrect(int user_id,string password);

        Task<ChangeResponse> ChangePassword(int user_id,string password);

    }
}
