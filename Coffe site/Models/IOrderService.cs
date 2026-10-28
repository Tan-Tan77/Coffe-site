using Microsoft.EntityFrameworkCore;

namespace Coffe_site.Models
{
    public interface IOrderService
    {


        public Task<object[]> GetListOfOrders(int userId);

       

    }
}
